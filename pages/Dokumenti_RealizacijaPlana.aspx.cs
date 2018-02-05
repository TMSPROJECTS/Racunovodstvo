using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

public partial class Dokumenti_RealizacijaPlana : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proProgram, string proProgramskaAktivnost, string proFunkcionalnaKlasifikacija, string proKonto, string proCekirano)
    {
        Dokumenti_RealizacijaPlana strana = new global::Dokumenti_RealizacijaPlana();
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
       // pages_Dokumenti_RealizacijaPlana strana = new pages_Dokumenti_RealizacijaPlana();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument,proProgram,proProgramskaAktivnost ,proFunkcionalnaKlasifikacija,proKonto,proCekirano);
        return poruka;
    }

    [System.Web.Services.WebMethod(true)]
    public static string[] izlistajPA(string prodokument)
    {
        Dokumenti_RealizacijaPlana strana = new global::Dokumenti_RealizacijaPlana();
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        // pages_Dokumenti_RealizacijaPlana strana = new pages_Dokumenti_RealizacijaPlana();
        string[] poruka = new string[2];
        poruka = strana.Izlistaj(prodokument);
        return poruka;
    }


    [System.Web.Services.WebMethod(true)]
    public static string[] izlistajFK(string prodokument, string prodokument2)
    {
        Dokumenti_RealizacijaPlana strana = new global::Dokumenti_RealizacijaPlana();
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        // pages_Dokumenti_RealizacijaPlana strana = new pages_Dokumenti_RealizacijaPlana();
        string[] poruka = new string[2];
        poruka = strana.Izlistaj2(prodokument,prodokument2);
        return poruka;
    }

    [System.Web.Services.WebMethod(true)]
    public static string[] prikaziTabele(string prodokument, string proProgram, string proProgramskaAktivnost, string proFunkcionalnaKlasifikacija, string proKonto, string proCekirano)
    {
        Dokumenti_RealizacijaPlana strana = new global::Dokumenti_RealizacijaPlana();
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        // pages_Dokumenti_RealizacijaPlana strana = new pages_Dokumenti_RealizacijaPlana();
        string[] poruka = new string[2];
        poruka = strana.Prikazi(prodokument, proProgram, proProgramskaAktivnost, proFunkcionalnaKlasifikacija, proKonto, proCekirano);
        return poruka;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Text1.Value = Request.QueryString["SIFRAY"];
        if (!IsPostBack)
        {

            program.Items.Add("-- Odaberite program --");
            programskaAktivnost.Items.Add("Sve programske aktivnosti");
            funkcionalnaKlasifikacija.Items.Add("Sve funkcionalne klasifikacije");
            
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
            System.Data.DataTable dtSviProgrami = Upiti.Select2("Program", "namena_sredstava_program", "ne", nazivPoslovnice);

            foreach (System.Data.DataRow red in dtSviProgrami.Rows)
            {
                program.Items.Add(red["Program"].ToString());
            }

            
           

        }
    }

    public string[] Prikazi(string vrednost, string proProgram, string proProgramskaAktivnost, string proFunkcionalnaKlasifikacija, string proKonto, string proCekirano)
    {
        string[] poruka = new string[2];

   

        return poruka;
    }

    public string[] Izlistaj2(string vrednost, string vrednost2)
    {
        string[] poruka = new string[2];
       
        try
        {
            string nazivPoslovnice2 = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice2 = nazivPoslovnice2 + "_" + nazivGodine;

            string nazivPoslovnice = (String)Session["odabranaPoslovnica"];

            string sifra = "";
            DataTable dtSifra = Upiti.Select2("Sifra", "namena_sredstava_programska_aktivnost", "ProgramskaAktivnost='" + vrednost2 + "'", nazivPoslovnice2);
            foreach (DataRow red in dtSifra.Rows)
            {
                sifra = red["Sifra"].ToString();
            }

            string zaPovratak = "";

            DataTable dtPAFK = Upiti.Select2("IDfk", "programska_aktivnost_funkcionalna_klasifikacija", "IDpa='" + sifra + "'", nazivPoslovnice2);

            DataTable dtFunkcionalnaKlasifikacija = Upiti.Select2("ID,sifra,Naziv", "funkcionalna_Klasifikacija", "ne", nazivPoslovnice2);

           

            bool prvo = true;
            foreach (DataRow red in dtPAFK.Rows)
            {
                foreach (DataRow redic in dtFunkcionalnaKlasifikacija.Rows)
                {
                    if (red["IDfk"].ToString() == redic["Sifra"].ToString())
                    {
                        if (prvo == true)
                        {
                            zaPovratak = redic["Naziv"].ToString();
                            prvo = false;
                        }
                        else
                        {
                            zaPovratak += "#" + redic["Naziv"].ToString();
                        }
                    }
                }
            }

            //bool prvo = true;
            //foreach (DataRow red in dtProgramskeAktivnosti.Rows)
            //{
            //    if (prvo == true)
            //    {
            //        zaPovratak = red["ProgramskaAktivnost"].ToString();
            //        prvo = false;
            //    }
            //    else
            //    {
            //        zaPovratak += "#" + red["ProgramskaAktivnost"].ToString();
            //    }
            //}

            poruka[0] = "D";
            poruka[1] = zaPovratak;
            return poruka;
        }
        catch
        {
            poruka[0] = "N";
            poruka[1] = "NE RADI";
            return poruka;
        }


    }

    public string[] Izlistaj(string vrednost)
    {
        string[] poruka = new string[2];

       

        try
        {
            string nazivPoslovnice2 = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice2 = nazivPoslovnice2 + "_" + nazivGodine;

            string nazivPoslovnice = (String)Session["odabranaPoslovnica"];

            string sifra = "";
            DataTable dtSifra = Upiti.Select2("Sifra", "namena_sredstava_program", "Program='" + vrednost + "'", nazivPoslovnice2);
            foreach (DataRow red in dtSifra.Rows)
            {
                sifra = red["Sifra"].ToString();
            }

            string zaPovratak = "";

            DataTable dtProgramskeAktivnosti = Upiti.Select2("ProgramskaAktivnost", "namena_sredstava_programska_aktivnost", "IDprograma='" + sifra + "'", nazivPoslovnice2);

            bool prvo = true;
            foreach (DataRow red in dtProgramskeAktivnosti.Rows)
            {
                if (prvo == true)
                {
                    zaPovratak = red["ProgramskaAktivnost"].ToString();
                    prvo = false;
                }
                else
                {
                    zaPovratak += "#" + red["ProgramskaAktivnost"].ToString();
                }
            }

            poruka[0] = "D";
            poruka[1] = zaPovratak;
            return poruka;
        }
        catch
        {
            poruka[0] = "N";
            poruka[1] = "NE RADI";
            return poruka;
        }

       

    }

    public string[] Sacuvaj(string vrednost, string proProgram, string proProgramskaAktivnost, string proFunkcionalnaKlasifikacija, string proKonto, string proCekirano)
    {
        string[] poruka = new string[2];

        if (proProgram == "-- Odaberite program --")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali program!";
            return poruka;
        }

        string nazivPoslovnice2 = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice2 = nazivPoslovnice2 + "_" + nazivGodine;

        string nazivPoslovnice = (String)Session["odabranaPoslovnica"];

        DataTable dtPodaciPoslovnice = Upiti.Select("Mesto,Adresa,PIB", "poslovnica", "Naziv='" + nazivPoslovnice + "'");

        string Korisnik = (String)Session["korisnickoIme"];

        BaseFont bfTimesNaslov = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        iTextSharp.text.Font timesNaslov = new iTextSharp.text.Font(bfTimesNaslov, 16, iTextSharp.text.Font.BOLD);

        BaseFont bfTimesNaslovIspod = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        iTextSharp.text.Font timesNaslovIspod = new iTextSharp.text.Font(bfTimesNaslovIspod, 12, iTextSharp.text.Font.ITALIC);

        DateTime vremeSada = DateTime.Now;

        string godina = ""; string sat = ""; string minut = ""; string dan = ""; string mesec = ""; string sekunda = ""; string milisekunda = "";

        godina = vremeSada.Year.ToString();
        mesec = vremeSada.Month.ToString();
        if (mesec.Length == 1)
        {
            mesec = "0" + mesec;
        }
        dan = vremeSada.Day.ToString();
        if (dan.Length == 1)
        {
            dan = "0" + dan;
        }
        sat = vremeSada.Hour.ToString();
        if (sat.Length == 1)
        {
            sat = "0" + sat;
        }
        minut = vremeSada.Minute.ToString();
        if (minut.Length == 1)
        {
            minut = "0" + minut;
        }
        sekunda = vremeSada.Second.ToString();
        if (sekunda.Length == 1)
        {
            sekunda = "0" + sekunda;
        }
        milisekunda  = vremeSada.Millisecond.ToString();
        if (milisekunda.Length == 1)
        {
            milisekunda = "0" + milisekunda;
        }

        string fileName = godina + mesec + dan + sat + minut + sekunda + milisekunda + "_" + Korisnik + ".pdf";

        FileStream fs = new FileStream("C:\\Luo\\LuoShare\\" + fileName, FileMode.Create, FileAccess.Write, FileShare.None);
        Document doc = new Document();
        doc.SetMargins(10f, 10f, 10f, 10f);
        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
        doc.Open();

        //Chunk c1 = new Chunk("prva");
        //doc.Add(c1);
        //Chunk c2 = new Chunk("druga");
        //doc.Add(c2);



        foreach (DataRow red in dtPodaciPoslovnice.Rows)
        {
            doc.Add(new Paragraph(nazivPoslovnice));
            doc.Add(new Paragraph(red["Adresa"].ToString()));
            doc.Add(new Paragraph(red["Mesto"].ToString()));
            doc.Add(new Paragraph("PIB: " + red["PIB"].ToString()));
            doc.Add(new Paragraph(""));
        }

        PdfPTable t1 = new PdfPTable(1);
        PdfPCell c1 = new PdfPCell(new Phrase("Realizacija plana", timesNaslov));
        c1.HorizontalAlignment = 1; // 0- levo 1- centar 2- desno
        c1.Border = iTextSharp.text.Rectangle.NO_BORDER;
        t1.AddCell(c1);

        doc.Add(t1);

        DataTable uzmiFP = Upiti.Select2("Naziv", "finansijski_plan", "SifraPlana='" + vrednost + "'", nazivPoslovnice2);

        foreach (DataRow red in uzmiFP.Rows)
        {
            PdfPTable t2 = new PdfPTable(1);
            PdfPCell c2 = new PdfPCell(new Phrase(red["Naziv"].ToString(), timesNaslovIspod));
            c2.HorizontalAlignment = 1; // 0- levo 1- centar 2- desno
            c2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            t2.AddCell(c2);

            doc.Add(t2);
        }


        //doc.Add(new Paragraph("This is Paragraph 1"));
        //doc.Add(new Paragraph("This is Paragraph 2"));
        doc.Add(new Paragraph(" "));


        //Krece glavna tabela

        System.Data.DataTable dtFunkcionalnaKlasifikacija = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice2);
        System.Data.DataTable dtProgramskaAktivnostFunkcionalnaKlasifikacija = Upiti.Select2("*", "programska_aktivnost_funkcionalna_klasifikacija", "ne", nazivPoslovnice2);

        DataTable dtProgramskaAktivnostGrupeTroskova = Upiti.Select2("*", "programska_aktivnost_grupe_troskova", "ne", nazivPoslovnice2);
        DataTable dtGrupeTroskova = Upiti.Select2("*", "grupe_troskova", "ne", nazivPoslovnice2);
        DataTable dtVrsteTroskova = Upiti.Select2("*", "vrste_troskova", "ne", nazivPoslovnice2);
        DataTable dtRasporedjenostPoVrstama = Upiti.Select2("*", "rasporedjenost_po_vrstama", "ne", nazivPoslovnice2);

        DataTable dtUzmiProgram = Upiti.Select2("Sifra", "namena_sredstava_program", "Program='" + proProgram + "'", nazivPoslovnice2);
        string sifraPrograma = "";
        foreach (DataRow red in dtUzmiProgram.Rows)
        {
            sifraPrograma = red["Sifra"].ToString();
        }

        string IDposlovneAktivnostiFilter = "ne";
        if (proProgramskaAktivnost != "Sve programske aktivnosti")
        {
            DataTable dtOdabranaProgramskaAktivnost = Upiti.Select2("Sifra", "namena_sredstava_programska_aktivnost", "ProgramskaAktivnost='" + proProgramskaAktivnost + "'", nazivPoslovnice2);
            foreach (DataRow red in dtOdabranaProgramskaAktivnost.Rows)
            {
                IDposlovneAktivnostiFilter = red["Sifra"].ToString();
            }
        }

        string IDfunkcionalnaKlasifikacijaFilter = "ne";
        if (proFunkcionalnaKlasifikacija != "Sve funkcionalne klasifikacije")
        {
            DataTable dtOdabranaFunkcionalnaKlasifikacija = Upiti.Select2("Sifra", "funkcionalna_klasifikacija", "Naziv='" + proFunkcionalnaKlasifikacija + "'", nazivPoslovnice2);
            foreach (DataRow red in dtOdabranaFunkcionalnaKlasifikacija.Rows)
            {
                IDfunkcionalnaKlasifikacijaFilter = red["Sifra"].ToString();
            }
        }


        DataTable dtUzmiProgramskeAktivnosti;

        if (proProgramskaAktivnost == "Sve programske aktivnosti" || proProgramskaAktivnost.Trim() == "")
        {
            dtUzmiProgramskeAktivnosti = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "IDprograma='" + sifraPrograma + "'", nazivPoslovnice2);
        }
        else
        {
            dtUzmiProgramskeAktivnosti = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "IDprograma='" + sifraPrograma + "' and ProgramskaAktivnost='" + proProgramskaAktivnost + "'", nazivPoslovnice2);
        }

       

        foreach (DataRow red in dtUzmiProgramskeAktivnosti.Rows)
        {
            string sifraPA = red["Sifra"].ToString();
            string NAZIVpa = red["ProgramskaAktivnost"].ToString();

            //////////////
            if (IDposlovneAktivnostiFilter != "ne" && IDposlovneAktivnostiFilter != sifraPA)
            {
                continue;
            }
            //////////////

            foreach (DataRow redPAFK in dtProgramskaAktivnostFunkcionalnaKlasifikacija.Rows)
            {
                if (sifraPA == redPAFK["IDpa"].ToString()) /// sada imamo program, programskuAktivnost, funkcionalnuKlasifikaciju
                {
                    string sifraFK = redPAFK["IDfk"].ToString();
                    string NAZIVfk = "";

                    //////////////
                    if (IDfunkcionalnaKlasifikacijaFilter != "ne" && IDfunkcionalnaKlasifikacijaFilter != sifraFK)
                    {
                        continue;
                    }
                    //////////////

                    foreach (DataRow redFK in dtFunkcionalnaKlasifikacija.Rows)
                    {
                        if (redFK["Sifra"].ToString() == sifraFK)
                        {
                            NAZIVfk = redFK["Naziv"].ToString();
                        }
                    }

                    var FontColour = new BaseColor(255, 255, 255);
                    var MyFont = FontFactory.GetFont("Times New Roman", 12, FontColour);
                    var MyFont2 = FontFactory.GetFont("Times New Roman", 10, FontColour);

                    PdfPTable glTable = new PdfPTable(7);
                    glTable.WidthPercentage = 100;
                    float[] widths = new float[] { 1f, 5f, 2f, 1f, 1f, 1f, 1f };
                    glTable.SetWidths(widths);

                    PdfPCell glCell = new PdfPCell(new Phrase("Ovo je naslov, proveri sta ide",MyFont));
                    glCell.Colspan = 7;
                    glCell.HorizontalAlignment = 1;
                    BaseColor color1 = new BaseColor(0, 184, 230);
                    glCell.BackgroundColor = color1;
                    glTable.AddCell(glCell);

                    PdfPCell glCell2 = new PdfPCell(new Phrase("Program: " + proProgram,MyFont2));
                    glCell2.Colspan = 7;
                    glCell2.HorizontalAlignment = 1;
                    BaseColor color12 = new BaseColor(0, 204, 255);
                    glCell2.BackgroundColor = color12;
                    glTable.AddCell(glCell2);

                    PdfPCell glCell3 = new PdfPCell(new Phrase("Programska aktivnost: " + NAZIVpa,MyFont2));
                    glCell3.Colspan = 7;
                    glCell3.HorizontalAlignment = 1;
                    BaseColor color123 = new BaseColor(51, 214, 255);
                    glCell3.BackgroundColor = color123;
                    glTable.AddCell(glCell3);

                    PdfPCell glCell4 = new PdfPCell(new Phrase("Funkcionalna klasifikacija: " + NAZIVfk,MyFont2));
                    glCell4.Colspan = 7;
                    glCell4.HorizontalAlignment = 1;
                    BaseColor color1234 = new BaseColor(102, 224, 255);
                    glCell4.BackgroundColor = color1234;
                    glTable.AddCell(glCell4);

                    glTable.AddCell("Konto");
                    glTable.AddCell("Naziv");
                    glTable.AddCell("Planirano");
                    glTable.AddCell("Placeno");
                    glTable.AddCell("Na cekanju");
                    glTable.AddCell("Ukupno");
                    glTable.AddCell("%");


                    foreach (DataRow redX in dtProgramskaAktivnostGrupeTroskova.Rows)
                    {
                        string sifraGT = "";

                        if (redX["IDpa"].ToString() == sifraPA)
                        {
                            sifraGT = redX["IDgt"].ToString();

                            foreach (DataRow redY in dtVrsteTroskova.Rows)
                            {
                                string sifraVrste = "";
                                string nazivVrste = "";
                                string konto = "";
                                if (redY["IDgrupe"].ToString() == sifraGT)
                                {
                                    sifraVrste = redY["Sifra"].ToString();
                                    nazivVrste = redY["Naziv"].ToString();
                                    konto = redY["Konto"].ToString();
                                    string iznos = "";
                                    foreach (DataRow redXY in dtRasporedjenostPoVrstama.Rows)
                                    {
                                        if (redXY["Godina"].ToString() == nazivGodine && redXY["FP"].ToString() == vrednost && redXY["PA"].ToString() == sifraPA && redXY["VT"].ToString() == sifraVrste && redXY["FK"].ToString() == sifraFK)
                                        {
                                            iznos = redXY["Vrednost"].ToString();
                                        }
                                    }

                                    if (iznos == "")
                                    {
                                        iznos = "0";
                                    }
                                    glTable.AddCell(konto);
                                    glTable.AddCell(nazivVrste);
                                    glTable.AddCell(iznos);
                                    glTable.AddCell("0");
                                    glTable.AddCell("0");
                                    glTable.AddCell("0");
                                    glTable.AddCell("0");

                                }
                            }
                        }
                    }


                    doc.Add(glTable);
                    doc.Add(new Paragraph(" "));
                }
            }
           


        }
   
        doc.Close();

        if (File.Exists("C:\\Luo\\LuoShare\\" + fileName))
        {
            Process.Start("C:\\Luo\\LuoShare\\" + fileName);
        }
        else
        {

           
        }

        poruka[0] = "N";
        poruka[1] = "OLE!";
        return poruka;


    }



}