using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;

namespace SqliteAbwehrController
{
    public class SqliteAbwehrController : MonoBehaviour {
        
        #region ConnectionVars
        static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        #endregion
        
        #region SqlVars
        static private readonly int[] schild = new int[] { 2, 6, 8 };
        static private readonly int[] ruestung = new int[] { 1, 2, 3 };
        static private readonly int[] umhang = new int[] { 7, 13, 16 };
        static private readonly int[] gesamt = new int[] { schild[0] + ruestung[0] + umhang[0], schild[1] + ruestung[1] + umhang[1], schild[2] + ruestung[2] + umhang[2] };
        //Database Query
        static private readonly string[] table_abwehr = new string[] { "Abwehr" };
        #endregion

        #region Start
        // Use this for initialization
        void Start () {
            Description();
	    }
        #endregion

        #region Main
        // Update is called once per frame
        void Update () {
            ConnectionDB();
            InsertingValues();
            SelectingColumns();
            DeleteColumns();
            #region Description();
            if (Input.GetKeyDown(KeyCode.H))
            {
                Description();
            }
            #endregion
            Exit();
        }
        #endregion

        #region Insert
        private static void InsertingValues()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                try
                {
                    string insertIntoWaffen = " INSERT INTO Abwehr(Schild, Ruestung, Umhang, Gesamt) " +
                                              " VALUES(@schild, @ruestung, @umhang, @gesamt);";

                    SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                    Command.Parameters.Add("@schild", System.Data.DbType.Int32).Value = schild[0];
                    Command.Parameters.Add("@ruestung", System.Data.DbType.Int32).Value = ruestung[0];
                    Command.Parameters.Add("@umhang", System.Data.DbType.Int32).Value = umhang[0];
                    Command.Parameters.Add("@gesamt", System.Data.DbType.String).Value = gesamt[0];

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
        }
        #endregion

        #region DELETE

        private static void DeleteColumns()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                try
                {
                    string deleteColumn = "DELETE FROM Abwehr " +
                                          " WHERE Gesamt = 10;";

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
                            Debug.Log("Table: " + table_abwehr[0]);
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
            Debug.Log("Press 'C' to connect with the database table " + table_abwehr[0] + ".");
            Debug.Log("Drücke 'S' zum Abfragen der Werte in " + table_abwehr[0] + ".");
            Debug.Log("Drücke 'I' zum Einfügen von Werten in " + table_abwehr[0] + ".");
            Debug.Log("Drücke 'D' zum Löschen von Werten in " + table_abwehr[0] + ".");
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