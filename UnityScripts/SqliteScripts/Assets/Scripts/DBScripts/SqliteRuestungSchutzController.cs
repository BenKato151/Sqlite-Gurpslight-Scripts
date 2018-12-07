﻿using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.IO;


namespace SqliteRuestungSchutzController
{
    public class SqliteRuestungSchutzController : MonoBehaviour {

        #region ConnectionVars
        public static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\UnityScripts\SqliteScripts\Assets\Scripts\Database\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly int[] sr = new int[] { 2, 6, 8 };
        static private readonly int[] id = new int[] { 1, 2, 3 };
        static private readonly int[] pv = new int[] { 7, 13, 16 };
        static private readonly string[] ort = new string[] { "Ebene 1", "Ebene 3", "Ebene 4" };
        static private readonly string[] table_RS = new string[] { "Ruestung_Schutz" };
        #endregion

        #region Insert
        public void InsertingValues()
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
        #endregion

        #region Select
        public void SelectingColumns()
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
        #endregion

        #region DELETE
        public void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Ruestung_Schutz " +
                                      " WHERE ID = 1";

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
        #endregion

        #region Exit
        public void Exit()
        {
            dbConnection.Close();
            Debug.Log("Connection closed!");
        }
        #endregion

    }
}