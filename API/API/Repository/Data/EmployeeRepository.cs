using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Collections;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var employee = new Employee();
            var account = new Account();
            var profiling = new Profiling();
            var education = new Education();
            var role = new AccountRole();

            var cekNIK = myContext.Employees.Find(registerVM.NIK);
            if (cekNIK == null)
            {
                var cekEmail = myContext.Employees.FirstOrDefault(a => a.Email == registerVM.Email);
                if (cekEmail == null)
                {
                    employee.NIK = registerVM.NIK;
                    employee.FirstName = registerVM.FirstName;
                    employee.LastName = registerVM.LastName;
                    employee.Email = registerVM.Email;
                    employee.BirthDate = registerVM.BirthDate;
                    employee.Salary = registerVM.Salary;
                    employee.PhoneNumber = registerVM.PhoneNumber;
                    employee.Gender = (Models.Gender)registerVM.Gender;

                    account.NIK = registerVM.NIK;
                    account.Password = Hashing.HashPassword(registerVM.Password);

                    var edu = myContext.Educations.FirstOrDefault
                        (b => b.Degree == registerVM.Degree && b.GPA == registerVM.GPA && b.UniversityId == registerVM.UniversityId);
                    int eduid = edu.Id;

                    profiling.NIK = registerVM.NIK;
                    profiling.EducationId = eduid;

                    role.RoleID = 2;
                    role.AccountId = registerVM.NIK;

                    myContext.AccountRoles.Add(role);
                    myContext.Employees.Add(employee);
                    myContext.Accounts.Add(account);
                    myContext.Profilings.Add(profiling);
                    var insert = myContext.SaveChanges();
                    return insert;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }

        public IQueryable ViewRegistrasi(string nik)
        {
            var employee = myContext.Employees.FirstOrDefault(e => e.NIK == nik);
            if (employee != null)
            {
                var data =
                    from em in myContext.Employees
                    join ac in myContext.Accounts on em.NIK equals ac.NIK
                    join pr in myContext.Profilings on ac.NIK equals pr.NIK
                    join ed in myContext.Educations on pr.EducationId equals ed.Id
                    join un in myContext.Universities on ed.UniversityId equals un.Id
                    where em.NIK == nik 
                    select new 
                    {
                        em.NIK, em.FirstName, em.LastName, em.Email, em.Gender,em.Salary, em.BirthDate, em.PhoneNumber,
                        ed.Degree, ed.GPA, un.Name
                    } ;
                return data;
            }
            else
            {
                return null;
            }
        }

        public ICollection ViewRegistrasi()
        {
            var data = (from em in myContext.Employees
                        join ac in myContext.Accounts on em.NIK equals ac.NIK
                        join pr in myContext.Profilings on ac.NIK equals pr.NIK
                        join ed in myContext.Educations on pr.EducationId equals ed.Id
                        join un in myContext.Universities on ed.UniversityId equals un.Id
                        select new
                        {
                            em.NIK,
                            em.FirstName,
                            em.LastName,
                            em.Email,
                            em.Gender,
                            em.Salary,
                            em.BirthDate,
                            em.PhoneNumber,
                            ed.Degree,
                            ed.GPA,
                            un.Name
                        }).ToList();
            return data;
        }
    }
}
