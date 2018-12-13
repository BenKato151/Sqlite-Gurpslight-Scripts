using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace SqliteAVerteidigungcontroller
{
    public class SqliteAVerteidigungController : MonoBehaviour
    {
        #region SqlConnection Vars
            static SqliteConnection dbConnection;
            static readonly string databasepath = @"D:/Scripts/UnityScripts/SqliteScripts/Assets/Scripts/Database/new_Char_Bogen1.sqlite";
            #endregion

        #region SqlVars
        //Database Query
        static private readonly string table_aver = "Aktive_Verteidigung";

        #endregion

        #region InputVars
        public Text FieldParieren;
        public Text FieldAusweichen;
        public Text FieldAbblocken;
        public Text FieldAbblockenUmh;
        public Text FieldID;
        public Text FieldDelete;
        #endregion

        #region Insert
        public void InsertingValues()
        {
            try
            {
                string insertIntoWaffen = " INSERT INTO Aktive_Verteidigung(Parieren, Ausweichen, " +
                                          " Abblocken, AblockenUmh, ID) " +
                                          "VALUES(@parieren, @ausweichen, @abblocken, @abblockenUmh, @idwert)" +
                                          ";";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@parieren", System.Data.DbType.Int32).Value = FieldParieren.text;
                Command.Parameters.Add("@ausweichen", System.Data.DbType.Int32).Value = FieldAusweichen.text;
                Command.Parameters.Add("@abblocken", System.Data.DbType.Int32).Value = FieldAbblocken.text;
                Command.Parameters.Add("@abblockenUmh", System.Data.DbType.String).Value = FieldAbblockenUmh.text;
                Command.Parameters.Add("@idwert", System.Data.DbType.Int32).Value = FieldID.text;

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Inserted values into " + table_aver + " successfuly!");
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
                string selecting = "SELECT * FROM Aktive_Verteidigung;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Debug.Log("Parieren: " + output["Parieren"]);
                    Debug.Log("Ausweichen: " + output["Ausweichen"]);
                    Debug.Log("Abblocken: " + output["Abblocken"]);
                    Debug.Log("Abblocken Unhang: " + output["AblockenUmh"]);
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
                string deleteColumn = " DELETE FROM Aktive_Verteidigung " +
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
                        Debug.Log("Table: " + table_aver);
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