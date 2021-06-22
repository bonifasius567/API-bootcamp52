using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public int Login(LoginVM loginVM)
        {
            var employee = new Employee();
            var account = new Account();
            var alternatif = context.Accounts.Find(loginVM.NIK);
            if (alternatif != null)
            {
                var test = context.Accounts.FirstOrDefault(a => a.NIK == loginVM.NIK && a.Password == loginVM.Password);
                if (test != null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                var cekPass = context.Accounts.FirstOrDefault(a => a.Password == loginVM.Password);
                var cekNIK = context.Employees.Find(cekPass.NIK);
                var cekEmail = context.Employees.FirstOrDefault(a => a.NIK == cekNIK.NIK && a.Email == loginVM.NIK);
                if (cekEmail != null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
