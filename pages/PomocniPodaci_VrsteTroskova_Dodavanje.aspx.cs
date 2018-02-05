using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_PomocniPodaci_VrsteTroskova_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proNaziv,string proJedinicaMere,string proGrupa, string proKonto,string proSifraPlacanja,string proPozivNaBroj)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_PomocniPodaci_VrsteTroskova_Dodavanje strana = new pages_PomocniPodaci_VrsteTroskova_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proNaziv,proJedinicaMere,proGrupa,proKonto,proSifraPlacanja,proPozivNaBroj);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

            //string vrednost = Request.QueryString["SIFRA10"];

            System.Data.DataTable dtSveGrupe = Upiti.Select2("*", "grupe_troskova", "ne", nazivPoslovnice);


            //funkcija.Items.Add("-- odaberite funkciju --");
            foreach (DataRow red in dtSveGrupe.Rows)
            {
                grupa.Items.Add(red["Naziv"].ToString());
            }
          
            ///////////////////
            /////////////////////
          


            if (Request.QueryString["SIFRA10"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;
                DataTable dtIzmena = Upiti.Select2("*", "vrste_troskova", "Sifra='" + Request.QueryString["SIFRA10"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["Sifra"].ToString();
                    naziv.Value = red["Naziv"].ToString();
                    jedinicaMere.Value = red["JedinicaMere"].ToString();
                    konto.Value = red["Konto"].ToString();
                    sifraPlacanja.Value = red["SifraPlacanja"].ToString();
                    pozivNaBroj.Value = red["PozivNaBroj"].ToString();

                    foreach (DataRow redic in dtSveGrupe.Rows)
                    {
                        if (red["IDgrupe"].ToString() == redic["Sifra"].ToString())
                        {
                            grupa.Value = redic["Naziv"].ToString();
                        }
                    }

                }

            }




        }
    }

    public string[] Sacuvaj(string vrednost, string proNaziv, string proJedinica, string proGrupa, string proKonto, string proSifraPlacanja, string proPozivNaBroj)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";


        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string vrednost = Request.QueryString["SIFRA10"];

        System.Data.DataTable dtSveGrupe = Upiti.Select2("*", "grupe_troskova", "ne", nazivPoslovnice);

        string idGrupe = "";

        if (proGrupa == "-- Izaberite --")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali grupu troškova!";
            return poruka;
            //lblObavestenje.Text = "Niste odabrali grupu troškova!";
            //return;
        }


        foreach (DataRow red in dtSveGrupe.Rows)
        {
            if (red["Naziv"].ToString() == proGrupa)
            {
                idGrupe = red["Sifra"].ToString();
            }
        }


        //string proNaziv = naziv.Value;
        //string proJedinica= jedinicaMere.Value;
        //string proKonto = konto.Value;
        //string proSifraPlacanja = sifraPlacanja.Value;
        //string proPozivNaBroj = pozivNaBroj.Value;

        if (proNaziv.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli naziv!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli naziv!";
            //return;
        }
        if (proJedinica.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli jedinicu mere!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli jedinicu mere!";
            //return;
        }
        




       // lblObavestenje.Text = "";



        if (vrednost != "")
        {
            DataTable dtProveriDaLiPostojiVec = Upiti.Select2("*", "vrste_troskova", "Sifra <> '" + vrednost + "' and Konto='" + proKonto.Trim() + "' and Naziv='" + proNaziv.Trim() + "'", nazivPoslovnice);

            if (dtProveriDaLiPostojiVec.Rows.Count > 0)
            {
                poruka[0] = "N";
                poruka[1] = "Uneli ste kombinaciju Konto/Naziv koja već postoji!";
                return poruka;
                //lblObavestenje.Text = "Uneli ste kombinaciju Konto/Naziv koja već postoji!";
                //return;
            }

            try
            {

                string naredbaUpdate = "Update vrste_troskova set Konto=@Konto, Naziv=@Naziv, IDgrupe=@IDgrupe, JedinicaMere=@JedinicaMere, SifraPlacanja=@SifraPlacanja, PozivNaBroj=@PozivNaBroj where Sifra='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@Konto", proKonto);
                komandaUpdate.Parameters.AddWithValue("@Naziv", proNaziv);
                komandaUpdate.Parameters.AddWithValue("@IDgrupe", idGrupe);
                komandaUpdate.Parameters.AddWithValue("@JedinicaMere", proJedinica);
                komandaUpdate.Parameters.AddWithValue("@SifraPlacanja", proSifraPlacanja);
                komandaUpdate.Parameters.AddWithValue("@PozivNaBroj", proPozivNaBroj);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili vrstu troškova!";
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

            DataTable dtProveriDaLiPostojiVec = Upiti.Select2("*", "vrste_troskova", "Konto='" + proKonto.Trim () + "' and Naziv='" + proNaziv.Trim () + "'", nazivPoslovnice);

            if (dtProveriDaLiPostojiVec.Rows.Count > 0)
            {
                poruka[0] = "N";
                poruka[1] = "Uneli ste kombinaciju Konto/Naziv koja već postoji!";
                return poruka;
                //lblObavestenje.Text = "Uneli ste kombinaciju Konto/Naziv koja već postoji!";
                //return;
            }

            string novaSifra = NovaSifra.VratiSifru("Sifra", "vrste_troskova", nazivPoslovnice, "VT");



            try
            {
                string naredbaInsert = "Insert into vrste_troskova (Sifra,Konto,Naziv,IDgrupe,JedinicaMere,Konto2,SifraPlacanja,PozivNaBroj) values (@Sifra,@Konto,@Naziv,@IDgrupe,@JedinicaMere,@Konto2,@SifraPlacanja,@PozivNaBroj)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@Konto", proKonto);
                komandaInsert.Parameters.AddWithValue("@Naziv", proNaziv);
                komandaInsert.Parameters.AddWithValue("@IDgrupe", idGrupe);
                komandaInsert.Parameters.AddWithValue("@JedinicaMere", proJedinica);
                komandaInsert.Parameters.AddWithValue("@Konto2", "");
                komandaInsert.Parameters.AddWithValue("@SifraPlacanja", proSifraPlacanja);
                komandaInsert.Parameters.AddWithValue("@PozivNaBroj", proPozivNaBroj);

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