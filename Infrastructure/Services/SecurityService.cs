using MetafarApiChallege.Infrastructure.Dtos;
using MetafarApiChallege.Infrastructure.Helpers;
using MetafarApiChallege.Infrastructure.Repositories.Models;
using MetafarApiChallege.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MetafarApiChallege.Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ICardService _cardService;
        private readonly AppSettings _appSettings;

        public SecurityService(ICardService cardService, AppSettings appSettings)
        {
            _cardService = cardService;
            _appSettings = appSettings;
        }

        public async Task<AuthenticateResponse> Authenticate(AccountRequest accountRequest)
        {
            Card card = await _cardService.GetByCardNumber(accountRequest.CardNumber);

            if (card == null)
                throw new AccountNotFoundException("Card not found.");

            if (card.LoginFailures >= _appSettings.CountLoginFailures)
                throw new Helpers.UnauthorizedAccessException("User not authorized due to too many failed login attempts.");

            if (card.Pin != accountRequest.Pin)
            {
                int failureCount = card.LoginFailures.GetValueOrDefault();
                await _cardService.UpdateLoginFailures(card.Id, failureCount + 1);
                throw new InvalidCredentialsException("Incorrect PIN.");
            }

            if (card.LoginFailures > 0)
                await _cardService.UpdateLoginFailures(card.Id, 0);

            var tokenResponse = GenerateToken(card, _appSettings.Secret!);
            return new AuthenticateResponse { Token = tokenResponse };
        }

        private string GenerateToken(Card card, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("IdCard", card.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(_appSettings.TokenExpiresHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
