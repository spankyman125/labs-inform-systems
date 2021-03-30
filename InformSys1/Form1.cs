using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace InformSys1
{
    public partial class Form1 : Form
    {
        NpgsqlConnection db_connection;
        string db_connection_string;
        DataSet dataset = new DataSet();
        HashSet<DataGridViewRow> rows_changed= new HashSet<DataGridViewRow>();
        NpgsqlConnectionStringBuilder builder;
 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            db_connection.Open();
            ButtonConnect.Enabled = false;
            ButtonDisconnect.Enabled = true;
            //ButtonGetTable.Enabled = true;
            buttonView.Enabled = true;
            button_Execute.Enabled = true;

            ConnectionInfoLabel.Text = "Connected";
            ConnectionInfoLabel.ForeColor = Color.Green;

            InitTreeView();
            InitListBox();
        }

        private void InitTreeView()
        {
            NpgsqlCommand db_command = new NpgsqlCommand("SELECT datname From pg_database WHERE datistemplate=false;", db_connection);
            NpgsqlDataReader reader = db_command.ExecuteReader();
            List<string> databases = new List<string>();
            while (reader.Read())
            {
                databases.Add(reader.GetString(0));
            }
            reader.Close();
            for (int i = 0; i < databases.Count(); i++)
            {
                db_connection.Close();
                builder.Database = databases[i];
                db_connection_string = builder.ConnectionString;
                db_connection = new(db_connection_string);
                db_connection.Open();
                db_command = new NpgsqlCommand("SELECT table_name FROM information_schema.tables  WHERE table_schema='public';", db_connection);
                reader = db_command.ExecuteReader();
                List<string> datatables = new List<string>();
                treeView.Nodes.Add(new TreeNode(databases[i]));
                while (reader.Read())
                {
                    datatables.Add(reader.GetString(0));
                    treeView.Nodes[i].Nodes.Add(new TreeNode(datatables.Last()));
                }
                reader.Close();

            }
        }

        private void InitListBox()
        {
            listBoxFunction.Items.Add("add");
            listBoxFunction.Items.Add("delete");
        }

        private void Disconnect_click(object sender, EventArgs e)
        {
            db_connection.Close();
            ButtonConnect.Enabled = true;
            ButtonDisconnect.Enabled = false;
            ButtonGetTable.Enabled = false;
            buttonView.Enabled = false;
            buttonSave.Enabled = false;
            button_Execute.Enabled = true;
            DataGridClear();


            ConnectionInfoLabel.Text = "Disconnected";
            ConnectionInfoLabel.ForeColor = Color.Red;
            listBoxFunction.Items.Clear();
            treeView.Nodes.Clear();
        }

        private void DataGridClear()
        {
            DataTable DT = (DataTable)dataGrid.DataSource;
            if (DT != null)
            {
                DT.Rows.Clear();
                DT.Columns.Clear();
            }
            dataGrid.Refresh();
        }

        private void ButtonInit_Click(object sender, EventArgs e)
        {
            builder = new NpgsqlConnectionStringBuilder()
            {
                Host = ServerTextBox.Text,
                Port = Int32.Parse(PortTextBox.Text),
                Username = UserTextBox.Text,
                Password = PasswordTextBox.Text,
                Database = DatabaseTextBox.Text
            };
            db_connection_string = builder.ConnectionString;
            try
            {
                db_connection = new(db_connection_string);
                db_connection.Open();
            }
            catch
            {
                InitInfoLabel.Text = "Error, no init";
                return;
            }
            if (db_connection.State == ConnectionState.Open)
            {
                InitInfoLabel.Text = "Connected to DB";
                InitInfoLabel.ForeColor = Color.Green;
                ButtonInit.Enabled = false;
                ButtonConnect.Enabled = true;
                ButtonDisconnect.Enabled = false;

                ServerTextBox.Enabled = false;
                PortTextBox.Enabled = false;
                UserTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;
                DatabaseTextBox.Enabled = false;

            }
            else
            {
                InitInfoLabel.Text = "Error, no init";
            }
            db_connection.Close();
        }

        private void ButtonGetTable_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void getTable()
        {
            if (treeView.SelectedNode != null) 
            {
                rows_changed.Clear();
                DataGridClear();
                builder.Database = treeView.SelectedNode.Parent.Text;
                db_connection_string = builder.ConnectionString;

                db_connection.Close();
                db_connection = new(db_connection_string);
                db_connection.Open();
                string table_name = treeView.SelectedNode.Text;
                NpgsqlCommand db_command = new("select *from " + table_name, db_connection);
                NpgsqlDataAdapter dataAdapter = new(db_command);
                dataAdapter.Fill(dataset, table_name);
                dataGrid.DataSource = dataset.Tables[table_name];
            }

        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_str = listBoxFunction.GetItemText(listBoxFunction.SelectedItem);
            labelSelectedFunction.Text = "Function:" + selected_str;
        }

        private void Button_Execute_Click(object sender, EventArgs e)
        {
           string funcName = listBoxFunction.GetItemText(listBoxFunction.SelectedItem);
            Form dialog = new Form();
            
            switch (funcName)
            {
                case "add":
                    TextBox[] textBoxes = new TextBox[6];
                    Label[] labels = new Label[6];
                    string[] arg_names = { "id", "type", "amount", "price", "publisher", "date" };
                    for (int i = 0; i < 6; i++)
                    {
                        labels[i] = new Label() { Left = 0, Top = 35*i, Text = arg_names[i]};
                        textBoxes[i] = new TextBox() { Left = 150, Top = 35*i };
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        dialog.Controls.Add(labels[i]);
                        dialog.Controls.Add(textBoxes[i]);
                    }
                    Button add_button = new Button() { Left = 150, Top = 6*35, Text = "Add Row" };

                    dialog.Controls.Add(add_button);

                    add_button.Click += (s, e) => {
                        using (var add_command = new NpgsqlCommand("select add(@0,@1,@2,@3,@4,@5)", db_connection))
                        {
                            add_command.Parameters.AddWithValue("0",Int32.Parse(textBoxes[0].Text));
                            add_command.Parameters.AddWithValue("1", textBoxes[1].Text);
                            add_command.Parameters.AddWithValue("2", Int32.Parse(textBoxes[2].Text));
                            add_command.Parameters.AddWithValue("3", Int32.Parse(textBoxes[3].Text));
                            add_command.Parameters.AddWithValue("4", textBoxes[4].Text);
                            NpgsqlTypes.NpgsqlDate datesql = new NpgsqlTypes.NpgsqlDate(DateTime.Parse(textBoxes[5].Text));
                            add_command.Parameters.AddWithValue("5", datesql);
                            add_command.ExecuteNonQuery();
                        }
                    };
                    dialog.ShowDialog();
                    break;

                case "delete":
                    Label label_id = new Label() { Left = 0, Top = 35, Text = "id"};
                    TextBox textBox_id = new TextBox() { Left = 100, Top = 35 };
                    Button ok_button = new Button() { Left = 0, Top = 90, Text="delete", Height=35 };
                    ok_button.Click += (s, e) =>
                    {
                        using (var delete_command = new NpgsqlCommand("select delete(@0)", db_connection))
                        {

                            delete_command.Parameters.AddWithValue("0", Int32.Parse(textBox_id.Text));
                            delete_command.ExecuteNonQuery();
                        }
                    };
                    dialog.Controls.Add(label_id);
                    dialog.Controls.Add(textBox_id);
                    dialog.Controls.Add(ok_button);
                    dialog.ShowDialog();
                    break;
            }
        }

        private void ButtonViewClick(object sender, EventArgs e)
        {
            string funcName = listBoxFunction.GetItemText(listBoxFunction.SelectedItem);
            Form dialog = new() { Height = 500, Width = 600 };
            RichTextBox functionCode = new() { Height = 500, Width = 600, ReadOnly = true };
            using (var codeShow_command = new NpgsqlCommand("select prosrc from pg_proc where proname=@0;", db_connection))
            {
                codeShow_command.Parameters.AddWithValue("0", funcName);
                NpgsqlDataReader code_reader;
                code_reader = codeShow_command.ExecuteReader();
                while (code_reader.Read())
                {
                    functionCode.Text += code_reader.GetString(0);
                }
                code_reader.Close();
                codeShow_command.ExecuteNonQuery();
            }
            dialog.Controls.Add(functionCode);
            dialog.ShowDialog();
            
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Form updateInfo = new Form() { Text="Confirm changes",Height = 400, Width = 920};
            DataTable table = ((DataTable)dataGrid.DataSource).Clone();
            Button confirmButton = new Button() { Text="Confirm", Left = 10,Top = 315, Height = 30 };
            Button cancelButton = new Button() { Text = "Cancel" , Left = 90, Top = 315, Height = 30 };

            foreach (DataGridViewRow row in rows_changed)
            {
                DataRow newRow = table.NewRow();
                DataRow oldRow = ((DataRowView)row.DataBoundItem).Row;
                newRow.ItemArray = oldRow.ItemArray;
                table.Rows.Add(newRow);
            }
            DataGridView changedRowsList = new() { Height = 300, Width = 900, BackgroundColor=Color.White, 
                                                                AutoSizeColumnsMode= DataGridViewAutoSizeColumnsMode.Fill };
            changedRowsList.DataSource = table;
            changedRowsList.ReadOnly = true;
            confirmButton.Click += (s, e) =>
            {
                string table_name = treeView.SelectedNode.Text;
                NpgsqlCommand db_command = new NpgsqlCommand("select *from " + table_name, db_connection);
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(db_command);
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(dataAdapter);
                try
                {
                    dataAdapter.Update(dataset, table_name);
                    updateInfo.Close();
                    getTable();
                }
                catch
                {
                    MessageBox.Show("Wrong data","Error");
                }
                
            };
            cancelButton.Click += (s, e) =>
              {
                  updateInfo.Close();
              };
            updateInfo.Controls.Add(changedRowsList);
            updateInfo.Controls.Add(confirmButton);
            updateInfo.Controls.Add(cancelButton);

            updateInfo.ShowDialog();


        }

        private void DataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            buttonSave.Enabled = true;
            dataGrid.Rows[rowIndex].Cells[colIndex].Style.BackColor =Color.LightSkyBlue;
            rows_changed.Add(dataGrid.Rows[rowIndex]);
        }

        private void TreeViewItemSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                treeView.SelectedNode = null;
            }
            else
                ButtonGetTable.Enabled = true;
        }

        private void TreeViewDoubleClick(object sender, EventArgs e)
        {
            getTable();
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
