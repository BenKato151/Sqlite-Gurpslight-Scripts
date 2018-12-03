using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.IO;


namespace SqliteRuestungSchutzController
{
    public class SqliteRuestungSchutzController : MonoBehaviour {

        #region ConnectionVars
        public static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly int[] sr = new int[] { 2, 6, 8 };
        static private readonly int[] id = new int[] { 1, 2, 3 };
        static private readonly int[] pv = new int[] { 7, 13, 16 };
        static private readonly string[] ort = new string[] { "Ebene 1", "Ebene 3", "Ebene 4" };
        static private readonly string[] table_RS = new string[] { "Ruestung_Schutz" };
        #endregion

        #region Start
        // Use this for initialization
        void Start () {
            Description();
	    }
        #endregion

        #region Main!
        // Update is called once per frame
        void Update () {
            ConnectionDB();
            SelectingColumns();
            InsertingValues();
            DeleteColumns();
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
                    string insertIntoWaffen = " INSERT INTO Ruestung_Schutz(Ort, SR, PV, ID) " +
                                              " VALUES(@Ort, @SR, @PV, @ID);";

                    SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                    Command.Parameters.Add("@Ort", System.Data.DbType.String).Value = ort[0];
                    Command.Parameters.Add("@SR", System.Data.DbType.Int32).Value = sr[0];
                    Command.Parameters.Add("@PV", System.Data.DbType.Int32).Value = pv[0];
                    Command.Parameters.Add("@ID", System.Data.DbType.Int32).Value = id[0];

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
                    string selecting = "SELECT * FROM Ruestung_Schutz;";
                    SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                    SqliteDataReader output = command.ExecuteReader();

                    while (output.Read())
                    {
                        Debug.Log("Ort: " + output["Ort"]);
                        Debug.Log("SR: " + output["SR"]);
                        Debug.Log("PV: " + output["PV"]);
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
                    string deleteColumn = "DELETE FROM Ruestung_Schutz " +
                                          " WHERE ID = 1";

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
                            Debug.Log("Table: " + table_RS[0]);
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
            Debug.Log("Press 'C' to connect with the database table " + table_RS[0] + ".");
            Debug.Log("Drücke 'S' zum Abfragen der Werte in " + table_RS[0] + ".");
            Debug.Log("Drücke 'I' zum Einfügen von Werten in " + table_RS[0] + ".");
            Debug.Log("Drücke 'D' zum Löschen von Werten in " + table_RS[0] + ".");
            Debug.Log("Drücke 'X' zum Verlassen der Datenbank.");
        }
        #endregion

        #region Exit
        void Exit()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                dbConnection.Close();
                Debug.Log("Connection closed!");
            }
        }
        #endregion
    }
}