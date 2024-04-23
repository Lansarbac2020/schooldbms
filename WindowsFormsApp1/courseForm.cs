using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class courseForm : Form
    {
        courseClass course = new courseClass();
        public courseForm()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {

            if (textBox_Cname.Text == "" || textBox_Chour.Text == "")
            {
                MessageBox.Show("Course Data missing", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string cName = textBox_Cname.Text;
                int chr = Convert.ToInt32(textBox_Chour.Text);
                string desc = textBox_description.Text;

                if (course.insertCourse(cName, chr, desc))
                {
                    showData();
                    button_clear.PerformClick();
                    MessageBox.Show("New course inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Course no inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_Cname.Clear();
            textBox_Chour.Clear();
            textBox_description.Clear();
        }

        private void courseForm_Load(object sender, EventArgs e)
        {
            showData();
        }
        private  void showData()
        {

            //show course list on datagridview
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
        }
    }
}
