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
        string db_connection_string;//123

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
            
        ConnectionInfoLabel.Text = "Connected";
        ConnectionInfoLabel.ForeColor = Color.Green;
        }

        private void Disconnect_click(object sender, EventArgs e)
        {
            db_connection.Close();
            ButtonConnect.Enabled = true;
            ButtonDisconnect.Enabled = false;
            ButtonGetTable.Enabled = false;

            TextInfo.Clear();
            ConnectionInfoLabel.Text = "Disconnected";
            ConnectionInfoLabel.ForeColor = Color.Red;
        }

        private void ButtonInit_Click(object sender, EventArgs e)
        {
            db_connection_string =
            "Server=" + ServerTextBox.Text + ";" +
            "Port="+ PortTextBox.Text + ";" +
            "User Id=" + UserTextBox.Text + ";" +
            "Password=" + PasswordTextBox.Text +";" +
            "Database=" + DatabaseTextBox.Text + ";";

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
            NpgsqlDataReader reader;
            string table_name = "items";
            NpgsqlCommand db_command = new NpgsqlCommand("select *from " + table_name, db_connection);
            reader = db_command.ExecuteReader();
            TextInfo.Clear();
            while (reader.Read())
            {
                TextInfo.AppendText(
                    string.Format(
                    "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\r\n",
                    reader.GetInt32(0).ToString(),
                    reader.GetString(1).ToString(),
                    reader.GetInt32(2).ToString(),
                    reader.GetInt32(3).ToString(),
                    reader.GetString(4).ToString(),
                    reader.GetDate(5).ToString()
                    ));
            }
            reader.Close();
        }
    }
}
