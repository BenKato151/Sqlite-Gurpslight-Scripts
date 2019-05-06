using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteAbwehrController
{
    public class SqliteAbwehrController : MonoBehaviour {
        
        #region ConnectionVars
        private static SqliteConnection dbConnection;
        //absolute path required
        private static string databasepath;
        private static readonly string relativePath = @"/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVar        
        //Database Query
        private static readonly string table = "Abwehr";
        #endregion

        #region InputVars
        public static Text FieldSchild;
        public static Text FieldRuestung;
        public static Text FieldUmhang;
        public static Text FieldID;
        public static Text FieldBekannterWert;
        public static Text FieldSelect;
        #endregion

        #region MsgVars
        public static Text console_msg;
        public static Text sqlOutput_msg;
        #endregion

        #region UpdateVars
        public Text Fieldwert;
        public Text Fieldcolumn;
        public Text FieldIDvalue;
        #endregion

        #region Insert
        public static void InsertingValues()
        {
            try
            {
                string insertIntoWaffen = " INSERT INTO Abwehr(Schild, Ruestung, Umhang, Gesamt, ID) " +
                                          " VALUES(@schild, @ruestung, @umhang, @gesamt, @ID);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@schild", System.Data.DbType.Int32).Value = FieldSchild.text;
                Command.Parameters.Add("@ruestung", System.Data.DbType.Int32).Value = FieldRuestung.text;
                Command.Parameters.Add("@umhang", System.Data.DbType.Int32).Value = FieldUmhang.text;
                Command.Parameters.Add("@gesamt", System.Data.DbType.Int32).Value = Int32.Parse(FieldRuestung.text) + Int32.Parse (FieldSchild.text) + Int32.Parse(FieldUmhang.text);
                Command.Parameters.Add("@ID", System.Data.DbType.Int32).Value = Int32.Parse(FieldID.text);

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

        #region Select
        public static void SelectingColumns()
        {
            try
            {
                string selecting = "SELECT * FROM Abwehr WHERE ID = " + FieldSelect.text;
                
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    sqlOutput_msg.text = "Schild: " + output["Schild"] + "\n" +
                                         "Rüstung: " + output["Ruestung"] + "\n" +
                                         "Umhang: " + output["Umhang"] + "\n" +
                                         "Gesamt: " + output["Gesamt"] + "\n" +
                                         "ID: " + output["ID"];

                }
                console_msg.text = "Searching in column:\n         " + table 
                                 + "\ncompleted!";

                if (sqlOutput_msg.text.Length < 1)
                {
                    console_msg.text = "Error:\nFailed to search values!\nID: " 
                                     + FieldSelect.text + " is out of range.";
                }
            }
            
            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to search values!";
            }
        }
        #endregion

        #region Update
        public void UpdatingValues()
        {
            try
            {
                string updatecommand = " UPDATE Abwehr " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE ID = @IDvalue;";

                SqliteCommand Command = new SqliteCommand(updatecommand, dbConnection);
                Command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = Fieldwert.text;
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

        #region DELETE
        public static void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Abwehr " +
                                      " WHERE ID = @wert;";

                SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);
                Command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = FieldBekannterWert.text;

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
        public static void ConnectionDB()
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
        public static void ExportXML()
        {
            try
            {
                string schildtext = "";
                string ruestungtext = "";
                string umhangtext = "";
                string gesamttext = "";
                string idtext = "";
                string selecting = "SELECT * FROM Abwehr";

                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    schildtext = "" + output["Schild"];
                    ruestungtext = "" + output["Ruestung"];
                    umhangtext = "" + output["Umhang"];
                    gesamttext = "" + output["Gesamt"];
                    idtext = "" + output["ID"];
                }

                XDocument abwehrXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XElement("Schild", schildtext),
                    new XElement("Ruestung", ruestungtext),
                    new XElement("Umhang", umhangtext),
                    new XElement("Gesamt", gesamttext),
                    new XElement("ID", idtext)
                    )
                );
                abwehrXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
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
                //Unterricht
            try
            {
                string dbpath = Application.dataPath + @"/Scripts/Database/new.sqlite";
                string xmlpath = Application.dataPath + @"/XMLDocuments/Imports/gurbslight_character_export.xml";
                XmlDocument abwehrtableFile = new XmlDocument();
                abwehrtableFile.Load(xmlpath);
                //XmlDocument document = new XmlDocument().LoadXml(abwehrtableFile);
                XmlNode selectabwehrschild =  abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[0];
                XmlNode selectabwehrruestung = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[1];
                XmlNode selectabwehrumhang = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[2];
                XmlNode selectabwehrgesamt = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[3];
                XmlNode selectabwehrid = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[4];


                SqliteConnection.CreateFile(dbpath);
                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (File.Exists(dbpath))
                {
                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Abwehr(Schild int, ruestung int, umhang int, gesamt int , ID int);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable,dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = "INSERT INTO Abwehr(Schild, ruestung, umhang, gesamt, ID) VALUES(@schild, @ruestung, @umhang, @gesamt, @id);";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@schild", System.Data.DbType.Int32).Value = selectabwehrschild.InnerText;
                        commandinsert.Parameters.Add("@ruestung", System.Data.DbType.Int32).Value = selectabwehrruestung.InnerText;
                        commandinsert.Parameters.Add("@umhang", System.Data.DbType.Int32).Value = selectabwehrumhang.InnerText;
                        commandinsert.Parameters.Add("@gesamt", System.Data.DbType.Int32).Value = selectabwehrgesamt.InnerText;
                        commandinsert.Parameters.Add("@id", System.Data.DbType.Int32).Value = selectabwehrid.InnerText;

                        commandinsert.ExecuteNonQuery();
                        commandinsert.Parameters.Clear();

                        Debug.Log("Success!");
                       
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }




















        }
        #endregion

        #region Exit
        public static void Exit()
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