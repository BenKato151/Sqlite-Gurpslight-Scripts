using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteFertigkeitencontroller
{
    public class SqliteFertigkeitenController : MonoBehaviour
    {

        #region SqlVars
        //Database Query
        private readonly string table = "Fertigkeiten";
        #endregion

        #region InputVars
        public Text Fieldname;
        public Text FieldCP;
        public Text FieldID;
        public Text FieldFW;
        public Text FieldArt;
        public Text FieldTyp;
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
                string insertIntoFertigkeiten = " INSERT INTO Fertigkeiten(CP, ID, FW, Art, Typ, Name) " +
                                          " VALUES(@cp, @id, @fw, @art, @typ, @name);";

                SqliteCommand command = new SqliteCommand(insertIntoFertigkeiten, SqliteConnectionManager.dbConnection);
                command.Parameters.Add("@cp", System.Data.DbType.Int32).Value = FieldCP.text;
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = FieldID.text;
                command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = FieldFW.text;
                command.Parameters.Add("@art", System.Data.DbType.String).Value = FieldArt.text;
                command.Parameters.Add("@typ", System.Data.DbType.String).Value = FieldTyp.text;
                command.Parameters.Add("@name", System.Data.DbType.String).Value = Fieldname.text;

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
                string updatecommand = " UPDATE Fertigkeiten " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE ID = @IDvalue;";

                SqliteCommand command = new SqliteCommand(updatecommand, SqliteConnectionManager.dbConnection);
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

        #region Select
        public void SelectingColumns()
        {
            try
            {
                sqlOutput_msg.text = "";
                string selecting = "SELECT * FROM Fertigkeiten WHERE ID = " + FieldSelectID.text;
                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    sqlOutput_msg.text = "Fertigkeiten Name: " + output["Name"] + "\n" +
                    "Fertigkeiten Typ: " + output["Typ"] + "\n" +
                    "Fertigkeiten Art: " + output["Art"] + "\n" +
                    "CP: " + output["CP"] + "\n" +
                    "FW: " + output["FW"] + "\n" +
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
                string deleteColumn = " DELETE FROM Fertigkeiten " +
                                      " WHERE ID = @idwert;";

                SqliteCommand command = new SqliteCommand(deleteColumn, SqliteConnectionManager.dbConnection);
                command.Parameters.Add("@idwert", System.Data.DbType.Int32).Value = FieldDelete.text;

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
                string cptext = "";
                string fwtext = "";
                string arttext = "";
                string typtext = "";
                string nametext = "";
                string idtext = "";
                string selecting = "SELECT * FROM Fertigkeiten";

                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    cptext = "" + output["CP"];
                    fwtext = "" + output["FW"];
                    arttext = "" + output["Art"];
                    typtext = "" + output["Typ"];
                    nametext = "" + output["Name"];
                    idtext = "" + output["ID"];
                }

                XDocument FertigkeitenXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XAttribute("Name", nametext),
                    new XAttribute("CP", cptext),
                    new XAttribute("FW", fwtext),
                    new XAttribute("Art", arttext),
                    new XAttribute("Typ", typtext),
                    new XAttribute("ID", idtext)
                    )
                );
                FertigkeitenXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
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

                XmlNode selectCP = attributeXMLFile.SelectNodes("/Characterbogen/Fertigkeiten")[0].ChildNodes[0];
                XmlNode selectID = attributeXMLFile.SelectNodes("/Characterbogen/Fertigkeiten")[0].ChildNodes[1];
                XmlNode selectFW = attributeXMLFile.SelectNodes("/Characterbogen/Fertigkeiten")[0].ChildNodes[2];
                XmlNode selectArt = attributeXMLFile.SelectNodes("/Characterbogen/Fertigkeiten")[0].ChildNodes[3];
                XmlNode selectTyp = attributeXMLFile.SelectNodes("/Characterbogen/Fertigkeiten")[0].ChildNodes[4];
                XmlNode selectName = attributeXMLFile.SelectNodes("/Characterbogen/Fertigkeiten")[0].ChildNodes[5];

                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (!File.Exists(dbpath))
                {
                    SqliteConnection.CreateFile(dbpath);

                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Fertigkeiten(CP int, ID int, FW int, Art string, Typ string, Name string);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = " INSERT INTO Fertigkeiten(CP, ID, FW, Art, Typ, Name) " +
                                          " VALUES(@cp, @id, @fw, @art, @typ, @name);";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@cp", System.Data.DbType.Int32).Value = selectCP.InnerText;
                        commandinsert.Parameters.Add("@id", System.Data.DbType.Int32).Value = selectID.InnerText;
                        commandinsert.Parameters.Add("@fw", System.Data.DbType.Int32).Value = selectFW.InnerText;
                        commandinsert.Parameters.Add("@art", System.Data.DbType.String).Value = selectArt.InnerText;
                        commandinsert.Parameters.Add("@typ", System.Data.DbType.String).Value = selectTyp.InnerText;
                        commandinsert.Parameters.Add("@name", System.Data.DbType.String).Value = selectName.InnerText;

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
