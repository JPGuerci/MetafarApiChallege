using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Repositories.Models;
using MetafarApiChallege.Infrastructure.Services.Interfaces;

namespace MetafarApiChallege.Infrastructure.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CardService(ICardRepository cardRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Card> GetByCardNumber(int cardNumber)
        {
            Card card = await _cardRepository.GetByCardNumber(cardNumber);
            return card;
        }

        public async Task UpdateLoginFailures(Guid idCard, int failuresCount)
        {
            Card card = await _cardRepository.GetById(idCard);
            card.LoginFailures = failuresCount;
            _cardRepository.Update(card);
            await _unitOfWork.CommitAsync();

        }
    }
}
