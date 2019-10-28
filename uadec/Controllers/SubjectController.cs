using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using uadec.Filters;
using uadec.Models;
using uadec.Repository;

namespace uadec.Controllers
{
    /// <summary>
    /// Subject Controller
    /// </summary>
    [Route("subject")]
    [ApiController]
    [CustomExceptionFilter] //Filter to handle controller exceptions

    public class SubjectController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly UadecContext DbContext;

        /// <summary>
        /// Subject Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        public SubjectController(ILogger<SubjectController> logger, UadecContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        /// <summary>
        /// Get All subjects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public ActionResult<List<Subject>> GetAll()
        {
            List<Subject> allSubjects = DbContext.Subjects.ToList();
            return allSubjects;
        }

        /// <summary>
        /// Add Subject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Subject> Add(Subject model)
        {
            DbContext.Add(model);
            DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Update subject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<Subject> Update(Subject model)
        {
            DbContext.Entry(model).State = EntityState.Modified;
            DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Delete Subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<Subject> Delete(int id)
        {
            Subject deleteModel = DbContext.Subjects.Find(id);
            if (deleteModel == null)
            {
                return NotFound();
            }
            DbContext.Subjects.Remove(deleteModel);
            DbContext.SaveChanges();
            return deleteModel;
        }

    }
}