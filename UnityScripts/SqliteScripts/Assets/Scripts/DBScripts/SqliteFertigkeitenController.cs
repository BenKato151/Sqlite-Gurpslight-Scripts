using System;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

namespace SqliteFertigkeitencontroller
{
    public class SqliteFertigkeitenController : MonoBehaviour {

        #region SqlConnection Vars
        static SqliteConnection dbConnection;
        private string databasepath;
        private readonly string relativePath = @"/Scripts/Database/new_Char_Bogen1.sqlite";
        #endregion

        #region SqlVars
        //Database Query
        static private readonly string table = "Fertigkeiten";
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
                string insertIntoWaffen = " INSERT INTO Fertigkeiten(CP, ID, FW, Art, Typ, Name) " +
                                          " VALUES(@cp, @id, @fw, @art, @typ, @name);";

                SqliteCommand Command = new SqliteCommand(insertIntoWaffen, dbConnection);
                Command.Parameters.Add("@cp", System.Data.DbType.Int32).Value = FieldCP.text;
                Command.Parameters.Add("@id", System.Data.DbType.Int32).Value = FieldID.text;
                Command.Parameters.Add("@fw", System.Data.DbType.Int32).Value = FieldFW.text;
                Command.Parameters.Add("@art", System.Data.DbType.String).Value = FieldArt.text;
                Command.Parameters.Add("@typ", System.Data.DbType.String).Value = FieldTyp.text;
                Command.Parameters.Add("@name", System.Data.DbType.String).Value = Fieldname.text;

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
                string updatecommand = " UPDATE Fertigkeiten " +
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

        #region Select
        public void SelectingColumns()
        {
            try
            {
                string selecting = "SELECT * FROM Fertigkeiten WHERE ID = " + FieldSelectID.text;
                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
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

                SqliteCommand Command = new SqliteCommand(deleteColumn, dbConnection);
                Command.Parameters.Add("@idwert", System.Data.DbType.Int32).Value = FieldDelete.text;

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
                string cptext = "";
                string fwtext = "";
                string arttext = "";
                string typtext = "";
                string nametext = "";
                string idtext = "";
                string selecting = "SELECT * FROM Fertigkeiten";

                SqliteCommand command = new SqliteCommand(selecting, dbConnection);
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

                XDocument abwehrXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment(" Table: " + table + " "),
                new XElement("table_" + table,
                    new XElement("Name", nametext),
                    new XElement("CP", cptext),
                    new XElement("FW", fwtext),
                    new XElement("Art", arttext),
                    new XElement("Typ", typtext),
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
