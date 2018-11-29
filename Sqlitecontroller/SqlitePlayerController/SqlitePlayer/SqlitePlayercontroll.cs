#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Runtime.InteropServices;
#endregion

namespace SqlitePlayercontroll
{
    class Sqlitecontroll
    {
        #region TODO
        /*TODO:         Add         Order commands
                        
                        Check       for PrimaryKey-Error
                                    Contraints

                        Edit        Using Array for Inserting automatically
             
        */
        #endregion

        #region ConnectionVars
        static SQLiteConnection dbConnection;
                                                            //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        public bool isClosed = false;
        #endregion

        #region SqlVars
        //Database Values
        static private readonly string[] player_name = new string[] { "Player1", "Player2", "Player3", "Player4", "Player5", "Player6", "Player7", "Player8" };
        static private readonly string[] gender = new string[] { "Mann", "Frau", "Anderes" };
        static private readonly string[] race = new string[] { "Mensch", "Elf", "Magier", "Engel", "Heiler", "Alien", "Dämon" };
        static private readonly string[] haircolor = new string[] { "schwarz", "grau", "braun", "lila", "grün", "blond", "orange" };
        static private readonly string[] eyecolor = new string[] { "blau", "braun", "grün", "grau" };
        static private readonly double[] mass = new double[] { 64.5d, 70.3d, 88.9d, 86.43d, 94.12d, 100.1d, 150.85d };
        static private readonly decimal[] height = new decimal[] { 65.54m, 83.65m, 75.95m, 90.43m, 55.84m, 87.65m, 95.95m, 80.43m };
        static private readonly string[] player_desc = new string[] { "Description1", "Description2", "Description3", "Description4", "Description5", "Description6", "Description7", "Description8" };
        static private readonly int[] player_ID = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        #endregion

        #region Main Method!
        static void Main(string[] args)
        {
            ConnectionDB();
            //InsertingValues();
            //DeleteColumns();
            SelectingColumns();
            ExitMethod();
        }
        #endregion

        #region Insert
        private static void InsertingValues()
        {
            try
            {
                string insertIntoSpieler = "INSERT INTO Spieler(name, geschlecht, rasse, haar, augen, gewicht, groese, beschreibung, SpielerID)" +
                                           "VALUES(@name, @geschlecht, @rasse, @haar, @augen, @gewicht," +
                                           "@groese, @beschreibung, @SpielerID)";
               /* string insertIntoSpielerArray = "INSERT INTO Spieler(name) VALUES("; foreach (var item in player_name)
                {
                    insertIntoSpielerArray += item + ",";
                }
                insertIntoSpielerArray = insertIntoSpielerArray.Remove(insertIntoSpielerArray.Length - 1);
                insertIntoSpielerArray += ")";
                */
                SQLiteCommand Command = new SQLiteCommand(insertIntoSpieler, dbConnection);
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
                Console.WriteLine("Inserted values successfuly!");
                Console.WriteLine("");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error! ");
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }
        #endregion

        #region Select
        private static void SelectingColumns()
        {
            Console.WriteLine("Searching...");

            try
            {
                string selecting = "SELECT * FROM Spieler";
                SQLiteCommand command = new SQLiteCommand(selecting, dbConnection);
                SQLiteDataReader output = command.ExecuteReader();                

                while (output.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Name: " + output["name"]);
                    Console.WriteLine("Geschlecht: " + output["geschlecht"]);
                    Console.WriteLine("Rasse: " + output["rasse"]);
                    Console.WriteLine("Haar: " + output["haar"]);
                    Console.WriteLine("Augen: " + output["augen"]);
                    Console.WriteLine("Gewicht: " + output["gewicht"]);
                    Console.WriteLine("Größe: " + output["groese"]);
                    Console.WriteLine("Beschreibung: " + output["beschreibung"]);
                    Console.WriteLine("Spieler ID: " + output["SpielerID"]);
                    Console.WriteLine("");
                }

                Console.WriteLine("");
                Console.WriteLine("Searching completed!");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error!   ");
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }
        #endregion
        
        #region DELETE
        
       private static void DeleteColumns()
       {
            try
            {
                string deleteColumn = "DELETE FROM Spieler " +
                                      " WHERE SpielerID = 1";

                SQLiteCommand Command = new SQLiteCommand(deleteColumn, dbConnection);
                //Command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = player_ID[0];

                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                Console.WriteLine("Deleted Row/s successfully!");
                Console.WriteLine("");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error! ");
                Console.WriteLine(e);
                Console.WriteLine("");
            }
       }
        
        #endregion

        #region Connection
       private static void ConnectionDB()
       {
            //Tries to get a connection with the database and if there is an path-error, it will catch it
            try
            {
                dbConnection = new SQLiteConnection("Data Source = " + databasepath + "; " + " Version = 3;");
                dbConnection.Open();

                if (File.Exists(databasepath))
                {
                    if (dbConnection != null)
                    {
                        Console.WriteLine("Connected to the database!");
                        Console.WriteLine("");
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Not Connected!    Error:    ");
                Console.WriteLine(e);
                Console.WriteLine("");
            }
       }
        #endregion
        
        #region Exit
      private static void ExitMethod()
      {
            dbConnection.Close();
            Console.WriteLine("");
            Console.WriteLine("Closed Database Connection!");

            //Only my like to close a console programm
            for (int i = 0; i < 50; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Drücke einen Key zum Verlassen!");
            Console.WriteLine("");
            Console.ReadKey();
      }
        #endregion

    }
}
