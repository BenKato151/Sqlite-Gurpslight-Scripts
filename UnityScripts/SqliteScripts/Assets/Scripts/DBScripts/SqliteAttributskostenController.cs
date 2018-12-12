using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace AttributskostenController
{
    public class SqliteAttributskostenController : MonoBehaviour {

        #region ConnectionVars
        static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\UnityScripts\SqliteScripts\Assets\Scripts\Database\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        //Database Query
        static private readonly string table_attrik = "Attributskosten";
        #endregion

        #region InputVars
        public Text FieldKosten;
        public Text FieldWert;
        #endregion

        #region Insert
        public void InsertingValues()
        {
            try
            {
                string insertIntoWaffen = " INSERT INTO Attributskosten(Wert, Kosten) " +
                                          " VALUES(@wert, @kosten);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = FieldWert.text;
                Command.Parameters.Add("@kosten", System.Data.DbType.Int32).Value = FieldKosten.text;

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Inserted values into " + table_attrik + " successfuly!");
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
                string selecting = "SELECT * FROM Attributskosten;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Debug.Log("Wert: " + output["Wert"]);
                    Debug.Log("Kosten: " + output["Kosten"]);
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
                string deleteColumn = "DELETE FROM Attributskosten " +
                                      " WHERE Wert = 1";

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
                        Debug.Log("Table: " + table_attrik);
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