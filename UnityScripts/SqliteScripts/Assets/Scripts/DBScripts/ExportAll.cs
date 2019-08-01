using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class ExportAll : MonoBehaviour {

    public Text console_msg;

    #region ExportXML
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
                    #region XML
                    XName root = "Characterbogen";
                    XName abwehr = "Abwehr";
                    XName aktive_Verteidigung = "Aktive_Verteidigung";
                    XName attribute = "Attribute";
                    XName attributskosten = "Attributskosten";
                    XName fertigkeiten = "Fertigkeiten";
                    XName ruestung_schutz = "Ruestung_Schutz";
                    XName spieler = "Spieler";
                    XName waffen = "Waffen";
                    XDocument file = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XComment("Gurbs Light Char"),
                        new XElement(root, new XElement(spieler), new XElement(abwehr), new XElement(aktive_Verteidigung), 
                            new XElement(attribute), new XElement(attributskosten), new XElement(fertigkeiten), 
                            new XElement(ruestung_schutz), new XElement(waffen)
                        )
                    );
                    #endregion

                    #region Spieler
                    List<string> spielerattributesnames = new List<string>();
                    List<string> spielerdbvalues = new List<string>();
                    string selectSpieler = "SELECT * FROM Spieler;";
                    SqliteCommand commandSpieler = new SqliteCommand(selectSpieler, dbConnection);
                    SqliteDataReader outputSpieler = commandSpieler.ExecuteReader();

                    while (outputSpieler.Read())
                    {
                        for (int i = 0; i < outputSpieler.FieldCount; i++)
                        {
                            spielerattributesnames.Insert(i, outputSpieler.GetName(i).ToString());
                            spielerdbvalues.Insert(i, outputSpieler[i].ToString());
                            //Debug.Log("Attribut: " + Search(i, attributesnames) + "\n Value: " + Search(i, dbvalues));
                            file.Root.Element(spieler).Add(new XElement(Search(i, spielerattributesnames), Search(i, spielerdbvalues)));
                        }
                    }
                    #endregion

                    #region Abwehr
                    List<string> abwehrattributesnames = new List<string>();
                    List<string> abwehrdbvalues = new List<string>();
                    string selectAbwehr = "SELECT * FROM Abwehr;";
                    SqliteCommand commandAbwehr = new SqliteCommand(selectAbwehr, dbConnection);
                    SqliteDataReader outputAbwehr = commandAbwehr.ExecuteReader();

                    while (outputAbwehr.Read())
                    {
                        for (int i = 0; i < outputAbwehr.FieldCount; i++)
                        {
                            abwehrattributesnames.Insert(i, outputAbwehr.GetName(i).ToString());
                            abwehrdbvalues.Insert(i, outputAbwehr[i].ToString());
                            file.Root.Element(abwehr).Add(new XElement(Search(i, abwehrattributesnames), Search(i, abwehrdbvalues)));
                        }
                    }
                    #endregion

                    #region Aktive_Verteidigung
                    List<string> aktive_Verattributesnames = new List<string>();
                    List<string> aktive_Verdbvalues = new List<string>();
                    string selectAktive_Verteidigung = "SELECT * FROM Aktive_Verteidigung;";
                    SqliteCommand commandAktive_Verteigigung = new SqliteCommand(selectAktive_Verteidigung, dbConnection);
                    SqliteDataReader outputAktive_Ver = commandAktive_Verteigigung.ExecuteReader();

                    while (outputAktive_Ver.Read())
                    {
                        for (int i = 0; i < outputAktive_Ver.FieldCount; i++)
                        {
                            aktive_Verattributesnames.Insert(i, outputAktive_Ver.GetName(i).ToString());
                            aktive_Verdbvalues.Insert(i, outputAktive_Ver[i].ToString());
                            file.Root.Element(aktive_Verteidigung).Add(new XElement(Search(i, aktive_Verattributesnames), Search(i, aktive_Verdbvalues)));
                        }
                    }
                    #endregion

                    #region Attribute
                    List<string> attribute_attributesnames = new List<string>();
                    List<string> attribute_dbvalues = new List<string>();
                    string selectAttribute = "SELECT * FROM Attribute;";
                    SqliteCommand commandAttribute = new SqliteCommand(selectAttribute, dbConnection);
                    SqliteDataReader outputAttribute = commandAttribute.ExecuteReader();

                    while (outputAttribute.Read())
                    {
                        for (int i = 0; i < outputAttribute.FieldCount; i++)
                        {
                            attribute_attributesnames.Insert(i, outputAttribute.GetName(i).ToString());
                            attribute_dbvalues.Insert(i, outputAttribute[i].ToString());
                            file.Root.Element(attribute).Add(new XElement(Search(i, attribute_attributesnames), Search(i, attribute_dbvalues)));
                        }
                    }
                    #endregion

                    #region Attributskosten
                    List<string> attributskosten_attributesnames = new List<string>();
                    List<string> attributskosten_dbvalues = new List<string>();
                    string selectAttributskosten = "SELECT * FROM Attributskosten;";
                    SqliteCommand commandAttributskosten = new SqliteCommand(selectAttributskosten, dbConnection);
                    SqliteDataReader outputAttributskosten = commandAttributskosten.ExecuteReader();

                    while (outputAttributskosten.Read())
                    {
                        for (int i = 0; i < outputAttributskosten.FieldCount; i++)
                        {
                            attributskosten_attributesnames.Insert(i, outputAttributskosten.GetName(i).ToString());
                            attributskosten_dbvalues.Insert(i, outputAttributskosten[i].ToString());
                            file.Root.Element(attributskosten).Add(new XElement(Search(i, attributskosten_attributesnames), Search(i, attributskosten_dbvalues)));
                        }
                    }
                    #endregion
                        
                    #region Fertigkeiten
                    List<string> fertigkeiten_attributesnames = new List<string>();
                    List<string> fertigkeiten_dbvalues = new List<string>();
                    string selectFertigkeiten = "SELECT * FROM Fertigkeiten;";
                    SqliteCommand commandFertigkeiten = new SqliteCommand(selectFertigkeiten, dbConnection);
                    SqliteDataReader outputFertigkeiten = commandFertigkeiten.ExecuteReader();

                    while (outputFertigkeiten.Read())
                    {
                        for (int i = 0; i < outputFertigkeiten.FieldCount; i++)
                        {
                            fertigkeiten_attributesnames.Insert(i, outputFertigkeiten.GetName(i).ToString());
                            fertigkeiten_dbvalues.Insert(i, outputFertigkeiten[i].ToString());
                            file.Root.Element(fertigkeiten).Add(new XElement(Search(i, fertigkeiten_attributesnames), Search(i, fertigkeiten_dbvalues)));
                        }
                    }
                    #endregion

                    #region Ruestung_Schutz
                    List<string> ruestung_schutz_attributesnames = new List<string>();
                    List<string> ruestung_schutz_dbvalues = new List<string>();
                    string selectruestung_schutz = "SELECT * FROM Ruestung_Schutz;";
                    SqliteCommand commandruestung_schutz = new SqliteCommand(selectruestung_schutz, dbConnection);
                    SqliteDataReader outputruestung_schutz = commandruestung_schutz.ExecuteReader();

                    while (outputruestung_schutz.Read())
                    {
                        for (int i = 0; i < outputruestung_schutz.FieldCount; i++)
                        {
                            ruestung_schutz_attributesnames.Insert(i, outputruestung_schutz.GetName(i).ToString());
                            ruestung_schutz_dbvalues.Insert(i, outputruestung_schutz[i].ToString());
                            file.Root.Element(ruestung_schutz).Add(new XElement(Search(i, ruestung_schutz_attributesnames), Search(i, ruestung_schutz_dbvalues)));
                        }
                    }
                    #endregion

                    #region Waffen
                    List<string> waffen_attributesnames = new List<string>();
                    List<string> waffen_dbvalues = new List<string>();
                    string selectwaffen = "SELECT * FROM Waffen;";
                    SqliteCommand commandwaffen = new SqliteCommand(selectwaffen, dbConnection);
                    SqliteDataReader outputwaffen = commandwaffen.ExecuteReader();

                    while (outputwaffen.Read())
                    {
                        for (int i = 0; i < outputwaffen.FieldCount; i++)
                        {
                            waffen_attributesnames.Insert(i, outputwaffen.GetName(i).ToString());
                            waffen_dbvalues.Insert(i, outputwaffen[i].ToString());
                            file.Root.Element(waffen).Add(new XElement(Search(i, waffen_attributesnames), Search(i, waffen_dbvalues)));
                        }
                    }
                    #endregion

                    file.Save(xmlpath + "char.xml");
                    console_msg.text = "Successfully exported!";

                    #region Cleared();
                    spielerattributesnames.Clear();
                    spielerdbvalues.Clear();
                    abwehrattributesnames.Clear();
                    abwehrdbvalues.Clear();
                    aktive_Verattributesnames.Clear();
                    aktive_Verdbvalues.Clear();
                    attribute_attributesnames.Clear();
                    attribute_dbvalues.Clear();
                    attributskosten_attributesnames.Clear();
                    attributskosten_dbvalues.Clear();
                    fertigkeiten_attributesnames.Clear();
                    fertigkeiten_dbvalues.Clear();
                    ruestung_schutz_attributesnames.Clear();
                    ruestung_schutz_dbvalues.Clear();
                    waffen_attributesnames.Clear();
                    waffen_dbvalues.Clear();
                    #endregion
                    dbConnection.Close();
                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError(e);
            console_msg.text = "Error:\nFailed to connect or to export XML!";
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
    #endregion

}
