using System.Security.Claims;
using Livros.API.Extensions;
using Livros.Domain.Models;
using Livros.Helper.EmailLog;
using Livros.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public UsersController(IUserService userService, IConfiguration configuration, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _configuration = configuration;            
        }

        [HttpPost]
        [Route("Register")]
        [Produces("application/json")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (user is not null)
                    await _userService.CreateUser(user);

                return Ok("Registro efetuado com sucesso!");
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Not possible register this user. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                if (id > 0)
                    await _userService.DeleteAccount(id);

                return Ok("User Delete with success");
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"This user not exist. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAccount(User user)
        {
            try
            {
                var authorize = await _userService.GetUserByName(User.GetUserName());
                if(authorize is null) return Unauthorized("Usuario invalido!");

                if (user is not null && user.Id > 0)
                {
                    await _userService.UpdateAccount(user);
                    return Ok("User update with success!");
                }

                throw new Exception();
                    
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Error trying update this user. Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Recovery")]
        [Produces("application/json")]
        public async Task<IActionResult> RecoveryPassword(string email)
        {
            try
            {
                if (email is not null)
                {
                    var emailLog = _configuration.GetSection("EmailSettings").Get<LogHelper.EmailLog>();
                    await _userService.RecoveryPassword(email.ToString(), emailLog);
                }

                return Ok("E-mail enviado com sucesso!");
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar recuperar a senha!.\nError: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Produces("application/json")]
        public async Task<IActionResult> ChangePassword(int id, string password)
        {
            try
            {
                await _userService.ChangePassword(id, password);

                return Ok("Senha alterada com sucesso!");
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar recuperar a senha!.\nError: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                
                var result = await _userService.GetUser(email, password);

                if(result.Email is not null)
                {
                    return Ok(new 
                    {
                        Nome = result.Name,
                        SobreNome = result.LastName,
                        token = _tokenService.CreateToken(result)
                    });
                }

                throw new Exception();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"E-mail ou senha incorretos");
            }
        }

        [HttpGet]
        [Route("GetUser")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _userService.GetUserByName(userName ?? "null");
                return Ok(user);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Token incorreto");
            }
        }

    }
}