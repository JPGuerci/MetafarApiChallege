using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace MetafarApiChallege.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        public Guid idCard
        {
            get
            {
                if (HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var idCardClaim = identity.FindFirst("IdCard");
                    if (idCardClaim != null && Guid.TryParse(idCardClaim.Value, out Guid idCard))
                    {
                        return idCard;
                    }
                }

                throw new UnauthorizedAccessException("User is not authorized or IdCard not found in the token.");
            }
        }
    }
}