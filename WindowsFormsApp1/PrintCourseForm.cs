using DGVPrinterHelper;
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
using DGVPrinterHelper;
using System.Reflection;


namespace WindowsFormsApp1
{
    public partial class PrintCourseForm : Form
    {
        courseClass course = new courseClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintCourseForm()
        {
            InitializeComponent();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //search course and show on datagridview
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course` WHERE CONCAT(`CourseName`) LIKE '%" + textBox_search.Text + "%'"));
            textBox_search.Clear();
        }
        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            DataGridView_course.DataSource = course.getCourse(new MySqlCommand("select * from `course`"));
        }
        private void button_print_Click(object sender, EventArgs e)
        {
            // print by DGVPrinter 
            printer.Title = "International HighSchool Courses List";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "lansarcenter";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_course);
        }

        

    }
}
