using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using uadec.BusinessLogic;
using uadec.Filters;
using uadec.Models;
using uadec.Repository;

namespace uadec.Controllers
{
    /// <summary>
    /// Parent Controller
    /// </summary>
    [Route("parent")]
    [ApiController]
    [CustomExceptionFilter] //Filter to handle controller exceptions


    public class ParentController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly UadecContext DbContext;

        /// <summary>
        /// Controller construstor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        public ParentController(ILogger<ParentController> logger, UadecContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        /// <summary>
        /// Get all Parents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public ActionResult<List<Parent>> GetAll()
        {
            List<Parent> resultList = DbContext.Parents.ToList();
            return resultList;
        }

        /// <summary>
        /// Add a Parent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Parent> Add(Parent model)
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
        /// Update a Parent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<Parent> Update(Parent model)
        {
            DbContext.Entry(model).State = EntityState.Modified;
            DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Delete a Parent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<Parent> Delete(int id)
        {
            Parent deleteModel = DbContext.Parents.Find(id);
            if (deleteModel == null)
            {
                return NotFound();
            }
            DbContext.Parents.Remove(deleteModel);
            DbContext.SaveChanges();
            return deleteModel;
        }

        [HttpGet]
        [Route("FindUser")]
        public ActionResult<List<Parent>> FindUser(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }

            List<Parent> usersFound = DbContext.Parents.Where(s =>
            (s.Name.IsEqualTo(userName) ||
            s.LastName.IsEqualTo(userName) ||
            s.LastNameMother.IsEqualTo(userName))).ToList();

            return usersFound;
        }
    }
}