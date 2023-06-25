using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Core.Security;
using Learn_core_mvc.Core.Enum;
using Learn_core_mvc.Repository;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Learn_core_mvc.Service
{
    public class SampleService : ISampleService //where T : new()
    {
        //ISampleSqlRepository<Employee> sampleEmployeeSql;
        //ISampleSqlRepository<Company> sampleCompanySql;
        ISampleSqlRepository sampleSql;
        public SampleService(
            //ISampleSqlRepository<Employee> sampleEmployeeSqlRepository,
            //ISampleSqlRepository<Company> sampleCompanySqlRepository,
            ISampleSqlRepository sampleSql
        )
        {
            //this.sampleEmployeeSql = sampleEmployeeSqlRepository;
            //this.sampleCompanySql = sampleCompanySqlRepository;
            this.sampleSql = sampleSql;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            //return await this.sampleEmployeeSql.GetEmployees();
            return await this.sampleSql.GetEmployees();
        }
        public async Task<Employee> GetEmployee(int id)
        {
            //return await this.sampleEmployeeSql.GetEmployee(id);
            return await this.sampleSql.GetEmployee(id);
        }
        public async Task<bool> UpdateEmployee(Employee emp)
        {
            //return await this.sampleEmployeeSql.UpdateEmployee(emp);
            return await this.sampleSql.UpdateEmployee(emp);
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            //return await this.sampleEmployeeSql.DeleteEmployee(id);
            return await this.sampleSql.DeleteEmployee(id);
        }
        public async Task<bool> CreateEmployee(Employee emp)
        {
            //return await this.sampleEmployeeSql.CreateEmployee(emp);
            return await this.sampleSql.CreateEmployee(emp);
        }
        public async Task<List<Company>> GetCompanies()
        {
            //return await this.sampleCompanySql.GetCompanies();
            return await this.sampleSql.GetCompanies();
        }
        public async Task<List<Employee>> GetEmployeesBySp()
        {
            return await this.sampleSql.GetEmployeesBySp();
        }
        public async Task<Employee> GetEmployeeByIdBySp(int id)
        {
            return await this.sampleSql.GetEmployeeByIdBySp(id);
        }
        public async Task<bool> CreateEmployeeBySp(Employee emp)
        {
            return await this.sampleSql.CreateEmployeeBySp(emp);
        }
        public async Task<bool> UpdateEmployeeBySp(Employee emp)
        {
            return await this.sampleSql.UpdateEmployeeBySp(emp);
        }
        public async Task<bool> DeleteEmployeeByIdBySp(int id)
        {
            return await this.sampleSql.DeleteEmployeeByIdBySp(id);
        }
        public async Task<DatatableResponse> GetDataTableEmps(DatatableProperties dtProp)
        {
            var totalRecords = await this.sampleSql.GetDTTotalEmpsCount();
            var totalRecordwithFilter = await this.sampleSql.GetDTTotalFilterEmpsCount(dtProp.SearchValue);
            var dtEmps = await this.sampleSql.GetDTEmpsLimit(dtProp);
            return new DatatableResponse() { TotalRecords = totalRecords, TotalRecordWithFilter = totalRecordwithFilter, Data = dtEmps };
        }

        public async Task<bool> LoginUser(string email,string password)
        {
            //var user = await this.sampleCompanySql.LoginUser(email);
            var user = await this.sampleSql.LoginUser(email);
            if (user!=null)
            {
                var encodingPasswordString = Secure.EncodePassword(password, user.UserSalt); 
                if (user.UserPassword == encodingPasswordString)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Login> GetUserByEmail(string email)
        {
            return await this.sampleSql.GetUserByEmail(email);
        }

        public async Task<Login> GetUserByToken(Guid token)
        {
            return await this.sampleSql.GetUserByToken(token);
        }

        public async Task<bool> UpdateUserPassword(long userId, string salt, string password)
        {
            var encodedPassword = Secure.EncodePassword(password, salt);
            return await this.sampleSql.UpdateUserPassword(userId, encodedPassword);
        }

        public async Task<bool> UpdateUserToken(long userId, Guid token)
        {
            return await this.sampleSql.UpdateUserToken(userId, token);
        }
        public async Task<bool> RegisterUser(Login user)
        {
            user.UserSalt = Guid.NewGuid().ToString(); 
            user.UserPassword = Secure.EncodePassword(user.UserPassword, user.UserSalt); 
            user.UserRoleId = (int)RoleEnum.User;
            //return await this.sampleCompanySql.RegisterUser(user);
            return await this.sampleSql.RegisterUser(user);
        }

        public async Task<bool> PasswordResetNotificationOnMail(string email, Guid token)
        {
            var passwordResetLink = "https://localhost:44325/PasswordReset/ResetPassword?token="+token;
            StringBuilder htmlStringBuilder = new StringBuilder();
            htmlStringBuilder.Append("<p>Dear Buddy,<p>");
            htmlStringBuilder.Append("<p>Click on link to reset the password</p>");
            htmlStringBuilder.Append(String.Format("<a href={0}>Go to reset password page.</a>", passwordResetLink));
            return await SendMail(email, htmlStringBuilder);
        }

        //I have installed below nuget packages for sending mail.
        //AWSSDK.Core
        //AWSSDK.SimpleEmail
        //MimeKit
        public async Task<bool> SendMail(string receiverEmailAddress, StringBuilder body=null)
        {
            var result = false;
            var accessKey = "AKIASPFX7RZIMQZ5PVGW";
            var secretKey = "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";
            string senderEmailAddress = "csr@grizzly.com";
            try
            {
                using (var client = new AmazonSimpleEmailServiceClient(accessKey, secretKey, RegionEndpoint.USEast1))
                {
                    var bodyBuilder = new BodyBuilder();

                    if (body!=null)
                    {
                        bodyBuilder.HtmlBody = body.ToString();
                    }
                    else
                    {
                        StringBuilder htmlStringBuilder = new StringBuilder();
                        htmlStringBuilder.Append("<p>Dear Buddy,<p>");
                        htmlStringBuilder.Append("<p>I have sent you smile. See attachment</p>");
                        bodyBuilder.HtmlBody = htmlStringBuilder.ToString();
                    }

                    string FixBase64ForImage(string Image)
                    {
                        StringBuilder sbText = new StringBuilder(Image, Image.Length);
                        sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
                        return sbText.ToString();
                    };
                    var base64Img = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPAgMAAABGuH3ZAAAAAXNSR0IArs4c6QAAAAlQTFRFAAANAAAA/PxQjQj98QAAAAF0Uk5TAEDm2GYAAAABYktHRACIBR1IAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAB3RJTUUH2gwXFQ4DaigKYQAAADhJREFUCNdjYBANYGBgzFrKwMC2apUDg1TUtAkQImvVqiXoROaqlUsYpLKWAZVMjZoA0QHWCzIFAJGSGI4XxkZDAAAAAElFTkSuQmCC";
                    Byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(base64Img));
                    var fileName = "Smile.Jpeg";
                    bodyBuilder.Attachments.Add(fileName, bitmapData);

                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Grizzly Customer Service", senderEmailAddress));
                    mimeMessage.To.Add(new MailboxAddress("", receiverEmailAddress));

                    mimeMessage.Subject = "You have been sent smile";
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    using (var messageStream = new MemoryStream())
                    {
                        await mimeMessage.WriteToAsync(messageStream);
                        var sendRequest = new SendRawEmailRequest { RawMessage = new RawMessage(messageStream) };
                        var response = await client.SendRawEmailAsync(sendRequest);
                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public async Task AddException(IExceptionHandlerPathFeature exceptionHandlerPathFeature)
        {
            ExceptionLogger exceptionLogger = new ExceptionLogger() {
                ExceptionMessage = exceptionHandlerPathFeature.Error.Message,
                ExceptionStackTrace = exceptionHandlerPathFeature.Error.StackTrace,
                LogTime = DateTime.Now
            };
            await this.sampleSql.InsertException(exceptionLogger);
        }
    }
}
