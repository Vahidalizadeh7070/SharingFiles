using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxController : ControllerBase
    {
        private readonly IInbox _inbox;
        private readonly IMapper _mapper;

        public InboxController(IInbox inbox, IMapper mapper)
        {
            _inbox = inbox;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InboxDTO>> Inbox(string userId)
        {
            var model = _inbox.List(userId);
            if(model.Count()==0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var mapModel = _mapper.Map<IEnumerable<InboxDTO>>(model);
            return StatusCode(StatusCodes.Status200OK, new
            {
                value = StatusCodes.Status200OK == 200 ? mapModel : null
            });
        }

        [HttpGet("{fileId}")]
        [Route("Count")]
        public ActionResult Count([FromQuery]string fileId)
        {
            var count_Download = _inbox.DownloadCount(fileId);
            return Ok(count_Download);
        }
    }
}
