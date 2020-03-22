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
    public class SecurityRoleController : ControllerBase
    { 
        private readonly SecurityRoleLogic _logic;

    public SecurityRoleController()
    {
        var repo = new EFGenericRepository<SecurityRolePoco>();
        _logic = new SecurityRoleLogic(repo);
    }
    [HttpGet]
    [Route("roles/{id}")]
    public ActionResult GetSecurityRole(Guid id)
    {
        SecurityRolePoco poco = _logic.Get(id);
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
    [Route("roles")]
    public ActionResult GetAllSecurityRole()
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
    [Route("roles")]
    public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] pocos)
    {
        _logic.Add(pocos);
        return Ok();
    }
    [HttpPut]
    [Route("roles")]
    public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] pocos)
    {
        _logic.Update(pocos);
        return Ok();
    }
    [HttpDelete]
    [Route("roles")]
    public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] pocos)
    {
        _logic.Delete(pocos);
        return Ok();
    }

}
       }

