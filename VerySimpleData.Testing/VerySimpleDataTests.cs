using DelphiPicks.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerySimpleData.Testing
{
	[TestClass]
    public class VerySimpleDataTests
    {
		[TestMethod]
		public void WillReturnDynamicObject()
		{
			var database = Database.WithNamedConnectionString("delphipicks-dev");

			var name = "My Name Is Jim";
			var user = database.UpdateUser(Id: "test", DisplayName: name, Provider: "Facebook");

			Assert.IsInstanceOfType(user, typeof(VerySimpleDataRecord));
			Assert.AreEqual(name, user.DisplayName);

			//foreach (var u in users)
			//{
			//	Assert.AreEqual("Facebook", u.Provider);
			//}
			//var u = users[0];
			//u.joke = 1;

			//Assert.AreEqual(3, users.Count);
			//Assert.AreEqual("My Name Is James", users[0].DisplayName);
			//Assert.AreEqual("Facebook", users[0].Provider);
		}

		[TestMethod]
		public void ReturningFalse()
		{
			var recordSet = new VerySimpleDataRecordSet() as dynamic;

			var record = recordSet["joke"];
		}

		//[TestMethod]
		public void Example()
		{
			var database = Database.WithNamedConnectionString("connectionString");
			var results = database.MyStoredProcedure(Param: "something", AnotherParam: "something-else");
		}
    }
}
