using API.Base;
using API.Context;
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
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var insert = employeeRepository.Register(registerVM);
                if (insert == 3)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Berhasil Insert" });
                }
                else if (insert == 2)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email sudah terdafftar" });
                }
                else if (insert == 4)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "NIK sudah terdafftar" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = insert, message = "Insert Gagal" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Insert gagal" });
            }
        }
    }
}
