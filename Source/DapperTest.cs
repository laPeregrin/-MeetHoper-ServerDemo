using _MeetHoper_ServerDemo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    [TestClass]
    public class DapperTest
    {


        [TestMethod]
        public async Task TestGetArrayByArrayId()
        {
            var ids = new Guid[]{ new Guid("f1f92d64-08a4-4dda-b5cf-45241275252f"),
                                    new Guid("0d076819-3c1f-4c20-b3ec-493ea7d86bbf")};

            var dapperHelper = new DapperUserHelper("Server=(localdb)\\MSSQLLocalDB;Database=MeetHoper_demo;Trusted_Connection=True;MultipleActiveResultSets=true;Timeout=600");
            var result = await dapperHelper.GetFromDBByIds(ids);
            Assert.IsTrue(result.Any());
        }

    }
}
