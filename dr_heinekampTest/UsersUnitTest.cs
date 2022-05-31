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
    public class UsersUnitTest
    {
        private UsersRepo repository;
        // IMapper object
        private IMapper _mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static UsersUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMem")
                .Options;
        }
        public UsersUnitTest()
        {
            var context = new AppDbContext(dbContextOptions);
            DummyDataDbInitializer db = new DummyDataDbInitializer();
            db.Seed(context);

            repository = new UsersRepo(context);
            // Use mapper 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UsersListDTO>();
            });
            var mapper = config.CreateMapper();

            _mapper = mapper;
        }

        #region List
        [Fact]
        public void List_Return_200OK()
        {
            //Arrange
            var controller = new UsersController(repository, _mapper);
            //Act
            var data = controller.List();
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        #endregion
    }
}
