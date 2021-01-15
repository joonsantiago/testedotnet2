using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Repositorys.Interfaces;
using Models.Core;
using Microsoft.AspNetCore.Authorization;
using Models.Dto;
using LubyBackend.Utils;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using LubyBackend.Services;

namespace LubyBackend.Controllers
{
    [ApiController]
    [Route("v1/users")]
    [Authorize]
    public class UserController : BaseController
    {
        IUserRepository userRepository;
        IConfiguration configuration;

        int workfactor;
        string urlValidateCPF;
        public UserController(IUserRepository userRepository, IConfiguration configuration) : base(configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;


            this.workfactor = Int32.Parse(configuration["bcrypt:workfactor"]);
            this.urlValidateCPF = configuration["external_links:validate_cpf"];
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<dynamic>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id user it's required" });
            }

            try
            {
                UserDto data_user = userRepository.GetById(id);
                if (data_user == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having user with the Id" });
                }
                return Ok(new { success = true, data = data_user, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find user by Id");
            }
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetAll([FromQuery(Name = "page")] int page = 0, [FromQuery(Name = "size")] int sizePage = 15)
        {
            int skip = page * sizePage;

            string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;


            try
            {
                int total = userRepository.Count();
                List<UserDto> list = userRepository.GetList(skip, sizePage);

                Pagination<UserDto> dataPagination = new Pagination<UserDto>
                {
                    Page = page,
                    TotalCount = total,
                    SizePage = sizePage,
                    Items = list
                };

                return dataPagination;
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find all users");
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> SaveUser([FromBody] User user)
        {
            List<string> validations_erro = new List<string>();
            bool updadtePassword = false;
            if (string.IsNullOrEmpty(user.Name))
            {
                validations_erro.Add("User name is required");
            }

            if (string.IsNullOrEmpty(user.CPF))
            {
                validations_erro.Add("User CPF is required");
            }

            if (user.Role <= 0)
            {
                validations_erro.Add("User role is required");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                validations_erro.Add("User e-mail is required");
            }

            if (!string.IsNullOrEmpty(user.Login) || !string.IsNullOrEmpty(user.Password))
            {
                if (!(!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password)))
                {
                    validations_erro.Add("User login and password is required");
                }
                else
                {
                    updadtePassword = true;
                    user.Password = BCryptService.GenerateBCryptHash(user.Password, workfactor);
                }
            }

            try
            {
                var responseCPFValid = HttpService.Get(urlValidateCPF + "/" + user.CPF);
                if (responseCPFValid.httpStatusCode != 200 && responseCPFValid.message != "Autorizado")
                {
                    validations_erro.Add("CPF não é valido");
                }

                var older_user_cpf = userRepository.GetUser(user.CPF);
                if(older_user_cpf != null)
                {
                    validations_erro.Add("Already exists user with this CPF");
                }

                var older_user_login = userRepository.GetUserLogin(user.Login);
                if (older_user_login != null)
                {
                    validations_erro.Add("Already exists user with this Login");
                }
            }
            catch (Exception ex1)
            {
                return CatchError(ex1, string.Format("Update user id({0}), validate CPF", user.Id));
            }

            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }


            try
            {
                user.Name = user.Name.ToUpper();
                user.Login = user.Login.ToLower();
                user.Email = user.Email.ToLower();
                User data_user = userRepository.Save(user, updadtePassword);
                return Ok(new { success = true, data = data_user, messages = "Item successfull created" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Save new user");
            }
        }

        [HttpPatch]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> UpdateUser([FromBody] User user)
        {
            List<string> validations_erro = new List<string>();
            bool updadtePassword = false;

            if (string.IsNullOrEmpty(user.Name))
            {
                validations_erro.Add("User name is required");
            }

            if (string.IsNullOrEmpty(user.CPF))
            {
                validations_erro.Add("User CPF is required");
            }

            if (user.Id <= 0)
            {
                validations_erro.Add("User id is required");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                validations_erro.Add("User e-mail is required");
            }

            if (user.Role <= 0)
            {
                validations_erro.Add("User role is required");
            }

            if (!string.IsNullOrEmpty(user.Login) || !string.IsNullOrEmpty(user.Password))
            {
                if( !(!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password)))
                {
                    validations_erro.Add("User login and password is required");
                }
                else
                {
                    updadtePassword = true;
                    user.Password = BCryptService.GenerateBCryptHash(user.Password, workfactor);
                }
            }

            try
            {
                var responseCPFValid = HttpService.Get(urlValidateCPF + "/" + user.CPF);
                if(responseCPFValid.httpStatusCode != 200 || responseCPFValid.message != "Autorizado")
                {
                    validations_erro.Add("CPF não é valido");
                }

                var older_user_cpf = userRepository.GetUser(user.CPF);
                if (older_user_cpf != null)
                {
                    validations_erro.Add("Already exists user with this CPF");
                }

                var older_user_login = userRepository.GetUserLogin(user.Login);
                if (older_user_login != null)
                {
                    validations_erro.Add("Already exists user with this Login");
                }
            }
            catch (Exception ex1)
            {
                return CatchError(ex1, string.Format("Update user id({0}), validate CPF", user.Id));
            }

            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                user.Name = user.Name.ToUpper();
                user.Login = user.Login.ToLower();
                user.Email = user.Email.ToLower();
                User data_user = userRepository.Save(user, updadtePassword);
                return Ok(new { success = true, data = data_user, messages = "Item successfull updated" });
            }
            catch (Exception ex2)
            {
                return CatchError(ex2, string.Format("Update user id({0})", user.Id));
            }

        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id user" });
            }

            try
            {
                User data_user = userRepository.GetUser(id);
                if (data_user == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having user with the Id" });
                }
                userRepository.Delete(data_user);
                return Ok(new { success = true, data = id, messages = "Item successfull deleted" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Delete user id ({0})", id));
            }
        }

    }
}
