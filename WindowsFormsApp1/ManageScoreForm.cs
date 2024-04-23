using MySql.Data.MySqlClient;
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
    public partial class ManageScoreForm : Form
    {
        courseClass course =new courseClass();
        ScoreClass score =new ScoreClass();
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            //populae combobox with courses one
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
            //show score data on datgridview
            showScore();
        }
        private void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StdNumber, student.StdFirstName, student.StdLastName, score.CourseName,score.Score,score.Description FROM student INNER JOIN score on score.StdNumber=student.StdNumber"));
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (textBox_stdNumber.Text == "" || textBox_score.Text == "" || textBox_description.Text.Equals(""))
            {
                MessageBox.Show("Course Data missing", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string stdNumber = textBox_stdNumber.Text;
                string cName = comboBox_course.Text;
                int scor = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_description.Text;

                if (score.updateScore(stdNumber, cName, scor, desc))
                {
                    showScore();
                    button_clear.PerformClick();
                    MessageBox.Show("Score updated Successfully", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Error score not updated", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_stdNumber.Text =="")
            {
                MessageBox.Show("Student Number is missing", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                  string stdNumber = textBox_stdNumber.Text;
                if(MessageBox.Show("Are you sure to delete this score","Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(stdNumber))
                    {
                        showScore() ;
                        MessageBox.Show("Score removed successfully","Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick() ;
                    }
                }
            }
        }
        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdNumber.Clear();
            textBox_score.Clear();
            textBox_description.Clear();
            textBox_search.Clear();

        }

        private void DataGridView_score_Click(object sender, EventArgs e)
        {
            textBox_stdNumber.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
            comboBox_course.Text = DataGridView_score.CurrentRow.Cells[3].Value.ToString();
            textBox_score.Text = DataGridView_score.CurrentRow.Cells[4].Value.ToString();
            textBox_description.Text = DataGridView_score.CurrentRow.Cells[5].Value.ToString();


        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StdNumber, student.StdFirstName, student.StdLastName, score.CourseName,score.Score,score.Description FROM student INNER JOIN score on score.StdNumber=student.StdNumber WHERE CONCAT(student.StdFirstName, student.StdLastName,score.CourseName) LIKE '%"+textBox_search.Text+"%'"));
        }
    }
}
