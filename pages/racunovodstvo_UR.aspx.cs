using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class pages_racunovodstvo_UR : System.Web.UI.Page
{
    string unetiZahtevi = "";
    [System.Web.Services.WebMethod(true)]
    public static string[] storno(string dokument)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_racunovodstvo_UR strana = new pages_racunovodstvo_UR();
        string[] poruka = new string[2];
        poruka = strana.Storniraj(dokument);
        return poruka;
    }

    [System.Web.Services.WebMethod(true)]
    public static string[] kreirajZS(string dokumenti)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_racunovodstvo_UR strana = new pages_racunovodstvo_UR();
        string[] poruka = new string[2];
        //razdvajam ulazne racune koji stizu
        string[] racuni = dokumenti.Split(',');
        //pozivanje funkcije koja kreira novi zahtev za sredstva
        poruka = strana.ZSkreiranje(racuni);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string[] ZSkreiranje(string[] dok)
    {
        //deklarisem proveru za upis u bazu
        string porukaUpisa = "D";

        string ulazni = "";
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        string[] poruka = new string[2];
        //punjenje promenljive ulazni za where uslov kao IN lista - ('', '')
        for (int i = 0; i < dok.Length; i++)
        {
            ulazni += "'" + dok[i] + "',";
        }
        ulazni = ulazni.Remove(ulazni.Length - 1);
        ulazni = ulazni.Replace("chk", "");

        //provera koliko postoji istih redova za ugovor, konto i dobavljaca, jer je to uslov za kreiranje novog ZS dokumenta
        DataTable dtTabela = new DataTable();
        string naredbaSelect = "select distinct u.Ugovor, s.Konto, u.ID_Partnera from ulazni_racuni u join ulazni_racuni_stavke s on u.Dokument = s.Dokument where u.Dokument in (" + ulazni + ")";
        MySqlCommand komandaSelect = new MySqlCommand(naredbaSelect, konekcija);
        MySqlDataAdapter adapter = new MySqlDataAdapter(komandaSelect);
        adapter.Fill(dtTabela);

        if(dtTabela.Rows.Count == 0)
        {
            poruka[0] = "N";
            poruka[1] = "Ne postoje stavke kako bi se kreirao Zahtev za sredstva!";
            return poruka;
        }

        foreach (DataRow red in dtTabela.Rows)
        {
            string novaSifraZS = NovaSifra.VratiSifru("SifraDokumenta", "zahtev_za_sredstva", nazivPoslovnice, "ZS");            
            string ulazniRacuni = "";
            //kupim sifre ulaznih racuna koji imaju isti ugovor, konto i id partnera
            DataTable dtPodaci = Upiti.Select2("distinct s.Dokument, u.Opis", "ulazni_racuni u join ulazni_racuni_stavke s on u.Dokument = s.Dokument", "u.Ugovor='" + red["Ugovor"].ToString() + "' and s.Konto ='" + red["Konto"].ToString() + "' and u.ID_Partnera = '" + red["ID_Partnera"].ToString() + "'", nazivPoslovnice);
            foreach (DataRow red1 in dtPodaci.Rows)
            {
                ulazniRacuni += "'" + red1["Dokument"].ToString() + "',";
            }

            ulazniRacuni = ulazniRacuni.Remove(ulazni.Length);
            //kreiram dokument i vracam poruku da li je uspesno kreiran, tacnije, uspesno upisan u bazu
            string povrat = kreirajDokument(novaSifraZS, ulazniRacuni, red["Ugovor"].ToString());
            if(povrat == "D")
            {
                //filtriram stavke ulaznih racuna koje pripadaju grupaciji ugovor - konto - dobavljac
                DataTable dtPodaci1 = Upiti.Select2("distinct s.ID, s.Konto, s.Iznos, u.Program, u.Programska_aktivnost, u.Funkcija, u.Izvor_finansiranja", "ulazni_racuni u join ulazni_racuni_stavke s on u.Dokument = s.Dokument", "u.Ugovor='" + red["Ugovor"].ToString() + "' and s.Konto ='" + red["Konto"].ToString() + "' and u.ID_Partnera = '" + red["ID_Partnera"].ToString() + "'", nazivPoslovnice);
                foreach (DataRow red2 in dtPodaci1.Rows)
                {
                    string novaSifraNM = NovaSifra.VratiSifru("Dokument1", "namena_sredstava", nazivPoslovnice, "NS");
                    //kreiram stavke za ZS dokument i vracam poruku da li je uspesno kreiran, tacnije, uspesno upisan u bazu
                    string povrat2 = kreirajDokumentStavke(novaSifraNM, red2["Iznos"].ToString(), novaSifraZS, red2["Konto"].ToString(), red2["Program"].ToString(), red2["Programska_aktivnost"].ToString(), red2["Funkcija"].ToString(), red2["Izvor_finansiranja"].ToString());
                    if(povrat2 == "N")
                    {
                        porukaUpisa = "N";
                        break;
                    }
                }
            }
            else
            {
                porukaUpisa = "N";
                break;
            }
        }
        if (porukaUpisa != "N")
        {
            poruka[0] = "D";
            poruka[1] = "Uspešno ste kreirali zahtev za sredstva!";
            return poruka;
        }
        else
        {
            poruka[0] = "N";
            poruka[1] = "Greška prilikom upisa, molimo Vas proverite sve podatke u ulaznom računu!";

            if (unetiZahtevi.Length > 0)
            {
                unetiZahtevi = unetiZahtevi.Substring(0, unetiZahtevi.Length - 1);
                string naredbaDelete = "delete from zahtev_za_sredstva where SifraDokumenta in (" + unetiZahtevi + ")";
                string naredbaDeleteS = "delete from namena_sredstava where Dokument in (" + unetiZahtevi + ")";
                MySqlCommand komandaDelete = new MySqlCommand(naredbaDelete, konekcija);
                MySqlCommand komandaDeleteS = new MySqlCommand(naredbaDeleteS, konekcija);

                konekcija.Open();
                komandaDelete.ExecuteNonQuery();
                komandaDeleteS.ExecuteNonQuery();
                konekcija.Close();
            }           

            return poruka;
        }
    }
    
            
    public string kreirajDokument(string sifra, string ulazni, string ugovor)
    {
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        string poruka = "D";
        string naredbaCreate = "Insert into zahtev_za_sredstva (SifraDokumenta, Datum, Korisnik, PoslednjaIzmena, Ugovor, UlazniRacun) values ('" + sifra + "', current_date, '" + Session["korisnickoIme"] + "', current_timestamp, '" + ugovor + "'," + ulazni + ")";
        
        try
        {
            MySqlCommand komandaCreate = new MySqlCommand(naredbaCreate, konekcija);

            konekcija.Open();
            komandaCreate.ExecuteNonQuery();
            konekcija.Close();

            unetiZahtevi += "'" + sifra + "',";

            poruka = "D";
        }
        catch (Exception ero)
        {
            konekcija.Close();
            poruka = "N";
        }
        return poruka;
    }
    public string kreirajDokumentStavke(string Brdok, string iznos, string zsBrdok, string konto, string program, string prog_akt, string funkcija, string izvorFin)
    {
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        string poruka = "D";
        string naredbaCreate = "Insert into namena_sredstava (Dokument1, Dokument, Iznos, Konto, IDprogram, IDprogramskaAktivnost, IDfunkcija, IDizvorFinansiranja) values ('" + Brdok + "','" + zsBrdok + "','" + iznos + "','" + konto + "','" + program +  "','" + prog_akt + "','" + funkcija + "','" + izvorFin + "')";

        try
        {
            MySqlCommand komandaCreate = new MySqlCommand(naredbaCreate, konekcija);

            konekcija.Open();
            komandaCreate.ExecuteNonQuery();
            konekcija.Close();

            poruka = "D";
        }
        catch (Exception ero)
        {
            konekcija.Close();
            poruka = "N";
        }
        return poruka;
    }



    public string[] Storniraj(string dok)
    {
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        string[] poruka = new string[2];

        string naredbaUpdate = "Update ulazni_racuni set Storno='D' where Dokument='" + dok + "'";

        try
        {
            MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);

            konekcija.Open();
            komandaUpdate.ExecuteNonQuery();
            konekcija.Close();

            poruka[0] = "D";
            poruka[1] = "Uspešno ste stornirali ulazni račun!";
        }
        catch (Exception ero)
        {
            konekcija.Close();
            poruka[0] = "N";
            //poruka[1] = ero.ToString();
            poruka[1] = "Greška prilikom storniranja ulaznog računa!";
        }
        return poruka;
    }
}