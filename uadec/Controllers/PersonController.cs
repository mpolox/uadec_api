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
    [Route("person")]
    [ApiController]
    [CustomExceptionFilter] //Filter to handle controller exceptions

    
    public class PersonController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly UadecContext DbContext;


        public PersonController(ILogger<PersonController> logger, UadecContext dbContext)
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
        public ActionResult<List<Person>> GetAll()
        {
            List<Person> allPersons = DbContext.People.ToList();
            return allPersons;
        }

        /// <summary>
        /// Add a student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Person> Add(Person model)
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
        public ActionResult<Person> Update(Person model)
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
        public ActionResult<Person> Delete(int id)
        {
            Person deleteModel = DbContext.People.Find(id);
            if (deleteModel == null)
            {
                return NotFound();
            }
            DbContext.People.Remove(deleteModel);
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
        public ActionResult<List<Person>> Find(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }

            List<Person> usersFound = DbContext.People.Where(s =>
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

        //[HttpPost]
        //[Route("ToParent")]
        //public ActionResult<bool> LinkToParent(int studentId, int parentId)
        //{
        //    Student student = DbContext.Students.Find(studentId);
        //    Parent parent = DbContext.Parents.Find(parentId);

        //    if (student == null || parent == null)
        //    {
        //        return NotFound();
        //    }

        //    bool alreadyExists = DbContext.StudentParents.Any(sp => sp.StudentId == studentId && sp.ParentId == parentId);
        //    if (alreadyExists)
        //    {
        //        return BadRequest("Already defined");
        //    }

        //    StudentParent newModel = new StudentParent
        //    {
        //        ParentId = parentId,
        //        StudentId = studentId
        //    };

        //    DbContext.Add(newModel);
        //    DbContext.SaveChanges();

        //    return true;
        //}

        //[HttpGet]
        //[Route("ParentId")]
        //public ActionResult<List<Student>> GetByParentId(int parentId)
        //{
        //    List<Student> students = DbContext.StudentParents.Where(s => s.ParentId == parentId).Select(s => s.Student).ToList();
        //    students.OrderBy(s => s.Name);
        //    return students;
        //}
    }
}
