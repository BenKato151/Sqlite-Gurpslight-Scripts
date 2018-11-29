using System;
using System.IO;
using System.Data.SQLite;

namespace SqliteRuestungSchutzController
{
    class SqliteRuestungSchutzcontroller
    {
        #region ConnectionVars
        static SQLiteConnection dbConnection;
                                                        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly int[] sr = new int[] { 2, 6, 8 };
        static private readonly int[] id = new int[] { 1, 2, 3 };
        static private readonly int[] pv = new int[] { 7, 13, 16 };
        static private readonly string[] ort = new string[] { "Ebene 1", "Ebene 3", "Ebene 4" };

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
                string insertIntoWaffen = " INSERT INTO Ruestung_Schutz(Ort, SR, PV, ID) " +
                                          " VALUES(@Ort, @SR, @PV, @ID);";

                SQLiteCommand Command = new SQLiteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@Ort", System.Data.DbType.String).Value = ort[0];
                Command.Parameters.Add("@SR", System.Data.DbType.Int32).Value = sr[0];
                Command.Parameters.Add("@PV", System.Data.DbType.Int32).Value = pv[0];
                Command.Parameters.Add("@ID", System.Data.DbType.Int32).Value = id[0];

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
                string selecting = "SELECT * FROM Ruestung_Schutz;";
                SQLiteCommand command = new SQLiteCommand(selecting, dbConnection);
                SQLiteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Ort: " + output["Ort"]);
                    Console.WriteLine("SR: " + output["SR"]);
                    Console.WriteLine("PV: " + output["PV"]);
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
                string deleteColumn = "DELETE FROM Ruestung_Schutz " +
                                      " WHERE ID = 1";

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
