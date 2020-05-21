using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zaginiony24.Infrastructure;
using Zaginiony24.ModelRepositories;
using Zaginiony24.Models;
using Zaginiony24.Models.Biding;
using Zaginiony24.Models.View;

namespace Zaginiony24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ManageController : Zaginiony24BaseController<ManageController>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly INoticeRepository _noticeRepository;
        private readonly ApplicationDbContext _context;

        public ManageController(ILogger<ManageController> logger,
            UserManager<AppUser> userManager,
            INoticeRepository noticeRepository,
            ApplicationDbContext context
            ) 
            : base(logger)
        {
            _userManager = userManager;
            _noticeRepository = noticeRepository;
            _context = context;
        }

        [HttpGet("GetUsersList")]
        public async Task<IActionResult> GetUsersList()
        {
            var result = new List<UserSettingsVm>();
            try
            {
                var users = await _context.Users.Include(u => u.Notices).ToListAsync();
                foreach (var user in users)
                {
                    result.Add(
                        new UserSettingsVm
                        {
                            Id = user.Id,
                            Username = user.UserName,
                            Email = user.Email,
                            CanLogIn = user.IsActive,
                            NoticesListedCount = user.Notices.Count()
                        });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResult<List<UserSettingsVm>>(new {Error = ex.Message}));
            }

            return Ok(new ApiResult<List<UserSettingsVm>> {Result = result});
        }

        [HttpGet("ListAllNotices")]
        public async Task<IActionResult> ListAllNotices()
        {
            var notices = await _noticeRepository.GetNoticesWthUsers();
            var result = new List<NoticeManageVm>();
            foreach (var notice in notices)
            {
                result.Add( new NoticeManageVm
                {
                    Id = notice.NoticeId,
                    Name = notice.Name,
                    Surname = notice.Surname,
                    DatePosted = notice.DatePosted,
                    Username = notice.AppUser.UserName
                });
            }

            return Ok(new ApiResult<List<NoticeManageVm>> {Result = result});
        }

        [HttpDelete("DeleteNotice")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            var notice = await _noticeRepository.GetAsync(id);
            await _noticeRepository.DeleteAsync(notice);
            return Ok(new ApiResult<string> {Result = notice.NoticeId.ToString()});
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery]Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Notices)
                .Where(u => u.Id == id.ToString())
                .FirstOrDefaultAsync();
            var userNotices = user.Notices.ToList();

            if (user.Notices != null)
            {
                foreach (var notice in userNotices)
                {
                    await _noticeRepository.DeleteAsync(notice);
                }
            }
            var result = await _userManager.DeleteAsync(user);
            return Ok(new ApiResult<bool> {Result = result.Succeeded});
        }

        [HttpPost("ChangeLockoutStatus")]
        public async Task<IActionResult> ChangeLockoutStatus([FromBody]ChangeLockoutStatusBm request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            user.IsActive = request.CanLogIn;
            await _userManager.UpdateAsync(user);
            return Ok(new ApiResult<bool> {Result = user.IsActive});
        }

    }
}
