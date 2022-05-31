using dr_heinekamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dr_heinekampTest.DbInitializer
{
    public class DummyDataDbInitializer
    {
        public DummyDataDbInitializer()
        {

        }
        public void Seed(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Users.AddRange(
                new ApplicationUser() { Id = "1af4383e-99c6-49ac-b2e9-56f18a841a8a", Email = "TestUser1@gmail.com" },
                new ApplicationUser() { Id = "454e1a1e-c9ee-41d0-8367-237dbfc5ebb7", Email = "TestUser2@yahoo.com" },
                new ApplicationUser() { Id = "7585bc91-edd3-47a1-b7bf-e1c1a1a41a16", Email = "TestUser3@outlook.com" }
            );

            context.UserFiles.AddRange(
                new UserFiles() { Id = "65fa56b5-aa4d-4df6-a957-3821ad76f3aa", Title = "Test1", File = "test1.pdf", UploadDate = DateTime.Now, UserId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a" },
                new UserFiles() { Id = "77ee8d71-8daf-45dd-92fe-77f2196d0bc2", Title = "Test2", File = "test2.docx", UploadDate = DateTime.Now, UserId = "454e1a1e-c9ee-41d0-8367-237dbfc5ebb7" },
                new UserFiles() { Id = "fe081a0f-c527-4ea7-901d-9c100238c344", Title = "Test3", File = "test2.docx", UploadDate = DateTime.Now, UserId = "7585bc91-edd3-47a1-b7bf-e1c1a1a41a16" }
            );

            context.SubFiles.AddRange(
                new SubFiles() { Id = "65fa56b5-aa4d-4df6-a957-3821ad76t3ab", FileName="test1.pdf",UserFilesId= "65fa56b5-aa4d-4df6-a957-3821ad76f3aa" },
                new SubFiles() { Id = "7f10158a-dada-4cd0-90d5-86bf7ea58d11", FileName = "test2.docx", UserFilesId = "fe081a0f-c527-4ea7-901d-9c100238c344" }
            );
            context.ShareLink_WithUsers.AddRange(
                new ShareLink_WithUsers() { Id = "1779ef65-c31b-45da-bac7-696b599b1e58", UserFilesId = "65fa56b5-aa4d-4df6-a957-3821ad76f3aa", ExpirationDate = DateTime.Now, Download = false, DownloadURL = "", UserId = "1af4383e-99c6-49ac-b2e9-56f18a841a8a" }
                );
            context.SaveChanges();
        }
    }
}
