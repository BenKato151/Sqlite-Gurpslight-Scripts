using System;
using System.IO;
using System.Data.SQLite;

namespace SqliteWaffencontroll
{
    class SqliteWaffencontroll
    {
        #region ConnectionVars
        static SQLiteConnection dbConnection;
        //absolute path required
        static private readonly string databasepath = @"D:\Scripts\Sqlitecontroller\DataBase\new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        static private readonly string[] waffenname = new string[] { "Waffe1", "Waffe2", "Waffe3" };
        static private readonly string[] ort = new string[] { "Ebene 1", "Ebene 2", "Ebene 3" };
        static private readonly int[] waffenID = new int[] { 1, 2, 3 };
        static private readonly int[] fw = new int[] { 3, 4, 6 };
        static private readonly int[] schaden = new int[] { 2, 4, 9 };
        static private readonly int[] mod = new int[] { 1, 2, 3 };
        static private readonly int[] zg = new int[] { 1, 2, 3 };
        static private readonly int[] ss = new int[] { 4, 5, 6 };
        static private readonly int[] einhalbs = new int[] { 1, 2, 3 };
        static private readonly int[] rw = new int[] { 5, 6, 7 };
        static private readonly int[] fg = new int[] { 3, 4, 5 };
        static private readonly int[] mag = new int[] { 3, 4, 5 };
        static private readonly int[] rs = new int[] { 6, 7, 8 };
        static private readonly int[] st = new int[] { 4, 5, 7 };
        static private readonly int[] lz = new int[] { 1, 2, 3 };
        static private readonly int[] bm = new int[] { 7, 8, 1 };
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
                string insertIntoWaffen = " INSERT INTO Waffen(Waffenname, WaffenID, FW, Schaden, Mod, " +
                                          " Ort, ZG, SS, EINHALBS, RW, FG, MAG, RS, ST, LZ, BM) " +
                                          " VALUES(@waffenname, @waffenID, @fw, @schaden, @mod, @ort, @zg, " +
                                          "@ss, @einhalbs, @rw, @fg, @mag, @rs, @st, @lz, @bm" +
                                          ");";

                SQLiteCommand Command = new SQLiteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@waffenname", System.Data.DbType.String).Value = waffenname[0];
                Command.Parameters.Add("@waffenID", System.Data.DbType.Int32).Value = waffenID[0];
                Command.Parameters.Add("@ort", System.Data.DbType.String).Value = ort[0];
                Command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = fw[0];
                Command.Parameters.Add("@schaden", System.Data.DbType.Int32).Value = schaden[0];
                Command.Parameters.Add("@mod", System.Data.DbType.Int32).Value = mod[0];
                Command.Parameters.Add("@zg", System.Data.DbType.Int32).Value = zg[0];
                Command.Parameters.Add("@ss", System.Data.DbType.Int32).Value = ss[0];
                Command.Parameters.Add("@einhalbs", System.Data.DbType.Int32).Value = einhalbs[0];
                Command.Parameters.Add("@rw", System.Data.DbType.Int32).Value = rw[0];
                Command.Parameters.Add("@fg", System.Data.DbType.Int32).Value = fg[0];
                Command.Parameters.Add("@mag", System.Data.DbType.Int32).Value = mag[0];
                Command.Parameters.Add("@rs", System.Data.DbType.Int32).Value = rs[0];
                Command.Parameters.Add("@st", System.Data.DbType.Int32).Value = st[0];
                Command.Parameters.Add("@lz", System.Data.DbType.Int32).Value = lz[0];
                Command.Parameters.Add("@bm", System.Data.DbType.Int32).Value = bm[0];

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
                string selecting = "SELECT * FROM Waffen";
                SQLiteCommand command = new SQLiteCommand(selecting, dbConnection);
                SQLiteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Waffenname: " + output["Waffenname"]);
                    Console.WriteLine("WaffenID: " + output["WaffenID"]);
                    Console.WriteLine("FW: " + output["FW"]);
                    Console.WriteLine("Schaden: " + output["Schaden"]);
                    Console.WriteLine("Mod: " + output["Mod"]);
                    Console.WriteLine("Ort: " + output["Ort"]);
                    Console.WriteLine("ZG: " + output["ZG"]);
                    Console.WriteLine("SS: " + output["SS"]);
                    Console.WriteLine("EINHALBS: " + output["EINHALBS"]);
                    Console.WriteLine("RW: " + output["RW"]);
                    Console.WriteLine("FG: " + output["FG"]);
                    Console.WriteLine("MAG: " + output["MAG"]);
                    Console.WriteLine("RS: " + output["RS"]);
                    Console.WriteLine("ST: " + output["ST"]);
                    Console.WriteLine("LZ: " + output["LZ"]);
                    Console.WriteLine("BM: " + output["BM"]);
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
                string deleteColumn = "DELETE FROM Waffen " +
                                      " WHERE WaffenID = 1";

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