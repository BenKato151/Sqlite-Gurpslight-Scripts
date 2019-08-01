using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace AttributskostenController
{
    public class SqliteAttributskostenController : MonoBehaviour
    {

        #region SqlVars
        //Database Query
        private readonly string table = "Attributskosten";
        #endregion

        #region InputVars
        public Text FieldKosten;
        public Text FieldWert;
        public Text FieldID;
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
                string insertIntoAttributskosten = " INSERT INTO Attributskosten(Wert, Kosten, ID) " +
                                          " VALUES(@wert, @kosten, @id);";

                SqliteCommand command = new SqliteCommand(insertIntoAttributskosten, SqliteConnectionManager.dbConnection);
                command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = FieldWert.text;
                command.Parameters.Add("@kosten", System.Data.DbType.Int32).Value = FieldKosten.text;
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = FieldID.text;

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
                string updatecommand = " UPDATE Attributskosten " +
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
                string selecting = "SELECT * FROM Attributskosten WHERE ID = " + FieldSelectID.text;
                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    sqlOutput_msg.text = "Wert: " + output["Wert"] + "\n" +
                                         "Kosten: " + output["Kosten"] + "\n" +
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
                string deleteColumn = " DELETE FROM Attributskosten " +
                                      " WHERE ID = @id";

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
                string werttext = "";
                string kostentext = "";
                string idtext = "";
                string selecting = "SELECT * FROM Attributskosten";

                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();
                
                while (output.Read())
                {
                    werttext = "" + output["Wert"];
                    kostentext = "" + output["Kosten"];
                    idtext = "" + output["ID"];
                }

                XDocument attriKostenXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XAttribute("Wert", werttext),
                    new XAttribute("Kosten", kostentext),
                    new XAttribute("ID", idtext)
                    )
                );
                attriKostenXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
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

                XmlNode selectKosten = attributeXMLFile.SelectNodes("/Characterbogen/Attributskosten")[0].ChildNodes[0];
                XmlNode selectWert = attributeXMLFile.SelectNodes("/Characterbogen/Attributskosten")[0].ChildNodes[1];
                XmlNode selectID = attributeXMLFile.SelectNodes("/Characterbogen/Attributskosten")[0].ChildNodes[2];

                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (!File.Exists(dbpath))
                {
                    SqliteConnection.CreateFile(dbpath);

                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Attributkosten(Wert int, Kosten int, ID int);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = " INSERT INTO Attributkosten(Wert, Kosten, ID) " +
                                          " VALUES(@wert, @kosten, @id);";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@wert", System.Data.DbType.Int32).Value = selectKosten.InnerText;
                        commandinsert.Parameters.Add("@kosten", System.Data.DbType.Int32).Value = selectWert.InnerText;
                        commandinsert.Parameters.Add("@id", System.Data.DbType.Int32).Value = selectID.InnerText;

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