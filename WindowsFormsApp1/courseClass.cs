using MySql.Data.MySqlClient;
using Mysqlx.Expr;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
   internal class courseClass
    {
        DbConnectcs connect= new DbConnectcs();
        // insertion of course
        public bool insertCourse(string cName, int hr, string desc)
        {
           MySqlCommand command = new MySqlCommand("INSERT INTO `course`(`CourseName`, `CourseHour`, `Description`) VALUES (@cn,@ch,@desc)", connect.getConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("cn",MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("ch",MySqlDbType.Int32).Value = hr;
            command.Parameters.Add("desc",MySqlDbType.VarChar).Value= desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }

        //function to get course 
        public DataTable getCourse(MySqlCommand command)
        {
            command.Connection = connect.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //update course data
        public bool updateCourse(int id, string cName, int hr, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `course` SET `CourseName`=@cn,`CourseHour`=@ch,`Description`=@desc WHERE `CourseId`=@id", connect.getConnection);
            //@id,@cn,@ch,@desc
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("ch", MySqlDbType.Int32).Value = hr;
            command.Parameters.Add("desc", MySqlDbType.VarChar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //course delete function
        public bool deleteCourse(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `course` WHERE`CourseId` =@id", connect.getConnection);
            //id
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
    }
}
