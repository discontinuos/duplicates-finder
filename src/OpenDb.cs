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
	class OpenDb
	{
		public static SQLiteConnection Open(string outpath)
		{
			SQLiteConnection conn = new SQLiteConnection("Data Source=" + outpath);
			conn.Open();
			set(conn, "temp_store", "MEMORY");
			set(conn, "journal_mode", "OFF");
			set(conn, "SYNCHRONOUS", "off");
			return conn;
		}

		private static void set (SQLiteConnection conn, string key, string value)
		{
			using (var cmd = new SQLiteCommand("PRAGMA " + key + "=" + value, conn))
      {
          cmd.ExecuteNonQuery();
      }
		}
	}
}