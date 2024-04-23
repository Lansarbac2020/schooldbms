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
    public partial class ManageStudentForm : Form
    {
        StudentClass student =new StudentClass();
        public ManageStudentForm()
        {
            InitializeComponent();
        }


        private void ManageStudentForm_Load(object sender, EventArgs e)
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
        // display student data from student to  textbox 
        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_id.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
           textBox_stdNumber.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textBox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();
            textBox_Fname.Text = DataGridView_student.CurrentRow.Cells[3].Value.ToString();
           
            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[6].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Male")
                radioButton_male.Checked = true;
            textBox_phone.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            textBox_adress.Text = DataGridView_student.CurrentRow.Cells[7].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[8].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox_student.Image=Image.FromStream(ms);          



        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdNumber.Clear();
            textBox_id.Clear();
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();
            textBox_adress.Clear();
            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            // brozser photo from computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif) | *.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchStudent(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        //function to verify
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
        private void button_update_Click(object sender, EventArgs e)
        {
            //Update student
            string stdNumber=textBox_stdNumber.Text;
            int id = Convert.ToInt32(textBox_id.Text); 
            string fname = textBox_Fname.Text;
            string lname = textBox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textBox_phone.Text;
            string adresss = textBox_adress.Text;
            string gender = radioButton_male.Checked ? "Male" : "Female";



            // check student age between 10 and 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 25)
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
                    if (student.updateStudent(  stdNumber, id, fname, lname, bdate, gender, phone, adresss, img))
                    {
                        showTable();
                        MessageBox.Show("Student data Update", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            //remove the selected student
            int id = Convert.ToInt32(textBox_id.Text);
            //a confirmaion message
            if (MessageBox.Show("Are you sure you  want to remove this student", "Remove student", MessageBoxButtons.YesNo, MessageBoxIcon.Question )==DialogResult.Yes)
            {
                if(student.deleteStudent(id))
                {
                    showTable();
                    MessageBox.Show("Student Removed", "Remove Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_clear.PerformClick();
                }
            }
        }

        private void textBox_stdNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
