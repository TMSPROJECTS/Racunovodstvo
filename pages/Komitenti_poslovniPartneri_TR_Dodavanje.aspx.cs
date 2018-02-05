using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_Komitenti_poslovniPartneri_TR_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proInputKomitent, string proInputRacun)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Komitenti_poslovniPartneri_TR_Dodavanje strana = new pages_Komitenti_poslovniPartneri_TR_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proInputKomitent, proInputRacun);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //if ((String)Session["sifraPartneraZaTekuciRacun"] == "" || (String)Session["sifraPartneraZaTekuciRacun"] == null)
            //{
            //    Response.Redirect("/pages/navbar.aspx");
            //}

            string Korisnik = (String)Session["sifraPartneraZaTekuciRacun"];

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

                DataTable dtIzmena = Upiti.Select2("*", "tekuci_racun", "ID='" + Request.QueryString["SIFRA20"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["ID"].ToString();
                    inputRacun.Value = red["Racun"].ToString();

                    DataTable dtUzmiPartnera = Upiti.Select2("Sifra,ImePrezime,JMBG", "poslovni_partneri", "Sifra='" + red["IDpartnera"].ToString() + "'", nazivPoslovnice);


                    foreach (DataRow redic in dtUzmiPartnera.Rows)
                    {
                        inputKomitent.Value = redic["Sifra"].ToString() + ", " + redic["ImePrezime"].ToString() + ", " + redic["JMBG"].ToString();
                    }

                }

            }

        }
    }

    public string[] Sacuvaj(string vrednost, string proInputKomitent, string proInputRacun)
    {
        if ((String)Session["sifraPartneraZaTekuciRacun"] == "" || (String)Session["sifraPartneraZaTekuciRacun"] == null)
        {
            Response.Redirect("/pages/navbar.aspx");
        }

        string KorisnikUgovor = (String)Session["sifraPartneraZaTekuciRacun"];

        string[] poruka = new string[3];

        //poruka[0] = "D";
        //poruka[1] = "Uspešno ste izmenili ulazni račun!";
        //poruka[0] = "N";
        //poruka[1] = Request.QueryString["SIFRA"].Trim();


        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string vrednost = Request.QueryString["SIFRA20"];

        string proTekuciRacun = proInputRacun;


        if (proTekuciRacun == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli tekući račun!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli tekući račun!";
            //return;
        }
        //lblObavestenje.Text = "";
        string Korisnik = (String)Session["korisnickoIme"];


        //Session["povratnaSesija1"] = KorisnikUgovor;
        //Session["povratnaSesija2"] = proTekuciRacun;

        DataTable dtIzmena = Upiti.Select2("*", "tekuci_racun", "ID='" + vrednost.Trim() + "'", nazivPoslovnice);

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
                DataTable dtOstali = Upiti.Select2("Racun,IDpartnera", "tekuci_racun", "ID <> '" + vrednost + "'", nazivPoslovnice);

                string naredbaUpdate = "Update tekuci_racun set Racun=@Racun, IDpartnera=@IDpartnera, PoslednjaIzmena=@PoslednjaIzmena where ID='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@Racun", proTekuciRacun);
                komandaUpdate.Parameters.AddWithValue("@IDpartnera", KorisnikUgovor);
                komandaUpdate.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);


                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili podatke tekućeg računa!";
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
                string novaSifra = NovaSifra.VratiSifru("ID", "tekuci_racun", nazivPoslovnice, "TR");


                string naredbaInsert = "Insert into tekuci_racun (ID,Racun,IDpartnera,PoslednjaIzmena) values (@ID,@Racun,@IDpartnera,@PoslednjaIzmena)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@ID", novaSifra);
                komandaInsert.Parameters.AddWithValue("@Racun", proTekuciRacun.Trim());
                komandaInsert.Parameters.AddWithValue("@IDpartnera", KorisnikUgovor);
                komandaInsert.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);

                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli podatke tekućeg računa!";
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
