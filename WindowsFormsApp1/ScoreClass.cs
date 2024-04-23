using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
class ScoreClass
    {
        DbConnectcs connect = new DbConnectcs();
        // funcion for adding score
        public bool insertScore( string stdnum , string courName, double scor, string desc)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`StdNumber`, `CourseName`, `Score`, `Description`) VALUES (@snum,@cn,@sco,@desc)", connect.getConnection);
            //@snum,@cn,@sco,@desc
            command.Parameters.Add("snum", MySqlDbType.VarChar).Value = stdnum;
            command.Parameters.Add("cn", MySqlDbType.VarChar).Value = courName;
            command.Parameters.Add("sco", MySqlDbType.Double).Value = scor;
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
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //function to check already course score
        public bool checkScore(string stdNumber, string cName)
        {
            DataTable table = getList(new MySqlCommand("SELECT * FROM `score` WHERE `StdNumber`= '" + stdNumber + "' AND `CourseName`='"+ cName+"'"));
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateScore(string stdNumber, string cName, double scor, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `score` SET `Score`=@sco,`Description`=@desc WHERE `StdNumber`=@snum AND `CourseName`=@scn", connect.getConnection);
            //@id,@cn,@ch,@desc
            command.Parameters.Add("@snum", MySqlDbType.VarChar).Value = stdNumber;
            command.Parameters.Add("scn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("sco", MySqlDbType.VarChar).Value = scor;
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
        //function for delte 
        public bool deleteScore(string stdNumber)
        {
               MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `StdNumber`=@snum", connect.getConnection);
            command.Parameters.Add("@snum", MySqlDbType.VarChar).Value = stdNumber;
            connect.openConnect();
            if(command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect() ;
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
