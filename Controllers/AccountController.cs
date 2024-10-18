using AutoMapper;
using MetafarApiChallege.Infrastructure.Dtos;
using MetafarApiChallege.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetafarApiChallege.Controllers
{
    public class AccountController : CommonController
    {
        private readonly IAccountService _accountService;
        private readonly ISecurityService _securityService;
        private readonly IOperationService _operationService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, ISecurityService securityService, IOperationService operationService, IMapper mapper)
        {
            _accountService = accountService;
            _securityService = securityService;
            _operationService = operationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticates the user and generates a JWT token.
        /// </summary>
        /// <param name="accountLogin">Account login credentials (card number and PIN).</param>
        /// <returns>JWT token if authentication is successful.</returns>
        /// <response code="200">Authentication successful, returns JWT token.</response>
        /// <response code="400">Bad request, invalid credentials.</response>
        /// <response code="404">Card not found or invalid data.</response>
        /// <response code="401">User not authorized due to too many failed login attempts.</response>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AccountRequest accountLogin)
        {
            AuthenticateResponse response = await _securityService.Authenticate(accountLogin);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves account details by IdCard.
        /// </summary>
        /// <returns>Account details including last extraction date.</returns>
        /// <response code="200">Account details successfully retrieved.</response>
        /// <response code="404">Account with the specified IdCard was not found.</response>
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            AccountDto response = await _accountService.GetByIdCard(idCard);
            return Ok(response);
        }

        /// <summary>
        /// Withdraws a specified amount from the account (CashOut).
        /// </summary>
        /// <param name="amount">Amount to be withdrawn from the account.</param>
        /// <returns>Details of the cash-out operation.</returns>
        /// <response code="200">Cash-out operation successful.</response>
        /// <response code="400">Bad request, invalid amount or insufficient funds.</response>
        [HttpPost("cashout")]
        public async Task<IActionResult> CashOut([FromBody] decimal amount)
        {
            if (amount <= 0) return BadRequest("The amount must be a positive number.");
            OperationDto response = await _accountService.CashOut(idCard, amount);
            return Ok(response);
        }

        /// <summary>
        /// Deposits a specified amount to the account (CashIn).
        /// </summary>
        /// <param name="amount">Amount to be deposited into the account.</param>
        /// <returns>Details of the cash-in operation.</returns>
        /// <response code="200">Cash-in operation successful.</response>
        /// <response code="400">Bad request, invalid amount.</response>
        [HttpPost("cashin")]
        public async Task<IActionResult> CashIn([FromBody] decimal amount)
        {
            if (amount <= 0) return BadRequest("The amount must be a positive number.");
            OperationDto response = await _accountService.CashIn(idCard, amount);
            return Ok(response);
        }

        /// <summary>
        /// Transfers a specified amount from one account to another.
        /// </summary>
        /// <param name="transferRequest">Transfer details including the amount and destination account.</param>
        /// <returns>Details of the transfer operation.</returns>
        /// <response code="200">Transfer operation successful.</response>
        /// <response code="400">Bad request, invalid transfer (e.g., insufficient funds or same account).</response>
        /// <response code="404">Account not found.</response>
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest transferRequest)
        {
            TransferDto transferDto = _mapper.Map<TransferDto>(transferRequest);
            transferDto.IdCardOrigin = idCard;
            TransferResponse response = await _accountService.Transfer(transferDto);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a paginated list of account operations.
        /// </summary>
        /// <param name="pageNumber">Page number for pagination (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Paginated list of operations related to the account.</returns>
        /// <response code="200">List of operations successfully retrieved.</response>
        [HttpGet("operations")]
        public async Task<IActionResult> GetOperations([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _operationService.GetOperations(idCard, pageNumber, pageSize);
            return Ok(response);
        }
    }
}
