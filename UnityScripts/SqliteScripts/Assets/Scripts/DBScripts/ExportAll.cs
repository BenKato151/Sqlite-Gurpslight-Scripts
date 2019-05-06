using System;
using System.IO;
using System.Xml.Linq;
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
                    #region SearchAbwehr
                    string abwehrfirstelement = "Schild";
                    string abwehrfirstname = "";
                    string abwehrsecondelement = "Rüstung";
                    string abwehrsecondname = "";
                    string abwehrthirdelement = "Umhang";
                    string abwehrthirdname = "";
                    string abwehrfourthelement = "Gesamt";
                    string abwehrfourthname = "";
                    string abwehrfifthelement = "ID";
                    string abwehrfifthname = "";
                    string selectAbwehr = "SELECT * FROM Abwehr;";

                    SqliteCommand abwehrcommand = new SqliteCommand(selectAbwehr, dbConnection);
                    SqliteDataReader abwehroutput = abwehrcommand.ExecuteReader();

                    while (abwehroutput.Read())
                    {                        
                        abwehrfirstname = "" + abwehroutput["Schild"];                        
                        abwehrsecondname = "" + abwehroutput["Ruestung"];                        
                        abwehrthirdname = "" + abwehroutput["Umhang"];                        
                        abwehrfourthname = "" + abwehroutput["Gesamt"];                        
                        abwehrfifthname = "" + abwehroutput["ID"];
                    }
                    #endregion

                    #region Select Attribute
                    string attributefirstelement = "Stärke";
                    string attributefirstname = "";
                    string attributesecondelement = "Geschicklichkeit";
                    string attributesecondname = "";
                    string attributethirdelement = "Intelligenz";
                    string attributethirdname = "";
                    string attributefourthelement = "Konstitution";
                    string attributefourthname = "";
                    string attributefifthelement = "ID";
                    string attributefifthname = "";
                    string selectAttribute = "SELECT * FROM Attribute;";

                    SqliteCommand attributecommand = new SqliteCommand(selectAttribute, dbConnection);
                    SqliteDataReader attributeoutput = attributecommand.ExecuteReader();

                    while (attributeoutput.Read())
                    {
                        attributefirstname = "" + attributeoutput["Staerke"];
                        attributesecondname = "" + attributeoutput["Geschicklichkeit"];
                        attributethirdname = "" + attributeoutput["Intelligenz"];
                        attributefourthname = "" + attributeoutput["Konstitution"];
                        attributefifthname = "" + attributeoutput["ID"];
                    }
                    #endregion

                    #region Select Attributskosten
                    string akostenfirstelement = "Wert";
                    string akostenfirstname = "";
                    string akostensecondelement = "Kosten";
                    string akostensecondname = "";
                    string akostenthirdelement = "ID";
                    string akostenthirdname = "";
                    string selectAkosten = "SELECT * FROM Attributskosten;";

                    SqliteCommand aKostencommand = new SqliteCommand(selectAkosten, dbConnection);
                    SqliteDataReader aKostenoutput = aKostencommand.ExecuteReader();

                    while (aKostenoutput.Read())
                    {
                        akostenfirstname = "" + aKostenoutput["Wert"];
                        akostensecondname = "" + aKostenoutput["Kosten"];
                        akostenthirdname = "" + aKostenoutput["ID"];
                    }
                    #endregion

                    #region Select Aktive Verteidigung
                    string aVertfirstelement = "Parieren";
                    string aVertfirstname = "";
                    string aVertsecondelement = "Ausweichen";
                    string aVertsecondname = "";
                    string aVertthirdelement = "Abblocken";
                    string aVertthirdname = "";
                    string aVertfourthelement = "AbblockenUmhang";
                    string aVertfourthname = "";
                    string aVertfifthelement = "ID";
                    string aVertfifthname = "";
                    string selectAver = "SELECT * FROM Aktive_Verteidigung;";

                    SqliteCommand aVertcommand = new SqliteCommand(selectAver, dbConnection);
                    SqliteDataReader aVeroutput = aVertcommand.ExecuteReader();

                    while (aVeroutput.Read())
                    {
                        aVertfirstname = "" + aVeroutput["Parieren"];
                        aVertsecondname = "" + aVeroutput["Ausweichen"];
                        aVertthirdname = "" + aVeroutput["Abblocken"];
                        aVertfourthname = "" + aVeroutput["AblockenUmh"];
                        aVertfifthname = "" + aVeroutput["ID"];
                    }
                    #endregion

                    #region Select Fertigkeiten
                    string fertigkeitenfirstelement = "FertigkeitenName";
                    string fertigkeitenfirstname = "";
                    string fertigkeitensecondelement = "FertigkeitenTyp";
                    string fertigkeitensecondname = "";
                    string fertigkeitenthirdelement = "FertigkeitenArt";
                    string fertigkeitenthirdname = "";
                    string fertigkeitenfourthelement = "CP";
                    string fertigkeitenfourthname = "";
                    string fertigkeitenfifthelement = "FW";
                    string fertigkeitenfifthname = "";
                    string fertigkeitensixelement = "ID";
                    string fertigkeitensixtname = "";
                    string selectFertigkeiten = "SELECT * FROM Fertigkeiten;";

                    SqliteCommand fertigkeitencommand = new SqliteCommand(selectFertigkeiten, dbConnection);
                    SqliteDataReader fertigkeitenoutput = fertigkeitencommand.ExecuteReader();

                    while (fertigkeitenoutput.Read())
                    {
                        fertigkeitenfirstname = "" + fertigkeitenoutput["Name"];
                        fertigkeitensecondname = "" + fertigkeitenoutput["Typ"];
                        fertigkeitenthirdname = "" + fertigkeitenoutput["Art"];
                        fertigkeitenfourthname = "" + fertigkeitenoutput["CP"];
                        fertigkeitenfifthname = "" + fertigkeitenoutput["FW"];
                        fertigkeitensixtname = "" + fertigkeitenoutput["ID"];
                    }
                    #endregion

                    #region Select Player
                    string playerfirstelement = "Name";
                    string playerfirstname = "";
                    string playersecondelement = "Geschlecht";
                    string playersecondname = "";
                    string playerthirdelement = "Rasse";
                    string playerthirdname = "";
                    string playerfourthelement = "Haar";
                    string playerfourthname = "";
                    string playerfifthelement = "Augen";
                    string playerfifthname = "";
                    string playersixelement = "Gewicht";
                    string playersixtname = "";
                    string playersevenelement = "Größe";
                    string playersevenname = "";
                    string playereightelement = "Beschreibung";
                    string playereightname = "";
                    string playernineelement = "ID";
                    string playerninename = "";
                    string selectplayer = "SELECT * FROM Spieler;";

                    SqliteCommand playercommand = new SqliteCommand(selectplayer, dbConnection);
                    SqliteDataReader playeroutput = playercommand.ExecuteReader();

                    while (playeroutput.Read())
                    {
                        playerfirstname = "" + playeroutput["name"];
                        playersecondname = "" + playeroutput["geschlecht"];
                        playerthirdname = "" + playeroutput["rasse"];
                        playerfourthname = "" + playeroutput["haar"];
                        playerfifthname = "" + playeroutput["augen"];
                        playersixtname = "" + playeroutput["gewicht"];
                        playersevenname = "" + playeroutput["groese"];
                        playereightname = "" + playeroutput["beschreibung"];
                        playerninename = "" + playeroutput["SpielerID"];
                    }
                    #endregion

                    #region Select Rüstung und Schutz
                    string rufirstelement = "Schild";
                    string rufirstname = "";
                    string rusecondelement = "Rüstung";
                    string rusecondname = "";
                    string ruthirdelement = "Umhang";
                    string ruthirdname = "";
                    string rufourthelement = "Gesamt";
                    string rufourthname = "";
                    string selectrust = "SELECT * FROM Ruestung_Schutz;";

                    SqliteCommand rucommand = new SqliteCommand(selectrust, dbConnection);
                    SqliteDataReader ruoutput = rucommand.ExecuteReader();

                    while (abwehroutput.Read())
                    {
                        rufirstname = "" + ruoutput["Ort"];
                        rusecondname = "" + ruoutput["SR"];
                        ruthirdname = "" + ruoutput["PV"];
                        rufourthname = "" + ruoutput["ID"];
                    }
                    #endregion

                    #region Select Waffen
                    //15 * 2 Vars
                    string waffenfirstelement = "Waffenname";
                    string waffenfirstname = "";
                    string waffensecondelement = "WaffenID";
                    string waffensecondname = "";
                    string waffenthirdelement = "FW";
                    string waffenthirdname = "";
                    string waffenfourthelement = "Schaden";
                    string waffenfourthname = "";
                    string waffenfifthelement = "Mod";
                    string waffenfifthname = "";
                    string waffensixelement = "Ort";
                    string waffensixtname = "";
                    string waffensevenelement = "ZG";
                    string waffensevenname = "";
                    string waffeneightelement = "SS";
                    string waffeneightname = "";
                    string waffennineelement = "EINHALBS";
                    string waffenninename = "";
                    string waffentenelement = "RW";
                    string waffentenname = "";
                    string waffenelevenelement = "FG";
                    string waffenelevenname = "";
                    string waffentwelveelement = "MAG";
                    string waffentwelvename = "";
                    string waffenthirdteenelement = "RS";
                    string waffenthirdteenname = "";
                    string waffenfourteenelement = "ST";
                    string waffenfourteenname = "";
                    string waffenfifteenelement = "LZ";
                    string waffenfifteenname = "";
                    string waffensixteenelement = "BM";
                    string waffensixteenname = "";
                    string selectWaffen = "SELECT * FROM Waffen;";

                    SqliteCommand waffencommand = new SqliteCommand(selectWaffen, dbConnection);
                    SqliteDataReader waffenoutput = waffencommand.ExecuteReader();

                    while (waffenoutput.Read())
                    {
                        waffenfirstname = "" + waffenoutput["Waffenname"];
                        waffensecondname = "" + waffenoutput["WaffenID"];
                        waffenthirdname = "" + waffenoutput["FW"];
                        waffenfourthname = "" + waffenoutput["Schaden"];
                        waffenfifthname = "" + waffenoutput["Mod"];
                        waffensixtname = "" + waffenoutput["Ort"];
                        waffensevenname = "" + waffenoutput["ZG"];
                        waffeneightname = "" + waffenoutput["SS"];
                        waffenninename = "" + waffenoutput["EINHALBS"];
                        waffentenname = "" + waffenoutput["RW"];
                        waffenelevenname = "" + waffenoutput["FG"];
                        waffentwelvename = "" + waffenoutput["MAG"];
                        waffenthirdteenname = "" + waffenoutput["RS"];
                        waffenfourteenname = "" + waffenoutput["ST"];
                        waffenfifteenname = "" + waffenoutput["LZ"];
                        waffensixteenname = "" + waffenoutput["BM"];
                    }
                    #endregion

                    #region XDocument
                    XDocument exportall = new XDocument(
                                    new XDeclaration("1.0", "utf-8", "yes"),
                                    new XComment("Gurbs Light Character"),
                                    new XElement("GurpsLightCharacter",
                                        new XElement("Player",
                                                new XElement(playerfirstelement, playerfirstname),
                                                new XElement(playersecondelement, playersecondname),
                                                new XElement(playerthirdelement, playerthirdname),
                                                new XElement(playerfourthelement, playerfourthname),
                                                new XElement(playerfifthelement, playerfifthname),
                                                new XElement(playersixelement, playersixtname),
                                                new XElement(playersevenelement, playersevenname),
                                                new XElement(playereightelement, playereightname),
                                                new XElement(playernineelement, playerninename)
                                                ),
                                        new XElement("Abwehr",
                                            new XElement(abwehrfirstelement, abwehrfirstname),
                                            new XElement(abwehrsecondelement, abwehrsecondname),
                                            new XElement(abwehrthirdelement, abwehrthirdname),
                                            new XElement(abwehrfourthelement, abwehrfourthname),
                                            new XElement(abwehrfifthelement, abwehrfifthname)
                                            ),
                                        new XElement("Attribute",
                                            new XElement(attributefirstelement, attributefirstname),
                                            new XElement(attributesecondelement, attributesecondname),
                                            new XElement(attributethirdelement, attributethirdname),
                                            new XElement(attributefourthelement, attributefourthname),
                                            new XElement(attributefifthelement, attributefifthname)
                                            ),
                                        new XElement("Attributskosten",
                                            new XElement(akostenfirstelement, akostenfirstname),
                                            new XElement(akostensecondelement, akostensecondname),
                                            new XElement(akostenthirdelement, akostenthirdname)
                                            ),
                                        new XElement("AktiveVerteidigung",
                                            new XElement(aVertfirstelement, aVertfirstname),
                                            new XElement(aVertsecondelement, aVertsecondname),
                                            new XElement(aVertthirdelement, aVertthirdname),
                                            new XElement(aVertfourthelement, aVertfourthname),
                                            new XElement(aVertfifthelement, aVertfifthname)
                                            ),
                                        new XElement("Fertigkeiten",
                                            new XElement(fertigkeitenfirstelement, fertigkeitenfirstname),
                                            new XElement(fertigkeitensecondelement, fertigkeitensecondname),
                                            new XElement(fertigkeitenthirdelement, fertigkeitenthirdname),
                                            new XElement(fertigkeitenfourthelement, fertigkeitenfourthname),
                                            new XElement(fertigkeitenfifthelement, fertigkeitenfifthname),
                                            new XElement(fertigkeitensixelement, fertigkeitensixtname)
                                            ),
                                        new XElement("RüstungSchutz", 
                                            new XElement(rufirstelement, rufirstname),
                                            new XElement(rusecondelement, rusecondname),
                                            new XElement(ruthirdelement, ruthirdname),
                                            new XElement(rufourthelement, rufourthname)
                                            ),
                                        new XElement("Waffen",
                                            new XElement(waffenfirstelement, waffenfirstname),
                                            new XElement(waffensecondelement, waffensecondname),
                                            new XElement(waffenthirdelement, waffenthirdname),
                                            new XElement(waffenfourthelement, waffenfourthname),
                                            new XElement(waffenfifthelement, waffenfifthname),
                                            new XElement(waffensixelement, waffensixtname),
                                            new XElement(waffensevenelement, waffensevenname),
                                            new XElement(waffeneightelement, waffeneightname),
                                            new XElement(waffennineelement, waffenninename),
                                            new XElement(waffentenelement, waffentenname),
                                            new XElement(waffenelevenelement, waffenelevenname),
                                            new XElement(waffentwelveelement, waffentwelvename),
                                            new XElement(waffenthirdteenelement, waffenthirdteenname),
                                            new XElement(waffenfourteenelement, waffenfourteenname),
                                            new XElement(waffenfifteenelement, waffenfifteenname),
                                            new XElement(waffensixteenelement, waffensixteenname)
                                            )
                                        )
                                    );
                    exportall.Save(xmlpath + "gurbslight_character_export.xml");
                    #endregion

                    Debug.Log("Successfully exported!");
                }
            }
        }

        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Error:\nFailed to connect or to export XML!");
        }
    }
}
