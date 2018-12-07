using System;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;

namespace SqliteplayerControll
{
    public class SqlitePlayerController : MonoBehaviour {

        #region SqlConnection Vars
        static SqliteConnection dbConnection;
        static readonly string databasepath = @"D:/Scripts/UnityScripts/SqliteScripts/Assets/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        //Database inserted Values
        static private readonly string[] player_name = new string[] { "Player1", "Player2", "Player3", "Player4", "Player5", "Player6", "Player7", "Player8" };
        static private readonly string[] gender = new string[] { "Mann", "Frau", "Anderes" };
        static private readonly string[] race = new string[] { "Mensch", "Elf", "Magier", "Engel", "Heiler", "Alien", "Dämon" };
        static private readonly string[] haircolor = new string[] { "schwarz", "grau", "braun", "lila", "grün", "blond", "orange" };
        static private readonly string[] eyecolor = new string[] { "blau", "braun", "grün", "grau" };
        static private readonly double[] mass = new double[] { 64.5d, 70.3d, 88.9d, 86.43d, 94.12d, 100.1d, 150.85d };
        static private readonly decimal[] height = new decimal[] { 65.54m, 83.65m, 75.95m, 90.43m, 55.84m, 87.65m, 95.95m, 80.43m };
        static private readonly string[] player_desc = new string[] { "Description1", "Description2", "Description3", "Description4", "Description5", "Description6", "Description7", "Description8" };
        static private readonly int[] player_ID = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //Database Query
        static private readonly string[] table_player = new string[] { "Spieler" };
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
                Command.Parameters.Add("@name", System.Data.DbType.String).Value = player_name[0];
                Command.Parameters.Add("@geschlecht", System.Data.DbType.String).Value = gender[0];
                Command.Parameters.Add("@rasse", System.Data.DbType.String).Value = race[0];
                Command.Parameters.Add("@haar", System.Data.DbType.String).Value = haircolor[0];
                Command.Parameters.Add("@augen", System.Data.DbType.String).Value = eyecolor[0];
                Command.Parameters.Add("@gewicht", System.Data.DbType.Double).Value = mass[0];
                Command.Parameters.Add("@groese", System.Data.DbType.Decimal).Value = height[0];
                Command.Parameters.Add("@beschreibung", System.Data.DbType.String).Value = player_desc[0];
                Command.Parameters.Add("@SpielerID", System.Data.DbType.Int32).Value = player_ID[0];
                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Inserted values into " + table_player[0] + " successfuly!");
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
                Debug.Log("Searching in table " + table_player[0] + " completed!");
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
                Command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = player_ID[0];
                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Debug.Log("Deleted Row/s in " + table_player[0] + " successfully!");
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
                        Debug.Log("Table: " + table_player[0]);
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
