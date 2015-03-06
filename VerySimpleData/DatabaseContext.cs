using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerySimpleData
{
	public class DatabaseContext : DynamicObject
	{
		private readonly IDatabase Database;

		public DatabaseContext(IDatabase database)
		{
			Database = database;
		}

		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			if (binder.CallInfo.ArgumentCount != args.Length)
			{
				result = false;

				return false;
			}

			var procedureName = binder.Name;
			var parameters = binder.CallInfo.ArgumentNames.Zip(args, (s, i) => new { s, i })
				.ToDictionary(item => item.s, item => item.i);

			result = Database.ExecuteStoredProcedure(procedureName, parameters);

			return true;
		}
	}
}