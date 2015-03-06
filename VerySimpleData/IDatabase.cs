using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerySimpleData
{
	public interface IDatabase
	{
		dynamic ExecuteStoredProcedure(string name, IDictionary<string, object> parameters);
	}
}
