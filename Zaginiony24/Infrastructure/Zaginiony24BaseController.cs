using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zaginiony24.Models;

namespace Zaginiony24.Infrastructure
{
    public class Zaginiony24BaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> _logger;

        public Zaginiony24BaseController(ILogger<TController> logger)
        {
            _logger = logger;
        }

        protected async Task<AppUser> GetUserFromClaims(
            [FromServices]UserManager<AppUser> userManager, 
            [FromServices]IJwtGenerator jwtGenerator,
            [FromServices]IUserAccessor userAccessor)
        {
            var user = await userManager.FindByNameAsync(userAccessor.GetCurrentUsername());
            return user;
        }
    }
}