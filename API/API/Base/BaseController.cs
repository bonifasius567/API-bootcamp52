using API.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        public ActionResult Get()
        {
            var get = repository.Get();
            return Ok(new { status = HttpStatusCode.OK, result = get, message = "Data ditemukan" });
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var get = repository.Get(key);
            if (get != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = get, message = "Data ditemukan" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "Data tidak ditemukan" });
            }
        }

        [HttpDelete]
        public ActionResult Delete(Key key)
        {
            var respone = repository.Delete(key);
            if (respone > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = respone, message = "Berhasil Delete" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = respone, message = "Delete gagal" });
            }
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                var insert = repository.Insert(entity);
                if (insert > 0)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Berhasil Insert" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.OK, result = insert, message = "Insert Gagal" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Insert gagal" });
            }
            
        }

        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            var response = repository.Update(entity, key);
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
