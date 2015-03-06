using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerySimpleData
{
	public class VerySimpleDataRecordSet : DynamicObject, IEnumerable
	{
		private readonly IList<VerySimpleDataRecord> _data;

		public VerySimpleDataRecordSet()
			: this(new List<VerySimpleDataRecord>())
		{ }

		public VerySimpleDataRecordSet(IList<VerySimpleDataRecord> data)
		{
			_data = data;
		}

		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			if (indexes.Length == 1 && indexes[0].GetType() == typeof(int))
			{
				result = _data[(int)indexes[0]];

				return true;
			}

			result = false;

			return false;
		}

		public int Count
		{
			get { return _data.Count; }
		}

		public IEnumerator GetEnumerator()
		{
			return _data.GetEnumerator();
		}
	}
}
