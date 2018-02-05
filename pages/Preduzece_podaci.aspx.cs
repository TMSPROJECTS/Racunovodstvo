using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


public partial class pages_poslovneJediniceUnos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string np = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

            string odabrano = (String)Session["odabranaPoslovnica"];

            DataTable dtPodaci = Upiti.Select("*", "poslovnica", "Naziv='" + odabrano + "'");

            foreach (DataRow red in dtPodaci.Rows)
            {
                sifra.Value = red["ID"].ToString();
                poslovnoIme.Value = red["Naziv"].ToString();
                skrPoslovnoIme.Value = red["SkracenoPoslovnoIme"].ToString();
                mesto.Value = red["Mesto"].ToString();
                adresa.Value = red["Adresa"].ToString();
                drzava.Value = red["Drzava"].ToString();
                email.Value = red["Email"].ToString();
                telefon.Value = red["Telefon"].ToString();
                fax.Value = red["Fax"].ToString();
                datumOsnivanja.Value = red["DatumOsnivanja"].ToString();
                pib.Value = red["PIB"].ToString();
                registarskiBr.Value = red["RegistarskiBroj"].ToString();
                maticniBr.Value = red["MaticniBroj"].ToString();
                sifraDel.Value = red["SifraDelatnosti"].ToString();
                vrstaDel.Value = red["VrstaDelatnosti"].ToString();
                direktor.Value = red["Direktor"].ToString();
                telDirektora.Value = red["TelefonDirektora"].ToString();
                finOsoba.Value = red["FinansijeKontakt"].ToString();
                finTel.Value = red["TelefonFinansije"].ToString();
                komercijalaOsoba.Value = red["KomercijalaKontakt"].ToString();
                komercijalaTel.Value = red["TelefonKomercijala"].ToString();

                
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (poslovnoIme.Value.Trim() == "")
        {
            lblObavestenje.Text = "Niste uneli naziv poslovnice!";
            return;
        }
        //string proSifra = sifra.Value;
        string proPoslovnoIme = poslovnoIme.Value;
        string proSkrPosIme = skrPoslovnoIme.Value;
        string proMesto = mesto.Value;
        string proAdresa = adresa.Value;
        string proDrzava = drzava.Value;
        string proEmail = email.Value;
        string proTelefon = telefon.Value;
        string proFax = fax.Value;

        string proDatumOsnivanja = datumOsnivanja.Value;
        string proPib = pib.Value;
        string proRegBr = registarskiBr.Value;
        string proMatBr = maticniBr.Value;
        string proSifDel = sifraDel.Value;
        string proVrsDel = vrstaDel.Value;

        string proDir = direktor.Value;
        string proTelDir = telDirektora.Value;
        string proFinOsoba = finOsoba.Value;
        string proFinTel = finTel.Value;
        string komercOsob = komercijalaOsoba.Value;
        string komercTel = komercijalaTel.Value;

        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        string odabrano = (String)Session["odabranaPoslovnica"];

        DataTable proveriDaLiPostojiOvoIme = Upiti.Select("Naziv", "poslovnica", "Naziv<>'" + odabrano + "'");
        foreach (DataRow red in proveriDaLiPostojiOvoIme.Rows)
        {
            if (red["Naziv"].ToString().Trim () == proPoslovnoIme.Trim ())
            {
                lblObavestenje.Text = "Uneli ste naziv poslovnice koji već postoji!";
                return;
            }
        }
        
       

        string naredbaUpdate = "Update poslovnica set Naziv=@Naziv,SkracenoPoslovnoIme=@SkracenoPoslovnoIme,Mesto=@Mesto,Adresa=@Adresa,Drzava=@Drzava,Email=@Email,Telefon=@Telefon,Fax=@Fax,DatumOsnivanja=@DatumOsnivanja,PIB=@PIB,RegistarskiBroj=@RegistarskiBroj,MaticniBroj=@MaticniBroj, SifraDelatnosti=@SifraDelatnosti,VrstaDelatnosti=@VrstaDelatnosti,Direktor=@Direktor,TelefonDirektora=@TelefonDirektora,FinansijeKontakt=@FinansijeKontakt,TelefonFinansije=@TelefonFinansije,KomercijalaKontakt=@KomercijalaKontakt,TelefonKomercijala=@TelefonKomercijala where Naziv= '" + odabrano + "'";

        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath());
        MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);

        komandaUpdate.Parameters.AddWithValue("@Naziv", proPoslovnoIme);
        komandaUpdate.Parameters.AddWithValue("@SkracenoPoslovnoIme", proSkrPosIme);
        komandaUpdate.Parameters.AddWithValue("@Mesto", proMesto);
        komandaUpdate.Parameters.AddWithValue("@Adresa", proAdresa);
        komandaUpdate.Parameters.AddWithValue("@Drzava", proDrzava);
        komandaUpdate.Parameters.AddWithValue("@Email", proEmail);
        komandaUpdate.Parameters.AddWithValue("@Telefon", proTelefon);
        komandaUpdate.Parameters.AddWithValue("@Fax", proFax);
        komandaUpdate.Parameters.AddWithValue("@DatumOsnivanja", proDatumOsnivanja);
        komandaUpdate.Parameters.AddWithValue("@PIB", proPib);
        komandaUpdate.Parameters.AddWithValue("@RegistarskiBroj", proRegBr);
        komandaUpdate.Parameters.AddWithValue("@MaticniBroj", proMatBr);
        komandaUpdate.Parameters.AddWithValue("@SifraDelatnosti", proSifDel);
        komandaUpdate.Parameters.AddWithValue("@VrstaDelatnosti", proVrsDel);
        komandaUpdate.Parameters.AddWithValue("@Direktor", proDir);
        komandaUpdate.Parameters.AddWithValue("@TelefonDirektora", proTelDir);
        komandaUpdate.Parameters.AddWithValue("@FinansijeKontakt", proFinOsoba);
        komandaUpdate.Parameters.AddWithValue("@TelefonFinansije", proFinTel);
        komandaUpdate.Parameters.AddWithValue("@KomercijalaKontakt", komercOsob);
        komandaUpdate.Parameters.AddWithValue("@TelefonKomercijala", komercTel);


        konekcija.Open();
        komandaUpdate.ExecuteNonQuery();
        konekcija.Close();

        Session["odabranaPoslovnica"] = proPoslovnoIme;

        Response.Redirect("/pages/navbar.aspx");

    }
}