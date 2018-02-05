using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_Komitenti_ugovoriPartnera_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proDDLkorisnik, string proBrUgovora, string proDatumOd, string proDatumDo, string proIznosUgovora, string proPreostaliIznos, string proOpis)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Komitenti_ugovoriPartnera_Dodavanje strana = new pages_Komitenti_ugovoriPartnera_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proDDLkorisnik ,proBrUgovora,proDatumOd,proDatumDo,proIznosUgovora , proPreostaliIznos ,proOpis );
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

            System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "poslovni_partneri", "Sifra is not null order by Sifra", nazivPoslovnice);

            foreach (System.Data.DataRow red in dtSviPodaci.Rows)
            {
                ddlKorisnik.Items.Add(red["Sifra"].ToString() + ", " + red["ImePrezime"].ToString() + ", " + red["JMBG"].ToString());
            }

            //string vrednost = Request.QueryString["SIFRA2"];

            //////////////////

            if (Request.QueryString["SIFRA2"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;
                DataTable dtIzmena = Upiti.Select2("*", "ugovori_partnera", "SifraUgovora='" + Request.QueryString["SIFRA2"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["SifraUgovora"].ToString();

                    string korisnik = red["IDpartnera"].ToString();


                    DataTable dtSvi = Upiti.Select2("*", "poslovni_partneri", "Sifra='" + korisnik + "'", nazivPoslovnice);

                    string korisnikZaDDL = "";

                    foreach (DataRow redic in dtSvi.Rows)
                    {
                        korisnikZaDDL = redic["Sifra"].ToString() + ", " + redic["ImePrezime"].ToString() + ", " + redic["JMBG"].ToString();
                    }

                    ddlKorisnik.Value = korisnikZaDDL;
                    brUgovora.Value = red["BrojUgovora"].ToString();

                    DateTime dt1 = DateTime.Parse(red["DatumUgovora"].ToString());
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

                    datumOd.Value = godina1 + "-" + mesec1 + "-" + dan1;
                    datumDo.Value = godina2 + "-" + mesec2 + "-" + dan2;
                    iznosUgovora.Value = red["IznosUgovora"].ToString();
                    preostaliIznos.Value = red["PreostaliIznos"].ToString();
                    opis.Value = red["Opis"].ToString();

                }

            }

            /////////////////


            

           

               
          }
           
        

                      
    }

    public string[] Sacuvaj(string vrednost, string SifraK , string brojUgovora, string datum, string vaziDo, string iznosUg, string ostaliIznos , string opiss)
    {
        string[] poruka = new string[2];

        //poruka[0] = "D";
        //poruka[1] = "Uspešno ste izmenili ulazni račun!";
        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";

        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string vrednost = Request.QueryString["SIFRA2"];

        if (SifraK == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali korisnika!";
            return poruka;
            //lblObavestenje.Text = "Niste odabrali korisnika!";
            //return;
        }

        string [] rastavi = SifraK.ToString().Split (new char[] {','});
        string sifraKorisnika = rastavi[0];
        //string brojUgovora = brUgovora.Value;
        //string datum = datumOd.Value;
        //string vaziDo = datumDo.Value;
        //string iznosUg = iznosUgovora.Value;
        ////string iznosUcesca = ucesce.Value;
        //string ostaliIznos = preostaliIznos.Value;
        //string opiss = opis.Value;

        if (brojUgovora == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli broj ugovora!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli broj ugovora!";
            //return;
        }
        if (datum == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli Datum ugovora!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli Datum ugovora!";
            //return;
        }
        if (vaziDo == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli datum do kog ugovor važi!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli datum do kog ugovor važi!";
            //return;
        }
        if (iznosUg == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli iznos ugovora!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli iznos ugovora!";
            //return;
        }

        try
        {
            double iznosUgovora2 = double.Parse(iznosUg);
        }
        catch
        {
            poruka[0] = "N";
            poruka[1] = "Iznos ugovora mora biti numerička vrednost!";
            return poruka;
            //lblObavestenje.Text = "Iznos ugovora mora biti numerička vrednost!";
            //return;
        }

        if (ostaliIznos.Trim() != "")
        {
            try
            {
                double ostaliIznos2 = double.Parse(ostaliIznos);
            }
            catch
            {
                poruka[0] = "N";
                poruka[1] = "Ostali iznos mora biti numerička vrednost!";
                return poruka;
                //lblObavestenje.Text = "Ostali iznos mora biti numerička vrednost!";
                //return;
            }
        }

       // lblObavestenje.Text = "";

        if (vrednost != "")
        {
            try
            {
                string naredbaUpdate = "Update ugovori_partnera set BrojUgovora=@BrojUgovora, DatumUgovora=@DatumUgovora, VaziDo=@VaziDo, IznosUgovora=@IznosUgovora, PreostaliIznos=@PreostaliIznos,Opis=@Opis,IDpartnera=@IDpartnera where SifraUgovora='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@BrojUgovora", brojUgovora);
                komandaUpdate.Parameters.AddWithValue("@DatumUgovora", datum);
                komandaUpdate.Parameters.AddWithValue("@VaziDo", vaziDo);
                komandaUpdate.Parameters.AddWithValue("@IznosUgovora", iznosUg);
                if (ostaliIznos.Trim() == "")
                {
                    komandaUpdate.Parameters.AddWithValue("@PreostaliIznos", "0");
                }
                else
                {
                    komandaUpdate.Parameters.AddWithValue("@PreostaliIznos", ostaliIznos);
                }
                komandaUpdate.Parameters.AddWithValue("@Opis", opiss);
                komandaUpdate.Parameters.AddWithValue("@IDpartnera", sifraKorisnika);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili podatke o ugovorima partnera!";

            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!!!!";
            }

            return poruka;

        }
        else
        { 

            string novaSifra = NovaSifra.VratiSifru("sifraUgovora", "ugovori_partnera", nazivPoslovnice, "UG");


            string Korisnik = (String)Session["korisnickoIme"];


            try
            {
                string naredbaInsert = "Insert into ugovori_partnera (SifraUgovora,BrojUgovora,DatumUgovora,VaziDo,IznosUgovora,PreostaliIznos,Opis,DatumUnosa,UgovorUneo,IDpartnera) values (@SifraUgovora,@BrojUgovora,@DatumUgovora,@VaziDo,@IznosUgovora,@PreostaliIznos,@Opis,@DatumUnosa,@UgovorUneo,@IDpartnera)";

                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@SifraUgovora", novaSifra);
                komandaInsert.Parameters.AddWithValue("@BrojUgovora", brojUgovora);
                komandaInsert.Parameters.AddWithValue("@DatumUgovora", datum);
                komandaInsert.Parameters.AddWithValue("@VaziDo", vaziDo);
                komandaInsert.Parameters.AddWithValue("IznosUgovora", iznosUg);
                if (ostaliIznos.Trim() == "")
                {
                    komandaInsert.Parameters.AddWithValue("@PreostaliIznos", "0");
                }
                else
                {
                    komandaInsert.Parameters.AddWithValue("@PreostaliIznos", ostaliIznos);
                }
                komandaInsert.Parameters.AddWithValue("@Opis", opiss);
                komandaInsert.Parameters.AddWithValue("@DatumUnosa", DateTime.Now);
                komandaInsert.Parameters.AddWithValue("@UgovorUneo", Korisnik);
                komandaInsert.Parameters.AddWithValue("@IDpartnera", sifraKorisnika);
                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli podatke o ugovorima partnera!";

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