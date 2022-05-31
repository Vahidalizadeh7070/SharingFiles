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
    public class InboxUnitTest
    {
        private InboxRepo repository;
        // IMapper object
        private IMapper _mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static InboxUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMem")
                .Options;
        }
        public InboxUnitTest()
        {
            var context = new AppDbContext(dbContextOptions);
            DummyDataDbInitializer db = new DummyDataDbInitializer();
            db.Seed(context);

            repository = new InboxRepo(context);
            // Use mapper 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShareLink_WithUsers, InboxDTO>();
            });
            var mapper = config.CreateMapper();

            _mapper = mapper;
        }

        #region List Inbox
        [Fact]
        public void InboxList_Return_200OK()
        {
            //Arrange
            var controller = new InboxController(repository, _mapper);

            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";

            //Act
            var data =  controller.Inbox(userId);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public void InboxList_Return_404_NotFound()
        {
            //Arrange
            var controller = new InboxController(repository, _mapper);

            var userId = "1af4383e-99c6-a9ac-b2e9-56f18as41a8b";

            //Act
            var data = controller.Inbox(userId);
            var result = data.Result as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        #endregion

        #region Count
        [Fact]
        public void Inbox_Count_OK()
        {
            //Arrange
            var controller = new InboxController(repository, _mapper);

            var fileId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";

            //Act
            var data = controller.Count(fileId);
            var result = data as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        #endregion
    }
}
