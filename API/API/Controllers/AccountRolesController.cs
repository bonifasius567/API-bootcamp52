using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        public AccountRolesController(AccountRoleRepository repository) : base(repository)
        {
        }
    }
}
