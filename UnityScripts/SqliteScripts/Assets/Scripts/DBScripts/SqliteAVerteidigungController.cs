using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteAVerteidigungcontroller
{
    public class SqliteAVerteidigungController : MonoBehaviour
    {
        
        #region SqlVars
        //Database Query
        private readonly string table = "Aktive_Verteidigung";

        #endregion

        #region MsgVars
        public Text console_msg;
        public Text sqlOutput_msg;
        #endregion

        #region InputVars
        public Text FieldParieren;
        public Text FieldAusweichen;
        public Text FieldAbblocken;
        public Text FieldAbblockenUmh;
        public Text FieldID;
        public Text FieldDelete;
        public Text FieldSelectID;
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
                string insertIntoAVerteidigung = " INSERT INTO Aktive_Verteidigung(Parieren, Ausweichen, " +
                                          " Abblocken, AblockenUmh, ID) " +
                                          "VALUES(@parieren, @ausweichen, @abblocken, @abblockenUmh, @idwert)" +
                                          ";";

                SqliteCommand command = new SqliteCommand(insertIntoAVerteidigung, SqliteConnectionManager.dbConnection);
                command.Parameters.Add("@parieren", System.Data.DbType.Int32).Value = FieldParieren.text;
                command.Parameters.Add("@ausweichen", System.Data.DbType.Int32).Value = FieldAusweichen.text;
                command.Parameters.Add("@abblocken", System.Data.DbType.Int32).Value = FieldAbblocken.text;
                command.Parameters.Add("@abblockenUmh", System.Data.DbType.String).Value = FieldAbblockenUmh.text;
                command.Parameters.Add("@idwert", System.Data.DbType.Int32).Value = FieldID.text;

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
                string updatecommand = " UPDATE Aktive_Verteidigung " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE ID = @IDvalue;";

                SqliteCommand Command = new SqliteCommand(updatecommand, SqliteConnectionManager.dbConnection);
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

        #region Select
        public void SelectingColumns()
        {
            try
            {
                sqlOutput_msg.text = "";
                string selecting = "SELECT * FROM Aktive_Verteidigung WHERE ID = " + FieldSelectID.text;
                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    sqlOutput_msg.text = "Parieren: " + output["Parieren"] + "\n" +
                                         "Ausweichen: " + output["Ausweichen"] + "\n" +
                                         "Abblocken: " + output["Abblocken"] + "\n" +
                                         "Abblocken Unhang: " + output["AblockenUmh"] + "\n" +
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

        #region DELETE
        public void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Aktive_Verteidigung " +
                                      " WHERE ID = @id;";

                SqliteCommand command = new SqliteCommand(deleteColumn, SqliteConnectionManager.dbConnection);
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
            SqliteConnectionManager.Connection(console_msg, table);
        }
        #endregion

        #region ExportXML
        public void ExportXML()
        {
            try
            {
                string parierentext = "";
                string ausweichentext = "";
                string abblockentext = "";
                string abblockenUmhtext = "";
                string idtext = "";
                string selecting = "SELECT * FROM Aktive_Verteidigung";

                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    parierentext = "" + output["Parieren"];
                    ausweichentext = "" + output["Ausweichen"];
                    abblockentext = "" + output["Abblocken"];
                    abblockenUmhtext = "" + output["AblockenUmh"];
                    idtext = "" + output["ID"];
                }

                XDocument AVerteidigungXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XAttribute("Parieren", parierentext),
                    new XAttribute("Ausweichen", ausweichentext),
                    new XAttribute("Abblocken", abblockentext),
                    new XAttribute("AblockenUmh", abblockenUmhtext),
                    new XAttribute("ID", idtext)
                    )
                );
                AVerteidigungXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
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
                string dbpath = Application.dataPath + @"/Scripts/Database/Exported_DBs/" + table + "_table_" + generateName.ToString() + ".sqlite";
                string xmlpath = Application.dataPath + @"/XMLDocuments/Exports/char.xml";
                XmlDocument attributeXMLFile = new XmlDocument();
                attributeXMLFile.Load(xmlpath);

                XmlNode selectParieren = attributeXMLFile.SelectNodes("/Characterbogen/AktiveVerteidigung")[0].ChildNodes[0];
                XmlNode selectAusweichen = attributeXMLFile.SelectNodes("/Characterbogen/AktiveVerteidigung")[0].ChildNodes[1];
                XmlNode selectAbblocken = attributeXMLFile.SelectNodes("/Characterbogen/AktiveVerteidigung")[0].ChildNodes[2];
                XmlNode selectAbblockenUmh = attributeXMLFile.SelectNodes("/Characterbogen/AktiveVerteidigung")[0].ChildNodes[2];
                XmlNode selectID = attributeXMLFile.SelectNodes("/Characterbogen/AktiveVerteidigung")[0].ChildNodes[2];

                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (!File.Exists(dbpath))
                {
                    SqliteConnection.CreateFile(dbpath);

                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Aktive_Verteidigung(Parieren int, Ausweichen int, Abblocken int, AblockenUmh, ID int);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = " INSERT INTO Aktive_Verteidigung(Parieren, Ausweichen, " +
                                          " Abblocken, AblockenUmh, ID) " +
                                          "VALUES(@parieren, @ausweichen, @abblocken, @abblockenUmh, @idwert)" +
                                          ";";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@parieren", System.Data.DbType.Int32).Value = selectParieren.InnerText;
                        commandinsert.Parameters.Add("@ausweichen", System.Data.DbType.Int32).Value = selectAusweichen.InnerText;
                        commandinsert.Parameters.Add("@abblocken", System.Data.DbType.Int32).Value = selectAbblocken.InnerText;
                        commandinsert.Parameters.Add("@abblockenUmh", System.Data.DbType.Int32).Value = selectAbblockenUmh.InnerText;
                        commandinsert.Parameters.Add("@idwert", System.Data.DbType.Int32).Value = selectID.InnerText;

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
            SqliteConnectionManager.Exit(console_msg, sqlOutput_msg);
        }
        #endregion

    }
}