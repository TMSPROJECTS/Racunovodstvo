using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

public partial class opstina_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath());

            string Korisnik = (String)Session["korisnickoIme"];
            string aktivnaSesija = (String)Session["aktivnaSesija"];

            bool jeAktivna = ProveriSesiju.ProveriAktivnuSesiju(Korisnik, aktivnaSesija);
            if (jeAktivna == true)
            {
                Session["korisnickoIme"] = null;
                string naredbaUpdate = "Update korisnici set Sesija='' where Sesija='" + aktivnaSesija + "' and Naziv='" + Korisnik + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
            }
        }

        if ((String)Session["odabranaGodina"] == null || (String)Session["odabranaGodina"] == "")
        {
            Response.Redirect("/Index.aspx");
        }

        if ((String)Session["odabranaPoslovnica"] == null || (String)Session["odabranaPoslovnica"] == "")
        {
            Response.Redirect("/izborJedinice.aspx");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string sifra = exampleInputPassword1.Value;

        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath());

        string odabranaPoslovnica =(String)Session["odabranaPoslovnica"];

        DataTable dtUzmiID = Upiti.Select("ID", "poslovnica", "Naziv='" + odabranaPoslovnica + "'");

        int IDposlovnice = 0;

        foreach (DataRow red in dtUzmiID.Rows)
        {
            IDposlovnice = int.Parse(red["ID"].ToString());
        }

        DataTable dtUzmiIDkorisnikaZaWhere = Upiti.Select("idKorisnika", "korisnici_poslovnica", "idPoslovnice='" + IDposlovnice + "'");

        string whereDodatakZaKorisnikeIposlovnice = " AND (";

       

        bool prvi = true;

        foreach (DataRow red in dtUzmiIDkorisnikaZaWhere.Rows)
        {
            if (prvi == true)
            {
                whereDodatakZaKorisnikeIposlovnice += "ID=" + red["idKorisnika"].ToString();
                prvi = false;
            }
            else
            {
                whereDodatakZaKorisnikeIposlovnice += " or ID=" + red["idKorisnika"].ToString();
            }

        }

        if (dtUzmiIDkorisnikaZaWhere.Rows.Count == 0)
        {

            whereDodatakZaKorisnikeIposlovnice = " and ID=0";

        }
        else
        {
            whereDodatakZaKorisnikeIposlovnice += ")";
        }


       

       

        if (exampleInputEmail1.Value.Trim() == "")
        {
            lblObavestenje.Text = "Niste uneli korisničko ime!";
            return;
        }
        if (exampleInputPassword1.Value.Trim() == "")
        {
            lblObavestenje.Text = "Niste uneli šifru!";
            return;
        }

        string pwd = exampleInputPassword1.Value;

        

        DataTable dtUzmiKorisnika2 = Upiti.Select("*", "korisnici", "Naziv='" + exampleInputEmail1.Value + "' and Status=0" + whereDodatakZaKorisnikeIposlovnice );

        if (dtUzmiKorisnika2.Rows.Count > 0)
        {
            lblObavestenje.Text = "Korisnik nije omogućen!";
            return;
        }

        DataTable dtUzmiKorisnika = Upiti.Select("*", "korisnici", "Naziv='" + exampleInputEmail1.Value + "' and Obrisano=0" + whereDodatakZaKorisnikeIposlovnice);

       

        if (dtUzmiKorisnika.Rows.Count == 0)
        {
            lblObavestenje.Text = "Nepostojeći korisnik!";
            return;
        }

        

        foreach (DataRow red in dtUzmiKorisnika.Rows)
        {

            if (red["iniPWD"].ToString().Trim() != "") // onda je ini pwd
            {
                if (red["iniPWD"].ToString() != pwd)
                {
                    lblObavestenje.Text = "Pogrešna šifra!";

                    string naredbaUpdate = "Update korisnici set BrojPogresnihPokusaja=BrojPogresnihPokusaja + 1 where Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString () + "'";
                    MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                    konekcija.Open();
                    komandaUpdate.ExecuteNonQuery();
                    konekcija.Close();

                    DataTable dtUzmiKo = Upiti.Select("*", "korisnici", "Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString() + "'");

                    int brojPogresnih = 0;

                    foreach (DataRow redic in dtUzmiKo.Rows)
                    {
                        brojPogresnih = int.Parse(redic["BrojPogresnihPokusaja"].ToString());
                    }

                    if (brojPogresnih >= 5) //ovde se definise koliko pokusaja pre nego sto postane disabled!!! 
                    {
                        string nunu = "Update korisnici set Status=0 where Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString() + "'";
                        MySqlCommand komnunu = new MySqlCommand(nunu, konekcija);
                        konekcija.Open();
                        komnunu.ExecuteNonQuery();
                        konekcija.Close();
                    }


                    return;
                }
                else
                {

                    if (Panel2.Visible == true)
                    {

                        if (novaSifra.Value.Trim() == "" || novaSifraR.Value.Trim() == "")
                        {
                            Panel2.Visible = false;
                            lblObavestenje.Text = "Nova šifra ne sme biti prazna!";
                            return;
                        }

                        if (novaSifra.Value != novaSifraR.Value)
                        {
                            Panel2.Visible = false;
                            lblObavestenje.Text = "Nova šifra koju ste uneli nije identična!";
                            return;
                        }

                        if (novaSifra.Value.Length < 5)
                        {
                            Panel2.Visible = false;
                            lblObavestenje.Text = "Nova šifra mora imati najmanje 5 karaktera!";
                            return;
                        }

                        ///////////////////////

                        string aktivnaSesija2 = "";
                        DateTime vremeSada2 = DateTime.Now;
                        string godina2 = vremeSada2.Year.ToString();
                        godina2 = godina2.Remove(0, 2);
                        string mesec2 = vremeSada2.Month.ToString();
                        if (mesec2.Length == 1)
                        {
                            mesec2 = "0" + mesec2;
                        }
                        string dan2 = vremeSada2.Day.ToString();
                        if (dan2.Length == 1)
                        {
                            dan2 = "0" + dan2;
                        }
                        string sat2 = vremeSada2.Hour.ToString();
                        if (sat2.Length == 1)
                        {
                            sat2 = "0" + sat2;
                        }
                        string minut2 = vremeSada2.Minute.ToString();
                        if (minut2.Length == 1)
                        {
                            minut2 = "0" + minut2;
                        }
                        string sekunda2 = vremeSada2.Second.ToString();
                        if (sekunda2.Length == 1)
                        {
                            sekunda2 = "0" + sekunda2;
                        }
                        string milisekunda2 = vremeSada2.Millisecond.ToString();
                        if (milisekunda2.Length == 1)
                        {
                            milisekunda2 = "0" + milisekunda2;
                        }

                        aktivnaSesija2 = godina2 + mesec2 + dan2 + sat2 + minut2 + sekunda2 + milisekunda2;

                        //////////////////


                        string numi = "Update korisnici set BrojPogresnihPokusaja=0, iniPWD=@iniPWD, PWD=@PWD, Sesija=@Sesija where Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString() + "'";
                        MySqlCommand komnumi = new MySqlCommand(numi, konekcija);
                        komnumi.Parameters.AddWithValue("@iniPWD", "");
                        komnumi.Parameters.AddWithValue("@PWD", novaSifra.Value);
                        komnumi.Parameters.AddWithValue("@Sesija", aktivnaSesija2);
                        konekcija.Open();
                        komnumi.ExecuteNonQuery();
                        konekcija.Close();

                        Session["aktivnaSesija"] = aktivnaSesija2;
                        Session["korisnickoIme"] = exampleInputEmail1.Value;
                        Response.Redirect("/pages/navbar.aspx");
                        return;
                    }
                    else
                    {
                        string numi = "Update korisnici set BrojPogresnihPokusaja=0 where Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString() + "'";
                        MySqlCommand komnumi = new MySqlCommand(numi, konekcija);
                        konekcija.Open();
                        komnumi.ExecuteNonQuery();
                        konekcija.Close();

                        Panel2.Visible = true;
                        exampleInputPassword1.Value = sifra;

                        return;

                    }
                   
                }
            }
            else
            {
                //lblObavestenje.Text = "dodjes ovde";
                //return;

                if (red["PWD"].ToString() != pwd)
                {
                    lblObavestenje.Text = "Pogrešna šifra!";

                    string naredbaUpdate = "Update korisnici set BrojPogresnihPokusaja=BrojPogresnihPokusaja + 1 where Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString() + "'";
                    MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                    konekcija.Open();
                    komandaUpdate.ExecuteNonQuery();
                    konekcija.Close();

                    DataTable dtUzmiKo = Upiti.Select("*", "korisnici", "Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString() + "'");

                    int brojPogresnih = 0;

                    foreach (DataRow redic in dtUzmiKo.Rows)
                    {
                        brojPogresnih = int.Parse(redic["BrojPogresnihPokusaja"].ToString());
                    }

                    if (brojPogresnih >= 5)
                    {
                        string nurt = "Update Korisnici set Status=0 where Naziv='" + exampleInputEmail1.Value + "' and ID='" + red["ID"].ToString() + "'";
                        MySqlCommand komnurt = new MySqlCommand(nurt, konekcija);
                        konekcija.Open();
                        komnurt.ExecuteNonQuery();
                        konekcija.Close();
                    }
                    return;
                }
            }
        }

       

        string aktivnaSesija = "";
        DateTime vremeSada = DateTime.Now;
        string godina = vremeSada.Year.ToString();
        godina = godina.Remove(0, 2);
        string mesec = vremeSada.Month.ToString();
        if (mesec.Length == 1)
        {
            mesec = "0" + mesec;
        }
        string dan = vremeSada.Day.ToString();
        if (dan.Length == 1)
        {
            dan = "0" + dan;
        }
        string sat = vremeSada.Hour.ToString();
        if (sat.Length == 1)
        {
            sat = "0" + sat;
        }
        string minut = vremeSada.Minute.ToString();
        if (minut.Length == 1)
        {
            minut = "0" + minut;
        }
        string sekunda = vremeSada.Second.ToString();
        if (sekunda.Length == 1)
        {
            sekunda = "0" + sekunda;
        }
        string milisekunda = vremeSada.Millisecond.ToString();
        if (milisekunda.Length == 1)
        {
            milisekunda = "0" + milisekunda;
        }

        aktivnaSesija = godina + mesec + dan + sat + minut + sekunda + milisekunda;

        string nu = "Update Korisnici set BrojPogresnihPokusaja=0,Sesija=@Sesija where Naziv='" + exampleInputEmail1.Value + "'" + whereDodatakZaKorisnikeIposlovnice;
        MySqlCommand komnu = new MySqlCommand(nu, konekcija);
        komnu.Parameters.AddWithValue("@Sesija", aktivnaSesija);
        konekcija.Open();
        komnu.ExecuteNonQuery();
        konekcija.Close();



        Session["korisnickoIme"] = exampleInputEmail1.Value;
        Session["aktivnaSesija"] = aktivnaSesija;

       
        Response.Redirect("/pages/navbar.aspx");


    }
}