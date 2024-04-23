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
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class PrintScoreForm : Form
    {
        ScoreClass score = new ScoreClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintScoreForm()
        {
            InitializeComponent();
        }

      

        private void button_print_Click(object sender, EventArgs e)
        {
            // print by DGVPrinter 
            printer.Title = "International HighSchool Score List";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "lansarcenter";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_score);
        }
        private void PrintScoreForm_Load(object sender, EventArgs e)
        {
            showScore();
        }
        private void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StdNumber, student.StdFirstName, student.StdLastName, score.CourseName,score.Score,score.Description FROM student INNER JOIN score on score.StdNumber=student.StdNumber"));
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StdNumber, student.StdFirstName, student.StdLastName, score.CourseName,score.Score,score.Description FROM student INNER JOIN score on score.StdNumber=student.StdNumber WHERE CONCAT(student.StdFirstName, student.StdLastName,score.CourseName) LIKE '%" + textBox_search.Text + "%'"));
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
