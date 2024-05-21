using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace WindowsFormsApp1
{
    class StudentClass
    {
        DbConnectcs connect = new DbConnectcs();
        // create a function to add a new students o the database

        public bool insertStudent( string stdNumber , string fname, string lname, DateTime bdate, string gender, string phone, string adress, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`StdNumber`,`StdFirstName`, `StdLastName`, `Birthdate`, `Gender`, `Phone`, `Adress`, `Photo`) VALUES(@snum,@fn, @ln, @bd, @gd, @ph, @adr, @img)", connect.getConnection);

            // @fn, @ln, @bd, @gd, @ph, @adr, @img
            command.Parameters.Add("@snum", MySqlDbType.VarChar).Value = stdNumber;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = adress;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

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

        //  to get student table
        public DataTable getStudentlist(MySqlCommand command)
        {
            command.Connection=connect.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;


        }
        // create a function to execute the count query (total male , female)
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.getConnection);
            connect.openConnect();
            string count =command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }
        //get the total student
        public string totalStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student; ");
        }
        // to get male student
        public string maleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Gender`= 'Male'");
        }
        // to get female student
        public string femaleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE `Gender`= 'Female'");
        }

        // function for search student (fname, lname, adress)
        public DataTable searchStudent(string searchData)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE CONCAT(`StdFirstName`,`StdLastName`,`Adress`, `StdNumber`) LIKE '%" + searchData +"%'", connect.getConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;


        }
        //function edit for student
        public bool updateStudent(string stdNumber, int id, string fname, string lname, DateTime bdate, string gender, string phone, string adress, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET  `stdNumber` =@snum ,`StdFirstName`=@fn,`StdLastName`=@ln,`Birthdate`=@bd,`Gender`=@gd,`Phone`=@ph,`Adress`=@adr,`Photo`=@img WHERE `StdId` =@id", connect.getConnection);

            // @fn, @ln, @bd, @gd, @ph, @adr, @img @snum
            command.Parameters.Add("@snum", MySqlDbType.VarChar).Value = stdNumber;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = adress;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

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
          //function to delete data
        public bool deleteStudent(int studentId)
        {
            try
            {
                // Créer une commande SQL pour supprimer l'étudiant de la base de données
                MySqlCommand command = new MySqlCommand("DELETE FROM student WHERE StdId = @studentId", connect.getConnection);
                command.Parameters.AddWithValue("@studentId", studentId);

                // Ouvrir la connexion à la base de données
                connect.openConnect();

                // Exécuter la commande SQL
                int rowsAffected = command.ExecuteNonQuery();

                // Fermer la connexion à la base de données
                connect.closeConnect();

                // Vérifier si la suppression a réussi
                if (rowsAffected > 0)
                {
                    // Suppression réussie : retourner true
                    return true;
                }
                else
                {
                    // Aucun enregistrement n'a été supprimé : retourner false
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Gérer les exceptions ici (par exemple, journalisation, affichage d'un message d'erreur)
                Console.WriteLine("Erreur lors de la suppression de l'étudiant : " + ex.Message);

                // Retourner false pour indiquer que l'opération de suppression a échoué
                return false;
            }
        }

        // function for any command in studentDb
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
