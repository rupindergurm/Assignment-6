using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
            
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeController()

        { 

        var repo = new EFGenericRepository<SystemLanguageCodePoco>();
        _logic = new SystemLanguageCodeLogic(repo);
    }
    [HttpGet]
    [Route("language/{id}")]
    public ActionResult GetSystemLanguageCode(String id)
    {
        SystemLanguageCodePoco poco = _logic.Get(id);
        if (poco == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(poco);
        }
    }
    [HttpGet]
    [Route("language")]
    public ActionResult GetAllSystemLanguageCode()
    {
        var applicants = _logic.GetAll();
        if (applicants == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(applicants);
        }
    }

    [HttpPost]
    [Route("language")]
    public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] appEduPocos)
    {
        _logic.Add(appEduPocos);
        return Ok();
    }
    [HttpPut]
    [Route("language")]
    public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
    {
        _logic.Update(pocos);
        return Ok();
    }
    [HttpDelete]
    [Route("langage")]
    public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
    {
        _logic.Delete(pocos);
        return Ok();
    }

}
       }






