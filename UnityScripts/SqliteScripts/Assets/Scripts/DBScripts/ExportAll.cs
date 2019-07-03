using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class ExportAll : MonoBehaviour {

    public void Export()
    {
        #region Vars
        SqliteConnection dbConnection;
        //absolute path required
        string databasepath;
        string xmlpath;
        string relativePathDB = @"/Scripts/Database/new_Char_Bogen1.sqlite";
        string relativePathXML = @"/XMLDocuments/Exports/";

        databasepath = Application.dataPath + relativePathDB;
        xmlpath = Application.dataPath + relativePathXML;
        
        #endregion

        try
        {
            dbConnection = new SqliteConnection("Data Source = " + databasepath + "; " + " Version = 3;");
            dbConnection.Open();

            

            if (File.Exists(databasepath))
            {
                if (dbConnection != null)
                {
                    XName root = "Characterbogen";
                    XDocument file = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XComment("Gurbs Light Char"),
                        new XElement(root)
                    );
                    
                    List<string> attributesnames = new List<string>();
                    List<string> tablenames = new List<string>();
                    List<string> dbvalues = new List<string>();
                    string selectall = "SELECT * FROM Abwehr, Fertigkeiten, Spieler;";
                    SqliteCommand command = new SqliteCommand(selectall, dbConnection);
                    SqliteDataReader output = command.ExecuteReader();

                    while (output.Read())
                    {
                        for (int i = 0; i < output.FieldCount; i++)
                        {
                            int rand = UnityEngine.Random.Range(0, 100);
                            attributesnames.Insert(i, output.GetName(i).ToString());
                            dbvalues.Insert(i, output[i].ToString());

                            //Debug.Log("Attribut: " + Search(i, attributesnames) + "\n Value: " + Search(i, dbvalues));
                            //NOT FINISHED
                            file.Root.Add(
                                new XElement("Character",
                                new XAttribute(Search(i, attributesnames) + rand.ToString(), Search(i, dbvalues)))
                                );
                        }

                    }
                    file.Save(xmlpath + "char.xml");
                    Debug.Log("Successfully exported!");

                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.Log("Error:\nFailed to connect or to export XML!");
        }
    }

    private string Search(int index, List<string> rows)
    {
        string returnValue = "";
        for (int i = 0; i < rows.Count; i++)
        {
            if (i == index)
            {                
                returnValue = rows[i].ToString();
            }
        }
        return returnValue;
    }
}
