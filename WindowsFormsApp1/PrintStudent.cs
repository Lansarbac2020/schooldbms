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
            showData(new MySqlCommand("SELECT * FROM `Student_Score_View`"));
        }
        //funcion to show the std list in datagridview
        public void showData(MySqlCommand command)
        {
            DataGridView_student.ReadOnly = true;
           // DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            DataGridView_student.Height = 80;
          DataGridView_student.DataSource = student.getList(command);

          //  imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            //image column
           // imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //chek the radio button
            string selectQuery;
            if (radioButton_all.Checked)
            {
                selectQuery = "SELECT * FROM `Student_Score_View`";

            }
            else if(radioButton_male.Checked)
            {
                selectQuery = "SELECT * FROM `Student_Score_View` WHERE `Gender`= 'Male'";
            }
            else
            {
                selectQuery = "SELECT * FROM `Student_Score_View` WHERE `Gender`= 'Female'";
            }
            showData(new MySqlCommand(selectQuery));
        }

       

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
