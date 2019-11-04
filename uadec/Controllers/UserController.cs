using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using uadec.Filters;
using uadec.Models;
using uadec.Repository;
using uadec.BusinessLogic;
using uadec.Helpers;
using System.Data.SqlClient;
using uadec.DTOs;

namespace uadec.Controllers
{
    /// <summary>
    /// Student Controller
    /// </summary>
    [Route("user")]
    [ApiController]
    [CustomExceptionFilter] //Filter to handle controller exceptions

    
    public class UserController : ControllerBase
    {
        private protected readonly ILogger Logger;
        private protected readonly UadecContext DbContext;


        public UserController(ILogger<UserController> logger, UadecContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public ActionResult<List<User>> GetAll()
        {
            List<User> allPersons = DbContext.Users.ToList();
            return allPersons;
        }

        /// <summary>
        /// Add a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> Add(User model)
        {
            if (model == null || model.Name == null)
            {
                return BadRequest("User name not defined");
            }

            bool isFound = DbContext.Users.Any(p => p.Email.IsEqualTo(model.Email));

            if (isFound)
            {
                return BadRequest("User already defined");
            }

            DbContext.Add(model);
            DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<User> Update(User model)
        {
            User foundModel = DbContext.Users.Find(model.Id);
            //implement mapping
            foundModel.Email = model.Email;
            foundModel.Name = model.Name;
            foundModel.LastName = model.LastName;
            foundModel.LastNameMother = model.LastNameMother;
            foundModel.Phone = model.Phone;
 
            DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<User> Delete(int id)
        {
            User deleteModel = DbContext.Users.Find(id);
            if (deleteModel == null)
            {
                return NotFound();
            }
            DbContext.Users.Remove(deleteModel);
            DbContext.SaveChanges();
            return deleteModel;
        }

        /// <summary>
        /// Find user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public ActionResult<List<User>> Find(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }

            List<User> usersFound = DbContext.Users.Where(s =>
            (s.Name.IsEqualTo(userName) ||
            s.LastName.IsEqualTo(userName) ||
            s.LastNameMother.IsEqualTo(userName))).ToList();

            return usersFound;
        }

        /// <summary>
        /// Find user name, uses SP
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("FindLike")]
        public ActionResult<List<UserModel>> FindLike(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }

            var parameters = new SqlParameter[] {
                new SqlParameter("@clientId", userName)
            };

            var usersFound = DbContext.UserSP.FromSql("GetUserByName @clientId", parameters).ToList();
            return usersFound;
        }
    }
}
