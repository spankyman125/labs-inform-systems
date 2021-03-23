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
            ButtonGetTable.Enabled = true;
            buttonView.Enabled = true;
            button_Execute.Enabled = true;

            ConnectionInfoLabel.Text = "Connected";
            ConnectionInfoLabel.ForeColor = Color.Green;
            initListBox();
        }

        private void initListBox()
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
            button_Execute.Enabled = true;

            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();
            dataGrid.Refresh();

            ConnectionInfoLabel.Text = "Disconnected";
            ConnectionInfoLabel.ForeColor = Color.Red;
            listBoxFunction.Items.Clear();
        }

        private void ButtonInit_Click(object sender, EventArgs e)
        {
            db_connection_string =
                "Server="   + ServerTextBox.Text    + ";" +
                "Port="     + PortTextBox.Text      + ";" +
                "User Id="  + UserTextBox.Text      + ";" +
                "Password=" + PasswordTextBox.Text  + ";" +
                "Database=" + DatabaseTextBox.Text  + ";";

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
            }
            else
            {
                InitInfoLabel.Text = "Error, no init";
            }
            db_connection.Close();
        }

        private void ButtonGetTable_Click(object sender, EventArgs e)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            dataGrid.Refresh();
            string table_name = "items";
            NpgsqlCommand db_command = new NpgsqlCommand("select *from " + table_name, db_connection);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(db_command);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset,table_name);
            dataGrid.DataSource = dataset.Tables[table_name];
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DatabaseTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LabelDatabase_Click(object sender, EventArgs e)
        {

        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_str = listBoxFunction.GetItemText(listBoxFunction.SelectedItem);
            labelSelectedFunction.Text = "Function:" + selected_str;
        }

        private void button_Execute_Click(object sender, EventArgs e)
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

        private void buttonViewClick(object sender, EventArgs e)
        {
            string funcName = listBoxFunction.GetItemText(listBoxFunction.SelectedItem);
            Form dialog = new Form() { Height = 500, Width = 600 };
            RichTextBox functionCode = new RichTextBox() { Height = 500, Width = 600, ReadOnly = true };
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

    }
}
