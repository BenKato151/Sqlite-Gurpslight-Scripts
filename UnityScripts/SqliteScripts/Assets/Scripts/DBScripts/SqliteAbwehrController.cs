using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace SqliteAbwehrController
{
    public class SqliteAbwehrController : MonoBehaviour {
        
        #region ConnectionVars
        static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\UnityScripts\SqliteScripts\Assets\Scripts\Database\new_Char_Bogen1.sqlite";
        #endregion
        
        #region SqlVar        
        //Database Query
        static private readonly string table_abwehr = "Abwehr";
        #endregion

        #region InputVars
        public Text FieldSchild;
        public Text FieldRuestung;
        public Text FieldUmhang;
        #endregion

        #region Insert
        public void InsertingValues()
        {
            try
            {
                string insertIntoWaffen = " INSERT INTO Abwehr(Schild, Ruestung, Umhang, Gesamt) " +
                                              " VALUES(@schild, @ruestung, @umhang, @gesamt);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@schild", System.Data.DbType.Int32).Value = FieldSchild.text;
                Command.Parameters.Add("@ruestung", System.Data.DbType.Int32).Value = FieldRuestung.text;
                Command.Parameters.Add("@umhang", System.Data.DbType.Int32).Value = FieldUmhang.text;
                Command.Parameters.Add("@gesamt", System.Data.DbType.Int32).Value = Int32.Parse(FieldRuestung.text) + Int32.Parse (FieldSchild.text) + Int32.Parse(FieldUmhang.text);

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Inserted values " + table_abwehr + " successfuly!");
            }

            catch (Exception e)
            {
                Debug.Log("Error! ");
                Debug.Log(e);
            }
            
        }
        #endregion

        #region Select
        public void SelectingColumns()
        {
            Debug.Log("Searching...");
            try
            {
                string selecting = "SELECT * FROM Abwehr;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Debug.Log("Schild: " + output["Schild"]);
                    Debug.Log("Rüstung: " + output["Ruestung"]);
                    Debug.Log("Umhang: " + output["Umhang"]);
                    Debug.Log("Gesamt: " + output["Gesamt"]);
                }
                Debug.Log("Searching completed!");
            }

            catch (Exception e)
            {
                Debug.Log("Error!   ");
                Debug.Log(e);
            }
        }
        #endregion

        #region DELETE
        public void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Abwehr " +
                                      " WHERE Gesamt = 10;";

                SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Deleted Row/s successfully!");
            }

            catch (Exception e)
            {
                Debug.Log("Error! ");
                Debug.Log(e);
            }
        }

        #endregion

        #region Connection
        public void ConnectionDB()
        {
            //Tries to get a connection with the database and if there is an path-error, it will catch it
            try
            {
                dbConnection = new SqliteConnection("Data Source = " + databasepath + "; " + " Version = 3;");
                dbConnection.Open();

                if (File.Exists(databasepath))
                {
                    if (dbConnection != null)
                    {
                        Debug.Log("Connected to the database!");
                        Debug.Log("Table: " + table_abwehr);
                    }
                }
            }

            catch (Exception e)
            {
                Debug.Log("Not Connected!    Error:    ");
                Debug.Log(e);
            }
            
        }
        #endregion

        #region Exit
       public void Exit()
       {
            dbConnection.Close();
            Debug.Log("Verbindung beendet");
       }
        #endregion

    }
}