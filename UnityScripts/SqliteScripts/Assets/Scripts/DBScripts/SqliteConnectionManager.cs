using System;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class SqliteConnectionManager : MonoBehaviour {

    public static SqliteConnection dbConnection;
    public static readonly string dbpath = Application.dataPath + @"/Scripts/Database/new_Char_Bogen1.sqlite";
   
    #region Connection
    public static void Connection(Text console_msg, string table)
    {

        try
        {
            dbConnection = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");

            if (File.Exists(dbpath))
            {
                if (dbConnection != null)
                {
                    dbConnection.Open();
                    console_msg.text = "Connected to the database!\n Table: " + table;
                }
            }
        }

        catch (Exception e)
        {
            Debug.Log(e);
            console_msg.text = "Error:\nFailed to connect!";
        }
    }
    #endregion

    #region Exit
    public static void Exit(Text console_msg, Text sqlOutput_msg)
    {
        try
        {
            if (dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnection.Close();
                console_msg.text = "\nConnection closed!";
                sqlOutput_msg.text = " ";

            }
            else
            {
                console_msg.text = "No connection to close";
            }
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Object reference not set to an instance of an object"))
            {
                console_msg.text = "No connection to close";
            }
            else
            {
                console_msg.text = "Error:\nFailed to close the connection!";
                Debug.LogError(e);
            }
        }
    }
    #endregion
}
