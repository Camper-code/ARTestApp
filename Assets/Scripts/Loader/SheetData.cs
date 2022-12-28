using System.Collections.Generic;
namespace Loader
{
	[System.Serializable]
	public class SheetData
	{
		[System.Serializable]
		public struct ValueRanges
		{
			public string range;
			public string majorDimension;
			public List<List<string>> values;
		}

		public string spreadsheetId;
		public List<ValueRanges> valueRanges;
	}
}