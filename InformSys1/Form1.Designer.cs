
namespace InformSys1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>s
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonConnect = new System.Windows.Forms.Button();
            this.ButtonDisconnect = new System.Windows.Forms.Button();
            this.InitInfoLabel = new System.Windows.Forms.Label();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.LabelServer = new System.Windows.Forms.Label();
            this.LabelPort = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LabelUser = new System.Windows.Forms.Label();
            this.UserTextBox = new System.Windows.Forms.TextBox();
            this.LabelDatabase = new System.Windows.Forms.Label();
            this.DatabaseTextBox = new System.Windows.Forms.TextBox();
            this.ButtonInit = new System.Windows.Forms.Button();
            this.ButtonGetTable = new System.Windows.Forms.Button();
            this.ConnectionInfoLabel = new System.Windows.Forms.Label();
            this.button_Execute = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.labelSelectedFunction = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ParamTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonConnect
            // 
            this.ButtonConnect.Enabled = false;
            this.ButtonConnect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ButtonConnect.Location = new System.Drawing.Point(980, 528);
            this.ButtonConnect.Name = "ButtonConnect";
            this.ButtonConnect.Size = new System.Drawing.Size(101, 29);
            this.ButtonConnect.TabIndex = 0;
            this.ButtonConnect.Text = "Connect";
            this.ButtonConnect.UseVisualStyleBackColor = true;
            this.ButtonConnect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // ButtonDisconnect
            // 
            this.ButtonDisconnect.Enabled = false;
            this.ButtonDisconnect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ButtonDisconnect.Location = new System.Drawing.Point(980, 493);
            this.ButtonDisconnect.Name = "ButtonDisconnect";
            this.ButtonDisconnect.Size = new System.Drawing.Size(101, 29);
            this.ButtonDisconnect.TabIndex = 1;
            this.ButtonDisconnect.Text = "Disconnect";
            this.ButtonDisconnect.UseVisualStyleBackColor = true;
            this.ButtonDisconnect.Click += new System.EventHandler(this.Disconnect_click);
            // 
            // InitInfoLabel
            // 
            this.InitInfoLabel.AutoSize = true;
            this.InitInfoLabel.ForeColor = System.Drawing.Color.Red;
            this.InitInfoLabel.Location = new System.Drawing.Point(8, 9);
            this.InitInfoLabel.Name = "InitInfoLabel";
            this.InitInfoLabel.Size = new System.Drawing.Size(179, 20);
            this.InitInfoLabel.TabIndex = 3;
            this.InitInfoLabel.Text = "Connection not initialized";
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Location = new System.Drawing.Point(8, 98);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(162, 27);
            this.ServerTextBox.TabIndex = 4;
            this.ServerTextBox.Text = "localhost";
            // 
            // LabelServer
            // 
            this.LabelServer.AutoSize = true;
            this.LabelServer.ForeColor = System.Drawing.Color.Black;
            this.LabelServer.Location = new System.Drawing.Point(8, 75);
            this.LabelServer.Name = "LabelServer";
            this.LabelServer.Size = new System.Drawing.Size(50, 20);
            this.LabelServer.TabIndex = 5;
            this.LabelServer.Text = "Server";
            // 
            // LabelPort
            // 
            this.LabelPort.AutoSize = true;
            this.LabelPort.ForeColor = System.Drawing.Color.Black;
            this.LabelPort.Location = new System.Drawing.Point(8, 128);
            this.LabelPort.Name = "LabelPort";
            this.LabelPort.Size = new System.Drawing.Size(35, 20);
            this.LabelPort.TabIndex = 7;
            this.LabelPort.Text = "Port";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(8, 151);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(162, 27);
            this.PortTextBox.TabIndex = 6;
            this.PortTextBox.Text = "5432";
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.ForeColor = System.Drawing.Color.Black;
            this.LabelPassword.Location = new System.Drawing.Point(8, 234);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(70, 20);
            this.LabelPassword.TabIndex = 11;
            this.LabelPassword.Text = "Password";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(8, 257);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(162, 27);
            this.PasswordTextBox.TabIndex = 10;
            this.PasswordTextBox.Text = "123321Qs";
            // 
            // LabelUser
            // 
            this.LabelUser.AutoSize = true;
            this.LabelUser.ForeColor = System.Drawing.Color.Black;
            this.LabelUser.Location = new System.Drawing.Point(8, 181);
            this.LabelUser.Name = "LabelUser";
            this.LabelUser.Size = new System.Drawing.Size(38, 20);
            this.LabelUser.TabIndex = 9;
            this.LabelUser.Text = "User";
            // 
            // UserTextBox
            // 
            this.UserTextBox.Location = new System.Drawing.Point(8, 204);
            this.UserTextBox.Name = "UserTextBox";
            this.UserTextBox.Size = new System.Drawing.Size(162, 27);
            this.UserTextBox.TabIndex = 8;
            this.UserTextBox.Text = "postgres";
            // 
            // LabelDatabase
            // 
            this.LabelDatabase.AutoSize = true;
            this.LabelDatabase.ForeColor = System.Drawing.Color.Black;
            this.LabelDatabase.Location = new System.Drawing.Point(8, 287);
            this.LabelDatabase.Name = "LabelDatabase";
            this.LabelDatabase.Size = new System.Drawing.Size(72, 20);
            this.LabelDatabase.TabIndex = 13;
            this.LabelDatabase.Text = "Database";
            // 
            // DatabaseTextBox
            // 
            this.DatabaseTextBox.Location = new System.Drawing.Point(8, 310);
            this.DatabaseTextBox.Name = "DatabaseTextBox";
            this.DatabaseTextBox.Size = new System.Drawing.Size(162, 27);
            this.DatabaseTextBox.TabIndex = 12;
            this.DatabaseTextBox.Text = "postgres";
            // 
            // ButtonInit
            // 
            this.ButtonInit.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ButtonInit.Location = new System.Drawing.Point(8, 356);
            this.ButtonInit.Name = "ButtonInit";
            this.ButtonInit.Size = new System.Drawing.Size(94, 29);
            this.ButtonInit.TabIndex = 15;
            this.ButtonInit.Text = "InitDB";
            this.ButtonInit.UseVisualStyleBackColor = true;
            this.ButtonInit.Click += new System.EventHandler(this.ButtonInit_Click);
            // 
            // ButtonGetTable
            // 
            this.ButtonGetTable.Enabled = false;
            this.ButtonGetTable.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ButtonGetTable.Location = new System.Drawing.Point(873, 528);
            this.ButtonGetTable.Name = "ButtonGetTable";
            this.ButtonGetTable.Size = new System.Drawing.Size(101, 29);
            this.ButtonGetTable.TabIndex = 14;
            this.ButtonGetTable.Text = "GetTable";
            this.ButtonGetTable.UseVisualStyleBackColor = true;
            this.ButtonGetTable.Click += new System.EventHandler(this.ButtonGetTable_Click);
            // 
            // ConnectionInfoLabel
            // 
            this.ConnectionInfoLabel.AutoSize = true;
            this.ConnectionInfoLabel.ForeColor = System.Drawing.Color.Red;
            this.ConnectionInfoLabel.Location = new System.Drawing.Point(8, 40);
            this.ConnectionInfoLabel.Name = "ConnectionInfoLabel";
            this.ConnectionInfoLabel.Size = new System.Drawing.Size(99, 20);
            this.ConnectionInfoLabel.TabIndex = 16;
            this.ConnectionInfoLabel.Text = "Disconnected";
            // 
            // button_Execute
            // 
            this.button_Execute.Enabled = false;
            this.button_Execute.Location = new System.Drawing.Point(12, 534);
            this.button_Execute.Name = "button_Execute";
            this.button_Execute.Size = new System.Drawing.Size(68, 29);
            this.button_Execute.TabIndex = 17;
            this.button_Execute.Text = "Execute";
            this.button_Execute.UseVisualStyleBackColor = true;
            this.button_Execute.Click += new System.EventHandler(this.Button_Execute_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(192, 9);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 29;
            this.dataGrid.Size = new System.Drawing.Size(889, 466);
            this.dataGrid.TabIndex = 19;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
            this.dataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellEndEdit);
            // 
            // labelSelectedFunction
            // 
            this.labelSelectedFunction.AutoSize = true;
            this.labelSelectedFunction.ForeColor = System.Drawing.Color.Black;
            this.labelSelectedFunction.Location = new System.Drawing.Point(12, 401);
            this.labelSelectedFunction.Name = "labelSelectedFunction";
            this.labelSelectedFunction.Size = new System.Drawing.Size(72, 20);
            this.labelSelectedFunction.TabIndex = 21;
            this.labelSelectedFunction.Text = "Функция:";
            this.labelSelectedFunction.Click += new System.EventHandler(this.labelSelectedFunction_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.ForeColor = System.Drawing.Color.RoyalBlue;
            this.buttonSave.Location = new System.Drawing.Point(873, 493);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(101, 29);
            this.buttonSave.TabIndex = 23;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(1101, 9);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(201, 554);
            this.treeView.TabIndex = 24;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewItemSelected);
            this.treeView.DoubleClick += new System.EventHandler(this.TreeViewDoubleClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 424);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 28);
            this.comboBox1.TabIndex = 25;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ParamTextBox
            // 
            this.ParamTextBox.Location = new System.Drawing.Point(12, 483);
            this.ParamTextBox.Name = "ParamTextBox";
            this.ParamTextBox.Size = new System.Drawing.Size(162, 27);
            this.ParamTextBox.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 460);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Значение:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 569);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ParamTextBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelSelectedFunction);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.button_Execute);
            this.Controls.Add(this.ConnectionInfoLabel);
            this.Controls.Add(this.ButtonInit);
            this.Controls.Add(this.ButtonGetTable);
            this.Controls.Add(this.LabelDatabase);
            this.Controls.Add(this.DatabaseTextBox);
            this.Controls.Add(this.LabelPassword);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LabelUser);
            this.Controls.Add(this.UserTextBox);
            this.Controls.Add(this.LabelPort);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.LabelServer);
            this.Controls.Add(this.ServerTextBox);
            this.Controls.Add(this.InitInfoLabel);
            this.Controls.Add(this.ButtonDisconnect);
            this.Controls.Add(this.ButtonConnect);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Form1";
            this.Text = "Inform Sys";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonConnect;
        private System.Windows.Forms.Button ButtonDisconnect;
        private System.Windows.Forms.Label InitInfoLabel;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.Label LabelServer;
        private System.Windows.Forms.Label LabelPort;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label LabelUser;
        private System.Windows.Forms.TextBox UserTextBox;
        private System.Windows.Forms.Label LabelDatabase;
        private System.Windows.Forms.TextBox DatabaseTextBox;
        private System.Windows.Forms.Button ButtonInit;
        private System.Windows.Forms.Button ButtonGetTable;
        private System.Windows.Forms.Label ConnectionInfoLabel;
        private System.Windows.Forms.Button button_Execute;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label labelSelectedFunction;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox ParamTextBox;
        private System.Windows.Forms.Label label1;
    }
}

