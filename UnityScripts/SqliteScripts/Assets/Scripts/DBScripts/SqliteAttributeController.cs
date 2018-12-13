using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace SqliteAttributeController
{
    public class SqliteAttributeController : MonoBehaviour {
    
        #region ConnectionVars
        static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\UnityScripts\SqliteScripts\Assets\Scripts\Database\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        //Database Query
        static private readonly string table_attribute = "Attribute";
        #endregion

        #region InputVars
        public Text FieldStaerke;
        public Text FieldGeschickl;
        public Text FieldIntelli;
        public Text FieldKonst;
        public Text FieldDelete;
        public Text FieldID;
        #endregion

        #region Insert
        public void InsertingValues()
        {
            try
            {
                string insertIntoWaffen = " INSERT INTO Attribute(Staerke, Geschicklichkeit, Intelligenz, Konstitution, ID) " +
                                          " VALUES(@starke, @geschicklichkeit, @intelligenz, @konstitution, @id);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@starke", System.Data.DbType.Int32).Value = FieldStaerke.text;
                Command.Parameters.Add("@geschicklichkeit", System.Data.DbType.Int32).Value = FieldGeschickl.text;
                Command.Parameters.Add("@intelligenz", System.Data.DbType.Int32).Value = FieldIntelli.text;
                Command.Parameters.Add("@konstitution", System.Data.DbType.Int32).Value = FieldKonst.text;
                Command.Parameters.Add("@id", System.Data.DbType.Int32).Value = FieldDelete.text;

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Inserted values into " + table_attribute +" successfuly!");
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
                    Debug.Log("ID: " + output["ID"]);
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
                string deleteColumn = " DELETE FROM Attribute " +
                                      " WHERE ID = @id;";

                SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);
                Command.Parameters.Add("@id", System.Data.DbType.Int32).Value = FieldDelete.text;

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
                        Debug.Log("Table: " + table_attribute);
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