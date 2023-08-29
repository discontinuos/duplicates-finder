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
	class ExpandPersonGroups
	{
		SQLiteConnection conn;
		int SIZE_LIMIT;
		SortedSet<int> inserted;
		public ExpandPersonGroups(int sizeLimit)
		{
			SIZE_LIMIT = sizeLimit;
		}

		public void Process(string outpath, bool allowDuplicated = false)
		{
			OpenOutput(outpath, allowDuplicated);
			inserted = new SortedSet<int>();

			Dictionary<int, int> next;
			Dictionary<int, int> counts;
			CalculatePrevNextDiccionaries(out next, out counts);

			// Va uno por uno...
			string stm = "SELECT * FROM Person_Groups_" + SIZE_LIMIT;
			using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
			{
				using (SQLiteDataReader rdr = cmd.ExecuteReader())
				{
					int n = 0;
					var total = GetCount();
					DateTime t = DateTime.Now;
					Console.WriteLine("Expandiendo miembros...");
					while (rdr.Read())
					{
						n++;
						if (n % 10 == 0) Console.WriteLine("Miembros: " + (Math.Floor(((double) n / total) * 10000))/100 + " % (" + n + " de " + total + "). Transcurridos: " + ((int) (DateTime.Now - t).TotalMinutes) + " minutos.");

						// Se fija si no está usado...
						int id = rdr.GetInt32(0);
						int lastId = rdr.GetInt32(1);

						int c = rdr.GetInt32(2);
						int houses = rdr.GetInt32(3);
						string hash = rdr.GetString(4);
						if (houses >= 5)
						{
							int current = id;
							for (int i = 0; i < houses; i++)
							{
								Insert(current, id, c, houses, hash, allowDuplicated);
								if (i == houses - 1)
								{
									if (current != lastId)
										throw new Exception("problema");
								}
								current = next[current];
							}
						}
					}
				}

			}

			string updateHash = "UPDATE  House_Groups_Members_" + SIZE_LIMIT + 
				" SET hash = (SELECT hash FROM Hashes WHERE Hashes.id = House_Groups_Members_" + SIZE_LIMIT + ".Id)";
			using (var cmd = new SQLiteCommand(updateHash, conn))
			{
				cmd.ExecuteNonQuery();
			}
			conn.Dispose();
		}

		

		private void Insert(int id, int groupStart, int c, int houses, string family, bool allowDuplicated)
			{
			string tableName = GetTableName(allowDuplicated);
		
			string insertCmd = "INSERT INTO " + tableName + "_" + SIZE_LIMIT + 
				"(Id, GroupStart, GroupHash, Persons, Houses) Values (" + id + ", " + groupStart + ",'"
										+ family + "'," + c + "," + houses + ")";
			if (!allowDuplicated)
			{
				if (inserted.Contains(id))
					return;
				else
					inserted.Add(id);
			}

			using (var cmd2 = new SQLiteCommand(insertCmd, conn))
			{
					cmd2.ExecuteNonQuery();
			}
		}

		void CalculatePrevNextDiccionaries(out Dictionary<int, int> next, out Dictionary<int, int> counts)
		{
			// Va uno por uno...
			next = new Dictionary<int, int>();
			counts = new Dictionary<int, int>();
			string stm = "SELECT Id, C FROM Hashes ORDER BY Id";
			int prevValue = -1;
			using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
			{
				using (SQLiteDataReader rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						int value = rdr.GetInt32(0);
						// actualiza diccinarios
						next[prevValue] = value;
						counts[value] = rdr.GetInt32(1);
						// sigue
						prevValue = value;
					}
				}
			}
		}

		private int GetCount()
		{
			string cleanCmd = "SELECT COUNT(*) FROM Person_Groups_" + SIZE_LIMIT;
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				return (int)(long) cmd.ExecuteScalar();
			}
		}

		private void OpenOutput(string outpath, bool allowDuplicated)
		{
			conn = OpenDb.Open(outpath);
			string tableName = GetTableName(allowDuplicated);
			string tableCmd = "CREATE TABLE IF NOT EXISTS " + tableName + "_" + SIZE_LIMIT + " (Id "
				+ " INT " + (allowDuplicated ? "" : "PRIMARY KEY") + ", GroupStart INT,  GroupHash TEXT, Persons INT, Houses INT, Hash TEXT)";
			using (var cmd = new SQLiteCommand(tableCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}

			string clearCmd = "DELETE FROM " + tableName + "_" + SIZE_LIMIT;
			using (var cmd = new SQLiteCommand(clearCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
		}

		private static string GetTableName(bool allowDuplicated)
		{
			string table = "House_Groups_Members";
			if (allowDuplicated)
				table += "_Duplicates";
			return table;
		}
	}
}