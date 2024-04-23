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
    public partial class ScoreForm : Form
    {
        courseClass course = new courseClass();
        StudentClass student = new StudentClass();
        ScoreClass score = new ScoreClass();
        public ScoreForm()
        {
           
            InitializeComponent();
        }

        //function for showing data on gridview
        private void showScore ()
        {
            DataGridView_student.DataSource = score.getList(new MySqlCommand("SELECT score.StdNumber, student.StdFirstName, student.StdLastName, score.CourseName,score.Score,score.Description FROM student INNER JOIN score on score.StdNumber=student.StdNumber"));
        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            //populae combobox with courses one
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
            
            //student list Datagrid view
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT  `StdNumber`,`StdFirstName`,`StdLastName` FROM `student`"));

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox_stdNumber.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Score Data missing", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string stdNumber=textBox_stdNumber.Text;
               
                string cName =comboBox_course.Text;
                double scor = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_description.Text;
                if (!score.checkScore(stdNumber, cName))
                {


                    if (score.insertScore(stdNumber, cName, scor, desc))
                    {
                        showScore();
                        button_clear.PerformClick();
                        MessageBox.Show("New score inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Score not inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("THe score for this course is already exists", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
           textBox_stdNumber.Clear();
            textBox_score.Clear();
            comboBox_course.SelectedIndex = 0;
            textBox_description.Clear();
        }

        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_stdNumber.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            
        }

        private void textBox_stdId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_sStudent_Click(object sender, EventArgs e)
        {
            //student list Datagrid view
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT  `StdNumber`,`StdFirstName`,`StdLastName` FROM `student`"));
        }

        private void button_sScore_Click(object sender, EventArgs e)
        {
            showScore();
        }

        private void comboBox_course_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
