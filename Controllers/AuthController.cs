using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using ClimaGloboApi.Dtos.Account;
using ClimaGloboApi.Dtos.User;
using ClimaGloboApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClimaGloboApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _context = applicationDbContext;
            _tokenService = new TokenService(configuration);
        }
        
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto login)
        {

            LoggedUserDto loggedUser = null;

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.email.StartsWith(login.email + "@") && login.senha == user.senha);

                if (user == null)
                {
                    return BadRequest("dados invalidos");
                }

                loggedUser = new LoggedUserDto{
                    nome = user.nome,
                    codigo = user.codigo,
                    email = user.email
                };

                // GEN JWT
                string jwt = _tokenService.GenerateToken(loggedUser);
                _tokenService.CreateCookie(HttpContext, jwt);
                
                return Ok(loggedUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, "erro interno");
            } 
        }
    }

}