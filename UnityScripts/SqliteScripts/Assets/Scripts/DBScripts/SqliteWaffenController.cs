using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.IO;
using UnityEngine.UI;

namespace SqliteWaffenController
{
    public class SqliteWaffenController : MonoBehaviour
    {
        #region ConnectionVars
        public static SqliteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\UnityScripts\SqliteScripts\Assets\Scripts\Database\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly string table_waffen = "Waffen";
        #endregion

        #region InputVars
        public Text Fieldwaffenname;
        public Text FieldFW;
        public Text FieldWaffenID;
        public Text Fieldmod;
        public Text Fieldst;
        public Text Fieldschaden;
        public Text FieldOrt;
        public Text Fieldzg;
        public Text Fieldbm;
        public Text Fieldmag;
        public Text Fieldfg;
        public Text Fieldrw;
        public Text Fieldeinhalbs;
        public Text Fieldss;
        public Text Fieldrs;
        public Text Fieldlz;
        public Text FieldDelete;
        #endregion

        #region UpdateVars
        public Text Fieldwert;
        public Text Fieldcolumn;
        public Text FieldIDvalue;
        #endregion

        #region Insert
        public void InsertingValues()
        {
            try
            {
                string insertIntoWaffen = " INSERT INTO Waffen(Waffenname, WaffenID, FW, Schaden, Mod, " +
                                          " Ort, ZG, SS, EINHALBS, RW, FG, MAG, RS, ST, LZ, BM) " +
                                          " VALUES(@waffenname, @waffenID, @fw, @schaden, @mod, @ort, @zg, " +
                                          " @ss, @einhalbs, @rw, @fg, @mag, @rs, @st, @lz, @bm" +
                                          " );";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@waffenname", System.Data.DbType.String).Value = Fieldwaffenname.text;
                Command.Parameters.Add("@waffenID", System.Data.DbType.Int32).Value = FieldWaffenID.text;
                Command.Parameters.Add("@ort", System.Data.DbType.String).Value = FieldOrt.text;
                Command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = FieldFW.text;
                Command.Parameters.Add("@schaden", System.Data.DbType.Int32).Value = Fieldschaden.text;
                Command.Parameters.Add("@mod", System.Data.DbType.Int32).Value = Fieldmod.text;
                Command.Parameters.Add("@zg", System.Data.DbType.Int32).Value = Fieldzg.text;
                Command.Parameters.Add("@ss", System.Data.DbType.Int32).Value = Fieldss.text;
                Command.Parameters.Add("@einhalbs", System.Data.DbType.Int32).Value = Fieldeinhalbs.text;
                Command.Parameters.Add("@rw", System.Data.DbType.Int32).Value = Fieldrw.text;
                Command.Parameters.Add("@fg", System.Data.DbType.Int32).Value = Fieldfg.text;
                Command.Parameters.Add("@mag", System.Data.DbType.Int32).Value = Fieldmag.text;
                Command.Parameters.Add("@rs", System.Data.DbType.Int32).Value = Fieldrs.text;
                Command.Parameters.Add("@st", System.Data.DbType.Int32).Value = Fieldst.text;
                Command.Parameters.Add("@lz", System.Data.DbType.Int32).Value = Fieldlz.text;
                Command.Parameters.Add("@bm", System.Data.DbType.Int32).Value = Fieldbm.text;
                
                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Inserted values into " + table_waffen + " successfuly!");
            }

            catch (Exception e)
            {
                Debug.Log("Error! ");
                Debug.Log(e);
            }
        }
        #endregion

        #region Update
        public void UpdatingValues()
        {
            try
            {
                string updatecommand = " UPDATE Waffen " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE WaffenID = @IDvalue;";

                SqliteCommand Command = new SqliteCommand(updatecommand, dbConnection);
                Command.Parameters.Add("@wert", System.Data.DbType.String).Value = Fieldwert.text;
                Command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = FieldIDvalue.text;

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Updated value in " + table_waffen + " successfuly!");
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
        #endregion

        #region DELETE
        public void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Waffen " +
                                      " WHERE WaffenID = @id";

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
                        Debug.Log("Table: " + table_waffen);

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