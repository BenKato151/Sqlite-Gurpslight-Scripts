using System;
using System.IO;
using System.Data.SQLite;

namespace SqliteAbwehrController
{
    class SqliteAbwehrController
    {
        #region ConnectionVars
        static SQLiteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly int[] schild = new int[] { 2, 6, 8 };
        static private readonly int[] ruestung = new int[] { 1, 2, 3 };
        static private readonly int[] umhang = new int[] { 7, 13, 16 };
        static private readonly int[] gesamt = new int[] { schild[0] + ruestung[0] + umhang[0], schild[1] + ruestung[1] + umhang[1], schild[2] + ruestung[2] + umhang[2] };
        
        #endregion

        #region Main!
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
                string insertIntoWaffen = " INSERT INTO Abwehr(Schild, Ruestung, Umhang, Gesamt) " +
                                          " VALUES(@schild, @ruestung, @umhang, @gesamt);";

                SQLiteCommand Command = new SQLiteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@schild", System.Data.DbType.Int32).Value = schild[0];
                Command.Parameters.Add("@ruestung", System.Data.DbType.Int32).Value = ruestung[0];
                Command.Parameters.Add("@umhang", System.Data.DbType.Int32).Value = umhang[0];
                Command.Parameters.Add("@gesamt", System.Data.DbType.String).Value = gesamt[0];

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
                string selecting = "SELECT * FROM Abwehr;";
                SQLiteCommand command = new SQLiteCommand(selecting, dbConnection);
                SQLiteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Schild: " + output["Schild"]);
                    Console.WriteLine("Rüstung: " + output["Ruestung"]);
                    Console.WriteLine("Umhang: " + output["Umhang"]);
                    Console.WriteLine("Gesamt: " + output["Gesamt"]);
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
                string deleteColumn = "DELETE FROM Abwehr " +
                                      " WHERE Gesamt = 10;" ;

                SQLiteCommand Command = new SQLiteCommand(deleteColumn, dbConnection);
                //Command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = //arrayname[0];

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
