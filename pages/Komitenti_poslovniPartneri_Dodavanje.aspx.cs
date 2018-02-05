using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


public partial class pages_Komitenti_poslovniPartneri_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proImePrezime, string proJMBG, string proMesto, string proTelefon, string proFax)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Komitenti_poslovniPartneri_Dodavanje strana = new pages_Komitenti_poslovniPartneri_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument,proImePrezime ,proJMBG ,proMesto ,proTelefon ,proFax);
        return poruka;
    }

    //public string[] SacuvajPartnera()
    //{
    //    string[] poruka = new string[2];
    //    return poruka;
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;


            if (Request.QueryString["SIFRA"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;
                DataTable dtIzmena = Upiti.Select2("*", "poslovni_partneri", "Sifra='" + Request.QueryString["SIFRA"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["Sifra"].ToString();
                    inputImePrezime.Value = red["ImePrezime"].ToString();
                    inputJMBG.Value = red["JMBG"].ToString();
                    inputMesto.Value = red["Mesto"].ToString();
                    inputTelefon.Value = red["Telefon"].ToString();
                    inputFax.Value = red["Fax"].ToString();
                }

            }

        }       
            
    }

    public string [] Sacuvaj(string vrednost, string proImePrezime,string proJMBG, string proMesto, string proTelefon, string proFax)
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
        //string vrednost = Request.QueryString["SIFRA"];

        //string proImePrezime = inputImePrezime.Value.Trim();
        //string proJMBG = inputJMBG.Value.Trim();
        //string proMesto = inputMesto.Value.Trim();
        //string proTelefon = inputTelefon.Value.Trim();
        //string proFax = inputFax.Value.Trim();

        if (proImePrezime == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli ime i prezime!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli ime i prezime!";
            //return;
        }
        if (proJMBG == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli JMBG!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli JMBG";
            //return;
        }

        try
        {
            
            long jmbgBroj = long.Parse(proJMBG);

        }
        catch
        {

            poruka[0] = "N";
            poruka[1] = "JMBG mora da sadrži samo cifre!";
            return poruka;

            //lblObavestenje.Text = "JMBG mora da sadrži samo cifre!";

            //return;
        }

        if (proJMBG.Length != 13)
        {
            poruka[0] = "N";
            poruka[1] = "JMBG mora imati 13 cifara!";
            return poruka;
            //lblObavestenje.Text = "JMBG mora imati 13 cifara!";
            //return;
        }

        //lblObavestenje.Text = "";

        //poruka[0] = "N";
        //poruka[1] = "HEJ" + vrednost;
        //return poruka;

        if (vrednost != "")
        {
            DataTable dtOstali = Upiti.Select2("Sifra,JMBG", "poslovni_partneri", "Sifra <> '" + vrednost + "'",nazivPoslovnice);

            foreach (DataRow red in dtOstali.Rows)
            {
                if (red["JMBG"].ToString() == proJMBG)
                {
                    poruka[0] = "N";
                    poruka[1] = "JMBG koji ste uneli pripada drugom poslovnom partneru!";
                    return poruka;
                    //lblObavestenje.Text = "JMBG koji ste uneli pripada drugom poslovnom partneru!";
                    //return;
                }
            }

          

            string naredbaUpdate = "Update poslovni_partneri set ImePrezime=@ImePrezime, JMBG=@JMBG, Mesto=@Mesto, Telefon=@Telefon, Fax=@Fax where Sifra='" + vrednost + "'";
            try
            {
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@ImePrezime", proImePrezime);
                komandaUpdate.Parameters.AddWithValue("@JMBG", proJMBG);
                komandaUpdate.Parameters.AddWithValue("@Mesto", proMesto);
                komandaUpdate.Parameters.AddWithValue("@Telefon", proTelefon);
                komandaUpdate.Parameters.AddWithValue("@Fax", proFax);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili podatke o partneru!";
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


            DataTable dtOstali = Upiti.Select2("Sifra,JMBG", "poslovni_partneri", "ne", nazivPoslovnice);

            foreach (DataRow red in dtOstali.Rows)
            {
                if (red["JMBG"].ToString() == proJMBG)
                {
                    poruka[0] = "N";
                    poruka[1] = "JMBG koji ste uneli pripada drugom poslovnom partneru!";
                    return poruka;
                    //lblObavestenje.Text = "JMBG koji ste uneli pripada drugom poslovnom partneru!";
                    //return;
                }
            }


            string novaSifra = NovaSifra.VratiSifru("sifra", "poslovni_partneri", nazivPoslovnice, "PA");


            string naredbaInsert = "Insert into poslovni_Partneri (Sifra,ImePrezime,JMBG,Mesto,Telefon,Fax,Naziv) values (@Sifra,@ImePrezime,@JMBG,@Mesto,@Telefon,@Fax,@Naziv)";
            try
            {


                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@ImePrezime", proImePrezime);
                komandaInsert.Parameters.AddWithValue("@JMBG", proJMBG);
                komandaInsert.Parameters.AddWithValue("@Mesto", proMesto);
                komandaInsert.Parameters.AddWithValue("@Telefon", proTelefon);
                komandaInsert.Parameters.AddWithValue("@Fax", proFax);
                komandaInsert.Parameters.AddWithValue("@Naziv", "");
                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste dodali podatke o partneru!";

            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
            }

            
            return poruka;

        }

        


        //Anchor_Click(sender, e);

        // Response.Redirect("navbar.aspx");
        //Response.Write("<script> HtmlElement Link = document.getElementById('linkPoslovni'); Link.InvokeMember(click)); </script>");

        //System.Web.UI.Control  link = this.FindControl("linkPoslovni");

        //System.Web.UI.HtmlControls.HtmlElement ovoJeLink = 

    }

  


    //private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
    //{
    //    foreach (HtmlElement el in webBrowser1.Document.GetElementsByTagName("input"))
    //    {
    //        if (el.Name == "gatewayIDV")
    //        {
    //            el.InvokeMember("Click");
    //        }
    //    }
    //}






}