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
	class FindPersonGroups
	{
		SQLiteConnection conn;
		int SIZE_LIMIT;
		SHA512Managed hasher = new SHA512Managed();

		public FindPersonGroups(int sizeLimit)
		{
			SIZE_LIMIT = sizeLimit;
		}
		public void Process(string outpath)
		{
			OpenOutput(outpath);

			// Limpia
			CleanUsed();
			Console.WriteLine("Construyendo diccionario de secuencias.");

			Dictionary<string, bool> savedGroups = new Dictionary<string, bool>();

			Dictionary<int, int> next;
			Dictionary<int, int> counts;
			CalculatePrevNextDiccionaries(out next, out counts);

			// Va uno por uno...
			string stm = "SELECT * FROM HashesTuples ORDER BY Id, CloneId";
			int found = 0;
			using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
			{
				using (SQLiteDataReader rdr = cmd.ExecuteReader())
				{
					int n = 0;
					var total = GetCount();
					DateTime t = DateTime.Now;
					Console.WriteLine("Buscando pares.");

					while (rdr.Read())
					{
						n++;
						if (n % 100 == 0) Console.WriteLine("Pares: " + (Math.Floor(((double) n / total) * 10000))/100 + " % (" + n + " de " + total + "). Transcurridos: " + ((int) (DateTime.Now - t).TotalMinutes) + " minutos. Encontrados: " + found);

						// Se fija si no está usado...
						int id = rdr.GetInt32(0);
						int clonedId = rdr.GetInt32(1);
						if (MatchesClonedPair(id, clonedId))
						{
							// Empieza la expansión
							int nextHouseId = id;
							int nextHouseClonedId = clonedId;
							
							int housePersonCount = rdr.GetInt32(2);
							int houseCount = 1;

							while (MatchesClonedPair(next[nextHouseId], next[nextHouseClonedId]) && next[nextHouseId] < clonedId)
							{
								nextHouseId = next[nextHouseId];
								nextHouseClonedId = next[nextHouseClonedId];

								housePersonCount += counts[nextHouseId];
								houseCount++;
							}
							if (housePersonCount >= SIZE_LIMIT)
							{
								var groupHash = SaveGroupPair(id, clonedId, nextHouseId, nextHouseClonedId, housePersonCount, houseCount);
								// lo guarda como grupo separado, sin repetir
								SaveGroup(savedGroups, id, nextHouseId, housePersonCount, houseCount, groupHash);
								SaveGroup(savedGroups, clonedId, nextHouseClonedId, housePersonCount, houseCount, groupHash);
							
								found++;
								// Los marca y guarda el resultado
								MarkUsed(id, nextHouseId,
												clonedId, nextHouseClonedId);
							}
						}
					}
				}

			}

			conn.Dispose();
		}

		private string SaveGroupPair(int id, int clonedId, int nextHouseId, int nextHouseClonedId, int housePersonCount, int houseCount)
		{
			string hash = GetHash(id, nextHouseId, clonedId, nextHouseClonedId);

			string insertCmd = "INSERT INTO Person_Groups_Pairs_" + SIZE_LIMIT + " (Id, CloneId, C, H, Hash) Values (" + id + ","
							+ clonedId + "," + housePersonCount + "," + houseCount + ", '" + hash + "')";
			using (var cmd2 = new SQLiteCommand(insertCmd, conn))
			{
				cmd2.ExecuteNonQuery();
			}
			return hash;
		}

		private void SaveGroup(Dictionary<string, bool> savedGroups, int id, int last, int housePersonCount, int houseCount, string hash)
		{
			string key = id.ToString() + "-" + hash;
			if (savedGroups.ContainsKey(key))
				return;
			string insertCmd = "INSERT INTO Person_Groups_" + SIZE_LIMIT + " (Id, Last, C, H, Hash) Values (" + id + ","
							+ last + "," + housePersonCount + "," + houseCount + ", '" + hash + "')";
			using (var cmd2 = new SQLiteCommand(insertCmd, conn))
			{
				cmd2.ExecuteNonQuery();
			}
			savedGroups[key] = true;
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

		private string GetHash(int idMin, int idMax, int clonedIdMin, int clonedIdMax)
		{
			string updateCmd = "SELECT GROUP_CONCAT(h.Hash, '-') FROM HashesTuples t JOIN Hashes h ON h.Id = t.Id WHERE t.Id >= " + idMin + " AND t.Id <= " + idMax + " AND t.cloneId >= " + clonedIdMin + " AND t.cloneId <= " + clonedIdMax + " ORDER BY t.Id";
			using (var cmd = new SQLiteCommand(updateCmd, conn))
			{
				var val = (string) cmd.ExecuteScalar();
				return HashBuilder.HashMultiKey(hasher, val);
			}
		}

		private int GetCounts(int idMin, int idMax, int clonedIdMin, int clonedIdMax)
		{
			string updateCmd = "SELECT SUM(C) + SUM(CloneC) FROM HashesTuples WHERE Id >= " + idMin + " AND Id <= " + idMax + " AND cloneId >= " + clonedIdMin + " AND cloneId <= " + clonedIdMax;
			using (var cmd = new SQLiteCommand(updateCmd, conn))
			{
				return (int) (long) cmd.ExecuteScalar();
			}
		}

		private bool GroupExists(int id, string hash)
		{
			string updateCmd = "SELECT count(*) FROM Person_Groups_" + SIZE_LIMIT + " WHERE Id = " + id + " AND Hash = '" + hash + "'";
			using (var cmd = new SQLiteCommand(updateCmd, conn))
			{
				return (int) (long) cmd.ExecuteScalar() > 0;
			}
		}
		private void MarkUsed(int idMin, int idMax, int clonedIdMin, int clonedIdMax)
		{
			string updateCmd = "UPDATE HashesTuples SET Used = 1 WHERE Id >= " + idMin + " AND Id <= " + idMax + " AND cloneId >= " + clonedIdMin + " AND cloneId <= " + clonedIdMax;
			using (var cmd = new SQLiteCommand(updateCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
		}
		
		private bool MatchesClonedPair(int id, int clonedId)
		{
			string cleanCmd = "SELECT COUNT(*) FROM HashesTuples WHERE Id = " + id + " AND cloneId = " + clonedId + " AND Used = 0";
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				return ((long) cmd.ExecuteScalar() > 0);
			}
		}
		private int GetCount()
		{
			string cleanCmd = "SELECT COUNT(*) FROM HashesTuples";
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				return (int)(long) cmd.ExecuteScalar();
			}
		}


		private void CleanUsed()
		{
			string cleanCmd = "UPDATE HashesTuples SET Used = 0 WHERE Used = 1";
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
		}

		private void OpenOutput(string outpath)
		{
			conn = OpenDb.Open(outpath);
						
      string tableCmd = "CREATE TABLE IF NOT EXISTS Person_Groups_Pairs_" + SIZE_LIMIT + " (Id INT, CloneId INT, C INT, H INT, Hash TEXT)";
      using (var cmd = new SQLiteCommand(tableCmd, conn))
      {
          cmd.ExecuteNonQuery();
      }
      string clearCmd = "DELETE FROM Person_Groups_Pairs_" + SIZE_LIMIT;
      using (var cmd = new SQLiteCommand(clearCmd, conn))
      {
          cmd.ExecuteNonQuery();
      }

			////////////////////////////////////
			string drop = "DROP TABLE IF EXISTS Person_Groups_" + SIZE_LIMIT + ";";
      using (var cmd = new SQLiteCommand(drop, conn))
      {
          cmd.ExecuteNonQuery();
      }
		  string tableCmd2 = "CREATE TABLE Person_Groups_" + SIZE_LIMIT + " (Id INT, Last INT, C INT, H INT, Hash TEXT)";
      using (var cmd = new SQLiteCommand(tableCmd2, conn))
      {
          cmd.ExecuteNonQuery();
      }
		}
		
	}
}