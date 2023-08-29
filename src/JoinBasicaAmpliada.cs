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
	class JoinBasicaAmpliada
	{
		SQLiteConnection conn;

		public JoinBasicaAmpliada()
		{
		}

		public void Process(string outpath)
		{
			OpenOutput(outpath);
			Dictionary<int, List<int>> dict = GetMatchesDiccionary();
			// Va uno por uno...
			string stm = "SELECT * FROM HashesAmpliado order by Id";
			int badJump = 0;
			using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
			{
				using (SQLiteDataReader rdr = cmd.ExecuteReader())
				{
					int n = 0;
					var total = GetCount();
					DateTime t = DateTime.Now;
					Console.WriteLine("Vinculando miembros...");
					int lastIdBasico = 0;
					int preLastId = 0;
					int idPrevio = 0;
					while (rdr.Read())
					{
						n++;
						if (n % 1000 == 0) Console.WriteLine("Miembros: " + (Math.Floor(((double)n / total) * 10000)) / 100 + " % (" + n + " de " + total + "). Transcurridos: " + ((int)(DateTime.Now - t).TotalMinutes) + " minutos.");

						// Se fija si no está usado...
						int id = rdr.GetInt32(0);
						// toma el primer mayor al último
						if (dict.ContainsKey(id))
						{
							List<int> valores = dict[id];
							int idBasico = GetIdBasico(lastIdBasico, valores);
							if (idBasico == -1)
							{
								// BADJUMP
								Insert(id, idPrevio, valores[0], 4);
								lastIdBasico = preLastId;
								badJump++;
							}
							else if (idBasico - lastIdBasico > 10000)
							{
								// RETAIN
								Insert(id, idPrevio, lastIdBasico, 2);
							}
							else
							{
								// OK
								Insert(id, idPrevio, idBasico, 1);
								preLastId = lastIdBasico;
								lastIdBasico = idBasico;
							}
						}
						else
						{
							// RETAIN-SKIP
							Insert(id, idPrevio, lastIdBasico, 0);
						}
						idPrevio = id;
					}
				}
				Console.WriteLine("Saltos dudosos: " + badJump);
			
			}
		}

		private static int GetIdBasico(int lastIdBasico, List<int> valores)
		{
			foreach (var i in valores)
			{
				if (i > lastIdBasico)
				{
					return i;
				}
			}
			return -1;
		}

		private Dictionary<int, List<int>> GetMatchesDiccionary()
		{
			string stm = "SELECT IdAmpliada, IdBasico FROM HashMatchAmpliada where idBasico is not null Order by IdAmpliada, IdBasico ";
			var ret = new Dictionary<int, List<int>>();
			int lastParent = -1;
			List<int> children = new List<int>();
			using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
			{
				using (SQLiteDataReader rdr = cmd.ExecuteReader())
				{
					Console.WriteLine("Leyendo relaciones...");
					while (rdr.Read())
					{
						int parentId = rdr.GetInt32(0);
						int childId = rdr.GetInt32(1);
						if (parentId != lastParent && lastParent != -1)
						{
							ret[lastParent] = children;
							children = new List<int>();
						}
						children.Add(childId);
						lastParent = parentId;  
					}
				}
			}
			ret[lastParent] = children;
			return ret;
		}


		private void Insert(int idAmpliada, int IdAmpliadaPrevio, int idBasico, int success)
		{
			string insertCmd = "INSERT INTO HashMatchAmpliadaFinal " + 
				"(IdAmpliada, IdAmpliadaPrevio, IdBasico, Success) Values (" + idAmpliada + "," + IdAmpliadaPrevio + "," +  idBasico + "," + success + ")";
			using (var cmd2 = new SQLiteCommand(insertCmd, conn))
			{
					cmd2.ExecuteNonQuery();
			}
		}

		private int GetCount()
		{
			string cleanCmd = "SELECT COUNT(*) FROM HashesAmpliado";
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				return (int)(long) cmd.ExecuteScalar();
			}
		}

		private void OpenOutput(string outpath)
		{
			conn = OpenDb.Open(outpath);
						
      string tableCmd = "CREATE TABLE IF NOT EXISTS HashMatchAmpliadaFinal (IdAmpliada "
				+ " INT PRIMARY KEY, IdAmpliadaPrevio INT, IdBasico INT, Success INT)";
      using (var cmd = new SQLiteCommand(tableCmd, conn))
      {
          cmd.ExecuteNonQuery();
      }
					
      string clearCmd = "DELETE FROM HashMatchAmpliadaFinal";
      using (var cmd = new SQLiteCommand(clearCmd, conn))
      {
          cmd.ExecuteNonQuery();
      }
		}
		
	}
}