using Org.BouncyCastle.Bcpg.OpenPgp;
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

namespace WindowsFormsApp1
{
    
    public partial class LoginForm : Form
    {
        StudentClass student = new StudentClass();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Red;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Transparent;
        }

        private void button_login_Click_1(object sender, EventArgs e)
        {
            if (textBox_username.Text =="" || textBox_password.Text == "")
            {
                MessageBox.Show("Need login data", "Wrong Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string usrname = textBox_username.Text;
                string pwd = textBox_password.Text;
                DataTable table = student.getList(new MySqlCommand("SELECT * FROM `user` WHERE `username`='"+usrname+"'AND `password`='"+pwd+"'"));
                if(table.Rows.Count > 0)
                {
                    Form1 main = new Form1();
                    this.Hide();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Your username or password are not exists", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

       
    }
}
