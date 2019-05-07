using System;
using System.IO;
using System.Xml;
using UnityEngine;
using Mono.Data.Sqlite;

public class ImportAll : MonoBehaviour {
    
    #region ImportXML
    public void ImportXML()
    {
        //Unterricht
        try
        {
            int zufall = UnityEngine.Random.Range(0, 1000);
            string dbpath = Application.dataPath + @"/Scripts/Database/abwehrtable_Num"+ zufall +  ".sqlite";
            string xmlpath = Application.dataPath + @"/XMLDocuments/Imports/gurbslight_character_export.xml";
            XmlDocument abwehrtableFile = new XmlDocument();
            abwehrtableFile.Load(xmlpath);
            //XmlDocument document = new XmlDocument().LoadXml(abwehrtableFile);
            XmlNode selectabwehrschild = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[0];
            XmlNode selectabwehrruestung = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[1];
            XmlNode selectabwehrumhang = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[2];
            XmlNode selectabwehrgesamt = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[3];
            XmlNode selectabwehrid = abwehrtableFile.SelectNodes("/GurpsLightCharacter/Abwehr")[0].ChildNodes[4];

            SqliteConnection dbconnect = new SqliteConnection("Data Source = " + dbpath + "; " + " Version = 3;");
            if (!File.Exists(dbpath))
            {
                SqliteConnection.CreateFile(dbpath);

                if (dbconnect != null)
                {
                    dbconnect.Open();
                    string createtable = "CREATE TABLE Abwehr(Schild int, ruestung int, umhang int, gesamt int , ID int);";
                    SqliteCommand commandCreateTable = new SqliteCommand(createtable, dbconnect);
                    commandCreateTable.ExecuteNonQuery();

                    string insertinto = "INSERT INTO Abwehr(Schild, ruestung, umhang, gesamt, ID) VALUES(@schild, @ruestung, @umhang, @gesamt, @id);";
                    SqliteCommand commandinsert = new SqliteCommand(insertinto, dbconnect);
                    commandinsert.Parameters.Add("@schild", System.Data.DbType.Int32).Value = selectabwehrschild.InnerText;
                    commandinsert.Parameters.Add("@ruestung", System.Data.DbType.Int32).Value = selectabwehrruestung.InnerText;
                    commandinsert.Parameters.Add("@umhang", System.Data.DbType.Int32).Value = selectabwehrumhang.InnerText;
                    commandinsert.Parameters.Add("@gesamt", System.Data.DbType.Int32).Value = selectabwehrgesamt.InnerText;
                    commandinsert.Parameters.Add("@id", System.Data.DbType.Int32).Value = selectabwehrid.InnerText;

                    commandinsert.ExecuteNonQuery();
                    commandinsert.Parameters.Clear();

                    Debug.Log("Successfully imported into: !" + dbpath);
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

}
