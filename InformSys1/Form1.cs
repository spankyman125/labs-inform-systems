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
            NpgsqlCommand db_command = new NpgsqlCommand("SELECT table_name FROM information_schema.tables  WHERE table_schema='public';", db_connection);
            NpgsqlDataReader reader = db_command.ExecuteReader();
            while (reader.Read())
                treeView.Nodes.Add(reader.GetString(0));
            reader.Close();
        }

        private void InitListBox()
        {
            comboBox1.Items.Add("Список преподавателей кафедры");
            comboBox1.Items.Add("Название кафедры преподавателя и сведения");
            comboBox1.Items.Add("Список дисциплин кафедры");
            comboBox1.Items.Add("Список кафедр");
        }

        private void Disconnect_click(object sender, EventArgs e)
        {
            db_connection.Close();
            ButtonConnect.Enabled = true;
            ButtonDisconnect.Enabled = false;
            ButtonGetTable.Enabled = false;
            buttonSave.Enabled = false;
            button_Execute.Enabled = true;
            DataGridClear();


            ConnectionInfoLabel.Text = "Disconnected";
            ConnectionInfoLabel.ForeColor = Color.Red;
            comboBox1.Items.Clear();
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
                //builder.Database = treeView.SelectedNode.Parent.Text;
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

        private void Button_Execute_Click(object sender, EventArgs e)
        {
           string funcName = comboBox1.GetItemText(comboBox1.SelectedItem);
           NpgsqlCommand command = null;

            switch (funcName)
            {
                case "Список преподавателей кафедры":
                    command = new NpgsqlCommand("select getEducators(@0)", db_connection);
                    command.Parameters.AddWithValue("0", ParamTextBox.Text);
                    break;

                case "Название кафедры преподавателя и сведения":
                    command = new NpgsqlCommand("select getDepartment(@0)", db_connection);
                    command.Parameters.AddWithValue("0", ParamTextBox.Text);
                    break;

                case "Список дисциплин кафедры":
                    command = new NpgsqlCommand("select getDisciplines(@0)", db_connection);
                    command.Parameters.AddWithValue("0", ParamTextBox.Text);
                    break;

                case "Список кафедр":
                    command = new NpgsqlCommand("select getDepartmentsAll()", db_connection);
                    break;


            }
            command.ExecuteNonQuery();
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
            ButtonGetTable.Enabled = true;
        }

        private void TreeViewDoubleClick(object sender, EventArgs e)
        {
            getTable();
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelSelectedFunction_Click(object sender, EventArgs e)
        {

        }
    }
}
