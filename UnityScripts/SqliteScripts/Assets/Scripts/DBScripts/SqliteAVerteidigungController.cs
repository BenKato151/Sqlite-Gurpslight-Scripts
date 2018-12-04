using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;

public class SqliteAVerteidigungController : MonoBehaviour
{

    #region SqlConnection Vars
    static SqliteConnection dbConnection;
    static readonly string databasepath = @"D:/Scripts/UnityScripts/SqliteScripts/Assets/Scripts/Database/new_Char_Bogen1.sqlite";
    #endregion

    #region SqlVars
    static private readonly int[] parieren = new int[] { 2, 6, 8 };
    static private readonly int[] ausweichen = new int[] { 1, 2, 3 };
    static private readonly int[] abblocken = new int[] { 7, 13, 16 };
    static private readonly int[] abblockenumh = new int[] { 23, 26, 12 };

    //Database Query
    static private readonly string[] table_aver = new string[] { "Aktive_Verteidigung" };

    #endregion

    #region Start
    // Use this for initialization
    void Start()
    {
        Description();
    }
    #endregion

    #region Main
    // Update is called once per frame
    void Update()
    {
        ConnectionDB();
        SelectingColumns();
        InsertingValues();
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
                string insertIntoWaffen = " INSERT INTO Aktive_Verteidigung(Parieren, Ausweichen, Abblocken, AblockenUmh) " +
                                          " VALUES(@parieren, @ausweichen, @abblocken, @abblockenUmh);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@parieren", System.Data.DbType.Int32).Value = parieren[0];
                Command.Parameters.Add("@ausweichen", System.Data.DbType.Int32).Value = ausweichen[0];
                Command.Parameters.Add("@abblocken", System.Data.DbType.Int32).Value = abblocken[0];
                Command.Parameters.Add("@abblockenUmh", System.Data.DbType.String).Value = abblockenumh[0];

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
                string selecting = "SELECT * FROM Aktive_Verteidigung;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Debug.Log("");
                    Debug.Log("Parieren: " + output["Parieren"]);
                    Debug.Log("Ausweichen: " + output["Ausweichen"]);
                    Debug.Log("Abblocken: " + output["Abblocken"]);
                    Debug.Log("Abblocken Unhang: " + output["AblockenUmh"]);
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
                string deleteColumn = "DELETE FROM Aktive_Verteidigung " +
                                      " WHERE Parieren = 2;";

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
                        Debug.Log("Table: " + table_aver[0]);
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
        Debug.Log("Press 'C' to connect with the database table " + table_aver[0] + ".");
        Debug.Log("Drücke 'S' zum Abfragen der Werte in " + table_aver[0] + ".");
        Debug.Log("Drücke 'I' zum Einfügen von Werten in " + table_aver[0] + ".");
        Debug.Log("Drücke 'D' zum Löschen von Werten in " + table_aver[0] + ".");
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
