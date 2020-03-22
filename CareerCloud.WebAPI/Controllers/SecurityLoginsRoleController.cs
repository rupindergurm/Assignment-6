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
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase

    {
        private readonly SecurityLoginsRoleLogic _logic;

    public SecurityLoginsRoleController()
    {
        var repo = new EFGenericRepository<SecurityLoginsRolePoco>();
        _logic = new SecurityLoginsRoleLogic(repo);
    }
    [HttpGet]
    [Route("role/{id}")]
    public ActionResult GetSecurityLoginsRole(Guid id)
    {
        SecurityLoginsRolePoco poco = _logic.Get(id);
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
    [Route("role")]
    public ActionResult GetAllSecurityLoginsRole()
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
    [Route("role")]
    public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] appEduPocos)
    {
        _logic.Add(appEduPocos);
        return Ok();
    }
    [HttpPut]
    [Route("role")]
    public ActionResult PutSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] pocos)
    {
        _logic.Update(pocos);
        return Ok();
    }
    [HttpDelete]
    [Route("role")]
    public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos)
    {
        _logic.Delete(pocos);
        return Ok();
    }

}
       }




