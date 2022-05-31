using AutoMapper;
using dr_heinekamp.DTOs;
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
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadFile downloadFile;
        private readonly IWebHostEnvironment environment;

        public DownloadController(IDownloadFile downloadFile, IWebHostEnvironment _environment)
        {
            this.downloadFile = downloadFile;
            environment = _environment;
        }

        [HttpGet("{userId}/{fileId}")]
        [Route("download")]
        public IActionResult Download([FromQuery] string userId, [FromQuery] string fileId)
        {
            var fileName = downloadFile.Download(userId, fileId);
            if (fileName != null)
            {
                //Build the File Path.
                string paths = Path.Combine(environment.WebRootPath, "UserFiles/") + fileName;

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(paths);
                downloadFile.UpdateDownloadStatus(userId,fileId);
                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("{userId}/{file}")]
        [Route("downloadbymyself")]
        public IActionResult DownloadByMySelf([FromQuery] string userId,[FromQuery] string file)
        {
            var fileName = downloadFile.DownloadByMySelf(userId, file);
            if (fileName != null)
            {
                //Build the File Path.
                string paths = Path.Combine(environment.WebRootPath, "UserFiles/") + fileName;

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(paths);
                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("{userId}/{fileId}")]
        [Route("downloadsubfilesbymyself")]
        public IActionResult DownloadSubFilesByMySelf([FromQuery] string userfileId, [FromQuery] string subfileid)
        {
            var fileName = downloadFile.DownloadSubFilesByMySelf(userfileId, subfileid);
            if (fileName != null)
            {
                //Build the File Path.
                string paths = Path.Combine(environment.WebRootPath, "UserFiles/SubFiles/") + fileName;

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(paths);
                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
