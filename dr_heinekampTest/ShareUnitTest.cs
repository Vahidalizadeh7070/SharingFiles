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
    public class ShareUnitTest
    {
        private SharedUserFilesRepo repository;
        // IMapper object
        private IMapper _mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static ShareUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMem")
                .Options;
        }
        public ShareUnitTest()
        {
            var context = new AppDbContext(dbContextOptions);
            DummyDataDbInitializer db = new DummyDataDbInitializer();
            db.Seed(context);

            repository = new SharedUserFilesRepo(context);
            // Use mapper 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShareLink_WithUsers, SharedUserFilesDTO>();
                cfg.CreateMap<SharedUserFilesDTO, ShareLink_WithUsers>();
            });
            var mapper = config.CreateMapper();

            _mapper = mapper;
        }

        #region ShareLink
        [Fact]
        public async void ShareLink_Return_200OK()
        {
            //Arrange
            var controller = new ShareController(repository, _mapper);
            var model = new SharedUserFilesDTO()
            {
                Id = "1779ef65-c31b-46da-bac7-696b599sys19",
                Download = true,
                DownloadURL = "http://test.com",
                ExpirationDate = DateTime.Now,
                UserFilesId = "77ee8d71-8daf-45dd-92fe-77f2196d0bc2",
                UserId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a"
            };
            //Act
            var data = await controller.ShareLink(model);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async void ShareLink_400_BadRequest()
        {
            //Arrange
            var controller = new ShareController(repository, _mapper);
            var model = new SharedUserFilesDTO()
            {
                Id = "1779ef65-c31b-46da-bac7-696b599sys19",
                Download = true,
                ExpirationDate = DateTime.Now,
                UserId = "1af4383e-99c6-49ac-b2e9-56f18a841s85"
            };
            //Act
            var data = await controller.ShareLink(model);
            var result = data.Result as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        #endregion
    }
}
