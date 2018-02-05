using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_Komitenti_poslovniPartneri_KlasifikacijaAsortimanaDobavljaca_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proInputKlasifikacija, string proInputNazivKlasifikacije)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Komitenti_poslovniPartneri_KlasifikacijaAsortimanaDobavljaca_Dodavanje strana = new pages_Komitenti_poslovniPartneri_KlasifikacijaAsortimanaDobavljaca_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proInputKlasifikacija, proInputNazivKlasifikacije);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //if ((String)Session["sifraPartneraZaKlasifikacijuAsortimana"] == "" || (String)Session["sifraPartneraZaKlasifikacijuAsortimana"] == null)
            //{
            //    Response.Redirect("/pages/navbar.aspx");
            //}

            string Korisnik = (String)Session["sifraPartneraZaKlasifikacijuAsortimana"];

            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;


            if (Request.QueryString["SIFRA20"] == null)
            {
                divDok.Visible = false;

                inputKomitent.Value = Korisnik;

            }
            else
            {
                divDok.Visible = true;

                DataTable dtIzmena = Upiti.Select2("*", "klasifikacija_asortimana", "Sifra='" + Request.QueryString["SIFRA20"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["Sifra"].ToString();
                    inputKlasifikacija.Value = red["Klasifikacija"].ToString();
                    inputNazivKlasifikacije.Value = red["NazivKlasifikacije"].ToString();

                    DataTable dtUzmiPartnera = Upiti.Select2("Sifra,ImePrezime,JMBG", "poslovni_partneri", "Sifra='" + red["IDpartnera"].ToString() + "'", nazivPoslovnice);


                    foreach (DataRow redic in dtUzmiPartnera.Rows)
                    {
                        inputKomitent.Value = redic["Sifra"].ToString() + ", " + redic["ImePrezime"].ToString() + ", " + redic["JMBG"].ToString();
                    }

                }

            }

        }
    }

    public string[] Sacuvaj(string vrednost, string proInputKlasifikacija, string proInputNazivKlasifikacije)
    {
        if ((String)Session["sifraPartneraZaKlasifikacijuAsortimana"] == "" || (String)Session["sifraPartneraZaKlasifikacijuAsortimana"] == null)
        {
            Response.Redirect("/pages/navbar.aspx");
        }

        string KorisnikUgovor = (String)Session["sifraPartneraZaKlasifikacijuAsortimana"];

        string[] poruka = new string[3];

        //poruka[0] = "D";
        //poruka[1] = "Uspešno ste izmenili ulazni račun!";
        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";

        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string vrednost = Request.QueryString["SIFRA20"];

        string proKlasifikacija = proInputKlasifikacija;
        string proNazivKlasifikacije = proInputNazivKlasifikacije;


        if (proKlasifikacija == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli klasifikaciju!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli tekući račun!";
            //return;
        }

        if (proNazivKlasifikacije == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli naziv klasifikacije!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli tekući račun!";
            //return;
        }
        //lblObavestenje.Text = "";
        string Korisnik = (String)Session["korisnickoIme"];


        //Session["povratnaSesija1"] = KorisnikUgovor;
        //Session["povratnaSesija2"] = KorisnikUgovor;//proKlasifikacija ;

        DataTable dtIzmena = Upiti.Select2("*", "klasifikacija_asortimana", "Sifra='" + vrednost.Trim() + "'", nazivPoslovnice);

        string sfr = "";

        foreach (DataRow red in dtIzmena.Rows)
        {

            DataTable dtUzmiPartnera = Upiti.Select2("Sifra", "poslovni_partneri", "Sifra='" + red["IDpartnera"].ToString() + "'", nazivPoslovnice);

            foreach (DataRow redic in dtUzmiPartnera.Rows)
            {
                sfr = redic["Sifra"].ToString();
            }

        }


        if (vrednost != "")
        {
            try
            {
                //DataTable dtOstali = Upiti.Select2("Racun,IDpartnera", "klasifikacija_asortimana", "ID <> '" + vrednost + "'", nazivPoslovnice);

                string naredbaUpdate = "Update klasifikacija_asortimana set Klasifikacija=@Klasifikacija, NazivKlasifikacije=@NazivKlasifikacije, IDpartnera=@IDpartnera, Korisnik=@Korisnik, PoslednjaIzmena=@PoslednjaIzmena where Sifra='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@Klasifikacija", proKlasifikacija);
                komandaUpdate.Parameters.AddWithValue("@NazivKlasifikacije", proNazivKlasifikacije);
                komandaUpdate.Parameters.AddWithValue("@IDpartnera", KorisnikUgovor);
                komandaUpdate.Parameters.AddWithValue("@Korisnik", Korisnik);
                komandaUpdate.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);


                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili podatke klasifikacije asortimana!";
                poruka[2] = KorisnikUgovor;
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
            try
            {
                string novaSifra = NovaSifra.VratiSifru("Sifra", "klasifikacija_asortimana", nazivPoslovnice, "KA");


                string naredbaInsert = "Insert into klasifikacija_asortimana (Sifra,IDpartnera,Klasifikacija,NazivKlasifikacije,Korisnik,PoslednjaIzmena) values (@Sifra,@IDpartnera,@Klasifikacija,@NazivKlasifikacije,@Korisnik,@PoslednjaIzmena)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@IDpartnera", KorisnikUgovor);
                komandaInsert.Parameters.AddWithValue("@Klasifikacija", proKlasifikacija.Trim());
                komandaInsert.Parameters.AddWithValue("@NazivKlasifikacije", proNazivKlasifikacije.Trim());
                komandaInsert.Parameters.AddWithValue("@Korisnik", Korisnik);
                komandaInsert.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);

                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli podatke klasifikacije asortimana!";
                poruka[2] = KorisnikUgovor;
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
