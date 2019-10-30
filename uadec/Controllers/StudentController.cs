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
    [Route("student")]
    [ApiController]
    [CustomExceptionFilter] //Filter to handle controller exceptions

    
    public class StudentController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly UadecContext DbContext;

        /// <summary>
        /// Controller construstor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        public StudentController(ILogger<StudentController> logger, UadecContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public ActionResult<List<Student>> GetAll()
        {            
            List<Student> allStudents = DbContext.Students.ToList();
            return allStudents;
        }

        /// <summary>
        /// Add a student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Student> Add(Student model)
        {
            if (model.Name == null)
            {
                return BadRequest("User name not defined");
            }
            DbContext.Add(model);
            DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Update a student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<Student> Update(Student model)
        {
            DbContext.Entry(model).State = EntityState.Modified;
            DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Delete a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<Student> Delete(int id)
        {
            Student deleteModel = DbContext.Students.Find(id);
            if (deleteModel == null)
            {
                return NotFound();
            }
            DbContext.Students.Remove(deleteModel);
            DbContext.SaveChanges();
            return deleteModel;
        }

        /// <summary>
        /// Find student
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public ActionResult<List<Student>> Find(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }

            List<Student> usersFound = DbContext.Students.Where(s => 
            (s.Name.IsEqualTo(userName) ||
            s.LastName.IsEqualTo(userName) ||
            s.LastNameMother.IsEqualTo(userName))).ToList();

            var parameters = new SqlParameter[] {
                    new SqlParameter("@clientId", userName)
                 };

            var f = DbContext.UserSP.FromSql("GetUserByName @clientId", parameters).ToList();
            return usersFound;
        }

        /// <summary>
        /// Find student name, uses SP
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
