using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.WebControls;

public partial class pages_racunovodstvo_UR_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proDatum, string proValuta, string proProgram, string proProgramskAktivnost, string proFunkcija, string proIzvorF, string proPartner, string proBrojF, string proUgovor, string proOpis, string proTekuci)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_racunovodstvo_UR_Dodavanje strana = new pages_racunovodstvo_UR_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.SacuvajUlazni(prodokument, proDatum, proValuta, proProgram, proProgramskAktivnost, proFunkcija, proIzvorF, proPartner, proBrojF, proUgovor, proOpis, proTekuci);
        return poruka;
    }

    [System.Web.Services.WebMethod(true)]
    public static string[] promeniTK(string dobavljac)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_racunovodstvo_UR_Dodavanje strana = new pages_racunovodstvo_UR_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.changeTK(dobavljac);
        return poruka;
    }

    [System.Web.Services.WebMethod(true)]
    public static string promeniPAK(string program)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_racunovodstvo_UR_Dodavanje strana = new pages_racunovodstvo_UR_Dodavanje();
        string poruka = "";
        poruka = strana.changePAK(program);
        return poruka;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));

        if (Request.QueryString["SIFRA"] == null)
        {
            divDok.Visible = false;

            DataTable dtTabela = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);
            foreach (DataRow redP in dtTabela.Rows)
            {
                selProgram.Items.Add(new ListItem(redP["Program"].ToString(), redP["Sifra"].ToString()));
            }
            if (dtTabela.Rows.Count > 0)
            {
                DataTable dtTabelaP = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "IDprograma = '" + dtTabela.Rows[0]["Sifra"].ToString()  + "'", nazivPoslovnice);
                foreach (DataRow redPR in dtTabelaP.Rows)
                {
                    selPogramAkt.Items.Add(new ListItem(redPR["ProgramskaAktivnost"].ToString(), redPR["Sifra"].ToString()));
                }
            }
            
            dtTabela = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
            foreach (DataRow redP in dtTabela.Rows)
            {
                selFunkcija.Items.Add(new ListItem(redP["Naziv"].ToString(), redP["Sifra"].ToString()));
            }
            dtTabela = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
            foreach (DataRow redP in dtTabela.Rows)
            {
                selIzvorF.Items.Add(new ListItem(redP["IzvorFinansiranja"].ToString(), redP["Sifra"].ToString()));
            }

            dtTabela = Upiti.Select2("*", "poslovni_partneri", "ne", nazivPoslovnice);
            foreach (DataRow redP in dtTabela.Rows)
            {
                selDob.Items.Add(new ListItem(redP["Naziv"].ToString(), redP["Sifra"].ToString()));
            }
            if(dtTabela.Rows.Count > 0)
            {
                DataTable dtTabelaT = Upiti.Select2("*", "tekuci_racun", "IDpartnera = '" + dtTabela.Rows[0]["Sifra"].ToString() + "'", nazivPoslovnice);
                foreach (DataRow redT in dtTabelaT.Rows)
                {
                    tekr.Items.Add(new ListItem(redT["Racun"].ToString(), redT["Sifra"].ToString()));
                }
                DataTable dtTabelaU = Upiti.Select2("*", "ugovori_partnera", "IDpartnera = '" + dtTabela.Rows[0]["Sifra"].ToString() + "'", nazivPoslovnice);
                foreach (DataRow redU in dtTabelaU.Rows)
                {
                    ugovorDob.Items.Add(new ListItem(redU["BrojUgovora"].ToString(), redU["SifraUgovora"].ToString()));
                }
            }
        }
        else
        {
            divDok.Visible = true;
            DataTable dtPostojeci = Upiti.Select2("*", "ulazni_racuni", "Dokument = '" + Request.QueryString["SIFRA"] + "'", nazivPoslovnice);

            foreach (DataRow red in dtPostojeci.Rows)
            {
                dokument.Value = red["Dokument"].ToString();
                datum.Value = Convert.ToDateTime(red["Datum"].ToString()).ToString("yyyy-dd-MM");
                valuta.Value = red["Valuta"].ToString();

                DataTable dtTabela = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);
                foreach (DataRow redP in dtTabela.Rows)
                {
                    selProgram.Items.Add(new ListItem(redP["Program"].ToString(), redP["Sifra"].ToString()));
                }
                selProgram.Value = red["Program"].ToString();

                if (red["Program"].ToString() != null || red["Program"].ToString() != "")
                {
                    dtTabela = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "IDprograma = '" + red["Program"].ToString() + "'", nazivPoslovnice);
                    foreach (DataRow redP in dtTabela.Rows)
                    {
                        selPogramAkt.Items.Add(new ListItem(redP["ProgramskaAktivnost"].ToString(), redP["Sifra"].ToString()));
                    }
                    selPogramAkt.Value = red["Programska_aktivnost"].ToString();
                }                

                dtTabela = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
                foreach (DataRow redP in dtTabela.Rows)
                {
                    selFunkcija.Items.Add(new ListItem(redP["Naziv"].ToString(), redP["Sifra"].ToString()));
                }
                selFunkcija.Value = red["Funkcija"].ToString();

                dtTabela = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
                foreach (DataRow redP in dtTabela.Rows)
                {
                    selIzvorF.Items.Add(new ListItem(redP["IzvorFinansiranja"].ToString(), redP["Sifra"].ToString()));
                }
                selIzvorF.Value = red["Izvor_finansiranja"].ToString();

                dtTabela = Upiti.Select2("*", "poslovni_partneri", "ne", nazivPoslovnice);
                foreach (DataRow redP in dtTabela.Rows)
                {
                    selDob.Items.Add(new ListItem(redP["Naziv"].ToString(), redP["Sifra"].ToString()));
                }
                selDob.Value = red["ID_partnera"].ToString();

                if (red["ID_partnera"].ToString() != null || red["ID_partnera"].ToString() != "")
                {
                    dtTabela = Upiti.Select2("*", "tekuci_racun", "IDpartnera = '" + red["ID_partnera"].ToString() + "'", nazivPoslovnice);
                    foreach (DataRow redP in dtTabela.Rows)
                    {
                        tekr.Items.Add(new ListItem(redP["Racun"].ToString(), redP["Sifra"].ToString()));
                    }
                    tekr.Value = red["TekuciRacun"].ToString();

                    dtTabela = Upiti.Select2("*", "ugovori_partnera", "IDpartnera = '" + red["ID_partnera"].ToString() + "'", nazivPoslovnice);
                    foreach (DataRow redP in dtTabela.Rows)
                    {
                        ugovorDob.Items.Add(new ListItem(redP["BrojUgovora"].ToString(), redP["SifraUgovora"].ToString()));
                    }
                    ugovorDob.Value = red["Ugovor"].ToString();

                }

                brojFakture.Value = red["Broj_fakture"].ToString();                
                napomena.Value = red["Opis"].ToString();
            }
        }
        konekcija.Close();
    }

    public string[] changeTK(string dobavljac)
    {
        string[] vrati = new string[2];
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));

        DataTable dtTabela = Upiti.Select2("*", "tekuci_racun", "IDpartnera = '" + dobavljac + "'", nazivPoslovnice);
        foreach (DataRow redP in dtTabela.Rows)
        {
            vrati[0] += "<option value='" + redP["Sifra"].ToString() + "'>" + redP["Racun"].ToString() + "</option>";
        }

        dtTabela = Upiti.Select2("*", "ugovori_partnera", "IDpartnera = '" + dobavljac + "'", nazivPoslovnice);
        foreach (DataRow redP in dtTabela.Rows)
        {
            vrati[1] += "<option value='" + redP["SifraUgovora"].ToString() + "'>" + redP["BrojUgovora"].ToString() + "</option>";
        }
        konekcija.Close();
        return vrati;
    }

    public string changePAK(string program)
    {
        string vrati = "";
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));

        DataTable dtTabela = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "IDprograma = '" + program + "'", nazivPoslovnice);
        foreach (DataRow redP in dtTabela.Rows)
        {
            vrati += "<option value=' " + redP["Sifra"].ToString() + "'>" + redP["ProgramskaAktivnost"].ToString() + "</option>";
        }
        konekcija.Close();
        return vrati;
    }
    public string[] SacuvajUlazni(string proDokument, string proDatum, string proValuta, string proProgram, string proProgramskAktivnost, string proFunkcija, string proIzvorF, string proPartner, string proBrojF, string proUgovor, string proOpis, string proTekuci)
    {
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string SifraDok = Request.QueryString["SIFRA"];
        string[] poruka = new string[2];

        if (proDokument != "")
        {

            string naredbaUpdate = "Update ulazni_racuni set Storno=@Storno, Datum=@Datum, Valuta=@Valuta, Program=@Program, Programska_aktivnost=@ProgramskAktivnost, Funkcija=@Funkcija, Izvor_finansiranja=@IzvorF, Opis=@Opis, Ugovor=@Ugovor, ID_partnera=@Partner, Broj_fakture=@BrojF, TekuciRacun=@Tekuci where Dokument='" + proDokument + "'";

            try
            {
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);

                komandaUpdate.Parameters.AddWithValue("@Storno", 'N');
                komandaUpdate.Parameters.AddWithValue("@Datum", proDatum);
                komandaUpdate.Parameters.AddWithValue("@Valuta", proValuta);
                komandaUpdate.Parameters.AddWithValue("@Program", proProgram);
                komandaUpdate.Parameters.AddWithValue("@ProgramskAktivnost", proProgramskAktivnost);
                komandaUpdate.Parameters.AddWithValue("@Funkcija", proFunkcija);
                komandaUpdate.Parameters.AddWithValue("@IzvorF", proIzvorF);
                komandaUpdate.Parameters.AddWithValue("@Partner", proPartner);
                komandaUpdate.Parameters.AddWithValue("@BrojF", proBrojF);
                komandaUpdate.Parameters.AddWithValue("@Ugovor", proUgovor);
                komandaUpdate.Parameters.AddWithValue("@Opis", proOpis);
                komandaUpdate.Parameters.AddWithValue("@Tekuci", proTekuci);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili ulazni račun!";
            }
            catch (Exception ero)
            {
                konekcija.Close();
                poruka[0] = "N";
                //poruka[1] = ero.ToString();
                poruka[1] = ero + "Greška prilikom izmene ulaznog računa!";
            }
            return poruka;
        }
        else
        {
            DataTable dtPokupiSifre = Upiti.Select2("max(Dokument) as Dokument", "ulazni_racuni", "ne", nazivPoslovnice);

            string poslednjaSifra = "";

            if (dtPokupiSifre.Rows.Count == 1)
            {
                poslednjaSifra = dtPokupiSifre.Rows[0]["Dokument"].ToString();
            }
            else
            {
                poslednjaSifra = "";
            }


            int razdvojenaSifra = 0;
            string novaSifra = "UR";

            if (poslednjaSifra.Trim() == "")
            {
                novaSifra = "UR0000001";
            }
            else
            {
                razdvojenaSifra = int.Parse(poslednjaSifra.Remove(0, 2));
                razdvojenaSifra++;

                int brojKaratreraSifra = razdvojenaSifra.ToString().Length;
                int brojNulaKojeTrebaDodati = 7 - brojKaratreraSifra;

                for (int i = 0; i < brojNulaKojeTrebaDodati; i++)
                {
                    novaSifra += "0";
                }

                novaSifra += razdvojenaSifra.ToString();
            }
            try
            {
                string naredbaInsert = "Insert into ulazni_racuni (Storno, Dokument, Datum, ID_partnera, Broj_fakture, Opis, Ugovor, Program, Programska_aktivnost, Funkcija, Izvor_finansiranja, Korisnik, Poslednja_izmena, Valuta, TekuciRacun) values (@Storno, @Dokument, @Datum, @Partner, @BrojF, @Opis, @Ugovor, @Program, @ProgramskAktivnost, @Funkcija, @IzvorF, @Korisnik, current_timestamp, @Valuta, @Tekuci)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);

                komandaInsert.Parameters.AddWithValue("@Storno", 'N');
                komandaInsert.Parameters.AddWithValue("@Dokument", novaSifra);
                komandaInsert.Parameters.AddWithValue("@Datum", proDatum);
                komandaInsert.Parameters.AddWithValue("@Valuta", proValuta);
                komandaInsert.Parameters.AddWithValue("@Program", proProgram);
                komandaInsert.Parameters.AddWithValue("@ProgramskAktivnost", proProgramskAktivnost);
                komandaInsert.Parameters.AddWithValue("@Funkcija", proFunkcija);
                komandaInsert.Parameters.AddWithValue("@IzvorF", proIzvorF);
                komandaInsert.Parameters.AddWithValue("@Partner", proPartner);
                komandaInsert.Parameters.AddWithValue("@BrojF", proBrojF);
                komandaInsert.Parameters.AddWithValue("@Ugovor", proUgovor);
                komandaInsert.Parameters.AddWithValue("@Opis", proOpis);
                komandaInsert.Parameters.AddWithValue("@Korisnik", Session["korisnickoIme"]);
                komandaInsert.Parameters.AddWithValue("@Tekuci", proTekuci);

                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli ulazni račun!";
            }
            catch (Exception ero)
            {
                konekcija.Close();

                poruka[0] = "N";
                poruka[1] = "Greška prilikom unosa ulaznog računa!";
            }
            return poruka;
        }
    }
}