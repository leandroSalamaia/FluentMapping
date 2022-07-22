using ApiCliente.Data;
using ApiCliente.Extensions;
using ApiCliente.Models;
using ApiCliente.Models.User;
using ApiCliente.Services;
using ApiCliente.ViewModels;
using ApiCliente.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace ApiCliente.Controllers
{
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private AppDbContext context;
        public AcountController(TokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost("v1/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

            var user = context.Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefault(x => x.Email == loginViewModel.Email);
            if (user is null)
                return StatusCode(401, new ResultViewModel<string>("Usuário não encontrado"));

            if (!PasswordHasher.Verify(user.PasswordHash, loginViewModel.Password))
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            try
            {
                var tokenService = new TokenService();
                var token = tokenService.GenerateToken(user);

                return Ok(new ResultViewModel<string>(token,null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Erro Interno, código: EX-AC-LO01"));
            }
        }

        [HttpPost("v1/acounts")]
        public async Task<IActionResult> Post([FromBody] RegisterViewModel registerViewModel) {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

            try
            {
                var password = PasswordGenerator.Generate(25, true, false);
                var user = new User()
                {
                    Name = registerViewModel.Nome,
                    Email = registerViewModel.Email,
                    PasswordHash = PasswordHasher.Hash(password)
                };
                context.Users.Add(user);
                context.SaveChanges();

                return Ok(new ResultViewModel<dynamic>(new { 
                    user = user.Email,
                    password
                }));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<string>("Erro Interno, código: EX-AC-PO01"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Erro Interno, código: EX-AC-PO02"));
            }


        }


    }
}
