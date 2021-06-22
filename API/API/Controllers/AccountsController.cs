using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                var insert = accountRepository.Login(loginVM);
                if (insert == 1)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Login Sukses" });
                }
                else
                {
                    return NotFound(new
                    {
                        status = HttpStatusCode.NotFound,
                        result = insert,
                        message = "nik/password yang anda masukkan tidak sesuai dengan data didatabase"
                    });
                }
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    result = 0,
                    message = "nik/password yang anda masukkan tidak sesuai dengan data didatabase"
                });
            }
        }
    }
}
