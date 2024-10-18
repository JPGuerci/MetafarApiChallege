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

        [AllowAnonymous]
        [HttpPost("authenticate")] 
        public async Task<IActionResult> Authenticate([FromBody] AccountRequest accountLogin)
        {
            AuthenticateResponse response = await _securityService.Authenticate(accountLogin);
            return Ok(response);
        }

        [HttpGet("get")] 
        public async Task<IActionResult> Get()
        {
            AccountDto response = await _accountService.GetByIdCard(idCard);
            return Ok(response);
        }

        [HttpPost("cashout")] 
        public async Task<IActionResult> CashOut([FromBody] decimal amount) 
        {
            if (amount <= 0) return BadRequest("The amount must be a positive number.");
            OperationDto response = await _accountService.CashOut(idCard, amount);
            return Ok(response);
        }

        [HttpPost("cashin")] 
        public async Task<IActionResult> CashIn([FromBody] decimal amount) 
        {
            if (amount <= 0) return BadRequest("The amount must be a positive number.");
            OperationDto response = await _accountService.CashIn(idCard, amount);
            return Ok(response);
        }

        [HttpPost("transfer")] 
        public async Task<IActionResult> Transfer([FromBody] TransferRequest transferRequest)
        {
            TransferDto transferDto = _mapper.Map<TransferDto>(transferRequest);
            transferDto.IdCardOrigin = idCard;
            TransferResponse response = await _accountService.Transfer(transferDto);
            return Ok(response);
        }

        [HttpGet("operations")]
        public async Task<IActionResult> GetOperations([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _operationService.GetOperations(idCard, pageNumber, pageSize);
            return Ok(response);
        }
    }
}
