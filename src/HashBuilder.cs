using Spss;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace finder
{
	class HashBuilder
	{
		SHA512Managed hasher = new SHA512Managed();
		SQLiteConnection conn;
		SQLiteCommand cmdInsert;

		public void Process(string dataPath, Dictionary<string, string[]> opts, string outpath)
		{
			var file = dataPath;

			if (File.Exists(outpath))
				File.Delete(outpath);

			SpssDataDocument doc = SpssDataDocument.Open(file, SpssFileAccess.Read);
			OpenOutput(outpath);

			Process(doc, opts);
			doc.Close();
			conn.Close();
			conn.Dispose();
		}

		private void OpenOutput(string outpath)
		{
			conn = OpenDb.Open(outpath);

			string dropCmd = "DROP TABLE IF EXISTS Hashes";
			using (var cmd = new SQLiteCommand(dropCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
			string createCmd = "CREATE TABLE IF NOT EXISTS Hashes (Id INT PRIMARY KEY, Hash TEXT, C INT)";
			using (var cmd = new SQLiteCommand(createCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
			string createIndexCmd = "CREATE INDEX idx_hashes ON Hashes (Hash);";
			using (var cmd = new SQLiteCommand(createIndexCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
			string insertCmd =
				"INSERT INTO Hashes (Id, Hash, C) VALUES(?,?,?)";
			cmdInsert = new SQLiteCommand(insertCmd, conn);
			cmdInsert.Parameters.Add("@Id", System.Data.DbType.Int32);
			cmdInsert.Parameters.Add("@Hash", System.Data.DbType.String);
			cmdInsert.Parameters.Add("@C", System.Data.DbType.Int32);
		}

		private void Process(SpssDataDocument doc, Dictionary<string, string[]> opts)
		{
			var fields = opts["fields"];
			var exclusions = opts["exclusions"];
			var keys = opts["keys"];
			fields = removeExclusions(fields, exclusions);

			string lastKeys = "";
			string multiKey = "";
			var total = doc.Cases.Count;
			int n = 0;
			int members = 0;
			DateTime t = DateTime.Now;
			foreach (SpssCase row in doc.Cases)
			{
				n++;
				if (n % 100 == 0) Status.Update("Hashing: " + (Math.Floor(((double)n / total) * 10000)) / 100 + " % (" + n + " de " + total + "). \n\nTranscurridos: " + ((int)(DateTime.Now - t).TotalMinutes) + " minutos.");

				string plainKey = keysPlain(row, keys);
				if (plainKey != lastKeys)
				{
					saveMultiKey(lastKeys, multiKey, members);
					multiKey = "";
					members = 0;
					lastKeys = plainKey;
				}
				// Lee todas las variables
				foreach (var field in fields)
				{
					multiKey += row[field].ToString() + "\t";
				}
				members++;
			}
			saveMultiKey(lastKeys, multiKey, members);
			Status.Hide("Se crearon exitosamente " + doc.Cases.Count.ToString() + " hashes.");
		}

		private void saveMultiKey(string plainKey, string multiKey, int count)
		{
			if (multiKey == "") return;

			var hash = HashMultiKey(hasher, multiKey);
			cmdInsert.Parameters["@Id"].Value = plainKey;
			cmdInsert.Parameters["@Hash"].Value = hash;
			cmdInsert.Parameters["@C"].Value = count;
			cmdInsert.ExecuteNonQuery();
		}

		public static string HashMultiKey(SHA512Managed hasher, string multiKey)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(multiKey);
			var hash = hasher.ComputeHash(bytes);
			var hashedInputStringBuilder = new System.Text.StringBuilder(128);
			foreach (var b in hash)
				hashedInputStringBuilder.Append(b.ToString("X2"));
			return hashedInputStringBuilder.ToString();
		}

		private string keysPlain(SpssCase row, string[] keys)
		{
			// Se fija si es un cambio de keys
			List<string> list = new List<string>();
			foreach (string key in keys)
			{
				list.Add(row[key].ToString());
			}
			return String.Join("\t", list);
		}

		private static string[] removeExclusions(string[] fields, string[] exclusions)
		{
			List<string> ret = new List<string>();
			foreach (string field in fields)
				if (exclusions.Contains(field) == false)
					ret.Add(field);
			return ret.ToArray();
		}
	}
}