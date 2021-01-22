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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DataHelper dbHlp = new DataHelper();
        Form1 f1 = (Form1)Application.OpenForms["Form1"];

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Show();
        }
        void refresh()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            numericUpDown1.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && textBox6.Text.Length > 0 && textBox7.Text.Length > 0 && textBox8.Text.Length > 0) try
                {
                    string date = DateTime.Now.ToString().Split()[0].Split('.')[2] +'.'+ DateTime.Now.ToString().Split()[0].Split('.')[1] +'.'+ DateTime.Now.ToString().Split()[0].Split('.')[0];
                    dbHlp.openConnection();
                    MySqlCommand command = new MySqlCommand("INSERT INTO `product`(`ID`, `Name`, `Cost`, `Image`, `Description`, `Category`, `Specifications`) " +
                        "VALUES ('" + textBox1.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" 
                        + textBox1.Text + "/0.jpeg','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')", dbHlp.GetConnection()) ;
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();
                    dbHlp.closeConnection();
                    dbHlp.openConnection();
                    command = new MySqlCommand("INSERT INTO `sklad`(`ID`, `Count`, `Date`, `Cost`)  " +
                         "VALUES ('" + textBox1.Text + "','" + numericUpDown1.Value + "','" + date + "','" + textBox3.Text +"')", dbHlp.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.ExecuteNonQuery();
                    dbHlp.closeConnection();
                    refresh();
                    MessageBox.Show("товар добавлен");
                }
                catch { MessageBox.Show("Заполнните верными данными!"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                string date = DateTime.Now.ToString().Split()[0].Split('.')[2] + '.' + DateTime.Now.ToString().Split()[0].Split('.')[1] + '.' + DateTime.Now.ToString().Split()[0].Split('.')[0];
                dbHlp.openConnection();
                MySqlCommand command = new MySqlCommand("INSERT INTO `sklad`(`ID`, `Count`, `Date`, `Cost`)  " +
                     "VALUES ('" + textBox1.Text + "','" + numericUpDown1.Value + "','" + date + "','" + textBox3.Text + "')", dbHlp.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.ExecuteNonQuery();
                dbHlp.closeConnection();
                refresh();
            }
            else MessageBox.Show("Введите ID, количество и цену");
        }
    }
}
