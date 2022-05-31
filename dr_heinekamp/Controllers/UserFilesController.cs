using AutoMapper;
using dr_heinekamp.Classes;
using dr_heinekamp.Classes.UserFiles_Classes;
using dr_heinekamp.Classes.UserFilesClasses;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using dr_heinekamp.Models.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dr_heinekamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFilesController : ControllerBase
    {
        private readonly IUserFiles _userFiles;
        private readonly IMapper _mapper;
        public static IWebHostEnvironment _webHostEnvironment;

        public UserFilesController(IUserFiles userFiles, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _userFiles = userFiles;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserFilesDTO>> List(string userId)
        {
            var users = _userFiles.List(userId);
            var statusCode = ListUserFilesStatusCodes.ReturnStatusCodes(users);
            return StatusCode(statusCode, new
            {
                Value = statusCode == 200 ? _mapper.Map<IEnumerable<UserFilesDTO>>(users) : null
            });
        }

        [HttpGet("{id}/{userId}")]
        [Route("details")]
        public ActionResult<UserFilesDTO> Details([FromQuery] string id, [FromQuery] string userId)
        {
            var users = _userFiles.Details(id, userId);
            var statusCode = DetailsUserFilesStatusCodes.ReturnStatusCodes(users);
            return StatusCode(statusCode, new
            {
                value = statusCode == 200 ? _mapper.Map<UserFilesDTO>(users) : null
            });
        }

        [HttpPost]
        [Route("upload")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<UserFilesDTO>> Upload([FromForm] UserFiles_UploadDTOs userFiles)
        {
            var fileName = UploadFiles.Upload(userFiles.UploadFile);
            var mapUserFile = _mapper.Map<UserFiles>(userFiles);
            var upload = await _userFiles.Upload(mapUserFile, fileName);
            int statusCode = UploadUserFilesStatusCodes.ReturnStatusCodes(upload);
            return StatusCode(statusCode, new
            {
                value = statusCode == 201 ? _mapper.Map<UserFilesDTO>(mapUserFile) : null
            });
        }

        [HttpDelete("{id}/{userId}")]
        [Route("delete")]
        public async Task<ActionResult> Delete([FromQuery] string id, [FromQuery] string userId)
        {
            var delete = await _userFiles.Delete(id, userId);
            var statusCode = DeleteUserFilesStatusCodes.ReturnStatusCodes(delete);
            return StatusCode(statusCode);
        }

        [HttpPost]
        [Route("uploadmultiple")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<string>> UploadMultiple([FromForm] AddSubFilesDTO subFiles)
        {
            int statusCode = 0;
            var mapSubFile = _mapper.Map<SubFiles>(subFiles);
            if (subFiles.UploadFile != null)
            {
                foreach (var item in subFiles.UploadFile)
                {
                    var fileName = UploadFiles.UploadMultiple(item);
                    var upload = await _userFiles.UploadMultiple(mapSubFile, item.FileName);
                    statusCode = UploadUserFilesStatusCodes.ReturnStatusCodesForMultipleUploadFile(upload);
                }
            }
            return StatusCode(statusCode, new
            {
                value = statusCode == 201 ? "Files has been uploaded successfully" : "Failed"
            });
        }

        [HttpGet("{fileId}")]
        [Route("listofsubfiles")]
        public ActionResult<IEnumerable<SubFilesDTO>> ListOfSubFiles([FromQuery] string fileId)
        {
            var subFiles = _userFiles.ListOfSubFiles(fileId);
            var statusCode = DetailsUserFilesStatusCodes.ReturnStatusCodesSubFiles(subFiles);
            return StatusCode(statusCode, new
            {
                value = statusCode == 200 ? _mapper.Map<IEnumerable<SubFilesDTO>>(subFiles) : null
            });
        }

        [HttpDelete("{id}/{userFilesId}")]
        [Route("deletesubfiles")]
        public async Task<ActionResult> DeleteSubFiles([FromQuery] string id, [FromQuery] string userFilesId)
        {
            var delete = await _userFiles.DeleteSubFiles(id, userFilesId);
            var statusCode = DeleteUserFilesStatusCodes.ReturnStatusCodesSubFiles(delete);
            return StatusCode(statusCode);
        }

    }
}
