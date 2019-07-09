using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml.Serialization;
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

                XmlNode select_wert_from_attributskosten = xmlFile.SelectSingleNode("/Characterbogen/Attributskosten/Wert");
                XmlNode select_kosten_from_attributskosten = xmlFile.SelectSingleNode("/Characterbogen/Attributskosten/Kosten");
                XmlNode select_id_from_attributskosten = xmlFile.SelectSingleNode("/Characterbogen/Attributskosten/ID");

                #endregion
                #region SQLite
                SqliteConnection dbconnection = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
                if (dbconnection != null)
                {
                    dbconnection.Open();

                    string insertinto = "INSERT INTO Attributskosten(Wert, Kosten, ID) VALUES (@wert, @kosten, @id)";
                    SqliteCommand command = new SqliteCommand(insertinto, dbconnection);
                    command.Parameters.Add("@wert", System.Data.DbType.Int32).Value = select_wert_from_attributskosten.InnerText;
                    command.Parameters.Add("@kosten", System.Data.DbType.Int32).Value = select_kosten_from_attributskosten.InnerText;
                    command.Parameters.Add("@id", System.Data.DbType.Int32).Value = select_id_from_attributskosten.InnerText;

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

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
