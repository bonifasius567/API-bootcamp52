using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

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
            var alternatif = context.Accounts.Find(loginVM.NIK);
            if (alternatif != null)
            {
                var account = context.Accounts.FirstOrDefault(a => a.NIK == loginVM.NIK);
                if (account != null && Hashing.ValidatePassword(loginVM.Password, account.Password))
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
                var cekEmail = context.Employees.FirstOrDefault(a => a.Email == loginVM.NIK);
                var account = context.Accounts.Find(cekEmail.NIK);
                if (account != null && Hashing.ValidatePassword(loginVM.Password, account.Password))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            Guid guid = Guid.NewGuid();
            string emailGuid = guid.ToString("N");

            var account = new Account();

            var email = context.Employees.FirstOrDefault(a => a.Email == resetPasswordVM.Email);
            if (email != null)
            {
                account.NIK = email.NIK;
                account.Password = emailGuid;
                context.Entry(account).State = EntityState.Modified;
                var insert = context.SaveChanges();

                if (insert > 0)
                {
                    var fromAddress = new MailAddress("henrisuni05@gmail.com", "From API");
                    var toAddress = new MailAddress(resetPasswordVM.Email, $"To {resetPasswordVM.Email}");
                    string fromPassword = "******";
                    string subject = "Reset Password";
                    string body = "Hello " + email.FirstName + System.Environment.NewLine + "Ini password baru anda : " + emailGuid;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                    return insert;

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var account = context.Accounts.Find(changePasswordVM.NIK);
            if (account != null && account.Password == changePasswordVM.OldPassword)
            {
                account.NIK = changePasswordVM.NIK;
                account.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
                context.Entry(account).State = EntityState.Modified;
                var insert = context.SaveChanges();
                return insert;
            }
            
            else
            {
                return 0;
            }
        }
    }
}
