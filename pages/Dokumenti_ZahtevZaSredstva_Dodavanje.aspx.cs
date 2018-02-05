using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_Dokumenti_ZahtevZaSredstva_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proBrojZahteva, string proDatumZahteva, string proRacun, string proUgovorDob, string proNapomena)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Dokumenti_ZahtevZaSredstva_Dodavanje strana = new pages_Dokumenti_ZahtevZaSredstva_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proBrojZahteva,proDatumZahteva,proRacun,proUgovorDob,proNapomena);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;


            if (Request.QueryString["SIFRA3"] == null)
            {
                divDok.Visible = false;

                brojZahteva.Value = "/" + DateTime.Now.Year;
            }
            else
            {
                divDok.Visible = true;
                DataTable dtIzmena = Upiti.Select2("*", "zahtev_za_sredstva", "SifraDokumenta='" + Request.QueryString["SIFRA3"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {

                    dokument.Value = red["SifraDokumenta"].ToString();

                    DateTime vreme = DateTime.Parse(red["Datum"].ToString());

                    string godina = vreme.Year.ToString();
                    string mesec = vreme.Month.ToString();
                    string dan = vreme.Day.ToString();

                    if (mesec.Length == 1)
                    {
                        mesec = "0" + mesec;
                    }
                    if (dan.Length == 1)
                    {
                        dan = "0" + dan;
                    }

                    datumZahteva.Value = godina + "-" + mesec + "-" + dan;
                    dokument.Value = red["SifraDokumenta"].ToString();
                    brojZahteva.Value = red["Broj"].ToString();
                    racun.Value = red["Racun"].ToString();
                    napomena.Value = red["Napomena"].ToString();
                    ugovorDob.Value = red["Ugovor"].ToString();

                }

            }

         
        }
    }

    public string[] Sacuvaj(string vrednost, string proBrojZahteva, string proDatumZahteva, string proRacun, string proUgovorDob, string proNapomena)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";

        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
       // string vrednost = Request.QueryString["SIFRA3"];

       // string proDokument = dokument.Value;
        //string proDatumZahteva = datumZahteva.Value;
        //string proBrojZahteva = brojZahteva.Value;
        //string proRacun = racun.Value;
        //string proNapomena = napomena.Value;

        if (proDatumZahteva.Trim () == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali datum!";
            return poruka;
            //lblObavestenje.Text = "Niste odabrali datum!";
            //return;
        }

        if (proRacun.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali račun!";
            return poruka;
            //lblObavestenje.Text = "Niste odabrali račun!";
            //return;
        }

        

        //lblObavestenje.Text = "";
        string Korisnik = (String)Session["korisnickoIme"];


        if (vrednost != "")
        {
            try
            {
                string naredbaUpdate = "Update zahtev_za_sredstva set Datum=@Datum,Racun=@Racun,Napomena=@Napomena,Ugovor=@Ugovor, Korisnik=@Korisnik,PoslednjaIzmena=@PoslednjaIzmena where SifraDokumenta='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@Datum", proDatumZahteva);
                //komandaUpdate.Parameters.AddWithValue("@Broj", proBrojZahteva);
                komandaUpdate.Parameters.AddWithValue("@Racun", proRacun);
                komandaUpdate.Parameters.AddWithValue("@Napomena", proNapomena);
                komandaUpdate.Parameters.AddWithValue("@Korisnik", Korisnik);
                komandaUpdate.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);
                komandaUpdate.Parameters.AddWithValue("@Ugovor", proUgovorDob);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili podatke o zahtevu za sredstva!";
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

           // DataTable dtPokupiSifre = Upiti.Select2("SifraDokumenta", "zahtev_za_sredstva", "S='S' order by SifraDokumenta asc", nazivPoslovnice);
            DataTable dtPokupiBroj = Upiti.Select2("max(Broj) as maksimum", "zahtev_za_sredstva", "ne", nazivPoslovnice);

           // string poslednjaSifra = "";
            string poslednjiBroj = "";

            //foreach (DataRow red in dtPokupiSifre.Rows)
            //{

            //    poslednjaSifra = red["SifraDokumenta"].ToString();
            //}

            int razdvojenaSifra = 0;


            //if (poslednjaSifra.Trim() == "")
            //{
            //    razdvojenaSifra = 1;
            //}
            //else
            //{
            //    razdvojenaSifra = int.Parse(poslednjaSifra.Remove(0, 2));
            //    razdvojenaSifra++;
            //}

            string novaSifra = NovaSifra.VratiSifru("SifraDokumenta", "zahtev_za_sredstva", nazivPoslovnice, "ZS");

            //int brojKaratreraSifra = razdvojenaSifra.ToString().Length;

            //int brojNulaKojeTrebaDodati = 7 - brojKaratreraSifra;

            //string novaSifra = "ZS";

            //for (int i = 0; i < brojNulaKojeTrebaDodati; i++)
            //{
            //    novaSifra += "0";
            //}

            //novaSifra += razdvojenaSifra.ToString();


            if (dtPokupiBroj.Rows[0]["maksimum"] != null && dtPokupiBroj .Rows[0]["maksimum"].ToString ().Trim () != "")
            {
                poslednjiBroj  = dtPokupiBroj.Rows[0]["maksimum"].ToString();
            }

            if (poslednjiBroj != "")
            {
                string[] rst = poslednjiBroj.Split(new char[] { '/' });

                if (DateTime.Now.Year.ToString() != rst[1].ToString())
                {
                    poslednjiBroj = "";
                }
            }

            string noviBroj = "";

            if (poslednjiBroj.Trim() == "")
            {
                noviBroj = "1/" + DateTime.Now.Year;
            }
            else
            {
                string[] rastavi = poslednjiBroj.Split(new char[] { '/' });
                noviBroj = (int.Parse(rastavi[0].ToString()) + 1) + "/" + DateTime.Now.Year;
            }


            try
            {
                string naredbaInsert = "Insert into zahtev_za_sredstva (SifraDokumenta,Datum,Broj,Racun,Korisnik,PoslednjaIzmena,Napomena, Ugovor) values (@SifraDokumenta,@Datum,@Broj,@Racun,@Korisnik,@PoslednjaIzmena,@Napomena, @Ugovor)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@SifraDokumenta", novaSifra);
                komandaInsert.Parameters.AddWithValue("Datum", proDatumZahteva);
                komandaInsert.Parameters.AddWithValue("@Broj", noviBroj);
                komandaInsert.Parameters.AddWithValue("@Racun", proRacun);
                komandaInsert.Parameters.AddWithValue("@Korisnik", Korisnik);
                komandaInsert.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);
                komandaInsert.Parameters.AddWithValue("@Napomena", proNapomena);
                komandaInsert.Parameters.AddWithValue("@Ugovor", proUgovorDob);
                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli novi zahtev za transfer sredstava!";
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