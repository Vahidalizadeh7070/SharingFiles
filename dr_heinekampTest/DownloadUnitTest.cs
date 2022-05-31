using AutoMapper;
using dr_heinekamp.Controllers;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using dr_heinekamp.Models.Repositories;
using dr_heinekampTest.DbInitializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace dr_heinekampTest
{
    public class DownloadUnitTest
    {
        private DownloadFile repository;
        // IMapper object
        private IMapper _mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static DownloadUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMem")
                .Options;
        }
        public DownloadUnitTest()
        {
            var context = new AppDbContext(dbContextOptions);
            DummyDataDbInitializer db = new DummyDataDbInitializer();
            db.Seed(context);

            repository = new DownloadFile(context,null);
            // Use mapper 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShareLink_WithUsers, DownloadDTO>();
            });
            var mapper = config.CreateMapper();

            _mapper = mapper;
        }

        [Fact]
        public void Download_Return_400BadRequest()
        {
            //Arrange
            var controller = new DownloadController(repository,null);
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";
            var fileId = "65fa56b5-aa4d-4df6-a957-3821ad76fwab";
            //Act
            var data = controller.Download(userId,fileId);
            var result = data as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        [Fact]
        public void DownloadByMySelf_Return_400BadRequest()
        {
            //Arrange
            var controller = new DownloadController(repository, null);
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";
            var fileId = "65fa56b5-aa4d-4df6-a957-3821ad76fwab";
            //Act
            var data = controller.DownloadByMySelf(userId, fileId);
            var result = data as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        [Fact]
        public void DownloadSubFileByMySelf_Return_400BadRequest()
        {
            //Arrange
            var controller = new DownloadController(repository, null);
            var userId = "65fa56b5-aa4d-4df6-a957-3821ad76f3aa";
            var subFile = "65fa56b5-aa4d-4df6-a957-3821ad76t3sb";
            //Act
            var data = controller.DownloadSubFilesByMySelf(userId, subFile);
            var result = data as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
    }
}
