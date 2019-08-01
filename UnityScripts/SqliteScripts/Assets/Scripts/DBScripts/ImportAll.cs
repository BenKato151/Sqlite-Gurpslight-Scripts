using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class ImportAll : MonoBehaviour {
    public Text console_msg;

    #region ImportXML
    public void ImportXML()
    {
        //Unterricht
        try
        {
            string dbpath = SqliteConnectionManager.dbpath;
            string xmlpath = Application.dataPath + @"/XMLDocuments/Exports/char.xml";

            if (File.Exists(dbpath) && File.Exists(xmlpath))
            {
                XmlDocument xmlFile = new XmlDocument();
                xmlFile.Load(xmlpath);                

                XmlNodeList selectall = xmlFile.SelectNodes("/Characterbogen");
                
                #region SQLite
                SqliteConnection dbconnection = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (dbconnection != null)
                {
                    dbconnection.Open();

                    foreach (XmlNode node in selectall)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            string table = node.ChildNodes[i].Name.ToString();
                            List<string> column = new List<string>();
                            List<string> value = new List<string>();

                            for (int k = 0; k < node.ChildNodes[i].ChildNodes.Count; k++)
                            {
                                column.Add(node.ChildNodes[i].ChildNodes[k].Name.ToString());
                                value.Add(node.ChildNodes[i].ChildNodes[k].InnerText.ToString());
                            }

                            string commandstring = "INSERT INTO " + table + " (" + string.Join(", ",column.ToArray()) + ") VALUES('" + string.Join("', '", value.ToArray())+ "')";
                            SqliteCommand command = new SqliteCommand(commandstring, dbconnection);
                            command.ExecuteNonQuery();
                        }
                    }
                    console_msg.text = "Successfully imported!";
                }
                #endregion
            }
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Abort due to constraint violation"))
            {
                console_msg.text = "Error:\nFailed to import XML because of constraints.";
            }
            else
            {
                console_msg.text = "Error:\nFailed to connect or to import XML!";
            }
            Debug.LogError(e);
        }
    }
    #endregion

}
