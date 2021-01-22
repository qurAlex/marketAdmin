using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace market_admin
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        DataHelper dbHlp = new DataHelper();
        Form1 f1 = (Form1)Application.OpenForms["Form1"];
        void refresher()
        {
            dbHlp.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `clients` INNER JOIN `smeta` ON `smeta`.`ID_client`=`clients`.`ID_Client` AND `smeta`.`Status`='--'", dbHlp.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
                listBox1.Items.Add(reader[5] + " " + reader[6] + " " + reader[3] + " " + reader[1] + " " + reader[4] + " " +  reader[8] + " " + reader[10]);
            dbHlp.closeConnection();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            refresher();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                dbHlp.openConnection();
                MySqlCommand command = new MySqlCommand("UPDATE `smeta` SET `Status`='+-' WHERE `ID_purshase`='" + listBox1.SelectedItem.ToString().Split()[0] + "'", dbHlp.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.ExecuteNonQuery();
                dbHlp.closeConnection();
                refresher();
                MessageBox.Show("Отправка подтверждена");
            }
            else MessageBox.Show("Выберите заказ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                dbHlp.openConnection();
                MySqlCommand command = new MySqlCommand("DELETE FROM `smeta` WHERE `ID_purshase`='"+listBox1.SelectedItem.ToString().Split()[0]+"'", dbHlp.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.ExecuteNonQuery();
                dbHlp.closeConnection();
                dbHlp.openConnection();
                command = new MySqlCommand("UPDATE `sklad` SET `Count`=`sklad`.`Count`+1 WHERE `ID`='"+listBox1.SelectedItem.ToString().Split()[1]+"'", dbHlp.GetConnection());
                adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.ExecuteNonQuery();
                dbHlp.closeConnection();
                refresher();
                MessageBox.Show("Заказ отменен");
            }
            else MessageBox.Show("Выберите заказ");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) refresher();
            else
            {
                dbHlp.openConnection();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `clients` INNER JOIN `smeta` ON `smeta`.`ID_client`=`clients`.`ID_Client` AND `smeta`.`Status`='--' AND `clients`.`FIO` LIKE '"+textBox1.Text+"%' ", dbHlp.GetConnection());
                MySqlDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();
                while (reader.Read())
                    listBox1.Items.Add(reader[5] + " " +reader[6] + " " + reader[3] + " " + reader[1] + " " + reader[4] + " " +  reader[8] + " " + reader[10]);
                dbHlp.closeConnection();
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Show();
        }
    }
}
