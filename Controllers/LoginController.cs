using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPVDB_Api.Helpers;
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
    public class LoginController : ControllerBase
    {
        private readonly ILoginInfoService _service;
        private readonly JwtHelpers _jwtHelpers;
        public LoginController(JwtHelpers jwtHelpers,ILoginInfoService service)
        {
            _service = service;
            _jwtHelpers = jwtHelpers;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (_service.ValidateUser(loginViewModel))
            {
                var data = _service.GetData(loginViewModel.Username);
                return Ok(new
                {
                    token = _jwtHelpers.GenerateToken(_service.GetRoleId((int)data.Id), (int)data.Id, data.Username)
                });
            }
            else
            {
                return Forbid();
            }
        }


    }
}
