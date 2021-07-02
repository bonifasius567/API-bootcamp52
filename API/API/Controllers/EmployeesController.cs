using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var insert = employeeRepository.Register(registerVM);
                if (insert > 2)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Berhasil Insert" });
                }
                else if (insert == 2)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email sudah terdafftar" });
                }
                else if (insert == 1)
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

        [HttpGet("ViewRegistrasi/{nik}")]
        public ActionResult ViewRegistrasi(string nik)
        {
            try
            {
                var insert = employeeRepository.ViewRegistrasi(nik);
                if (insert != null)
                {
                    return Ok(insert);
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = insert, message = "Data Registrasi tidak ditemukan" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Data Registrasi tidak ditemukan" });
            }
        }

        [HttpGet("ViewRegistrasi")]
        public ActionResult ViewRegistrasi()
        {
            try
            {
                var view = employeeRepository.ViewRegistrasi();
                if (view != null)
                {
                    return Ok(view);
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = view, message = "Data Registrasi tidak ditemukan" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Data Registrasi tidak ditemukan" });
            }
        }
    }
}
