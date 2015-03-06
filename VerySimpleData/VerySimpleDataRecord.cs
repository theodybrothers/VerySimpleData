using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerySimpleData
{
	public class VerySimpleDataRecord : DynamicObject
	{
		private readonly Dictionary<string, object> _data;

		public VerySimpleDataRecord()
			:this(new Dictionary<string, object>())
		{ }

		public VerySimpleDataRecord(Dictionary<string, object> data)
		{
			_data = data;
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (_data.ContainsKey(binder.Name))
			{
				result = _data[binder.Name];

				return true;
			}
			
			return base.TryGetMember(binder, out result);
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_data[binder.Name] = value;

			return true;
		}

		public int ColumnCount
		{
			get { return _data.Count; }
		}
	}
}
