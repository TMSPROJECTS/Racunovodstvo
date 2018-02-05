using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_PomocniPodaci_IzvorFinansiranja_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proSifra, string proNaziv)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_PomocniPodaci_IzvorFinansiranja_Dodavanje strana = new pages_PomocniPodaci_IzvorFinansiranja_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proSifra,proNaziv );
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;


            if (Request.QueryString["SIFRA13"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;
                DataTable dtIzmena = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "Sifra='" + Request.QueryString["SIFRA13"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["Sifra"].ToString();
                    sifra.Value = red["ID"].ToString();
                    naziv.Value = red["IzvorFinansiranja"].ToString();

                }


            }
        }
    }

    public string[] Sacuvaj(string vrednost, string proSifra, string proNaziv)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string vrednost = Request.QueryString["SIFRA13"];

        System.Data.DataTable dtSveGrupe = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);


        //string proSifra = sifra.Value;
        //string proNaziv = naziv.Value;

        if (proSifra.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli šifru!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli šifru!";
            //return;
        }
        if (proNaziv.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli naziv!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli naziv!";
            //return;
        }


        //lblObavestenje.Text = "";
        string Korisnik = (String)Session["korisnickoIme"];

        DataTable dtProveriDaLiPostoji = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ID='" + proSifra.Trim() + "' or IzvorFinansiranja='" + proNaziv.Trim() + "'", nazivPoslovnice);


        if (vrednost != "")
        {


            DataTable dtZaProveru = Upiti.Select2("ID,IzvorFinansiranja", "namena_sredstava_izvor_finansiranja", "Sifra='" + vrednost + "'", nazivPoslovnice);

            bool daLiJeOstaloIstoID = false;
            bool daLiJeOstaloIstoProgram = false;

            foreach (DataRow red in dtZaProveru.Rows)
            {
                if (red["ID"].ToString().Trim() == proSifra.Trim())
                {
                    daLiJeOstaloIstoID = true;
                }
                if (red["IzvorFinansiranja"].ToString().Trim() == proNaziv.Trim())
                {
                    daLiJeOstaloIstoProgram = true;
                }
            }




            if (daLiJeOstaloIstoID == true)
            {
                if (dtProveriDaLiPostoji.Rows.Count > 1)
                {
                    foreach (DataRow red in dtProveriDaLiPostoji.Rows)
                    {
                        if (red["ID"].ToString() == proSifra.Trim())
                        {
                            poruka[0] = "N";
                            poruka[1] = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            //return;
                        }

                    }
                }
            }
            else
            {
                if (dtProveriDaLiPostoji.Rows.Count > 0)
                {
                    foreach (DataRow red in dtProveriDaLiPostoji.Rows)
                    {
                        if (red["ID"].ToString() == proSifra.Trim())
                        {
                            poruka[0] = "N";
                            poruka[1] = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            //return;
                        }

                    }
                }
            }


            if (daLiJeOstaloIstoProgram == true)
            {
                if (dtProveriDaLiPostoji.Rows.Count > 1)
                {
                    foreach (DataRow red in dtProveriDaLiPostoji.Rows)
                    {
                        if (red["IzvorFinansiranja"].ToString() == proNaziv.Trim())
                        {
                            poruka[0] = "N";
                            poruka[1] = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            //return;
                        }

                    }
                }
            }
            else
            {
                if (dtProveriDaLiPostoji.Rows.Count > 0)
                {
                    foreach (DataRow red in dtProveriDaLiPostoji.Rows)
                    {
                        if (red["IzvorFinansiranja"].ToString() == proNaziv.Trim())
                        {
                            poruka[0] = "N";
                            poruka[1] = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/izvor finansiranja koju ste uneli već postoji u bazi!";
                            //return;
                        }

                    }
                }
            }

            try
            {

                string naredbaUpdate = "Update namena_sredstava_izvor_finansiranja set ID=@ID, IzvorFinansiranja=@IzvorFinansiranja, Uneo=@Uneo where Sifra='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@ID", proSifra.Trim());
                komandaUpdate.Parameters.AddWithValue("@IzvorFinansiranja", proNaziv.Trim());
                komandaUpdate.Parameters.AddWithValue("@Uneo", Korisnik);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili izvor finansiranja!";
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

            if (dtProveriDaLiPostoji.Rows.Count > 0)
            {
                foreach (DataRow red in dtProveriDaLiPostoji.Rows)
                {
                    if (red["ID"].ToString() == proSifra.Trim())
                    {
                        poruka[0] = "N";
                        poruka[1] = "Šifra programa koju ste uneli već postoji u bazi!";
                 
                        //lblObavestenje.Text = "Šifra programa koju ste uneli već postoji u bazi!";
                    }
                    if (red["IzvorFinansiranja"].ToString() == proNaziv.Trim())
                    {
                        poruka[0] = "N";
                        poruka[1] = "Izvor finanisranja koji ste uneli već postoji u baz!";

                        //lblObavestenje.Text = "Izvor finanisranja koji ste uneli već postoji u bazi!";

                    }
                    return poruka;
                }
            }

            string novaSifra = NovaSifra.VratiSifru("Sifra", "namena_sredstava_izvor_finansiranja", nazivPoslovnice, "IF");

            try
            {

                string naredbaInsert = "Insert into namena_sredstava_izvor_finansiranja (Sifra,ID,IzvorFinansiranja,Uneo) values (@Sifra,@ID,@IzvorFinansiranja,@Uneo)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@ID", proSifra.Trim());
                komandaInsert.Parameters.AddWithValue("@IzvorFinansiranja", proNaziv.Trim());
                komandaInsert.Parameters.AddWithValue("@Uneo", Korisnik);


                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();
                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli izvor finansiranja!";
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