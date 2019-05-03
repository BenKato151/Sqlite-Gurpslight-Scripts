using System;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteAttributeController
{
    public class SqliteAttributeController : MonoBehaviour {
    
        #region ConnectionVars
        static SqliteConnection dbConnection;
        //absolute path required
        private string databasepath;
        private readonly string relativePath = @"/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        //Database Query
        static private readonly string table = "Attribute";
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
                string insertIntoWaffen = " INSERT INTO Attribute(Staerke, Geschicklichkeit, Intelligenz, Konstitution, ID) " +
                                          " VALUES(@starke, @geschicklichkeit, @intelligenz, @konstitution, @id);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
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
        public void DeleteColumns()
        {
            try
            {
                string deleteColumn = " DELETE FROM Attribute " +
                                      " WHERE ID = @id;";

                SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);
                Command.Parameters.Add("@id", System.Data.DbType.Int32).Value = FieldDelete.text;

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

                XDocument abwehrXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XElement("Staerke", staerketext),
                    new XElement("Geschicklichkeit", geschicklichkeittext),
                    new XElement("Intelligenz", intelligenztext),
                    new XElement("Konstitution", konstititiontext),
                    new XElement("ID", idtext)
                    )
                );
                abwehrXML.Save(Application.dataPath + "/XMLDocuments/" + table + "_export.xml");
                console_msg.text = "Export XML in column:\n         " + table
                                 + "\ncompleted!\n"
                                 + "saved file in: \n"
                                 + Application.dataPath + "/XMLDocuments/" + table + "_export.xml";
            }
            catch (Exception e)
            {
                Debug.Log(e);
                console_msg.text = "Error:\nFailed to export values!";
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