using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using uadec.BusinessLogic;
using uadec.Controllers;
using uadec.Models;
using uadec.Repository;
using Xunit;

namespace uadecTest.StudentManager
{
    public class StudentManagerTest : IDisposable
    {
        public UadecContext uadecContext { get; set; }
        public UserController userController;

        const int ZERO = 0;
        User USER_01 = new User
        {
            Email = "test@123.com",
            Name = "Marcopolo",
            LastName = "Ramos",
            LastNameMother = "Peña",
            Phone = "3216549870"
        };
        User USER_02 = new User
        {
            Email = "test2@123.com",
            Name = "Marcopolo2",
            LastName = "Ramos2",
            LastNameMother = "Peña2",
            Phone = "32165498702"
        };

        public StudentManagerTest()
        {
            /* Create a Memory Database instead of using the SQL */
            var options = new DbContextOptionsBuilder<UadecContext>().UseInMemoryDatabase(databaseName: "database_name").Options;

            uadecContext = new UadecContext(options);
            userController = new UserController(null, uadecContext);
        }

        public void Dispose()
        {
            uadecContext.Database.EnsureDeleted();
        }

        private bool AddUser(User user)
        {
            var response = userController.Add(user);
            Assert.NotNull(response);
            Assert.True(response.Value.Id > ZERO);
            return true;
        }

        private bool DeleteUser(User user)
        {
            var response = userController.Delete(user.Id);
            Assert.Equal(response.Value.Email, user.Email);
            Assert.Equal(response.Value.Name, user.Name);
            return true;
        }

        private bool CheckEmpty()
        {
            var responseAll = userController.GetAll();
            Assert.Empty(responseAll.Value);
            return true;
        }
        #region Business Tests
        [Fact]
        public void EqualNamesTest()
        {
            string compareString = "galván";
            string expectedString_01 = "Galvan";
            string expectedString_02 = "galvan";
            string expectedString_03 = "gálvan";
            string expectedString_04 = "GalvÁn";
            string expectedString_05 = "galván";
            string expectedString_06 = "galván ";
            Assert.True(compareString.IsEqualTo(expectedString_01));
            Assert.True(compareString.IsEqualTo(expectedString_02));
            Assert.True(compareString.IsEqualTo(expectedString_03));
            Assert.True(compareString.IsEqualTo(expectedString_04));
            Assert.True(compareString.IsEqualTo(expectedString_05));
            Assert.True(compareString.IsEqualTo(expectedString_06));
        }

        [Fact]
        public void DifferentNamesTest()
        {
            string compareString = "Marcopolo";
            string expectedString_01 = "Nacopolo";
            string expectedString_02 = "polo";
            string expectedString_03 = "Marcopoloo";
            string expectedString_04 = "Marco";
            string expectedString_05 = "Marc0polo";
            Assert.False(compareString.IsEqualTo(expectedString_01));
            Assert.False(compareString.IsEqualTo(expectedString_02));
            Assert.False(compareString.IsEqualTo(expectedString_03));
            Assert.False(compareString.IsEqualTo(expectedString_04));
            Assert.False(compareString.IsEqualTo(expectedString_05));
        }
        #endregion

        #region User Controller Tests
        [Fact]
        public void UserController_AddNull()
        {
            var response = userController.Add(null);            
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void UserController_Add()
        {
            //Test
            Assert.True(AddUser(USER_01));
        }

        [Fact]
        public void UserController_GetAll()
        {
            //Preparation
            Assert.True(AddUser(USER_01));
            Assert.True(AddUser(USER_02));

            //Test
            var result = userController.GetAll();
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public void UserController_Udate()
        {
            //Preparation
            Assert.True(AddUser(USER_01));

            //Test
            var result = userController.
        }
        #endregion
    }
}
