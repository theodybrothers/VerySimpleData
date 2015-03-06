using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerySimpleData
{
	public static class Database
	{
		public static dynamic WithNamedConnectionString(string name)
		{
			var connectionStringSettings = ConfigurationManager.ConnectionStrings[name];

			if (connectionStringSettings == null)
			{
				throw new ArgumentException("No connection string found for: " + name);
			}

			return WithConnectionString(connectionStringSettings.ConnectionString);
		}

		public static dynamic WithConnectionString(string connectionString)
		{
			IDatabase database = new SqlServerDatabase(connectionString);
			var dynamicContext = new DatabaseContext(database);

			return dynamicContext;
		}
	}
}
