using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerySimpleData
{
	public class SqlServerDatabase : IDatabase
	{
		private readonly SqlConnection Connection;

		public SqlServerDatabase(string connectionString)
		{
			Connection = new SqlConnection(connectionString);
		}

		public dynamic ExecuteStoredProcedure(string name, IDictionary<string, object> parameters)
		{
			var sqlCommand = Connection.CreateCommand();
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandText = name;
			var sqlAdapter = new SqlDataAdapter();
			sqlAdapter.SelectCommand = sqlCommand;
			var datatable = new DataTable();

			foreach (var parameter in parameters)
			{
				sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
			}

			try
			{
				Connection.Open();
				sqlAdapter.Fill(datatable);
			}
			finally
			{
				sqlAdapter.Dispose();
				sqlCommand.Dispose();
				Connection.Close();
			}

			return GetReturnObject(datatable);
		}

		private dynamic GetReturnObject(DataTable datatable)
		{
			if (datatable.Columns.Count == 1 && datatable.Rows.Count == 1) //Scalar
			{
				return datatable.Rows[0][0];
			}
			else if (datatable.Columns.Count > 0 && datatable.Rows.Count == 1) //Single record
			{
				return GetRecord(datatable.Columns, datatable.Rows[0]);
			}
			else if (datatable.Columns.Count > 0 && datatable.Rows.Count > 1) //Multiple records
			{
				return GetRecordSet(datatable);
			}
			else
			{
				return new VerySimpleDataRecord();
			}
		}

		private dynamic GetRecord(DataColumnCollection columns, DataRow row)
		{
			var data = new Dictionary<string, object>();

			foreach (DataColumn column in columns)
			{
				data.Add(column.ColumnName, row[column.ColumnName]);
			}

			var record = new VerySimpleDataRecord(data);

			return record;
		}

		private dynamic GetRecordSet(DataTable datatable)
		{
			var records = new List<VerySimpleDataRecord>();
			
			foreach (DataRow row in datatable.Rows)
			{
				var record = GetRecord(datatable.Columns, row);

				records.Add(record);
			}

			var recordSet = new VerySimpleDataRecordSet(records);

			return recordSet;
		}
	}
}
