using AutoMapper;
using dr_heinekamp.Controllers;
using dr_heinekamp.DTOs;
using dr_heinekamp.Models;
using dr_heinekamp.Models.Repositories;
using dr_heinekampTest.DbInitializer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace dr_heinekampTest
{
    public class UserFilesUnitTest
    {
        private UserFilesRepo repository;
        // IMapper object
        private IMapper _mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        static UserFilesUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMem")
                .Options;
        }
        public UserFilesUnitTest()
        {
            var context = new AppDbContext(dbContextOptions);
            DummyDataDbInitializer db = new DummyDataDbInitializer();
            db.Seed(context);

            repository = new UserFilesRepo(context);
            // Use mapper 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserFiles, UserFilesDTO>();
                cfg.CreateMap<UserFiles_UploadDTOs, UserFiles>();
                cfg.CreateMap<UserFilesDTO, UserFiles>();
                cfg.CreateMap<AddSubFilesDTO, SubFiles>();
                cfg.CreateMap<SubFiles, SubFilesDTO>();
            });
            var mapper = config.CreateMapper();

            _mapper = mapper;
        }
        #region List
        [Fact]
        public void List_Return_200OK()
        {
            //Arrange
            var controller = new UserFilesController(repository,_mapper,null);
            // An existing userId in dummy data 
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";
            //Act
            var data = controller.List(userId);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public void List_Return_404()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8b";
            //Act
            var data = controller.List(userId);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound,result.StatusCode);
        }
        #endregion
        #region Details
        [Fact]
        public void Details_Return_200OK()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";
            var id = "65fa56b5-aa4d-4df6-a957-3821ad76f3aa";
            //Act
            var data = controller.Details(id, userId);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public void Details_Return_404NotFound()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";
            var id = "65fa56b5-aa4d-4df6-a957-3821ad76f3ab";
            //Act
            var data = controller.Details(id, userId);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        #endregion
        #region Upload
        [Fact]
        public async void Upload_Return_200OK()
        {
            var file = new Mock<IFormFile>();
            var sourceImg = File.OpenRead(@"H:\WebSite Project\dr_heinekamp\dr_heinekamp\wwwroot\UserFiles\3f46efa4-2a3e-495f-8d21-dc652615e853New Text Document (2).txt");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = "QQ.png";
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var files = new UserFiles_UploadDTOs
            {
                Id = "65fa56b5-aa54d-4fa6-a157-3821ads6f4cb",
                File = "ed993ced-830e-4b70-a43d-d54974750c72CoverLetter.pdf",
                Title = "TestNew",
                UploadFile = inputFile,
                UploadDate=DateTime.Now,
                UserId= "1af4383e-99c6-49ac-b2e9-56f18a841a8a"
            };
            
            //Act
            var data = await controller.Upload(files);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }
        #endregion
        #region Delete
        [Fact]
        public async void Delete_Return_200OK()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);

            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";
            var id = "65fa56b5-aa4d-4df6-a957-3821ad76f3aa";

            //Act
            var data = await controller.Delete(id, userId);
            
            var result = data as StatusCodeResult;
            
            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public void Delete_Return_400BadRequest()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var userId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a";
            var id = "65fa56b5-aa4f-4df6-a55t-3821ad76f3ab";
            //Act
            var data = controller.Delete(id, userId);
            var result = data.Result as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        #endregion
        #region Upload Multiple Files
        [Fact]
        public async void UploadMultipleFiles_Return_200OK()
        {
            var file = new Mock<IFormFile>();
            var sourceImg = File.OpenRead(@"H:\WebSite Project\dr_heinekamp\dr_heinekamp\wwwroot\UserFiles\SubFiles\Bacholar.pdf");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = "Bacholar.pdf";
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;
            var listFiles = new List<IFormFile>();
            for (int i = 0; i < 1;i++)
            {
                listFiles.Add(inputFile);
            }

            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var files = new AddSubFilesDTO
            {
                Id = "65fa56b5-aa54d-4fa6-a157-3821ads6f4cb",
                FileName = "ed993ced-830e-4b70-a43d-d54974750c72CoverLetter.pdf",
                UploadFile = listFiles,
                UserFilesId= "65fa56b5-aa4d-4df6-a957-3821ad76f3aa"
            };

            //Act
            var data = await controller.UploadMultiple(files);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }
        #endregion
        #region List Sub Files
        [Fact]
        public void ListOfSubFiles_Return_200OK()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // An existing userId in dummy data 
            var fileId = "65fa56b5-aa4d-4df6-a957-3821ad76f3aa";
            //Act
            var data = controller.ListOfSubFiles(fileId);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public void ListOfSubFiles_Return_404()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);
            // An existing userId in dummy data 
            var fileId = "65fa56b2-aa4d-4dd6-a957-3821ad76f3aa";
            //Act
            var data = controller.ListOfSubFiles(fileId);
            var result = data.Result as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        #endregion
        #region Delete Sub Files
        [Fact]
        public async void DeleteSubFiles_Return_200OK()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);

            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var userFileId = "65fa56b5-aa4d-4df6-a957-3821ad76f3aa";
            var id = "65fa56b5-aa4d-4df6-a957-3821ad76t3ab";

            //Act
            var data = await controller.DeleteSubFiles(id, userFileId);

            var result = data as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async void DeleteSubFiles_Return_400BadRequest()
        {
            //Arrange
            var controller = new UserFilesController(repository, _mapper, null);

            // This userId does not exist in the dummy data, so this is the cause of return null from our action
            var userFileId = "65fa56b5-aa4d-4df6-a957-3821ad76f4aa";
            var id = "65fa56b5-aa4d-4df6-a957-3821ad76t3ac";

            //Act
            var data = await controller.DeleteSubFiles(id, userFileId);

            var result = data as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        #endregion
    }
}
