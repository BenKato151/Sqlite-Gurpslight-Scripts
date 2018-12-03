using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.IO;

namespace MyNamespace
{
    public class SqliteWaffenController : MonoBehaviour
    {


        #region ConnectionVars
        public static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly string[] waffenname = new string[] { "Waffe1", "Waffe2", "Waffe3" };
        static private readonly string[] ort = new string[] { "Ebene 1", "Ebene 2", "Ebene 3" };
        static private readonly int[] waffenID = new int[] { 1, 2, 3 };
        static private readonly int[] fw = new int[] { 3, 4, 6 };
        static private readonly int[] schaden = new int[] { 2, 4, 9 };
        static private readonly int[] mod = new int[] { 1, 2, 3 };
        static private readonly int[] zg = new int[] { 1, 2, 3 };
        static private readonly int[] ss = new int[] { 4, 5, 6 };
        static private readonly int[] einhalbs = new int[] { 1, 2, 3 };
        static private readonly int[] rw = new int[] { 5, 6, 7 };
        static private readonly int[] fg = new int[] { 3, 4, 5 };
        static private readonly int[] mag = new int[] { 3, 4, 5 };
        static private readonly int[] rs = new int[] { 6, 7, 8 };
        static private readonly int[] st = new int[] { 4, 5, 7 };
        static private readonly int[] lz = new int[] { 1, 2, 3 };
        static private readonly int[] bm = new int[] { 7, 8, 1 };

        static private readonly string[] table_waffen = new string[] { "Waffen" };
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
            InsertingValues();
            SelectingColumns();
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
                    string insertIntoWaffen = " INSERT INTO Waffen(Waffenname, WaffenID, FW, Schaden, Mod, " +
                                              " Ort, ZG, SS, EINHALBS, RW, FG, MAG, RS, ST, LZ, BM) " +
                                              " VALUES(@waffenname, @waffenID, @fw, @schaden, @mod, @ort, @zg, " +
                                              "@ss, @einhalbs, @rw, @fg, @mag, @rs, @st, @lz, @bm" +
                                              ");";

                    SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                    Command.Parameters.Add("@waffenname", System.Data.DbType.String).Value = waffenname[0];
                    Command.Parameters.Add("@waffenID", System.Data.DbType.Int32).Value = waffenID[0];
                    Command.Parameters.Add("@ort", System.Data.DbType.String).Value = ort[0];
                    Command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = fw[0];
                    Command.Parameters.Add("@schaden", System.Data.DbType.Int32).Value = schaden[0];
                    Command.Parameters.Add("@mod", System.Data.DbType.Int32).Value = mod[0];
                    Command.Parameters.Add("@zg", System.Data.DbType.Int32).Value = zg[0];
                    Command.Parameters.Add("@ss", System.Data.DbType.Int32).Value = ss[0];
                    Command.Parameters.Add("@einhalbs", System.Data.DbType.Int32).Value = einhalbs[0];
                    Command.Parameters.Add("@rw", System.Data.DbType.Int32).Value = rw[0];
                    Command.Parameters.Add("@fg", System.Data.DbType.Int32).Value = fg[0];
                    Command.Parameters.Add("@mag", System.Data.DbType.Int32).Value = mag[0];
                    Command.Parameters.Add("@rs", System.Data.DbType.Int32).Value = rs[0];
                    Command.Parameters.Add("@st", System.Data.DbType.Int32).Value = st[0];
                    Command.Parameters.Add("@lz", System.Data.DbType.Int32).Value = lz[0];
                    Command.Parameters.Add("@bm", System.Data.DbType.Int32).Value = bm[0];

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
                    string selecting = "SELECT * FROM Waffen";
                    SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                    SqliteDataReader output = command.ExecuteReader();

                    while (output.Read())
                    {
                        Debug.Log("Waffenname: " + output["Waffenname"]);
                        Debug.Log("WaffenID: " + output["WaffenID"]);
                        Debug.Log("FW: " + output["FW"]);
                        Debug.Log("Schaden: " + output["Schaden"]);
                        Debug.Log("Mod: " + output["Mod"]);
                        Debug.Log("Ort: " + output["Ort"]);
                        Debug.Log("ZG: " + output["ZG"]);
                        Debug.Log("SS: " + output["SS"]);
                        Debug.Log("EINHALBS: " + output["EINHALBS"]);
                        Debug.Log("RW: " + output["RW"]);
                        Debug.Log("FG: " + output["FG"]);
                        Debug.Log("MAG: " + output["MAG"]);
                        Debug.Log("RS: " + output["RS"]);
                        Debug.Log("ST: " + output["ST"]);
                        Debug.Log("LZ: " + output["LZ"]);
                        Debug.Log("BM: " + output["BM"]);
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
                    string deleteColumn = "DELETE FROM Waffen " +
                                          " WHERE WaffenID = 1";

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
            Debug.Log("Press 'C' to connect with the database table " + table_waffen[0] + ".");
            Debug.Log("Drücke 'S' zum Abfragen der Werte in " + table_waffen[0] + ".");
            Debug.Log("Drücke 'I' zum Einfügen von Werten in " + table_waffen[0] + ".");
            Debug.Log("Drücke 'D' zum Löschen von Werten in " + table_waffen[0] + ".");
            Debug.Log("Drücke 'X' zum Verlassen der Datenbank.");
        }
        #endregion

        #region Exit
        void Exit()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                dbConnection.Close();
            }
        }
        #endregion

    }
}