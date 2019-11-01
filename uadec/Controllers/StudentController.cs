using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using uadec.DTOs;
using uadec.Filters;
using uadec.Models;
using uadec.Repository;

namespace uadec.Controllers
{
    [Route("student")]
    [ApiController]
    [CustomExceptionFilter] //Filter to handle controller exceptions
    public class StudentController : ControllerBase
    {
        private protected readonly ILogger Logger;
        private protected readonly UadecContext DbContext;


        public StudentController(ILogger<StudentController> logger, UadecContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<User> Add(int userId)
        {
            User user = DbContext.Users.Find(userId);
            bool isExisting = DbContext.Students.Any(s => s.UserId == userId);

            if (user == null)
            {
                return BadRequest("user does not exist");
            }
            if (isExisting)
            {
                return BadRequest("student already defined");
            };

            Student model = new Student
            {
                UserId = userId
            };

            DbContext.Add(model);
            DbContext.SaveChanges();
            return user;
        }

        [HttpPost]
        [Route("AddList")]
        public ActionResult<List<Student>> AddList(ItemsById items)
        {
            if (items == null)
            {
                return BadRequest("No Ids entered");
            }

            List<int> ids = items.Items.Select(i => i.Id).Distinct().ToList();
            List<int> users = DbContext.Users.Where(u => ids.Contains(u.Id)).Select(u => u.Id).ToList();

            if (users.Count == 0)
            {
                return BadRequest("users do not exist");
            }

            //Get already defined students
            var existingStudents = DbContext.Students.Where(s => users.Contains(s.UserId)).Select(s => s.UserId);

            //Remove users already defined as students
            var newStudentsIds = users.Where(u => !existingStudents.Contains(u));

            //Create Student model
            List<Student> newStudentsModel = newStudentsIds.Select(s => new Student
            {
                UserId = s
            }).ToList();


            if (newStudentsModel.Count == 0)
            {
                return newStudentsModel;
            }

            DbContext.AddRange(newStudentsModel);
            DbContext.SaveChanges();
            return newStudentsModel;
        }
    }
}