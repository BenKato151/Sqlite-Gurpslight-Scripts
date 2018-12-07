using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;

namespace SqliteAttributeController
{
    public class SqliteAttributeController : MonoBehaviour {
    
        #region ConnectionVars
        static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\UnityScripts\SqliteScripts\Assets\Scripts\Database\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly int[] staerke = new int[] { 2, 6, 8 };
        static private readonly int[] geschicklichkeit = new int[] { 1, 2, 3 };
        static private readonly int[] konstitution = new int[] { 7, 13, 16 };
        static private readonly int[] intelligenz = new int[] { 2, 6, 8 };

        //Database Query
        static private readonly string[] table_attribute = new string[] { "Attribute" };
        #endregion

        #region Insert
        public void InsertingValues()
        {
            try
            {
                string insertIntoWaffen = " INSERT INTO Attribute(Staerke, Geschicklichkeit, Intelligenz, Konstitution) " +
                                          " VALUES(@starke, @geschicklichkeit, @intelligenz, @konstitution);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@starke", System.Data.DbType.Int32).Value = staerke[0];
                Command.Parameters.Add("@geschicklichkeit", System.Data.DbType.Int32).Value = geschicklichkeit[0];
                Command.Parameters.Add("@intelligenz", System.Data.DbType.Int32).Value = intelligenz[0];
                Command.Parameters.Add("@konstitution", System.Data.DbType.Int32).Value = konstitution[0];

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Inserted values successfuly!");
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
                string selecting = "SELECT * FROM Attribute;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Debug.Log("Stärke: " + output["Staerke"]);
                    Debug.Log("Geschicklichkeit: " + output["Geschicklichkeit"]);
                    Debug.Log("Intelligenz: " + output["Intelligenz"]);
                    Debug.Log("Konstitution: " + output["Konstitution"]);
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
                string deleteColumn = "DELETE FROM Attribute " +
                                          " WHERE Intelligenz = 2;";

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
                        Debug.Log("Table: " + table_attribute[0]);
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