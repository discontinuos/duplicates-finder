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
	class DuplicateJoin
	{
		SHA512Managed hasher = new SHA512Managed();
		SQLiteConnection conn;

		public void Process(string outpath)
		{
			Status.Update("Buscando pares...");
			var rows = CreateTuples(outpath);

			conn.Dispose();
			Status.Hide("Obtenidos: " + rows.ToString() + " registros idénticos (pares)");
		}

		private int CreateTuples(string outpath)
		{
			conn = OpenDb.Open(outpath);

		

			string drpoCmd = "DROP TABLE IF EXISTS HashesTuples";
			using (var cmd = new SQLiteCommand(drpoCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}

			string tableCmd = "CREATE TABLE HashesTuples (Id INT, CloneId INT, C INT, CloneC INT, Used INT)";
			using (var cmd = new SQLiteCommand(tableCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}


			string insertCmd = "INSERT INTO HashesTuples (Id, CloneId, C, CloneC, Used) " +
					  " SELECT t1.Id, t2.Id, t1.C, t2.C, 0 FROM Hashes t1 JOIN Hashes t2 WHERE t1.Hash = t2.Hash AND t1.C = t2.C AND t1.Id <> t2.Id AND t1.Id < t2.Id";
			using (var cmd = new SQLiteCommand(insertCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
			

			string indexCmd = "CREATE UNIQUE INDEX idx_joins ON HashesTuples (Id, CloneId);";
			using (var cmd = new SQLiteCommand(indexCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}

			string indexClone = "CREATE INDEX idx_joinsCloned ON HashesTuples (CloneId);";
			using (var cmd = new SQLiteCommand(indexClone, conn))
			{
				cmd.ExecuteNonQuery();
			}

			return Status.GetTableSize(conn, "HashesTuples");

		}

	}
}