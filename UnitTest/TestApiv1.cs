using Microsoft.EntityFrameworkCore;
using SurfApi.Models;
using SurfApi;
using SurfApi.Controllers;
using SurfApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest
{
    [TestClass]
    public class TestApiv1
    {
        RentalsApiv1Controller controller;
        public static SurfApiContext GetSurfApiContext(string dbName)
        {
            // Create options for DbContext
            var options = new DbContextOptionsBuilder<SurfApiContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create DbContext
            var dbContext = new SurfApiContext(options);

            dbContext.Surfboard.AddRange(
                new Surfboard
                {
                    ID = 1,
                    Name = "The Minilog",
                    Length = 6,
                    Width = 21,
                    Thickness = 2.75,
                    Volume = 38.8,
                    Price = 565,
                    Type = "Shortboard",
                    Equipment = " ",
                    Image = "/Images/surfboard1.jpg",
                    RowVersion = new byte[] {1,2,3,4,5}
            }) ;
            dbContext.SaveChanges();

            return dbContext;
        }
        [TestInitialize]
        public void Initialize() 
        {
            var testdb = GetSurfApiContext("Testdb");

            controller = new(testdb);
        }



        [TestMethod]
        public void GetAllBoards_Normal_Variable()
        {
            //Arrange

            //Act
            var allboards = controller.GetAllBoards("NotSignedIn");

            //Assert
            Assert.IsNotNull(allboards);
            
        }

        [TestMethod]
        public void GetSurfBoard_Normal_One()
        {
            //Arrange

            //Act
            var testboard =  controller.GetSurfboard(1) as OkObjectResult
            var surfboard = testboard.Value;

            //Assert
            Assert.IsNotNull (surfboard);
            Assert.AreEqual("The Minilog", surfboard.Name);
        }
    }
}