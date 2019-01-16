using UnityEngine;
using System;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace SqliteAbwehrController
{
    public class SqliteAbwehrController : MonoBehaviour {
        
        #region ConnectionVars
        static SqliteConnection dbConnection;
        //absolute path required
        private string databasepath;
        private readonly string relativePath = @"/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVar        
        //Database Query
        static private readonly string table = "Abwehr";
        #endregion

        #region InputVars
        public Text FieldSchild;
        public Text FieldRuestung;
        public Text FieldUmhang;
        public Text FieldID;
        public Text FieldBekannterWert;
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
                string insertIntoWaffen = " INSERT INTO Abwehr(Schild, Ruestung, Umhang, Gesamt, ID) " +
                                          " VALUES(@schild, @ruestung, @umhang, @gesamt, @ID);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@schild", System.Data.DbType.Int32).Value = FieldSchild.text;
                Command.Parameters.Add("@ruestung", System.Data.DbType.Int32).Value = FieldRuestung.text;
                Command.Parameters.Add("@umhang", System.Data.DbType.Int32).Value = FieldUmhang.text;
                Command.Parameters.Add("@gesamt", System.Data.DbType.Int32).Value = Int32.Parse(FieldRuestung.text) + Int32.Parse (FieldSchild.text) + Int32.Parse(FieldUmhang.text);
                Command.Parameters.Add("@ID", System.Data.DbType.Int32).Value = Int32.Parse(FieldID.text);

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

        #region Select
        public void SelectingColumns()
        {
            try
            {
                string selecting = "SELECT * FROM Abwehr;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    sqlOutput_msg.text = "Schild: " + output["Schild"] + "\n" +
                                         "Rüstung: " + output["Ruestung"] + "\n" +
                                         "Umhang: " + output["Umhang"] + "\n" +
                                         "Gesamt: " + output["Gesamt"] + "\n" +
                                         "ID: " + output["ID"];
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

        #region Update
        public void UpdatingValues()
        {
            try
            {
                string updatecommand = " UPDATE Abwehr " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE ID = @IDvalue;";

                SqliteCommand Command = new SqliteCommand(updatecommand, dbConnection);
                Command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = Fieldwert.text;
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

        #region DELETE
        public void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Abwehr " +
                                      " WHERE ID = @wert;";

                SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);
                Command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = FieldBekannterWert.text;

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