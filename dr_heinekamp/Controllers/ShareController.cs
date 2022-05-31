using AutoMapper;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
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
    public class ShareController : ControllerBase
    {
        private readonly ISharedUserFiles _sharedUserFiles;
        private readonly IMapper _mapper;
        public ShareController(ISharedUserFiles sharedUserFiles, IMapper mapper)
        {
            _sharedUserFiles = sharedUserFiles;
            _mapper = mapper;
        }



        [HttpPost]
        public async Task<ActionResult<SharedUserFilesDTO>> ShareLink([FromBody] SharedUserFilesDTO sharedUserFilesDTO)
        {
            ShareLink_WithUsers share = null;
            if (sharedUserFilesDTO.UserFilesId != null)
            {
                var mapperModel = _mapper.Map<ShareLink_WithUsers>(sharedUserFilesDTO);
                if (_sharedUserFiles.Exist(sharedUserFilesDTO.UserFilesId, sharedUserFilesDTO.UserId))
                {
                    share = await _sharedUserFiles.Reshare(mapperModel.UserFilesId, mapperModel.UserId);
                }
                else
                {
                    share = await _sharedUserFiles.Add(mapperModel);
                }
                return Ok(_mapper.Map<SharedUserFilesDTO>(share));
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}