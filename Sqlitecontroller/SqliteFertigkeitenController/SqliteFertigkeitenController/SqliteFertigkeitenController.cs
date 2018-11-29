using System;
using System.IO;
using System.Data.SQLite;

namespace SqliteWaffencontroller
{
    class SqliteWaffencontroller
    {
        #region ConnectionVars
        static SQLiteConnection dbConnection;
                                                        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly int[] cp = new int[] { 2, 6, 8 };
        static private readonly int[] id = new int[] { 1, 2, 3 };
        static private readonly int[] fw = new int[] { 7, 13, 16 };
        static private readonly string[] art = new string[] { "mentale", "physisch", "physisch" };
        static private readonly string[] typ = new string[] { "Typ1", "Typ2", "Typ3" };
        static private readonly string[] name = new string[] { "Widerstehen", "Angreifen", "Fangen" };
        
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
                string insertIntoWaffen = " INSERT INTO Fertigkeiten(CP, ID, FW, Art, Typ, Name) " +
                                          " VALUES(@cp, @id, @fw, @art, @typ, @name);";

                SQLiteCommand Command = new SQLiteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@cp", System.Data.DbType.Int32).Value = cp[0];
                Command.Parameters.Add("@id", System.Data.DbType.Int32).Value = id[0];
                Command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = fw[0];
                Command.Parameters.Add("@art", System.Data.DbType.String).Value = art[0];
                Command.Parameters.Add("@typ", System.Data.DbType.String).Value = typ[0];
                Command.Parameters.Add("@name", System.Data.DbType.String).Value = name[0];

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
                string selecting = "SELECT * FROM Fertigkeiten;";
                SQLiteCommand command = new SQLiteCommand(selecting, dbConnection);
                SQLiteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Fertigkeiten Name: " + output["Name"]);
                    Console.WriteLine("Fertigkeiten Typ: " + output["Typ"]);
                    Console.WriteLine("Fertigkeiten Art: " + output["Art"]);
                    Console.WriteLine("CP: " + output["CP"]);
                    Console.WriteLine("FW: " + output["FW"]);
                    Console.WriteLine("ID: " + output["ID"]);
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
                string deleteColumn = "DELETE FROM Fertigkeiten " +
                                      " WHERE ID = 1;";

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