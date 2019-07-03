using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteplayerControll
{
    public class SqlitePlayerController : MonoBehaviour {

        #region SqlConnection Vars
        static SqliteConnection dbConnection;
        private string databasepath;
        private readonly string relativePath = @"/Scripts/Database/new_Char_Bogen1.sqlite";
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
                string selecting = "SELECT * FROM Spieler WHERE SpielerID = " + FieldSelectID.text;
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

        #region ExportXML
        public void ExportXML()
        {
            try
            {
                string geschlechttext = "";
                string rassetext = "";
                string haartext = "";
                string augentext = "";
                string nametext = "";
                string idtext = "";
                string gewichttext = "";
                string groesettext = "";
                string beschreibungtext = "";
                string selecting = "SELECT * FROM Spieler";

                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    geschlechttext = "" + output["geschlecht"];
                    rassetext = "" + output["rasse"];
                    haartext = "" + output["haar"];
                    augentext = "" + output["augen"];
                    gewichttext = "" + output["gewicht"];
                    groesettext = "" + output["groese"];
                    beschreibungtext = "" + output["beschreibung"];
                    nametext = "" + output["name"];
                    idtext = "" + output["SpielerID"];
                }

                XDocument PlayerXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XAttribute("Name", nametext),
                    new XAttribute("Geschlecht", geschlechttext),
                    new XAttribute("Rasse", rassetext),
                    new XAttribute("Haar", haartext),
                    new XAttribute("Augen", augentext),
                    new XAttribute("Gewicht", gewichttext),
                    new XAttribute("Groese", groesettext),
                    new XAttribute("Beschreibung", beschreibungtext),
                    new XAttribute("ID", idtext)
                    )
                );
                PlayerXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
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
                string dbpath = Application.dataPath + @"/Scripts/Database/Exported_DBs/Player_table_Num" + generateName + ".sqlite";
                string xmlpath = Application.dataPath + @"/XMLDocuments/Imports/gurbslight_character_export.xml";
                XmlDocument attributeXMLFile = new XmlDocument();
                attributeXMLFile.Load(xmlpath);

                XmlNode selectName = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[0];
                XmlNode selectGeschlecht = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[1];
                XmlNode selectRasse = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[2];
                XmlNode selectHaar = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[3];
                XmlNode selectAugen = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[4];
                XmlNode selectGewicht = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[5];
                XmlNode selectGroese = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[6];
                XmlNode selectBeschreibung = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[7];
                XmlNode selectSpielerID = attributeXMLFile.SelectNodes("/GurpsLightCharacter/Player")[0].ChildNodes[8];

                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (!File.Exists(dbpath))
                {
                    SqliteConnection.CreateFile(dbpath);

                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Spieler(name string, geschlecht string, rasse string, haar string, augen string, gewicht double, groese decimal, beschreibung string, SpielerID int);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = " INSERT INTO Spieler(name, geschlecht, rasse, haar, augen, gewicht, groese, beschreibung, SpielerID)" +
                                           " VALUES(@name, @geschlecht, @rasse, @haar, @augen, @gewicht," +
                                           " @groese, @beschreibung, @SpielerID)";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@name", System.Data.DbType.String).Value = selectName.InnerText;
                        commandinsert.Parameters.Add("@geschlecht", System.Data.DbType.String).Value = selectGeschlecht.InnerText;
                        commandinsert.Parameters.Add("@rasse", System.Data.DbType.String).Value = selectRasse.InnerText;
                        commandinsert.Parameters.Add("@haar", System.Data.DbType.String).Value = selectHaar.InnerText;
                        commandinsert.Parameters.Add("@augen", System.Data.DbType.String).Value = selectAugen.InnerText;
                        commandinsert.Parameters.Add("@gewicht", System.Data.DbType.Double).Value = selectGewicht.InnerText;
                        commandinsert.Parameters.Add("@groese", System.Data.DbType.Decimal).Value = selectGroese.InnerText;
                        commandinsert.Parameters.Add("@beschreibung", System.Data.DbType.String).Value = selectBeschreibung.InnerText;
                        commandinsert.Parameters.Add("@SpielerID", System.Data.DbType.Int32).Value = selectSpielerID.InnerText;

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
