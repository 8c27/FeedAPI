using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPVDB_Api.Models.ViewModels;
using SEPVDB_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPVDB_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogininfoController : ControllerBase
    {
        private readonly ILoginInfoService _service;

        public LogininfoController(ILoginInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllDatas());
        }
        [HttpGet("~/LoginRoles")]
        public IActionResult GetLoginRoles()
        {
            return Ok(_service.GetAllRoles());
        }


        [HttpPost]
        public IActionResult Post(LoginInfoViewModel viewModel)
        {

            _service.InsertData(viewModel);
            return Ok(new { result = "inserted" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_service.DeleteData(id)) return Ok(new { result = "deleted" });
                else return NotFound(new { result = "找不到目標資源" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, LoginInfoViewModel viewModel)
        {
          
            try
            {
                if (_service.EditData(id, viewModel)) return Ok(new { result = "updated" });
                else return NotFound(new { result = "找不到目標資源" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword (ChangePasswordViewModel viewModel)
        {
            try
            {
                var id = Int32.Parse(User.Claims.FirstOrDefault(t => t.Type == "user_id").Value);
                // TODO: 透過api取得admin權限的所有角色
                if (_service.ChangePassword(id, viewModel, User.IsInRole("admin")))
                {
                    return Ok(new { result = "Password Changed" });
                }
                else
                {
                    return BadRequest(new { result = "Password Change faild" });
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
