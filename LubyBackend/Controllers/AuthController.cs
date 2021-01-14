using LubyBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Repositorys.Context;
using Repositorys.Core;
using Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LubyBackend.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        public IConfiguration Configuration;
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.Configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {

            string header_encrypted = Request.Headers["encrypted"];
            bool encripted = !string.IsNullOrEmpty(header_encrypted) ? bool.Parse(header_encrypted) : false;

            string hash = model.Password;

            if (!encripted)
            {
                int workfactor = Int32.Parse(Configuration["bcrypt:workfactor"]);
                hash = BCryptService.GenerateBCryptHash(hash, workfactor);
            }

            var user = userRepository.GetUser(model.Login.ToLower(), hash);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

    }
}
