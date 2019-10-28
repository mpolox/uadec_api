using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using uadec.Filters;
using uadec.BusinessLogic;

namespace uadec.Controllers
{
    [Route("Notification")]
    [ApiController]
    [CustomExceptionFilter] //Filter to handle controller exceptions
    public class NotificationController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> GetAll()
        {
            return Ok("This is the GetAll endpoint");
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok("This is a single Get for id: " + id);
        }

        [HttpGet]
        [Route("Error")]
        public ActionResult<bool> GetError()
        {
            bool result = StudentsManager.GetSuperError(1);
            return result;
        }

    }
}