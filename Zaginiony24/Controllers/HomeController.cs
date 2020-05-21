using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class HomeController : Zaginiony24BaseController<HomeController>
    {
        private readonly INoticeRepository _noticeRepository;

        public HomeController(ILogger<HomeController> logger,
            INoticeRepository noticeRepository) 
            : base(logger)
        {
            _noticeRepository = noticeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]HomeFilterQuery query)
        {
            var notices = new List<Notice>();
            if (string.IsNullOrEmpty(query.Gender) && string.IsNullOrEmpty(query.District))
                notices = await _noticeRepository.GetAllNotices();
            else
                notices = await _noticeRepository.GetNotices(query.Gender, query.District);

            var result = new List<NoticeShortcutVm>();

            foreach (var notice in notices)
            {
                result.Add(new NoticeShortcutVm
                {
                    Id = notice.NoticeId,
                    Name = notice.Name,
                    Surname = notice.Surname,
                    DateOfDisappearance = notice.DateOfDisappearance.ToShortDateString(),
                    Gender = notice.Gender,
                    Age = notice.Age,
                    LastSeenPlace = notice.LastSeenPlace,
                    District = notice.District
                });
            }

            return Ok(new ApiResult<List<NoticeShortcutVm>> {Result = result});
        }

    }
}