using System;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace SqliteplayerControll
{
    public class SqlitePlayerController : MonoBehaviour {

        #region SqlConnection Vars
        static SqliteConnection dbConnection;
        static readonly string databasepath = @"D:/Scripts/UnityScripts/SqliteScripts/Assets/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        //Database Query
        static private readonly string table = "Spieler";
        #endregion

        #region InputVars
        public Text Fieldname;
        public Text Fieldgeschlecht;
        public Text FieldID;
        public Text FieldHaar;
        public Text FieldRasse;
        public Text FieldAugen;
        public Text FieldGewicht;
        public Text FieldGroese;
        public Text FieldDesc;
        public Text FieldDelete;
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
                string insertIntoSpieler = " INSERT INTO Spieler(name, geschlecht, rasse, haar, augen, gewicht, groese, beschreibung, SpielerID)" +
                                           " VALUES(@name, @geschlecht, @rasse, @haar, @augen, @gewicht," +
                                           " @groese, @beschreibung, @SpielerID)";
                   
                SqliteCommand Command = new SqliteCommand(insertIntoSpieler, dbConnection);
                Command.Parameters.Add("@name", System.Data.DbType.String).Value = Fieldname.text;
                Command.Parameters.Add("@geschlecht", System.Data.DbType.String).Value = Fieldgeschlecht.text;
                Command.Parameters.Add("@rasse", System.Data.DbType.String).Value = FieldRasse.text;
                Command.Parameters.Add("@haar", System.Data.DbType.String).Value = FieldHaar.text;
                Command.Parameters.Add("@augen", System.Data.DbType.String).Value = FieldAugen.text;
                Command.Parameters.Add("@gewicht", System.Data.DbType.Double).Value = FieldGewicht.text;
                Command.Parameters.Add("@groese", System.Data.DbType.Decimal).Value = FieldGroese.text;
                Command.Parameters.Add("@beschreibung", System.Data.DbType.String).Value = FieldDesc.text;
                Command.Parameters.Add("@SpielerID", System.Data.DbType.Int32).Value = FieldID.text;
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
                string updatecommand = " UPDATE Spieler " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE SpielerID = @IDvalue;";

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
                string selecting = "SELECT * FROM Spieler;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                
                SqliteDataReader output = command.ExecuteReader();
                while (output.Read())
                {
                    sqlOutput_msg.text = "Name: " + output["name"] + "\n" +
                                         "Geschlecht: " + output["geschlecht"] + "\n" +
                                         "Rasse: " + output["rasse"] + "\n" +
                                         "Haar: " + output["haar"] + "\n" +
                                         "Augen: " + output["augen"] + "\n" +
                                         "Gewicht: " + output["gewicht"] + "\n" +
                                         "Größe: " + output["groese"] + "\n" +
                                         "Beschreibung: " + output["beschreibung"] + "\n" +
                                         "Spieler ID: " + output["SpielerID"];
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
                string deleteColumn = " DELETE FROM Spieler " +
                                      " WHERE SpielerID = @IDvalue";

                SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);
                Command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = FieldDelete.text;

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
