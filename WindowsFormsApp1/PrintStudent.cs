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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using DGVPrinterHelper;

namespace WindowsFormsApp1
{
    public partial class PrintStudent : Form
    {
        StudentClass student = new StudentClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintStudent()
        {
            InitializeComponent();
        }

        private void radioButton_all_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PrintStudent_Load(object sender, EventArgs e)
        {
            showData(new MySqlCommand("SELECT * FROM `student`"));
        }
        //funcion to show the std list in datagridview
        public void showData(MySqlCommand command)
        {
            DataGridView_student.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
          //  DataGridView_student.Height = 80;
            DataGridView_student.DataSource = student.getList(command);

            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            //image column
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //chek the radio button
            string selectQuery;
            if (radioButton_all.Checked)
            {
                selectQuery = "SELECT * FROM `student`";

            }
            else if(radioButton_male.Checked)
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`= 'Male'";
            }
            else
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`= 'Female'";
            }
            showData(new MySqlCommand(selectQuery));
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            // print by DGVPrinter 
            printer.Title = "International HighSchool Students List";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "lansarcenter";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_student);
        }
    }
}
