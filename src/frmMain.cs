using Spss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace finder
{
	public partial class frmMain : Form
	{
		const int C_SOURCE = 0;
		const int C_TARGET = 1;
		const int S_NUM_CUT = 0;

		public frmMain()
		{
			InitializeComponent();
		}

		private void cmdHash_Click(object sender, EventArgs e)
		{
			if (lwKeys.Items.Count == 0)
			{
				MessageBox.Show(this, "Debe haber un campo de clave única.");
				return;
			}
			var opts = createConfig();

			HashBuilder p = new HashBuilder();
			p.Process(lblSrc.Text, opts, lblTarget.Text);

			UpdateCmdStart();

		}

		private void cmdGroups_Click(object sender, EventArgs e)
		{
			var opts = createConfig();

			FindGroups f = new FindGroups((int) numCut.Value);
			f.Process(lblTarget.Text);
		}

		private Dictionary<string, string[]> createConfig()
		{
			var fields = SelectedVariables(lwVariables);
			var keys = SelectedVariables(lwKeys);
			var exclusions = SelectedVariables(lwExcluded);
			return new Dictionary<string, string[]> { 
				{
				"fields", fields.ToArray()
				},
				{
				"keys", keys.ToArray()
				},
				{
				"exclusions", exclusions.ToArray()
				},
				{
					"files", new string[] { lblSrc.Text, lblTarget.Text }
				},
				{
					"settings", new string[] { numCut.Value.ToString() }
				}
			};
		}

		private List<string> SelectedVariables(ListView lwVariables)
		{
			var list = new List<string>();
			foreach(ListViewItem l in lwVariables.Items)
			{
				list.Add(l.Text);
			}
			return list; 
		}

		private void cmdExpande_Click(object sender, EventArgs e)
		{
			var opts = createConfig();

			string key = opts["keys"][0];
			ExpandGroups exp = new ExpandGroups((int) numCut.Value, key);
			exp.Process(lblTarget.Text);
		}

		private void btnSource_Click(object sender, EventArgs e)
		{
			string file = GetByDialog("Archivo de casos");
			if (file == "") return;
			lblSrc.Text = file;
			lblTarget.Text = "";
			if (lblSrc.Text.ToLower().EndsWith(".sav"))
			{
				lblTarget.Text = lblSrc.Text.Substring(0, lblSrc.Text.Length - 3) + "sqlite";
			}
			ReceiveSource();
			SaveState();
		}

		private void ReceiveSource()
		{
			LoadVariables();
			UpdateCmdStart();
			UpdateMoveButtons();
		}

		private void LoadVariables()
		{
			lwVariables.Items.Clear();
			lwKeys.Items.Clear();
			lwExcluded.Items.Clear();
			if (lblSrc.Text == "")
			{
				return;
			}
			var mainFile = SpssDataDocument.Open(lblSrc.Text, SpssFileAccess.Read);

			// Carga las variables con nombres en común
			foreach (var v1 in mainFile.Variables)
			{
				ListViewItem i = new ListViewItem();
				i.Text = v1.Name;
				i.SubItems.Add(v1.Label);
				i.Checked = true;
				lwVariables.Items.Add(i);
			}

		}

		private string GetByDialog(string v1)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.Filter = "SPSS File (.sav)|*.sav|All Files (*.*)|*.*";
			openFileDialog1.FilterIndex = 1;
			openFileDialog1.Title = "Select " + v1;
			openFileDialog1.Multiselect = false;

			DialogResult userClickedOK = openFileDialog1.ShowDialog(this);

			if (userClickedOK == System.Windows.Forms.DialogResult.OK)
				return openFileDialog1.FileName;
			else
				return "";
		}

		private string SaveByDialog(string v1)
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			if (lblTarget.Text != "")
				saveFileDialog1.InitialDirectory = Path.GetDirectoryName(lblTarget.Text);
			else
				if (lblSrc.Text != "")
				saveFileDialog1.InitialDirectory = Path.GetDirectoryName(lblSrc.Text);

			saveFileDialog1.FileName = "resultado.sqlite";
			saveFileDialog1.Filter = "Arrchivo SQLite (.sqlite)|*.sqlite|All Files (*.*)|*.*";
			saveFileDialog1.FilterIndex = 1;
			saveFileDialog1.Title = "Selecione " + v1;
			saveFileDialog1.OverwritePrompt = true;

			DialogResult userClickedOK = saveFileDialog1.ShowDialog(this);

			if (userClickedOK == System.Windows.Forms.DialogResult.OK)
				return saveFileDialog1.FileName;
			else
				return "";
		}

		private void btnTarget_Click(object sender, EventArgs e)
		{
			string file = SaveByDialog("archivo de destino");
			if (file == "") return;
			lblTarget.Text = file;
			UpdateCmdStart();
			SaveState();
		}
		private void UpdateCmdStart()
		{
			cmdHash.Enabled = (lblSrc.Text != "" && lblTarget.Text != "") && lwKeys.Items.Count > 0;
			cmdFindPairs.Enabled = lblTarget.Text != "" && File.Exists(lblTarget.Text) && lwKeys.Items.Count > 0;
			cmdGroups.Enabled = lblTarget.Text != "" && File.Exists(lblTarget.Text) && lwKeys.Items.Count > 0;
			cmdExpande.Enabled = lblTarget.Text != "" && File.Exists(lblTarget.Text) && lwKeys.Items.Count > 0;
			cmdOpenTarget.Enabled = lblTarget.Text != "" && File.Exists(lblTarget.Text) && lwKeys.Items.Count > 0;
		}

		private void cmdKeyAdd_Click(object sender, EventArgs e)
		{
			MoveItems(lwVariables, lwKeys);
		}

		private void MoveItems(ListView src, ListView target)
		{
			foreach (ListViewItem item in src.SelectedItems)
			{
				src.Items.Remove(item);
				target.Items.Add(item);
			}
			UpdateMoveButtons();
			UpdateCmdStart();
			SaveState();
		}

	

		private void UpdateMoveButtons()
		{
			cmdKeyAdd.Enabled = lwVariables.SelectedItems.Count > 0 && lwKeys.Items.Count == 0;
			cmdExcludedAdd.Enabled = lwVariables.SelectedItems.Count > 0;

			cmdKeyRemove.Enabled = lwKeys.SelectedItems.Count > 0;
			cmdExcludedRemove.Enabled = lwExcluded.SelectedItems.Count > 0;
		}

		private void cmdExcludedAdd_Click(object sender, EventArgs e)
		{
			MoveItems(lwVariables, lwExcluded);
		}

		private void cmdKeyRemove_Click(object sender, EventArgs e)
		{
			MoveItems(lwKeys, lwVariables);
		}

		private void cmdExcludedRemove_Click(object sender, EventArgs e)
		{
			MoveItems(lwExcluded, lwVariables);
		}

		private void lwVariables_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			UpdateMoveButtons();
		}

		private void lwKeys_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			UpdateMoveButtons();

		}

		private void lwExcluded_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			UpdateMoveButtons();

		}

		private void cmdFindPairs_Click(object sender, EventArgs e)
		{
			var opts = createConfig();

			DuplicateJoin j = new DuplicateJoin();
			j.Process(lblTarget.Text);
		}

		
		private void frmMain_Load(object sender, EventArgs e)
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
			
			Status.label = lblStatus;
			Status.panel = grpStatus;
			LoadState();
		}
		void MyHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception e = (Exception)args.ExceptionObject;
			MessageBox.Show(this, e.Message, "Se ha producido un error");
		}
		private void SaveState()
		{
			string configFilename = GetConfigFilename();
			var config = createConfig();
			var ret = new JavaScriptSerializer().Serialize(config);
			File.WriteAllText(configFilename, ret);
		}

		private void LoadState()
		{
			try
			{
				Dictionary<string, string[]> ret;
				string config = GetConfigFilename();
				if (!File.Exists(config))
					return;
				var json = File.ReadAllText(config);
				ret = new JavaScriptSerializer().Deserialize<Dictionary<string, string[]>>(json);
				// recorre ret
				var files = ret["files"];
				if (File.Exists(files[C_SOURCE]))
				{
					lblSrc.Text = files[C_SOURCE];
					ReceiveSource();
				}
				if (files[C_TARGET] != "" && Directory.Exists(Path.GetDirectoryName(files[C_TARGET])))
				{
					lblTarget.Text = files[C_TARGET];
					UpdateCmdStart();
				}
				// va por los grupos
				SelectItemsFromList(lwVariables, ret["keys"]);
				MoveItems(lwVariables, lwKeys);
				SelectItemsFromList(lwVariables, ret["exclusions"]);
				MoveItems(lwVariables, lwExcluded);

				numCut.Value = int.Parse(ret["settings"][S_NUM_CUT]);
				lwKeys.SelectedItems.Clear();
				lwExcluded.SelectedItems.Clear();

			}
			catch (Exception ex)
			{

			}
		}

		private void SelectItemsFromList(ListView lwVariables, string[] strings)
		{
			foreach(ListViewItem item in lwVariables.Items)
			{
				if (strings.Contains(item.Text))
					item.Selected = true;
			}
		}

		private static string GetConfigFilename()
		{
			return Assembly.GetExecutingAssembly().Location + ".info.config";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(lblTarget.Text);
		}

		private void numCut_ValueChanged(object sender, EventArgs e)
		{
			SaveState();
		}

		private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(link.Text);
		}
	}
}
