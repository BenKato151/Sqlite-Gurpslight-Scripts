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
        static private readonly string table_player = "Spieler";
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
                Debug.Log("Inserted values into " + table_player + " successfuly!");
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
                string updatecommand = " UPDATE Spieler " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE SpielerID = @IDvalue;";

                SqliteCommand Command = new SqliteCommand(updatecommand, dbConnection);
                Command.Parameters.Add("@wert", System.Data.DbType.String).Value = Fieldwert.text;
                Command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = FieldIDvalue.text;

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Updated value in " + table_player + " successfuly!");
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
                string selecting = "SELECT * FROM Spieler;";
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                
                SqliteDataReader output = command.ExecuteReader();
                while (output.Read())
                {
                    Debug.Log("Name: " + output["name"]);
                    Debug.Log("Geschlecht: " + output["geschlecht"]);
                    Debug.Log("Rasse: " + output["rasse"]);
                    Debug.Log("Haar: " + output["haar"]);
                    Debug.Log("Augen: " + output["augen"]);
                    Debug.Log("Gewicht: " + output["gewicht"]);
                    Debug.Log("Größe: " + output["groese"]);
                    Debug.Log("Beschreibung: " + output["beschreibung"]);
                    Debug.Log("Spieler ID: " + output["SpielerID"]);
                }
                Debug.Log("Searching in table " + table_player + " completed!");
            }

            catch (Exception e)
            {
                Debug.Log("Error!");
                Debug.Log(e);
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
                Debug.Log("Deleted Row/s in " + table_player + " successfully!");
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
                        Debug.Log("Table: " + table_player);
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
            Debug.Log("Verbindung beendet");
        }
        #endregion

    }
}
