using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace market_admin
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        DataHelper dbHlp = new DataHelper();
        Form1 f1 = (Form1)Application.OpenForms["Form1"];
        void refresher()
        {
            dbHlp.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `product`", dbHlp.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
                listBox1.Items.Add(reader[0] + " " + reader[1] + " " + reader[2]);
            dbHlp.closeConnection();
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            refresher();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (textBox2.Text.Length == 0) refresher();
            else
            {
                dbHlp.openConnection();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `product` WHERE `Name` LIKE '%" + textBox2.Text + "%' ", dbHlp.GetConnection());
                MySqlDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();
                while (reader.Read())
                    listBox1.Items.Add(reader[0] + " " + reader[1] + " " + reader[2]);
                dbHlp.closeConnection();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                dbHlp.openConnection();
                MySqlCommand command = new MySqlCommand("DELETE FROM `product` WHERE `ID`='" + listBox1.SelectedItem.ToString().Split()[0] + "'", dbHlp.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.ExecuteNonQuery();
                dbHlp.closeConnection();
                MessageBox.Show("Товар удален из каталога");
            }
            else MessageBox.Show("Выберите товар");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0&&textBox1.Text.Length>0)try
            {
                dbHlp.openConnection();
                MySqlCommand command = new MySqlCommand("UPDATE `product` SET `Cost`='"+textBox1.Text+"' WHERE `ID`='" + listBox1.SelectedItem.ToString().Split()[0] + "'", dbHlp.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.ExecuteNonQuery();
                dbHlp.closeConnection();
                    refresher();
                    MessageBox.Show("Цена изменена");
            }
                catch { MessageBox.Show("Введите цену через точку"); }
        }
    }
}
