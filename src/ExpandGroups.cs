using Spss;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace finder
{
	class ExpandGroups
	{
		SQLiteConnection conn;
		int SIZE_LIMIT;
		string Key;
		SortedSet<int> inserted;

		public ExpandGroups(int sizeLimit, string key)
		{
			SIZE_LIMIT = sizeLimit;
			Key = key;
		}
		public void Process(string outpath)
		{
			OpenOutput(outpath);
			inserted = new SortedSet<int>();
			// Va uno por uno...
			string stm = "SELECT * FROM Groups_" + SIZE_LIMIT;
			using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
			{
				using (SQLiteDataReader rdr = cmd.ExecuteReader())
				{
					int n = 0;
					var total = GetCount();
					DateTime t = DateTime.Now;
					Status.Update("Expandiendo miembros...");
					while (rdr.Read())
					{
						n++;
						if (n % 10 == 0) Status.Update("Miembros: " + (Math.Floor(((double)n / total) * 10000)) / 100 + " % (" + n + " de " + total + "). \n\nTranscurridos: " + ((int)(DateTime.Now - t).TotalMinutes) + " minutos.");

						// Se fija si no está usado...
						int id = rdr.GetInt32(0);
						int clonedId = rdr.GetInt32(1);

						int c = rdr.GetInt32(2);
						int size = rdr.GetInt32(3);
						string hash = rdr.GetString(4);

						if (!isUsed(id, hash))
						{
							for (int i = 0; i < size; i++)
							{
								Insert(id + i, c, size, hash);
							}
						}
						if (!isUsed(clonedId, hash))
							for (int i = 0; i < size; i++)
							{
								Insert(clonedId + i, c, size, hash);
							}
					}
				}

			}

			string updateHash = "UPDATE  GroupsMembers_" + SIZE_LIMIT +
				" SET hash = (SELECT hash FROM Hashes WHERE id = GroupsMembers_" + SIZE_LIMIT + "." + Key + ")";
			using (var cmd = new SQLiteCommand(updateHash, conn))
			{
				cmd.ExecuteNonQuery();
			}
			var tabla = "GroupsMembers_" + SIZE_LIMIT;

			int gruposExpandidos = Status.GetTableSize(conn, tabla);
			if (gruposExpandidos > 0)
			{
				Status.Hide("Se insertaron exitosamente " + gruposExpandidos.ToString() + " filas en la tabla '" + tabla + "'.");
			} else
			{
				Status.Hide("No se encontraron grupos para expandir o miembros de los grupos.");
			}

			conn.Dispose();
		}

		private int getClonedCount(int clonedId)
		{
			string cleanCmd = "SELECT COUNT(*) FROM Groups_" + SIZE_LIMIT + " WHERE CloneId = " + clonedId;
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				return (int)(long)cmd.ExecuteScalar();
			}
		}

		private int getIdCount(int id)
		{
			string cleanCmd = "SELECT COUNT(*) FROM Groups_" + SIZE_LIMIT + " WHERE Id = " + id;
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				return (int)(long)cmd.ExecuteScalar();
			}
		}

		private bool isUsed(int id, string hash)
		{
			string select = "SELECT COUNT(*) FROM GroupsMembers_" + SIZE_LIMIT + " WHERE " + Key + " = " + id;
			int inserteds = 0;
			using (var cmd = new SQLiteCommand(select, conn))
			{
				inserteds = (int)(long)cmd.ExecuteScalar();
				if (inserteds == 0) return false;
			}
			string hashed = "SELECT COUNT(*) FROM GroupsMembers_" + SIZE_LIMIT + " WHERE " + Key + " = " + id + " AND GroupHash = '" + hash + "'";
			int insertedsEqual = 0;
			using (var cmd = new SQLiteCommand(hashed, conn))
			{
				insertedsEqual = (int)(long)cmd.ExecuteScalar();
				//	if (inserteds != insertedsEqual) throw new Exception();
			}
			return true;
		}
		private void Insert(int id, int c, int size, string hash)
		{
			string insertCmd = "INSERT INTO GroupsMembers_" + SIZE_LIMIT +
				"(" + Key + ", GroupHash, Persons, Houses) Values (" + id + ",'"
										+ hash + "'," + c + "," + size + ")";
			if (inserted.Contains(id))
				return;
			else
				inserted.Add(id);
			using (var cmd2 = new SQLiteCommand(insertCmd, conn))
			{
				cmd2.ExecuteNonQuery();
			}
		}

		private int GetCount()
		{
			string cleanCmd = "SELECT COUNT(*) FROM Groups_" + SIZE_LIMIT;
			using (var cmd = new SQLiteCommand(cleanCmd, conn))
			{
				return (int)(long)cmd.ExecuteScalar();
			}
		}

		private void OpenOutput(string outpath)
		{
			conn = OpenDb.Open(outpath);
			string dropCmd = "DROP TABLE IF EXISTS GroupsMembers_" + SIZE_LIMIT;
			using (var cmd2 = new SQLiteCommand(dropCmd, conn))
			{
				cmd2.ExecuteNonQuery();
			}
			string tableCmd = "CREATE TABLE GroupsMembers_" + SIZE_LIMIT + " (" + Key
					  + " INT PRIMARY KEY, GroupHash TEXT, Persons INT, Houses INT, Hash TEXT)";
			using (var cmd = new SQLiteCommand(tableCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}

			string clearCmd = "DELETE FROM GroupsMembers_" + SIZE_LIMIT;
			using (var cmd = new SQLiteCommand(clearCmd, conn))
			{
				cmd.ExecuteNonQuery();
			}
		}

	}
}