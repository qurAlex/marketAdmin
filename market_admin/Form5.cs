using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace market_admin
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        DataHelper dbHlp = new DataHelper();
        Form1 f1 = (Form1)Application.OpenForms["Form1"];
        double cost()
        {
            double r = 0;
            dbHlp.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT  `Cost` FROM `product` WHERE `ID`='"+textBox1.Text+"'", dbHlp.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                r= Convert.ToDouble(reader[0]);
            dbHlp.closeConnection();
            return r;
        }
        string id_C()
        {
            string s = "";
            dbHlp.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT MAX(`ID_Client`)FROM `clients`", dbHlp.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                s = reader[0]+"";
            dbHlp.closeConnection();
            return s;
        }
        string id_p()
        {
            string s = "";
            dbHlp.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT MAX(`ID_purshase`) FROM `smeta`", dbHlp.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                s = reader[0] + "";
            dbHlp.closeConnection();
            return s;
        }
        void refresher()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            numericUpDown1.Value = 1;
        }
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 )
            {
                double co = cost();
                if (co > 0)
                {
                    int id = Convert.ToInt32(id_p());
                    string dou = co.ToString().Split(',')[0] + '.' + co.ToString().Split(',')[1];
                    string date = DateTime.Now.ToString().Split()[0].Split('.')[2] + '.' + DateTime.Now.ToString().Split()[0].Split('.')[1] + '.' + DateTime.Now.ToString().Split()[0].Split('.')[0];
                    dbHlp.openConnection();
                    MySqlCommand command = new MySqlCommand("INSERT INTO `smeta`(`ID_purshase`, `ID`, `Date`, `Cost`, `Count`, `Status`) VALUES" +
                        " ('"+(id+1)+"','" + textBox1.Text + "','" + date + "','" + dou +"','" + numericUpDown1.Value + "','++')", dbHlp.GetConnection());
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();
                    dbHlp.closeConnection();
                    dbHlp.openConnection();
                    command = new MySqlCommand("UPDATE `sklad` SET `Count`=`sklad`.`Count`-1 WHERE `ID`='" + textBox1.Text + "'", dbHlp.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();
                    dbHlp.closeConnection();
                    MessageBox.Show("Покупка добавлена");
                    refresher();
                }
                else MessageBox.Show("Введите верные данные");
            }
            else MessageBox.Show("Введите данные");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                double co = cost();
                if (co > 0)
                {
                    int s = Convert.ToInt32(id_C());
                    string pas = "" +Convert.ToChar(new Random().Next(97, 122)) + Convert.ToChar(new Random().Next(97, 122)) + Convert.ToChar(new Random().Next(97, 122))
                        + Convert.ToChar(new Random().Next(97, 122)) + Convert.ToChar(new Random().Next(48, 57));
                    string dou = co.ToString().Split(',')[0] + '.' + co.ToString().Split(',')[1];
                    string date = DateTime.Now.ToString().Split()[0].Split('.')[2] + '.' + DateTime.Now.ToString().Split()[0].Split('.')[1] + '.' + DateTime.Now.ToString().Split()[0].Split('.')[0];
                    dbHlp.openConnection();
                    MySqlCommand command = new MySqlCommand("INSERT INTO `clients`( `ID_Client`,`TNumber`, `Password`, `FIO`, `Adres`) VALUES " +
                        "('"+(s+1)+"','"+textBox4.Text+"','"+pas+"','"+textBox2.Text+"','"+textBox5.Text+"')", dbHlp.GetConnection());
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();
                    dbHlp.closeConnection();
                    int p = Convert.ToInt32(id_p());
                    dbHlp.openConnection();
                     command = new MySqlCommand("INSERT INTO `smeta`(`ID_purshase`,`ID_Client`, `ID`, `Date`, `Cost`, `Count`, `Status`) VALUES" +
                        " ('"+(p+1)+"','"+(s+1)+"','" + textBox1.Text + "','" + date + "','" + dou + "','" + numericUpDown1.Value + "','+-')", dbHlp.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();
                    dbHlp.closeConnection();
                    dbHlp.openConnection();
                    command = new MySqlCommand("UPDATE `sklad` SET `Count`=`sklad`.`Count`-1 WHERE `ID`='" + textBox1.Text + "'", dbHlp.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();
                    dbHlp.closeConnection();
                    MessageBox.Show("Покупка добавлена");
                    refresher();
                }
                else MessageBox.Show("Введите верные данные");
            }
            else MessageBox.Show("Введите данные");
        }
    }
}
