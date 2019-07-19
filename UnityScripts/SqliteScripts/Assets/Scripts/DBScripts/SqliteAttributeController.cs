using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteAttributeController
{
    public class SqliteAttributeController : MonoBehaviour {
    
        #region ConnectionVars
        private static SqliteConnection dbConnection;
        //absolute path required
        private string databasepath;
        private readonly string relativePath = @"/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        //Database Query
        private readonly string table = "Attribute";
        #endregion

        #region InputVars
        public Text FieldStaerke;
        public Text FieldGeschickl;
        public Text FieldIntelli;
        public Text FieldKonst;
        public Text FieldDelete;
        public Text FieldSelectID;
        public Text FieldID;
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
                string insertIntoAttribute = " INSERT INTO Attribute(Staerke, Geschicklichkeit, Intelligenz, Konstitution, ID) " +
                                          " VALUES(@starke, @geschicklichkeit, @intelligenz, @konstitution, @id);";

                SqliteCommand Command = new SqliteCommand(insertIntoAttribute, dbConnection);
                Command.Parameters.Add("@starke", System.Data.DbType.Int32).Value = Int32.Parse(FieldStaerke.text);
                Command.Parameters.Add("@geschicklichkeit", System.Data.DbType.Int32).Value = Int32.Parse(FieldGeschickl.text);
                Command.Parameters.Add("@intelligenz", System.Data.DbType.Int32).Value = Int32.Parse(FieldIntelli.text);
                Command.Parameters.Add("@konstitution", System.Data.DbType.Int32).Value = Int32.Parse(FieldKonst.text);
                Command.Parameters.Add("@id", System.Data.DbType.Int32).Value = Int32.Parse(FieldID.text);

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
        public void SelectingColumns()
        {
            try
            {
                sqlOutput_msg.text = "";
                string selecting = "SELECT * FROM Attribute WHERE ID = " + FieldSelectID.text;
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    sqlOutput_msg.text = "Stärke: " + output["Staerke"] + "\n" +
                                         "Geschicklichkeit: " + output["Geschicklichkeit"] + "\n" +
                                         "Intelligenz: " + output["Intelligenz"] + "\n" +
                                         "Konstitution: " + output["Konstitution"] + "\n" +
                                         "ID: " + output["ID"];
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

        #region Update
        public void UpdatingValues()
        {
            try
            {
                string updatecommand = " UPDATE Attribute " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE ID = @IDvalue;";

                SqliteCommand command = new SqliteCommand(updatecommand, dbConnection);
                command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = Fieldwert.text;
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

        #region DELETE
        public void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Attribute " +
                                      " WHERE ID = @id;";

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
                string staerketext = "";
                string geschicklichkeittext = "";
                string intelligenztext = "";
                string konstititiontext = "";
                string idtext = "";
                string selecting = "SELECT * FROM Attribute";

                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    staerketext = "" + output["Staerke"];
                    geschicklichkeittext = "" + output["Geschicklichkeit"];
                    intelligenztext = "" + output["Intelligenz"];
                    konstititiontext = "" + output["Konstitution"];
                    idtext = "" + output["ID"];
                }

                XDocument attributeXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XAttribute("Staerke", staerketext),
                    new XAttribute("Geschicklichkeit", geschicklichkeittext),
                    new XAttribute("Intelligenz", intelligenztext),
                    new XAttribute("Konstitution", konstititiontext),
                    new XAttribute("ID", idtext)
                    )
                );
                attributeXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
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
                string dbpath = Application.dataPath + @"/Scripts/Database/" + table + "_table_" + generateName.ToString() + ".sqlite";
                string xmlpath = Application.dataPath + @"/XMLDocuments/Exports/char.xml";
                XmlDocument attributeXMLFile = new XmlDocument();
                attributeXMLFile.Load(xmlpath);

                XmlNode selectStaerke = attributeXMLFile.SelectNodes("/Characterbogen/Attribute")[0].ChildNodes[0];
                XmlNode selectGeschicklichkeit = attributeXMLFile.SelectNodes("/Characterbogen/Attribute")[0].ChildNodes[1];
                XmlNode selectIntelligenz = attributeXMLFile.SelectNodes("/Characterbogen/Attribute")[0].ChildNodes[2];
                XmlNode selectKonstitution = attributeXMLFile.SelectNodes("/Characterbogen/Attribute")[0].ChildNodes[3];
                XmlNode selectid = attributeXMLFile.SelectNodes("/Characterbogen/Attribute")[0].ChildNodes[4];

                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (!File.Exists(dbpath))
                {
                    SqliteConnection.CreateFile(dbpath);

                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Attribute(Staerke int, Geschicklichkeit int, Intelligenz int, Konstitution int , ID int);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = " INSERT INTO Attribute(Staerke, Geschicklichkeit, Intelligenz, Konstitution, ID) " +
                                          " VALUES(@starke, @geschicklichkeit, @intelligenz, @konstitution, @id);";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@starke", System.Data.DbType.Int32).Value = selectStaerke.InnerText;
                        commandinsert.Parameters.Add("@geschicklichkeit", System.Data.DbType.Int32).Value = selectGeschicklichkeit.InnerText;
                        commandinsert.Parameters.Add("@intelligenz", System.Data.DbType.Int32).Value = selectIntelligenz.InnerText;
                        commandinsert.Parameters.Add("@konstitution", System.Data.DbType.Int32).Value = selectKonstitution.InnerText;
                        commandinsert.Parameters.Add("@id", System.Data.DbType.Int32).Value = selectid.InnerText;

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
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                    console_msg.text = "\nConnection closed!";
                    sqlOutput_msg.text = " ";

                }
                else
                {
                    console_msg.text = "No connection to close";
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Object reference not set to an instance of an object"))
                {
                    console_msg.text = "No connection to close";
                }
                else
                {
                    console_msg.text = "Error:\nFailed to close the connection!";
                    Debug.LogError(e);
                }
            }

        }
        #endregion

    }
}