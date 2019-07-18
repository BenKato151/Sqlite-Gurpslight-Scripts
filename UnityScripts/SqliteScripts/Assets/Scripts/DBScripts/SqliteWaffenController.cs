using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

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

                SqliteCommand command = new SqliteCommand(insertIntoWaffen, dbConnection);
                command.Parameters.Add("@waffenname", System.Data.DbType.String).Value = Fieldwaffenname.text;
                command.Parameters.Add("@waffenID", System.Data.DbType.Int32).Value = FieldWaffenID.text;
                command.Parameters.Add("@ort", System.Data.DbType.String).Value = FieldOrt.text;
                command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = FieldFW.text;
                command.Parameters.Add("@schaden", System.Data.DbType.Int32).Value = Fieldschaden.text;
                command.Parameters.Add("@mod", System.Data.DbType.Int32).Value = Fieldmod.text;
                command.Parameters.Add("@zg", System.Data.DbType.Int32).Value = Fieldzg.text;
                command.Parameters.Add("@ss", System.Data.DbType.Int32).Value = Fieldss.text;
                command.Parameters.Add("@einhalbs", System.Data.DbType.Int32).Value = Fieldeinhalbs.text;
                command.Parameters.Add("@rw", System.Data.DbType.Int32).Value = Fieldrw.text;
                command.Parameters.Add("@fg", System.Data.DbType.Int32).Value = Fieldfg.text;
                command.Parameters.Add("@mag", System.Data.DbType.Int32).Value = Fieldmag.text;
                command.Parameters.Add("@rs", System.Data.DbType.Int32).Value = Fieldrs.text;
                command.Parameters.Add("@st", System.Data.DbType.Int32).Value = Fieldst.text;
                command.Parameters.Add("@lz", System.Data.DbType.Int32).Value = Fieldlz.text;
                command.Parameters.Add("@bm", System.Data.DbType.Int32).Value = Fieldbm.text;
                
                command.ExecuteNonQuery();
                command.Parameters.Clear();
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

                SqliteCommand command = new SqliteCommand(updatecommand, dbConnection);
                command.Parameters.Add("@wert", System.Data.DbType.String).Value = Fieldwert.text;
                command.Parameters.Add("@IDvalue", System.Data.DbType.Int32).Value = FieldIDvalue.text;

                command.ExecuteNonQuery();
                command.Parameters.Clear();
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

                SqliteCommand command = new SqliteCommand(deleteColumn, dbConnection);
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = FieldDelete.text;

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                console_msg.text = "Deleted Row/s successfully!";
                sqlOutput_msg.text = "";
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

                if (File.Exists(databasepath))
                {
                    if (dbConnection != null)
                    {
                        dbConnection.Open();
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

        #region ExportXML
        public void ExportXML()
        {
            try
            {
                #region Vars
                string waffennametext = "";
                string fwtext = "";
                string schadentext = "";
                string modtext = "";
                string orttext = "";
                string waffenidtext = "";
                string zgtext = "";
                string sstext = "";
                string einhalbstext = "";
                string rwtext = "";
                string fgtext = "";
                string magtext = "";
                string rstext = "";
                string sttext = "";
                string lztext = "";
                string bmtext = "";
                string selecting = "SELECT * FROM Waffen";
                #endregion

                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                #region Read
                while (output.Read())
                {
                    waffennametext = "" + output["Waffenname"];
                    waffenidtext = "" + output["WaffenID"];
                    fwtext = "" + output["FW"];
                    schadentext = "" + output["Schaden"];
                    modtext = "" + output["Mod"];
                    orttext = "" + output["Ort"];
                    zgtext = "" + output["ZG"];
                    sstext = "" + output["SS"];
                    einhalbstext = "" + output["EINHALBS"];
                    rwtext = "" + output["RW"];
                    fgtext = "" + output["FG"];
                    magtext = "" + output["MAG"];
                    rstext = "" + output["RS"];
                    sttext = "" + output["ST"];
                    lztext = "" + output["LZ"];
                    bmtext = "" + output["BM"];
                }
                #endregion

                #region save
                XDocument WaffenXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XAttribute("Waffenname", waffennametext),
                    new XAttribute("WaffenID", waffenidtext),
                    new XAttribute("FW", fwtext),
                    new XAttribute("Schaden",schadentext),
                    new XAttribute("Mod",modtext),
                    new XAttribute("Ort",orttext),
                    new XAttribute("ZG",zgtext),
                    new XAttribute("SS",sstext),
                    new XAttribute("Einhalbs",einhalbstext),
                    new XAttribute("RW",rwtext),
                    new XAttribute("FG",fgtext),
                    new XAttribute("MAG",magtext),
                    new XAttribute("RS",rstext),
                    new XAttribute("ST",sttext),
                    new XAttribute("LZ",lztext),
                    new XAttribute("BM",bmtext)
                    )
                );
                #endregion

                WaffenXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
                console_msg.text = "Export XML in column:\n         " + table
                                 + "\ncompleted!\n"
                                 + "saved file in: \n"
                                 + Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml";
            }
            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to export values!";
            }
        }
        #endregion

        #region ImportXML
        public void ImportXML()
        {
            try
            {
                int generateName = UnityEngine.Random.Range(0, 1000);
                string dbpath = Application.dataPath + @"/Scripts/Database/Exported_DBs/waffen_table_Num" + generateName + ".sqlite";
                string xmlpath = Application.dataPath + @"/XMLDocuments/Imports/gurbslight_character_export.xml";
                XmlDocument attributeXMLFile = new XmlDocument();
                attributeXMLFile.Load(xmlpath);

                XmlNode selectname = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[0];
                XmlNode selectID = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[1];
                XmlNode selectfw = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[2];
                XmlNode selectschaden = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[3];
                XmlNode selectmod = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[4];
                XmlNode selectOrt = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[5];
                XmlNode selectzg = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[6];
                XmlNode selectss = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[7];
                XmlNode selecteinhalbs = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[8];
                XmlNode selectrw = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[9];
                XmlNode selectfg = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[10];
                XmlNode selectmag = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[11];
                XmlNode selectrs = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[12];
                XmlNode selectst = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[13];
                XmlNode selectlz = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[14];
                XmlNode selectbm = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Waffen")[0].ChildNodes[15];

                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (!File.Exists(dbpath))
                {
                    SqliteConnection.CreateFile(dbpath);

                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Waffen(Waffenname string, WaffenID int, FW int, Schaden int, Mod int, " +
                                          " Ort string, ZG int, SS int, EINHALBS int, RW int, FG int, MAG int, RS int, ST int, LZ int, BM int);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = " INSERT INTO Waffen(Waffenname, WaffenID, FW, Schaden, Mod, " +
                                          " Ort, ZG, SS, EINHALBS, RW, FG, MAG, RS, ST, LZ, BM) " +
                                          " VALUES(@waffenname, @waffenID, @fw, @schaden, @mod, @ort, @zg, " +
                                          " @ss, @einhalbs, @rw, @fg, @mag, @rs, @st, @lz, @bm" +
                                          " );";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@waffenname", System.Data.DbType.String).Value = selectname.InnerText;
                        commandinsert.Parameters.Add("@waffenID", System.Data.DbType.String).Value = selectID.InnerText;
                        commandinsert.Parameters.Add("@fw", System.Data.DbType.String).Value = selectfw.InnerText;
                        commandinsert.Parameters.Add("@schaden", System.Data.DbType.String).Value = selectschaden.InnerText;
                        commandinsert.Parameters.Add("@mod", System.Data.DbType.String).Value = selectmod.InnerText;
                        commandinsert.Parameters.Add("@ort", System.Data.DbType.String).Value = selectOrt.InnerText;
                        commandinsert.Parameters.Add("@zg", System.Data.DbType.String).Value = selectzg.InnerText;
                        commandinsert.Parameters.Add("@ss", System.Data.DbType.String).Value = selectss.InnerText;
                        commandinsert.Parameters.Add("@einhalbs", System.Data.DbType.String).Value = selecteinhalbs.InnerText;
                        commandinsert.Parameters.Add("@rw", System.Data.DbType.String).Value = selectrw.InnerText;
                        commandinsert.Parameters.Add("@fg", System.Data.DbType.String).Value = selectfg.InnerText;
                        commandinsert.Parameters.Add("@mag", System.Data.DbType.String).Value = selectmag.InnerText;
                        commandinsert.Parameters.Add("@rs", System.Data.DbType.String).Value = selectrs.InnerText;
                        commandinsert.Parameters.Add("@st", System.Data.DbType.String).Value = selectst.InnerText;
                        commandinsert.Parameters.Add("@lz", System.Data.DbType.String).Value = selectlz.InnerText;
                        commandinsert.Parameters.Add("@bm", System.Data.DbType.String).Value = selectbm.InnerText;

                        commandinsert.ExecuteNonQuery();
                        commandinsert.Parameters.Clear();

                        console_msg.text = "Successfully imported into: " + dbpath;
                    }
                }
                else
                {
                    Debug.Log("Please delete the existing file in: " + dbpath);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
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