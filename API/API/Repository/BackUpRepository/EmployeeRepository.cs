using API.Context;
using API.Models;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(string nik)
        {
            try
            {
                var find = myContext.Employees.SingleOrDefault(find => find.NIK == nik);
                myContext.Employees.Remove(find);
                var delete = myContext.SaveChanges();
                return delete;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }

        public IEnumerable<Employee> Get()
        {
            var employee = myContext.Employees.ToList();
            return employee;
        }

        public Employee Get(string nik)
        {
            var employee = myContext.Employees.SingleOrDefault(employee => employee.NIK == nik);
            return employee;
        }

        public int Insert(Employee employee)
        {
            myContext.Employees.Add(employee);
            var insert = myContext.SaveChanges();
            return insert;
        }
    
            //perbedaan find, firstordefault, singelordefault?
            //var find = myContext.Employees.SingleOrDefault(employee => employee.NIK == nik);

        public int Update(Employee employee, string nik)
        {
            try
            {
                Employee employees = new Employee();
                if (employee.FirstName != null)
                    employees.FirstName = employee.FirstName;
                if (employee.LastName != null)
                    employees.LastName = employee.LastName;
                if (employee.PhoneNumber != null)
                    employees.PhoneNumber = employee.PhoneNumber;
                if (employee.Email != null)
                    employees.Email = employee.Email;
                if (employee.Salary != 0)
                    employees.Salary = employee.Salary;
                if (employee.BirthDate != null)
                    employees.BirthDate = employee.BirthDate;

                myContext.Entry(employee).State = EntityState.Modified;
                var update = myContext.SaveChanges();
                return update;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

    }
}
