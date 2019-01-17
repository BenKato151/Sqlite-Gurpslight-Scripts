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
        private string databasepath;
        private readonly string relativePath = @"/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly string table = "Waffen";
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
        public Text FieldSelectID;
        #endregion

        #region MsgVars
        public Text console_msg;
        public Text sqlOutput_msg;
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
                console_msg.text = "Inserted values in table:\n         " + table
                                  + "\nsuccessfuly!";
            }

            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to insert values!";
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
                console_msg.text = "Updated value in \n         " + table +
                                   "\nsuccessfuly!";
            }

            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to update values!";
            }
        }
        #endregion

        #region Select
        public void SelectingColumns()
        {
            try
            {
                string selecting = "SELECT * FROM Waffen WHERE WaffenID = " + FieldSelectID.text;
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    sqlOutput_msg.text = "Waffenname: " + output["Waffenname"] + "\n" +
                                         "WaffenID: " + output["WaffenID"] + "\n" +
                                         "FW: " + output["FW"] + "\n" +
                                         "Schaden: " + output["Schaden"] + "\n" +
                                         "Mod: " + output["Mod"] + "\n" +
                                         "Ort: " + output["Ort"] + "\n" +
                                         "ZG: " + output["ZG"] + "\n" +
                                         "SS: " + output["SS"] + "\n" +
                                         "EINHALBS: " + output["EINHALBS"] + "\n" +
                                         "RW: " + output["RW"] + "\n" +
                                         "FG: " + output["FG"] + "\n" +
                                         "MAG: " + output["MAG"] + "\n" +
                                         "RS: " + output["RS"] + "\n" +
                                         "ST: " + output["ST"] + "\n" +
                                         "LZ: " + output["LZ"] + "\n" +
                                         "BM: " + output["BM"];
                }
                console_msg.text = "Searching in column:\n         " + table
                                 + "\ncompleted!";
            }

            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to search values!";
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
                console_msg.text = "Deleted Row/s successfully!";
            }

            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to delete rows";
            }
        }
        #endregion

        #region Connection
        public void ConnectionDB()
        {
            databasepath = Application.dataPath + relativePath;
            //Tries to get a connection with the database and if there is an path-error, it will catch it
            try
            {
                dbConnection = new SqliteConnection("Data Source = " + databasepath + "; " + " Version = 3;");
                dbConnection.Open();

                if (File.Exists(databasepath))
                {
                    if (dbConnection != null)
                    {
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
        public void Exit()
        {
            try
            {
                dbConnection.Close();
                console_msg.text = "\nConnection closed!";
                sqlOutput_msg.text = " ";
            }
            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to close the connection!";
            }
        }
        #endregion

    }
}