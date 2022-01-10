using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerHandler.Models;
using ServerHandler.Services;
using System;
using System.Threading.Tasks;

namespace Source
{
    class APiHandlerTest
    {
        [TestClass]
        public class DapperTest
        {


            [TestMethod]
            public async Task CreateAccount_loginPasswordBaseAdress_UserTokenResponse()
            {
                var tkn = new ServerHandler.Models.AccessToken()
                {
                    UserName = "aboba1",
                    Password = "12345678"
                };
                var baseAdress = "https://a4141-50d6.b.d-f.pw";
                var apiWorker = new APIWorkerService(baseAdress);

                var res = await apiWorker.CreateAccountAsync(tkn);
                Assert.IsNotNull(res);
            }

            [TestMethod]
            public async Task Login_loginPasswordBaseAdress_UserTokenResponse()
            {
                var tkn = new ServerHandler.Models.AccessToken()
                {
                    UserName = "aboba",
                    Password = "12345678",
                    Email = string.Empty,
                };
                var baseAdress = "https://a4141-50d6.b.d-f.pw";
                var apiWorker = new APIWorkerService(baseAdress);

                var res = await apiWorker.LoginAsync(tkn);
                Assert.IsNotNull(res);
            }
        }
    }
}
