using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class ImportAll : MonoBehaviour {
    
    #region ImportXML
    public void ImportXML()
    {
        //Unterricht
        try
        {
            string dbpath = Application.dataPath + @"/Scripts/Database/new_Char_Bogen1.sqlite";
            string xmlpath = Application.dataPath + @"/XMLDocuments/Exports/char.xml";

            if (File.Exists(dbpath) && File.Exists(xmlpath))
            {
                #region XML
                XmlDocument xmlFile = new XmlDocument();
                xmlFile.Load(xmlpath);

                List<string> keys = new List<string>();
                

                XmlNodeList selectall = xmlFile.SelectNodes("/Characterbogen");

                #endregion
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
                            for (int k = 0; k < node.ChildNodes[i].ChildNodes.Count; k++)
                            {
                                string column = node.ChildNodes[i].ChildNodes[k].Name.ToString();
                                string value = node.ChildNodes[i].ChildNodes[k].InnerText.ToString();
                                string commandstring = "INSERT INTO " + table + " (" + column + ") VALUES(" + value+ ")";
                                SqliteCommand command = new SqliteCommand(commandstring, dbconnection);
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    Debug.Log("Success");

                }
                #endregion
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.Log("Error:\nFailed to connect or to import XML!");
        }
    }
    #endregion

}
