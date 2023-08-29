namespace finder
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.cmdHash = new System.Windows.Forms.Button();
			this.cmdGroups = new System.Windows.Forms.Button();
			this.cmdExpande = new System.Windows.Forms.Button();
			this.lblSrc = new System.Windows.Forms.Label();
			this.btnSource = new System.Windows.Forms.Button();
			this.btnTarget = new System.Windows.Forms.Button();
			this.lblTarget = new System.Windows.Forms.Label();
			this.cmdFindPairs = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.lwVariables = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lwKeys = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lwExcluded = new System.Windows.Forms.ListView();
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdKeyAdd = new System.Windows.Forms.Button();
			this.cmdExcludedAdd = new System.Windows.Forms.Button();
			this.cmdExcludedRemove = new System.Windows.Forms.Button();
			this.cmdKeyRemove = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.numCut = new System.Windows.Forms.NumericUpDown();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.textBox12 = new System.Windows.Forms.TextBox();
			this.cmdOpenTarget = new System.Windows.Forms.Button();
			this.grpStatus = new System.Windows.Forms.GroupBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.textBox13 = new System.Windows.Forms.TextBox();
			this.textBox14 = new System.Windows.Forms.TextBox();
			this.textBox15 = new System.Windows.Forms.TextBox();
			this.textBox16 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.link = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.numCut)).BeginInit();
			this.grpStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdHash
			// 
			this.cmdHash.Enabled = false;
			this.cmdHash.Location = new System.Drawing.Point(32, 433);
			this.cmdHash.Name = "cmdHash";
			this.cmdHash.Size = new System.Drawing.Size(145, 85);
			this.cmdHash.TabIndex = 16;
			this.cmdHash.Text = "1. Calcula Hashes";
			this.cmdHash.UseVisualStyleBackColor = true;
			this.cmdHash.Click += new System.EventHandler(this.cmdHash_Click);
			// 
			// cmdGroups
			// 
			this.cmdGroups.Enabled = false;
			this.cmdGroups.Location = new System.Drawing.Point(32, 582);
			this.cmdGroups.Name = "cmdGroups";
			this.cmdGroups.Size = new System.Drawing.Size(145, 52);
			this.cmdGroups.TabIndex = 18;
			this.cmdGroups.Text = "3. Busca grupos";
			this.cmdGroups.UseVisualStyleBackColor = true;
			this.cmdGroups.Click += new System.EventHandler(this.cmdGroups_Click);
			// 
			// cmdExpande
			// 
			this.cmdExpande.Enabled = false;
			this.cmdExpande.Location = new System.Drawing.Point(32, 640);
			this.cmdExpande.Name = "cmdExpande";
			this.cmdExpande.Size = new System.Drawing.Size(145, 52);
			this.cmdExpande.TabIndex = 19;
			this.cmdExpande.Text = "4. Expande grupos";
			this.cmdExpande.UseVisualStyleBackColor = true;
			this.cmdExpande.Click += new System.EventHandler(this.cmdExpande_Click);
			// 
			// lblSrc
			// 
			this.lblSrc.AutoSize = true;
			this.lblSrc.Location = new System.Drawing.Point(174, 23);
			this.lblSrc.Name = "lblSrc";
			this.lblSrc.Size = new System.Drawing.Size(0, 13);
			this.lblSrc.TabIndex = 3;
			// 
			// btnSource
			// 
			this.btnSource.Location = new System.Drawing.Point(93, 18);
			this.btnSource.Name = "btnSource";
			this.btnSource.Size = new System.Drawing.Size(67, 23);
			this.btnSource.TabIndex = 1;
			this.btnSource.Text = "Origen";
			this.btnSource.UseVisualStyleBackColor = true;
			this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
			// 
			// btnTarget
			// 
			this.btnTarget.Location = new System.Drawing.Point(93, 56);
			this.btnTarget.Name = "btnTarget";
			this.btnTarget.Size = new System.Drawing.Size(67, 23);
			this.btnTarget.TabIndex = 3;
			this.btnTarget.Text = "Destino";
			this.btnTarget.UseVisualStyleBackColor = true;
			this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
			// 
			// lblTarget
			// 
			this.lblTarget.AutoSize = true;
			this.lblTarget.Location = new System.Drawing.Point(174, 61);
			this.lblTarget.Name = "lblTarget";
			this.lblTarget.Size = new System.Drawing.Size(0, 13);
			this.lblTarget.TabIndex = 3;
			// 
			// cmdFindPairs
			// 
			this.cmdFindPairs.Enabled = false;
			this.cmdFindPairs.Location = new System.Drawing.Point(32, 524);
			this.cmdFindPairs.Name = "cmdFindPairs";
			this.cmdFindPairs.Size = new System.Drawing.Size(145, 52);
			this.cmdFindPairs.TabIndex = 17;
			this.cmdFindPairs.Text = "2. Busca pares";
			this.cmdFindPairs.UseVisualStyleBackColor = true;
			this.cmdFindPairs.Click += new System.EventHandler(this.cmdFindPairs_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(48, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "SQLite";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "SPSS";
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(195, 441);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(449, 73);
			this.textBox1.TabIndex = 7;
			this.textBox1.TabStop = false;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(195, 536);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(384, 52);
			this.textBox2.TabIndex = 7;
			this.textBox2.Text = "2. Abre la base de SQLite y generar un listado de todos los pares de elementos (e" +
    "j. viviendas) que tienen hashes iguales.";
			// 
			// lwVariables
			// 
			this.lwVariables.AllowColumnReorder = true;
			this.lwVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
			this.lwVariables.FullRowSelect = true;
			this.lwVariables.HideSelection = false;
			this.lwVariables.Location = new System.Drawing.Point(32, 128);
			this.lwVariables.Name = "lwVariables";
			this.lwVariables.Size = new System.Drawing.Size(405, 261);
			this.lwVariables.TabIndex = 8;
			this.lwVariables.UseCompatibleStateImageBehavior = false;
			this.lwVariables.View = System.Windows.Forms.View.Details;
			this.lwVariables.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lwVariables_ItemSelectionChanged);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 160;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Label";
			this.columnHeader3.Width = 200;
			// 
			// lwKeys
			// 
			this.lwKeys.AllowColumnReorder = true;
			this.lwKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4});
			this.lwKeys.FullRowSelect = true;
			this.lwKeys.HideSelection = false;
			this.lwKeys.Location = new System.Drawing.Point(562, 128);
			this.lwKeys.Name = "lwKeys";
			this.lwKeys.Size = new System.Drawing.Size(395, 109);
			this.lwKeys.TabIndex = 12;
			this.lwKeys.UseCompatibleStateImageBehavior = false;
			this.lwKeys.View = System.Windows.Forms.View.Details;
			this.lwKeys.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lwKeys_ItemSelectionChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 160;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Label";
			this.columnHeader4.Width = 200;
			// 
			// lwExcluded
			// 
			this.lwExcluded.AllowColumnReorder = true;
			this.lwExcluded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
			this.lwExcluded.FullRowSelect = true;
			this.lwExcluded.HideSelection = false;
			this.lwExcluded.Location = new System.Drawing.Point(562, 280);
			this.lwExcluded.Name = "lwExcluded";
			this.lwExcluded.Size = new System.Drawing.Size(395, 109);
			this.lwExcluded.TabIndex = 15;
			this.lwExcluded.UseCompatibleStateImageBehavior = false;
			this.lwExcluded.View = System.Windows.Forms.View.Details;
			this.lwExcluded.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lwExcluded_ItemSelectionChanged);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Name";
			this.columnHeader5.Width = 160;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Label";
			this.columnHeader6.Width = 200;
			// 
			// cmdKeyAdd
			// 
			this.cmdKeyAdd.Enabled = false;
			this.cmdKeyAdd.Location = new System.Drawing.Point(452, 143);
			this.cmdKeyAdd.Name = "cmdKeyAdd";
			this.cmdKeyAdd.Size = new System.Drawing.Size(98, 32);
			this.cmdKeyAdd.TabIndex = 9;
			this.cmdKeyAdd.Text = ">>";
			this.cmdKeyAdd.UseVisualStyleBackColor = true;
			this.cmdKeyAdd.Click += new System.EventHandler(this.cmdKeyAdd_Click);
			// 
			// cmdExcludedAdd
			// 
			this.cmdExcludedAdd.Enabled = false;
			this.cmdExcludedAdd.Location = new System.Drawing.Point(452, 303);
			this.cmdExcludedAdd.Name = "cmdExcludedAdd";
			this.cmdExcludedAdd.Size = new System.Drawing.Size(98, 32);
			this.cmdExcludedAdd.TabIndex = 13;
			this.cmdExcludedAdd.Text = ">>";
			this.cmdExcludedAdd.UseVisualStyleBackColor = true;
			this.cmdExcludedAdd.Click += new System.EventHandler(this.cmdExcludedAdd_Click);
			// 
			// cmdExcludedRemove
			// 
			this.cmdExcludedRemove.Enabled = false;
			this.cmdExcludedRemove.Location = new System.Drawing.Point(452, 341);
			this.cmdExcludedRemove.Name = "cmdExcludedRemove";
			this.cmdExcludedRemove.Size = new System.Drawing.Size(98, 32);
			this.cmdExcludedRemove.TabIndex = 14;
			this.cmdExcludedRemove.Text = "<<";
			this.cmdExcludedRemove.UseVisualStyleBackColor = true;
			this.cmdExcludedRemove.Click += new System.EventHandler(this.cmdExcludedRemove_Click);
			// 
			// cmdKeyRemove
			// 
			this.cmdKeyRemove.Enabled = false;
			this.cmdKeyRemove.Location = new System.Drawing.Point(452, 181);
			this.cmdKeyRemove.Name = "cmdKeyRemove";
			this.cmdKeyRemove.Size = new System.Drawing.Size(98, 32);
			this.cmdKeyRemove.TabIndex = 10;
			this.cmdKeyRemove.Text = "<<";
			this.cmdKeyRemove.UseVisualStyleBackColor = true;
			this.cmdKeyRemove.Click += new System.EventHandler(this.cmdKeyRemove_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(570, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(223, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Campo de la clave única (debe ser numérica):";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(570, 253);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(193, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Campos excluídos para la comparación";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(42, 103);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(127, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Campos de comparación:";
			// 
			// numCut
			// 
			this.numCut.Location = new System.Drawing.Point(202, 606);
			this.numCut.Name = "numCut";
			this.numCut.Size = new System.Drawing.Size(52, 20);
			this.numCut.TabIndex = 21;
			this.numCut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numCut.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numCut.ValueChanged += new System.EventHandler(this.numCut_ValueChanged);
			// 
			// textBox4
			// 
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Location = new System.Drawing.Point(270, 593);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(287, 38);
			this.textBox4.TabIndex = 7;
			this.textBox4.Text = "3. Recorre los pares identificando grupos sucesivos de pares idénticos y guardánd" +
    "olos en una tabla en SQLite.";
			// 
			// textBox5
			// 
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox5.Location = new System.Drawing.Point(195, 652);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(449, 40);
			this.textBox5.TabIndex = 7;
			this.textBox5.Text = resources.GetString("textBox5.Text");
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(204, 588);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 20;
			this.label6.Text = "Corte:";
			// 
			// textBox3
			// 
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Location = new System.Drawing.Point(736, 412);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(124, 21);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = "Tablas / base de salida";
			// 
			// textBox6
			// 
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Location = new System.Drawing.Point(343, 412);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(124, 21);
			this.textBox6.TabIndex = 7;
			this.textBox6.Text = "Descripción";
			// 
			// textBox7
			// 
			this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox7.Location = new System.Drawing.Point(51, 412);
			this.textBox7.Multiline = true;
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.Size = new System.Drawing.Size(124, 21);
			this.textBox7.TabIndex = 7;
			this.textBox7.Text = "Paso";
			// 
			// textBox8
			// 
			this.textBox8.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox8.Location = new System.Drawing.Point(662, 412);
			this.textBox8.Multiline = true;
			this.textBox8.Name = "textBox8";
			this.textBox8.ReadOnly = true;
			this.textBox8.Size = new System.Drawing.Size(2, 275);
			this.textBox8.TabIndex = 14;
			// 
			// textBox9
			// 
			this.textBox9.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox9.Location = new System.Drawing.Point(189, 520);
			this.textBox9.Multiline = true;
			this.textBox9.Name = "textBox9";
			this.textBox9.ReadOnly = true;
			this.textBox9.Size = new System.Drawing.Size(760, 2);
			this.textBox9.TabIndex = 15;
			// 
			// textBox10
			// 
			this.textBox10.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox10.Location = new System.Drawing.Point(189, 431);
			this.textBox10.Multiline = true;
			this.textBox10.Name = "textBox10";
			this.textBox10.ReadOnly = true;
			this.textBox10.Size = new System.Drawing.Size(760, 2);
			this.textBox10.TabIndex = 16;
			// 
			// textBox11
			// 
			this.textBox11.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox11.Location = new System.Drawing.Point(189, 576);
			this.textBox11.Multiline = true;
			this.textBox11.Name = "textBox11";
			this.textBox11.ReadOnly = true;
			this.textBox11.Size = new System.Drawing.Size(760, 2);
			this.textBox11.TabIndex = 16;
			// 
			// textBox12
			// 
			this.textBox12.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox12.Location = new System.Drawing.Point(189, 638);
			this.textBox12.Multiline = true;
			this.textBox12.Name = "textBox12";
			this.textBox12.ReadOnly = true;
			this.textBox12.Size = new System.Drawing.Size(760, 2);
			this.textBox12.TabIndex = 16;
			// 
			// cmdOpenTarget
			// 
			this.cmdOpenTarget.Enabled = false;
			this.cmdOpenTarget.Location = new System.Drawing.Point(177, 83);
			this.cmdOpenTarget.Name = "cmdOpenTarget";
			this.cmdOpenTarget.Size = new System.Drawing.Size(40, 21);
			this.cmdOpenTarget.TabIndex = 4;
			this.cmdOpenTarget.Text = "Abrir";
			this.cmdOpenTarget.UseVisualStyleBackColor = true;
			this.cmdOpenTarget.Click += new System.EventHandler(this.button1_Click);
			// 
			// grpStatus
			// 
			this.grpStatus.Controls.Add(this.lblStatus);
			this.grpStatus.Location = new System.Drawing.Point(321, 143);
			this.grpStatus.Name = "grpStatus";
			this.grpStatus.Size = new System.Drawing.Size(343, 166);
			this.grpStatus.TabIndex = 17;
			this.grpStatus.TabStop = false;
			this.grpStatus.Text = "Procesando";
			this.grpStatus.Visible = false;
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(27, 40);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(295, 63);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "label7";
			// 
			// textBox13
			// 
			this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox13.Location = new System.Drawing.Point(714, 469);
			this.textBox13.Multiline = true;
			this.textBox13.Name = "textBox13";
			this.textBox13.ReadOnly = true;
			this.textBox13.Size = new System.Drawing.Size(158, 17);
			this.textBox13.TabIndex = 7;
			this.textBox13.Text = "Tabla Hashes en SQLite";
			// 
			// textBox14
			// 
			this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox14.Location = new System.Drawing.Point(714, 544);
			this.textBox14.Multiline = true;
			this.textBox14.Name = "textBox14";
			this.textBox14.ReadOnly = true;
			this.textBox14.Size = new System.Drawing.Size(158, 17);
			this.textBox14.TabIndex = 7;
			this.textBox14.Text = "Tabla HashesTuples en SQLite";
			// 
			// textBox15
			// 
			this.textBox15.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox15.Location = new System.Drawing.Point(714, 602);
			this.textBox15.Multiline = true;
			this.textBox15.Name = "textBox15";
			this.textBox15.ReadOnly = true;
			this.textBox15.Size = new System.Drawing.Size(158, 17);
			this.textBox15.TabIndex = 7;
			this.textBox15.Text = "Tabla Groups_<corte> en SQLite";
			// 
			// textBox16
			// 
			this.textBox16.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox16.Location = new System.Drawing.Point(714, 660);
			this.textBox16.Multiline = true;
			this.textBox16.Name = "textBox16";
			this.textBox16.ReadOnly = true;
			this.textBox16.Size = new System.Drawing.Size(213, 20);
			this.textBox16.TabIndex = 7;
			this.textBox16.Text = "Tabla GroupMembers_<corte> en SQLite";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(223, 89);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 13);
			this.label7.TabIndex = 5;
			this.label7.Text = "Recomendado:";
			// 
			// link
			// 
			this.link.AutoSize = true;
			this.link.Location = new System.Drawing.Point(300, 89);
			this.link.Name = "link";
			this.link.Size = new System.Drawing.Size(127, 13);
			this.link.TabIndex = 6;
			this.link.TabStop = true;
			this.link.Text = "https://sqlitebrowser.org/";
			this.link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(987, 715);
			this.Controls.Add(this.link);
			this.Controls.Add(this.grpStatus);
			this.Controls.Add(this.textBox12);
			this.Controls.Add(this.textBox11);
			this.Controls.Add(this.textBox10);
			this.Controls.Add(this.textBox9);
			this.Controls.Add(this.textBox8);
			this.Controls.Add(this.numCut);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdKeyRemove);
			this.Controls.Add(this.cmdExcludedRemove);
			this.Controls.Add(this.lwExcluded);
			this.Controls.Add(this.lwKeys);
			this.Controls.Add(this.lwVariables);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox16);
			this.Controls.Add(this.textBox15);
			this.Controls.Add(this.textBox14);
			this.Controls.Add(this.textBox13);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdOpenTarget);
			this.Controls.Add(this.btnTarget);
			this.Controls.Add(this.btnSource);
			this.Controls.Add(this.lblTarget);
			this.Controls.Add(this.lblSrc);
			this.Controls.Add(this.cmdExpande);
			this.Controls.Add(this.cmdGroups);
			this.Controls.Add(this.cmdFindPairs);
			this.Controls.Add(this.cmdExcludedAdd);
			this.Controls.Add(this.cmdKeyAdd);
			this.Controls.Add(this.cmdHash);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Identificador de elementos duplicados";
			this.Load += new System.EventHandler(this.frmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.numCut)).EndInit();
			this.grpStatus.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdHash;
		private System.Windows.Forms.Button cmdGroups;
		private System.Windows.Forms.Button cmdExpande;
		private System.Windows.Forms.Label lblSrc;
		private System.Windows.Forms.Button btnSource;
		private System.Windows.Forms.Button btnTarget;
		private System.Windows.Forms.Label lblTarget;
		private System.Windows.Forms.Button cmdFindPairs;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.ListView lwVariables;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView lwKeys;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ListView lwExcluded;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button cmdKeyAdd;
		private System.Windows.Forms.Button cmdExcludedAdd;
		private System.Windows.Forms.Button cmdExcludedRemove;
		private System.Windows.Forms.Button cmdKeyRemove;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numCut;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.TextBox textBox10;
		private System.Windows.Forms.TextBox textBox11;
		private System.Windows.Forms.TextBox textBox12;
		private System.Windows.Forms.Button cmdOpenTarget;
		private System.Windows.Forms.GroupBox grpStatus;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.TextBox textBox13;
		private System.Windows.Forms.TextBox textBox14;
		private System.Windows.Forms.TextBox textBox15;
		private System.Windows.Forms.TextBox textBox16;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.LinkLabel link;
	}
}