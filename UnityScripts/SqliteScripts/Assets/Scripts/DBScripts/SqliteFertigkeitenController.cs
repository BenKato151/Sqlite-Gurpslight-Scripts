using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;

namespace SqliteFertigkeitencontroller
{
    public class SqliteFertigkeitenController : MonoBehaviour {

        #region SqlConnection Vars
        static SqliteConnection dbConnection;
        static readonly string databasepath = @"D:/Scripts/UnityScripts/SqliteScripts/Assets/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly int[] cp = new int[] { 2, 6, 8 };
        static private readonly int[] id = new int[] { 1, 2, 3 };
        static private readonly int[] fw = new int[] { 7, 13, 16 };
        static private readonly string[] art = new string[] { "mentale", "physisch", "physisch" };
        static private readonly string[] typ = new string[] { "Typ1", "Typ2", "Typ3" };
        private new static readonly string[] name = new string[] { "Widerstehen", "Angreifen", "Fangen" };

        //Database Query
        static private readonly string[] table_fertigkeiten = new string[] { "Fertigkeiten" };

        #endregion

        #region Start
        // Use this for initialization
        void Start () {
            Description();
	    }
        #endregion

        #region main
        // Update is called once per frame
        void Update () {
            ConnectionDB();
            SelectingColumns();
            InsertingValues();
            DeleteColumns();
            Exit();
            #region Description();
            if (Input.GetKeyDown(KeyCode.H))
            {
                Description();
            }
            #endregion
        }
        #endregion

        #region Insert
        private static void InsertingValues()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                try
                {
                    string insertIntoWaffen = " INSERT INTO Fertigkeiten(CP, ID, FW, Art, Typ, Name) " +
                                              " VALUES(@cp, @id, @fw, @art, @typ, @name);";

                    SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                    Command.Parameters.Add("@cp", System.Data.DbType.Int32).Value = cp[0];
                    Command.Parameters.Add("@id", System.Data.DbType.Int32).Value = id[0];
                    Command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = fw[0];
                    Command.Parameters.Add("@art", System.Data.DbType.String).Value = art[0];
                    Command.Parameters.Add("@typ", System.Data.DbType.String).Value = typ[0];
                    Command.Parameters.Add("@name", System.Data.DbType.String).Value = name[0];

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
        }
        #endregion

        #region Select
        private static void SelectingColumns()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Searching...");

                try
                {
                    string selecting = "SELECT * FROM Fertigkeiten;";
                    SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                    SqliteDataReader output = command.ExecuteReader();

                    while (output.Read())
                    {
                        Debug.Log("Fertigkeiten Name: " + output["Name"]);
                        Debug.Log("Fertigkeiten Typ: " + output["Typ"]);
                        Debug.Log("Fertigkeiten Art: " + output["Art"]);
                        Debug.Log("CP: " + output["CP"]);
                        Debug.Log("FW: " + output["FW"]);
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
        }
        #endregion

        #region DELETE
        private static void DeleteColumns()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                try
                {
                    string deleteColumn = "DELETE FROM Fertigkeiten " +
                                          " WHERE ID = 1;";

                    SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);
                    //Command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = //arrayname[0];

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
        }

        #endregion

        #region Connection
        private static void ConnectionDB()
        {
            if (Input.GetKeyDown(KeyCode.C))
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
                            Debug.Log("Table: " + table_fertigkeiten[0]);
                        }
                    }
                }

                catch (Exception e)
                {
                    Debug.Log("Not Connected!    Error:    ");
                    Debug.Log(e);
                }
            }
        }
        #endregion

        #region Description
        void Description()
        {
            Debug.Log("Press 'C' to connect with the database table " + table_fertigkeiten[0] + ".");
            Debug.Log("Drücke 'S' zum Abfragen der Werte in " + table_fertigkeiten[0] + ".");
            Debug.Log("Drücke 'I' zum Einfügen von Werten in " + table_fertigkeiten[0] + ".");
            Debug.Log("Drücke 'D' zum Löschen von Werten in " + table_fertigkeiten[0] + ".");
            Debug.Log("Drücke 'X' zum Verlassen der Datenbank.");
            Debug.Log("Drücke 'H' für diese Anzeige.");
        }
        #endregion

        #region Exit
        void Exit()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                dbConnection.Close();
                Debug.Log("Verbindung beendet");
            }
        }
        #endregion


    }
}
