using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zaginiony24.Infrastructure;
using Zaginiony24.ModelRepositories;
using Zaginiony24.Models;
using Zaginiony24.Models.View;

namespace Zaginiony24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticeController : Zaginiony24BaseController<NoticeController>
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _userAccessor;

        public NoticeController(ILogger<NoticeController> logger,
            INoticeRepository noticeRepository,
            UserManager<AppUser> userManager,
            IUserAccessor userAccessor) 
            : base(logger)
        {
            _noticeRepository = noticeRepository;
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        [HttpGet("ListAll")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _noticeRepository.GetAllNotices();
            if (result == null)
                return BadRequest(new ApiResult<dynamic>(new {Notice = ErrorCodes.CannotFindAnyNotice}));
            return Ok(new ApiResult<List<Notice>> {Result = result});
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details([FromQuery]Guid id)
        {
            var notice = await _noticeRepository.GetAsync(id);
            if (notice == null)
                return NotFound(new ApiResult<dynamic>(new {Id = ErrorCodes.InvalidNoticeId}));
            var user = await _userManager.FindByIdAsync(notice.AppUserId);

            var userInfo = new UserInfo
            {
                Username = user?.UserName,
                Email = user?.Email,
                Name = user?.Name
            };
            var result = new NoticeDetails
            {
                Notice = notice,
                User = userInfo
            };
            return Ok(new ApiResult<NoticeDetails> {Result = result});
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]Notice notice)
        {
            if (notice == null)
                return BadRequest(new ApiResult<string>(new {Notice = ErrorCodes.BadRequest}));

            var appUser = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
            notice.DatePosted = DateTime.Now;
            notice.AppUser = appUser;
            var result = await _noticeRepository.CreateAsync(notice);

            return Created("", new ApiResult<string> {Result = result.NoticeId.ToString()});
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            var notice = await _noticeRepository.GetAsync(id);
            await _noticeRepository.DeleteAsync(notice);
            return Ok(new ApiResult<string> {Result = notice.NoticeId.ToString()});
        }

    }
}