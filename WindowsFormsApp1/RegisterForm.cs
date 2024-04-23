using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class RegisterForm : Form
    {
        StudentClass student = new StudentClass();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

       

        private void button_add_Click(object sender, EventArgs e)
        {
            //Add new student
            string stdNumber = textBox_stdNumber.Text;
            string fname = textBox_Fname.Text;
            string lname = textBox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textBox_phone.Text;
            string adresss = textBox_adress.Text;
            string gender = radioButton_male.Checked ? "Male" : "Female";



            // check student age between 10 and 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 30)
            {
                MessageBox.Show("The student age must be between 10 and 25", "Invalid BirthDate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    // to get photo from picturebox
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    if (student.insertStudent( stdNumber, fname, lname, bdate, gender, phone, adresss, img))
                    {
                        showTable();
                        MessageBox.Show("New student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //function for verify
        bool verify()
        {
            if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") ||
                 (textBox_phone.Text == "") || (textBox_adress.Text == "") ||
                 (pictureBox_student.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
     

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
        // to show student list in DataGrid view
        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentlist(new MySqlCommand("SELECT * FROM `student`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            // brozser photo from computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif) | *.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdNumber.Clear();
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();
            textBox_adress.Clear();
            radioButton_male.Checked = true;
            dateTimePicker1.Value=DateTime.Now;
            pictureBox_student.Image = null;
        }

   

        private void textBox_stdNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
