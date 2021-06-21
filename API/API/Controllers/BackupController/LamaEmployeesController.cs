using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LamaEmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public LamaEmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        //RETURN status code, result, message
        //menampilkan employee tanpa parameter dan pakai parameter

        [HttpGet]
        public ActionResult Get()
        {
            var get = employeeRepository.Get();
            if (get != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = get, message = "Data ditemukan" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "Data tidak ditemukan" });
            }
        }

        [HttpGet("{nik}")]
        public ActionResult Get(string nik)
        {
            var get = employeeRepository.Get(nik);
            if (get != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = get, message = "Data ditemukan" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "Data tidak ditemukan" });
            }
        }

        [HttpPost]
        public ActionResult Insert(Employee employees)
        {
            var insert = employeeRepository.Insert(employees);
            if (insert > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Berhasil Insert" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = insert, message = "Insert Gagal" });
            }
        }

        //EmployeesController
        [HttpDelete]
        public ActionResult Delete(string nik)
        {
            var respone = employeeRepository.Delete(nik);
            if (respone > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = respone, message = "Berhasil Delete" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = respone, message = "Delete gagal" });
            }

        }

        [HttpPut]
        public ActionResult Update (Employee employee, string nik)
        {
            var response = employeeRepository.Update(employee, nik);
            if (response > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = response, message = "Berhasil Update" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Update Gagal" });
            }
        }

    }
}
