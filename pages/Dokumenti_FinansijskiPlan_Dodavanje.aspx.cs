using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


public partial class pages_Dokumenti_FinansijskiPlan_Dodavanje : System.Web.UI.Page
{

    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proInputGodina, string proInputNaziv, string proDatumZahteva)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Dokumenti_FinansijskiPlan_Dodavanje strana = new pages_Dokumenti_FinansijskiPlan_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proInputGodina, proInputNaziv ,proDatumZahteva);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;


            if (Request.QueryString["SIFRA5"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;

                DataTable dtIzmena = Upiti.Select2("*", "finansijski_plan", "SifraPlana='" + Request.QueryString["SIFRA5"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    inputNaziv.Value = red["Naziv"].ToString();
                    DateTime vreme = DateTime.Parse(red["Datum"].ToString());

                    godina.Value = red["Godina"].ToString();
                    
                    dokument.Value = red["SifraPlana"].ToString();
                    string godina2 = vreme.Year.ToString();
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

                    datumZahteva.Value = godina2 + "-" + mesec + "-" + dan;
                }

            }

        }
    }

    public string[] Sacuvaj(string vrednost, string proGodina, string proNaziv, string proDatum)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        // string vrednost = Request.QueryString["SIFRA5"];

        //string proNaziv = inputNaziv.Value.Trim();
        //string proDatum = datumZahteva.Value.Trim();

       

        if (proNaziv == "")
        { 
            poruka[0] = "N";
            poruka[1] = "Niste uneli naziv!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli naziv!";
            //return;
        }
        if (proDatum == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli datum!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli datum!";
            //return;
        }

        //lblObavestenje.Text = "";

        string Korisnik = (String)Session["korisnickoIme"];


        if (vrednost != "")
        {
            try
            {
                string naredbaUpdate = "Update finansijski_plan set Naziv=@Naziv, Datum=@Datum,Korisnik=@Korisnik,PoslednjaIzmena=@PoslednjaIzmena,Godina=@Godina where SifraPlana='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@Naziv", proNaziv);
                komandaUpdate.Parameters.AddWithValue("@Datum", proDatum);
                komandaUpdate.Parameters.AddWithValue("@Korisnik", Korisnik);
                komandaUpdate.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);
                komandaUpdate.Parameters.AddWithValue("@Godina", proGodina);


                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili podatke o finansijskom planu!";
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

            string novaSifra = NovaSifra.VratiSifru("SifraPlana", "Finansijski_plan", nazivPoslovnice, "FP");

            try
            {
                string naredbaInsert = "Insert into finansijski_plan (SifraPlana,Sredstva,Plan,Naziv,Datum,Korisnik,PoslednjaIzmena,Godina) values (@SifraPlana,@Sredstva,@Plan,@Naziv,@Datum,@Korisnik,@PoslednjaIzmena,@Godina)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@SifraPlana", novaSifra);
                komandaInsert.Parameters.AddWithValue("@Sredstva", "plan");
                komandaInsert.Parameters.AddWithValue("@Plan", "realizacija");
                komandaInsert.Parameters.AddWithValue("@Naziv", proNaziv);
                komandaInsert.Parameters.AddWithValue("@Datum", proDatum);
                komandaInsert.Parameters.AddWithValue("@Korisnik", Korisnik);
                komandaInsert.Parameters.AddWithValue("@PoslednjaIzmena", DateTime.Now);
                komandaInsert.Parameters.AddWithValue("@Godina", proGodina);
                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli finansijski plan!";
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