using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        int startpoint = 0;
private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            guna2WinProgressIndicator1.Start();
            if (startpoint > 40)
            {
                LoginForm loginForm = new LoginForm();
                guna2WinProgressIndicator1.Stop();
                timer1.Stop();
                this.Hide();
                loginForm.Show();
            }
        }
    }
}
