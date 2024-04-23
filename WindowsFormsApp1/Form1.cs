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
    public partial class Form1 : Form
    {
        StudentClass student =new StudentClass();
        public Form1()
        {
            InitializeComponent();
            customizeDesign();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            studentCount();
        }

        //create a function to display student count 
        private void studentCount()
        {
            label_totalStd.Text = "Total Students :" + student.totalStudent();
            label_maleStd.Text = "Male :" + student.maleStudent();
            label_femaleStd.Text = "Female :" + "" + student.femaleStudent();
        }

        private void customizeDesign()
        {
            panel_stdsubmenu.Visible = false;
            panel_courseSubmenu.Visible = false;
            panel_scoreSubmenu.Visible = false;

        }

        private void hideSubMenu()
        {
            if (panel_stdsubmenu.Visible == true)
                panel_stdsubmenu.Visible = false;
            if (panel_courseSubmenu.Visible == true)
                panel_courseSubmenu.Visible = false;
            if (panel_scoreSubmenu.Visible == true)
                panel_scoreSubmenu.Visible = false;
        }
        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubMenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }

        private void button_std_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_stdsubmenu);
        }
        #region StdSubmenu
        private void button_registration_Click(object sender, EventArgs e)
        {
            openChildForm(new RegisterForm());
            hideSubMenu();
        }

        private void button_manageStudent_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageStudentForm());
            hideSubMenu();

        }

        private void button_status_Click(object sender, EventArgs e)
        {
            hideSubMenu();

        }

        private void button_Stdprint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintStudent());
            hideSubMenu();

        }
        #endregion StdSubmenu
        private void button_course_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_courseSubmenu);
        }
        #region CourseSubmenu
        private void button_newcourse_Click(object sender, EventArgs e)
        {
            openChildForm(new courseForm());
            hideSubMenu();

        }

        private void button_manageCourse_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageCourseForm());
            hideSubMenu();

        }

        private void button_coursePrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintCourseForm());
            hideSubMenu();

        }
        #endregion CourseSubmenu
        private void button_score_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_scoreSubmenu);
        }
        #region ScoreSubmenu
        private void button_newscore_Click(object sender, EventArgs e)
        {
            openChildForm(new ScoreForm());
            hideSubMenu();

        }

        private void button_manageScore_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageScoreForm());
            hideSubMenu();

        }

        private void button_scorePrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintScoreForm());
            hideSubMenu();

        }
        #endregion ScoreSubmenu
        //to show register form in mainform
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
                activeForm.Close();
            activeForm = childForm; 
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            if(activeForm != null)
                activeForm.Close();
            panel_main.Controls.Add(panel_cover);
            studentCount();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_exit_Click_1(object sender, EventArgs e)
        {
            LoginForm login =new LoginForm();
            this.Hide();
            login.Show();
        }

        private void comboBox_course_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_cmale.Text = "Male : " + student.exeCount("SELECT COUNT (*) FROM student INNER join score on score.StdNumber =student.StdNumber WHERE score.CourseName='" + comboBox_course.Text + "' AND student.Gender='Male'");
            label_cfemale.Text = "Female : " + student.exeCount("SELECT COUNT (*) FROM student INNER join score on score.StdNumber =student.StdNumber WHERE score.CourseName='" + comboBox_course.Text + "' AND student.Gender='Female'");
        }
    }
}
