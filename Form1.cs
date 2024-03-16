using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demka
{
    public partial class Form1 : Form
    {
        private string login = "";
        private string pass = "";
        private int count;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            pass = textBox2.Text;
            string getedPassword = "";

            if (login.Length == 0 | pass.Length == 0)
            {
                return;
            }

            Program.con.Open();
            NpgsqlCommand commandGetLogin = new NpgsqlCommand($"SELECT pass FROM reg WHERE login = '{login}'", Program.con);
            try
            {
                if(commandGetLogin.ExecuteScalar() == null)
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.con.Close();
                    return;
                }
                else
                {
                    getedPassword = commandGetLogin.ExecuteScalar().ToString();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
                        Program.con.Close();
                return;
            }
            Program.con.Close();

            if(getedPassword != pass)
            {
                count++;
                if(count == 3)
                {
                    this.Enabled = false;
                    await Task.Delay(20000);
                    this.Enabled = true;
                }
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }
            else
            {
                Form2 f2 = new Form2();
                    f2.Show();
                MessageBox.Show("Вход выполнен");
                    Hide();
            }

}
    }
}
