using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_PomocniPodaci_Dokaznica_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proKonto, string proNamena, string proDobavljac, string proTekuciRacun, string proSifPlacanja, string proPozivNaBr, string proVaziOd, string proVaziDo, string proIznos)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_PomocniPodaci_Dokaznica_Dodavanje strana = new pages_PomocniPodaci_Dokaznica_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proKonto,proNamena,proDobavljac,proTekuciRacun,proSifPlacanja ,proPozivNaBr, proVaziOd ,proVaziDo ,proIznos);
        return poruka;
    }

    [System.Web.Services.WebMethod(true)]
    public static string[] rcn(string prodokument)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_PomocniPodaci_Dokaznica_Dodavanje strana = new pages_PomocniPodaci_Dokaznica_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Rcn(prodokument);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

            DataTable dtPP = Upiti.Select2("Sifra,ImePrezime,JMBG", "poslovni_partneri", "ne", nazivPoslovnice);

            foreach (DataRow red in dtPP.Rows)
            {
                dobavljac.Items.Add(red["Sifra"].ToString() + ", " + red["ImePrezime"].ToString() + ", " + red["JMBG"].ToString());
            }


            if (Request.QueryString["SIFRA11"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;
                DataTable dtIzmena = Upiti.Select2("*", "dokaznica", "Sifra='" + Request.QueryString["SIFRA11"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["Sifra"].ToString();
                    konto.Value = red["Konto"].ToString();
                    namena.Value = red["Namena"].ToString();
                    //dobavljac.Value = red["Dobavljac"].ToString();
                    takuciRacun.Value = red["TekuciRacun"].ToString();
                    sifPlacanja.Value = red["SifraPlacanja"].ToString();
                    pozivNaBr.Value = red["PozivNaBroj"].ToString();

                    string korisnik = red["Dobavljac"].ToString();


                    DataTable dtSvi = Upiti.Select2("*", "poslovni_partneri", "Sifra='" + korisnik + "'", nazivPoslovnice);

                    string korisnikZaDDL = "";

                    foreach (DataRow redic in dtSvi.Rows)
                    {
                        korisnikZaDDL = redic["Sifra"].ToString() + ", " + redic["ImePrezime"].ToString() + ", " + redic["JMBG"].ToString();

                        DataTable dtRacuna = Upiti.Select2("*", "tekuci_racun", "IDpartnera='" + korisnik + "'", nazivPoslovnice);
                        
                        foreach (DataRow reee in dtRacuna.Rows)
                        {
                            takuciRacun.Items.Add(reee["Racun"].ToString());
                            if (reee["ID"].ToString() == red["TekuciRacun"].ToString())
                            {
                                takuciRacun.Value = reee["Racun"].ToString();
                            }
                        }
                    }

                    dobavljac.Value = korisnikZaDDL;

                    


                    DateTime dt1 = DateTime.Parse(red["VaziOd"].ToString());
                    DateTime dt2 = DateTime.Parse(red["VaziDo"].ToString());

                    string godina1 = dt1.Year.ToString();
                    string mesec1 = dt1.Month.ToString();
                    string dan1 = dt1.Day.ToString();

                    if (mesec1.Length == 1)
                    {
                        mesec1 = "0" + mesec1;
                    }

                    if (dan1.Length == 1)
                    {
                        dan1 = "0" + dan1;
                    }

                    string godina2 = dt2.Year.ToString();
                    string mesec2 = dt2.Month.ToString();
                    string dan2 = dt2.Day.ToString();

                    if (mesec2.Length == 1)
                    {
                        mesec2 = "0" + mesec2;
                    }

                    if (dan2.Length == 1)
                    {
                        dan2 = "0" + dan2;
                    }

                    vaziOd.Value = godina1 + "-" + mesec1 + "-" + dan1;
                    vaziDo.Value = godina2 + "-" + mesec2 + "-" + dan2;
                    iznos.Value = red["Iznos"].ToString();
                }

            }

           
        }
    }
    public string[] Rcn(string vrednost)
    {
        string[] poruka = new string[2];
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

        string[] rastavi = vrednost.Split(new char[] { ',' });

        DataTable dtRacuna= Upiti.Select2("*", "tekuci_racun", "IDpartnera='" + rastavi[0]  + "'", nazivPoslovnice);

        // takuciRacun.Items.Clear();

        bool prvi = true;
        foreach (DataRow red in dtRacuna.Rows)
        {
            if (prvi == true)
            {
                poruka[1] += red["Racun"].ToString();
                prvi = false;
            }
            else
            {
                poruka[1] += "#" + red["Racun"].ToString();
            }

            
        }

        

        poruka[0] = "D";
        return poruka;
    }
    public string[] Sacuvaj(string vrednost,string proKonto, string proNamena, string  proDobavljac, string  proTekuciRacin, string  proSifraPlacanja , string  proPozivNaPr, string  proVaziOd , string proVaziDo , string  proIznos)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
       // string vrednost = Request.QueryString["SIFRA11"];

        //string proKonto = konto.Value.Trim();
        //string proNamena = namena.Value.Trim();
        //string proDobavljac = dobavljac.Value.Trim();
        //string proTekuciRacin = dobavljac.Value.Trim();
        //string proSifraPlacanja = sifPlacanja.Value.Trim();
        //string proPozivNaPr = pozivNaBr.Value.Trim();
        //string proVaziOd =vaziOd.Value;
        //string proVaziDo =vaziDo.Value;
        //string proIznos = iznos.Value;

        if (proKonto == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli konto!";
            return poruka;
        }

        if (proNamena == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli namenu!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli namenu!";
            //return;
        }

        if (proDobavljac.Trim() == "--Izaberite--" || proDobavljac.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali dobavljača!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli dobavljača!";
            //return;
        }
      

        if (proSifraPlacanja == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli šifru plaćanja!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli šifru plaćanja!";
            //return;
        }

        if (proPozivNaPr == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli poziv na broj!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli poziv na broj!";
            //return;
        }

        if (proVaziOd == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli važi od!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli važi od!";
            //return;
        }

        if (proVaziDo == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli važi do!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli važi do!";
            //return;
        }

        if (proIznos == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli iznos!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli iznos!";
            //return;
        }

        string dobavljac = "";
        string tekuciRacun = "";

        string[] rastavljaj = proDobavljac.Split(new char[] { ',' });

        dobavljac = rastavljaj[0];

        DataTable dtRacuna = Upiti.Select2("*", "tekuci_racun", "IDpartnera='" + dobavljac + "' and Racun='" + proTekuciRacin + "'", nazivPoslovnice);
        foreach (DataRow er in dtRacuna.Rows)
        {
            tekuciRacun = er["ID"].ToString();
        }


        //lblObavestenje.Text = "";

        string Korisnik = (String)Session["korisnickoIme"];

        if (vrednost != "")
        {

            try
            {
                string naredbaUpdate = "Update dokaznica set Namena=@Namena,Dobavljac=@Dobavljac,TekuciRacun=@TekuciRacun,SifraPlacanja=@SifraPlacanja,PozivNaBroj=@PozivNaBroj,VaziOd=@VaziOd,VaziDo=@VaziDo,Iznos=@Iznos,Konto=@Konto,Korisnik=@Korisnik where Sifra='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@Namena", proNamena);
                komandaUpdate.Parameters.AddWithValue("@Dobavljac", dobavljac);
                komandaUpdate.Parameters.AddWithValue("@TekuciRacun", tekuciRacun);
                komandaUpdate.Parameters.AddWithValue("@SifraPlacanja", proSifraPlacanja);
                komandaUpdate.Parameters.AddWithValue("@PozivNaBroj", proPozivNaPr);
                komandaUpdate.Parameters.AddWithValue("@VaziOd", proVaziOd);
                komandaUpdate.Parameters.AddWithValue("@VaziDo", proVaziDo);
                komandaUpdate.Parameters.AddWithValue("@Iznos", proIznos);
                komandaUpdate.Parameters.AddWithValue("@Konto", proKonto);
                komandaUpdate.Parameters.AddWithValue("@Korisnik", Korisnik);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili dokaznicu!";
            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
            }
            return poruka;

        }
        else
        {

            string novaSifra = NovaSifra.VratiSifru("Sifra", "dokaznica", nazivPoslovnice, "DO");


            try
            {

                string naredbaInsert = "Insert into dokaznica (Sifra,Namena,Dobavljac,TekuciRacun,SifraPlacanja,PozivNaBroj,VaziOd,VaziDo,Iznos,Konto,Korisnik) values (@Sifra,@Namena,@Dobavljac,@TekuciRacun,@SifraPlacanja,@PozivNaBroj,@VaziOd,@VaziDo,@Iznos,@Konto,@Korisnik)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@Namena", proNamena);
                komandaInsert.Parameters.AddWithValue("@Dobavljac", dobavljac);
                komandaInsert.Parameters.AddWithValue("@TekuciRacun", tekuciRacun);
                komandaInsert.Parameters.AddWithValue("@SifraPlacanja", proSifraPlacanja);
                komandaInsert.Parameters.AddWithValue("@PozivNaBroj", proPozivNaPr);
                komandaInsert.Parameters.AddWithValue("@VaziOd", proVaziOd);
                komandaInsert.Parameters.AddWithValue("@VaziDo", proVaziDo);
                komandaInsert.Parameters.AddWithValue("@Iznos", proIznos);
                komandaInsert.Parameters.AddWithValue("@Konto", proKonto);
                komandaInsert.Parameters.AddWithValue("@Korisnik", Korisnik);

                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli dokaznicu!";
            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
            }
            return poruka;
        }
    }
}