using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_PomocniPodaci_GrupeTroskova_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proNaziv)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_PomocniPodaci_GrupeTroskova_Dodavanje strana = new pages_PomocniPodaci_GrupeTroskova_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proNaziv);
        return poruka;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;


            if (Request.QueryString["SIFRA9"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;
                DataTable dtIzmena = Upiti.Select2("*", "grupe_troskova", "Sifra='" + Request.QueryString["SIFRA9"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["Sifra"].ToString();
                    inputNaziv.Value = red["Naziv"].ToString();
 
                }

            }


        }
    }

    public string[] Sacuvaj(string vrednost, string proNaziv)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";


        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string vrednost = Request.QueryString["SIFRA9"];

        //string proNaziv = inputNaziv.Value.Trim();
       


        if (proNaziv == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli naziv!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli naziv!";
            //return;
        }
       

      //  lblObavestenje.Text = "";

        string Korisnik = (String)Session["korisnickoIme"];

        if (vrednost != "")
        {
            DataTable dtProveriDaLiPostojiNaziv = Upiti.Select2("Naziv", "grupe_troskova", "Sifra<> '" + vrednost + "' and Naziv='" + proNaziv + "'", nazivPoslovnice);

            if (dtProveriDaLiPostojiNaziv.Rows.Count > 0)
            {
                poruka[0] = "N";
                poruka[1] = "Uneti naziv već postoji!";
                return poruka;
                //lblObavestenje.Text = "Uneti naziv već postoji!";
                //return;
            }

            try
            {
                string naredbaUpdate = "Update grupe_troskova set Naziv=@Naziv,Uneo=@Uneo where Sifra='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@Naziv", proNaziv);
                komandaUpdate.Parameters.AddWithValue("@Uneo", Korisnik);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili grupu troškova!";
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

            string novaSifra = NovaSifra.VratiSifru("Sifra", "grupe_troskova", nazivPoslovnice, "GT");

            DataTable dtProveriDaLiPostojiNaziv = Upiti.Select2("Naziv", "grupe_troskova", "Naziv='" + proNaziv + "'",nazivPoslovnice);

            if (dtProveriDaLiPostojiNaziv.Rows.Count > 0)
            {
                poruka[0] = "N";
                poruka[1] = "Uneti naziv već postoji!";
                return poruka;
                //lblObavestenje.Text = "Uneti naziv već postoji!";
                //return;
            }

            try
            {
                string naredbaInsert = "Insert into grupe_troskova (Sifra,Naziv,Uneo) values (@Sifra,@Naziv,@Uneo)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@Naziv", proNaziv);
                komandaInsert.Parameters.AddWithValue("@Uneo", Korisnik);

                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli grupu troškova!";
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