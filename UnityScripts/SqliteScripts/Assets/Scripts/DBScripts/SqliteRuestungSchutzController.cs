using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteRuestungSchutzController
{
    public class SqliteRuestungSchutzController : MonoBehaviour
    {
        #region SqlVars
        private readonly string table = "Ruestung_Schutz";
        #endregion

        #region InputVars
        public Text FieldOrt;
        public Text FieldSR;
        public Text FieldPV;
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
                string insertIntoruestungSchutz = " INSERT INTO Ruestung_Schutz(Ort, SR, PV, ID) " +
                                          " VALUES(@Ort, @SR, @PV, @ID);";

                SqliteCommand command = new SqliteCommand(insertIntoruestungSchutz, SqliteConnectionManager.dbConnection);
                command.Parameters.Add("@Ort", System.Data.DbType.String).Value = FieldOrt.text;
                command.Parameters.Add("@SR", System.Data.DbType.Int32).Value = FieldSR.text;
                command.Parameters.Add("@PV", System.Data.DbType.Int32).Value = FieldPV.text;
                command.Parameters.Add("@ID", System.Data.DbType.Int32).Value = FieldID.text;

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
                string updatecommand = " UPDATE Ruestung_Schutz " +
                                       " SET " + Fieldcolumn.text + " = @wert " +
                                       " WHERE ID = @IDvalue;";

                SqliteCommand command = new SqliteCommand(updatecommand, SqliteConnectionManager.dbConnection);
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
                sqlOutput_msg.text = "";
                string selecting = "SELECT * FROM Ruestung_Schutz WHERE ID = " + FieldSelectID.text;
                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {

                    sqlOutput_msg.text = "Ort: " + output["Ort"] + "\n" +
                                         "SR: " + output["SR"] + "\n" +
                                         "PV: " + output["PV"] + "\n" +
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
                string deleteColumn = " DELETE FROM Ruestung_Schutz " +
                                      " WHERE ID = @idwert";

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
                string orttext = "";
                string srtext = "";
                string pvtext = "";
                string idtext = "";
                string selecting = "SELECT * FROM Ruestung_Schutz";

                SqliteCommand command = new SqliteCommand(selecting, SqliteConnectionManager.dbConnection);
                SqliteDataReader output = command.ExecuteReader();

                while (output.Read())
                {
                    orttext = "" + output["Ort"];
                    srtext = "" + output["SR"];
                    pvtext = "" + output["PV"];
                    idtext = "" + output["ID"];
                }

                XDocument ruestungSchutzXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XAttribute("Ort", orttext),
                    new XAttribute("SR", srtext),
                    new XAttribute("PV", pvtext),
                    new XAttribute("ID", idtext)
                    )
                );
                ruestungSchutzXML.Save(Application.dataPath + "/XMLDocuments/Exports/" + table + "_export.xml");
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

                XmlNode selectOrt = attributeXMLFile.SelectNodes("/Characterbogen/RüstungSchutz")[0].ChildNodes[0];
                XmlNode selectSR = attributeXMLFile.SelectNodes("/Characterbogen/RüstungSchutz")[0].ChildNodes[1];
                XmlNode selectPV = attributeXMLFile.SelectNodes("/Characterbogen/RüstungSchutz")[0].ChildNodes[2];
                XmlNode selectID = attributeXMLFile.SelectNodes("/Characterbogen/RüstungSchutz")[0].ChildNodes[3];
                
                SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (!File.Exists(dbpath))
                {
                    SqliteConnection.CreateFile(dbpath);

                    if (dbconnect != null)
                    {
                        dbconnect.Open();
                        string createtable = "CREATE TABLE Ruestung_Schutz(Ort string, SR int, PV int, ID int, augen string, gewicht double, groese decimal, beschreibung string, SpielerID int);";
                        SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                        commandCreateTable.ExecuteNonQuery();

                        string insertinto = " INSERT INTO Ruestung_Schutz(Ort, SR, PV, ID) " +
                                          " VALUES(@Ort, @SR, @PV, @ID);";
                        SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                        commandinsert.Parameters.Add("@Ort", System.Data.DbType.String).Value = selectOrt.InnerText;
                        commandinsert.Parameters.Add("@SR", System.Data.DbType.String).Value = selectSR.InnerText;
                        commandinsert.Parameters.Add("@PV", System.Data.DbType.String).Value = selectPV.InnerText;
                        commandinsert.Parameters.Add("@ID", System.Data.DbType.String).Value = selectID.InnerText;
                        
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