using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace simple_json_ini
{
	public class JSON_INI
	{
		private string file_name = "", file_text = "";
		private bool json_valid = false;

		public JSON_INI(string file_name)
		{
			this.file_name = file_name;

			if (!File.Exists(file_name))
			{
				File.WriteAllText(file_name, "");
				return;
			}
		}

		public bool isJsonValid(string json)
		{
			try
			{
				return JsonConvert.DeserializeObject(json) != null;
			}
			catch
			{
				return false;
			}
		}

		public void readSettings()
		{
			file_text = File.ReadAllText(file_name);
			json_valid = isJsonValid(file_text);
		}

		public object readItem(string item)
		{
			if (!json_valid)
				return null;

			dynamic json = JsonConvert.DeserializeObject(file_text);
			var retValue = json[item];

			if (!file_text.Contains(item))
				return null;

			return retValue;
		}

		public bool readBool(string item, bool normal = false)
		{
			var value = readItem(item);

			if (value == null)
			{
				writeItem(item, normal);
				return normal;
			}

			return Convert.ToBoolean(value);
		}

		public long readLong(string item, long min = 0)
		{
			var value = readItem(item);

			if (value == null)
			{
				writeItem(item, min);
				return min;
			}

			return Convert.ToInt64(value);
		}

		public double readDouble(string item, double min = 0)
		{
			var value = readItem(item);

			if (value == null)
			{
				writeItem(item, 0);
				return min;
			}

			return Convert.ToDouble(value);
		}

		public string readString(string item, string normal = "")
		{
			var value = readItem(item);

			if (value == null)
			{
				writeItem(item, normal);
				return normal;
			}

			return Convert.ToString(value);
		}

		public void writeItem(string item, object value)
		{
			string clean_json = "";

			if (!json_valid)
			{
				dynamic json = new ExpandoObject();
				var dictionary = (IDictionary<string, object>)json;
				dictionary.Add(item, value);
				clean_json = JsonConvert.SerializeObject(dictionary);
			}
			else
			{
				var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(file_text);

				if (dictionary.ContainsKey(item))
					dictionary[item] = value;
				else
					dictionary.Add(item, value);

				clean_json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
			}

			File.WriteAllText(file_name, clean_json);
			file_text = clean_json;
			json_valid = true;
		}
	}
}
