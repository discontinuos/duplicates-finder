using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace finder
{
	internal class Status
	{
		public static Control panel;
		public static System.Windows.Forms.Label label;
		public static void Update(string text)
		{
			label.Text = text;
			label.Refresh();
			if (!panel.Visible)
			{
				panel.Visible = true;
				panel.Refresh();
			}
		}

		public static void Hide(string message) {
			panel.Visible = false;
			MessageBox.Show(panel.Parent, message, "¡Listo!");
		}
		public static void Show()
		{
			panel.Visible = true;
		}

		public static int GetTableSize(SQLiteConnection conn, string tableName)
		{
			string total = "SELECT COUNT(*) FROM " + tableName;
			using (var cmd = new SQLiteCommand(total, conn))
			{
				return int.Parse(cmd.ExecuteScalar().ToString());
			}
		}
	}
}
