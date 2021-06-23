using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

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
                    employee.Gender = (Models.Gender)registerVM.Gender;

                    account.NIK = registerVM.NIK;
                    account.Password = Hashing.HashPassword(registerVM.Password);

                    //education.Degree = registerVM.Degree;
                    //education.GPA = registerVM.GPA;
                    //education.UniversityId = registerVM.UniversityId;
                    //myContext.Educations.Add(education);

                    var edu = myContext.Educations.FirstOrDefault
                        (b => b.Degree == registerVM.Degree && b.GPA == registerVM.GPA && b.UniversityId == registerVM.UniversityId);
                    int eduid = edu.Id;

                    profiling.NIK = registerVM.NIK;
                    profiling.EducationId = eduid;

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

    }
}
