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
	class FindHouseholdGroups
	{
		SQLiteConnection conn;
		int SIZE_LIMIT;
		SHA512Managed hasher = new SHA512Managed();

		public FindHouseholdGroups(int sizeLimit)
		{
			SIZE_LIMIT = sizeLimit;
		}
		public void Process(string outpath)
		{
			OpenOutput(outpath);

			// Limpia
			CleanUsed();

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
						if (isValid(id, clonedId))
						{
							// Empieza la expansión
							int topMargin = 1;
							int bottomMargin = 1;
							while (isValid(id - topMargin, clonedId - topMargin) && clonedId - topMargin > id)
							{
								topMargin++;
							}
							topMargin--;
							while (isValid(id + bottomMargin, clonedId + bottomMargin) && id + bottomMargin < clonedId - topMargin)
							{
								bottomMargin++;
							}
							bottomMargin--;
							int size = 1 + topMargin + bottomMargin;
							if (size >= SIZE_LIMIT)
							{
								string insertCmd = "INSERT INTO Groups_" + SIZE_LIMIT + " (Id, CloneId, C, Size, Hash) Values (" +  (id - topMargin) + ","
												+ (clonedId - topMargin) + "," + GetCounts(id - topMargin, id + bottomMargin, 
												clonedId - topMargin, clonedId + bottomMargin) + "," + size + ", '" + GetHash(id - topMargin, id + bottomMargin, 
												clonedId - topMargin, clonedId + bottomMargin) + "')";
								using (var cmd2 = new SQLiteCommand(insertCmd, conn))
								{
										cmd2.ExecuteNonQuery();
								}
								found++;
								// Los marca y guarda el resultado
								MarkUsed(id - topMargin, id + bottomMargin, clonedId - topMargin, clonedId + bottomMargin);
							}
						}
					}
				}

			}

			conn.Dispose();
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

		private void MarkUsed(int idMin, int idMax, int clonedIdMin, int clonedIdMax)
		{
			string updateCmd = "UPDATE HashesTuples SET Used = 1 WHERE Id >= " + idMin + " AND Id <= " + idMax + " AND cloneId >= " + clonedIdMin + " AND cloneId <= " + clonedIdMax;
			using (var cmd = new SQLiteCommand(updateCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
		}
		
		private bool isValid(int id, int clonedId)
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
						
      string tableCmd = "CREATE TABLE IF NOT EXISTS Groups_" + SIZE_LIMIT + " (Id INT, CloneId INT, C INT, Size INT, Hash TEXT)";
      using (var cmd = new SQLiteCommand(tableCmd, conn))
      {
          cmd.ExecuteNonQuery();
      }
					
      string clearCmd = "DELETE FROM Groups_" + SIZE_LIMIT;
      using (var cmd = new SQLiteCommand(clearCmd, conn))
      {
          cmd.ExecuteNonQuery();
      }

		}
		
	}
}