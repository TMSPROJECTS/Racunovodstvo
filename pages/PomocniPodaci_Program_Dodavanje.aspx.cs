using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_PomocniPodaci_Program_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proSifraPrograma, string proNaziv, string proOdgovornoLice, string proSvrha, string proOpis)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_PomocniPodaci_Program_Dodavanje strana = new pages_PomocniPodaci_Program_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument, proSifraPrograma,proNaziv,proOdgovornoLice,proSvrha ,proOpis);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;



            if (Request.QueryString["SIFRA12"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;
                //sifraPr.Disabled = true;

                DataTable dtIzmena = Upiti.Select2("*", "namena_sredstava_program", "Sifra='" + Request.QueryString["SIFRA12"].Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {
                    dokument.Value = red["SIFRA"].ToString();
                    sifraPr.Value = red["ID"].ToString();
                    naziv.Value = red["Program"].ToString();
                    svrha.Value = red["Svrha"].ToString();
                    opis.Value = red["Opis"].ToString();
                    odgLice.Value = red["OdgovornoLice"].ToString();
                }

            }


        }
    }

    public string[] Sacuvaj(string vrednost, string proSifra, string proNaziv, string proOdgovornoLice, string proSvrha, string proOpis)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string vrednost = Request.QueryString["SIFRA12"];

        System.Data.DataTable dtSveGrupe = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);

      
        //string proSifra = sifraPr.Value;
        //string proNaziv = naziv.Value;
        //string proOdgovornoLice = odgLice.Value;
        //string proSvrha = svrha.Value;
        //string proOpis = odgLice.Value;

        

        if (proSifra.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste uneli šifru programa!";
            return poruka;
            //lblObavestenje.Text = "Niste uneli šifru programa!";
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

        DataTable dtProveriDaLiPostoji = Upiti.Select2("*", "namena_sredstava_program", "ID='" + proSifra.Trim() + "' or Program='" + proNaziv.Trim() + "'", nazivPoslovnice);


        if (vrednost != "")
        {
          

            DataTable dtZaProveru = Upiti.Select2("ID,program", "namena_sredstava_program", "Sifra='" + vrednost + "'", nazivPoslovnice);

            bool daLiJeOstaloIstoID = false;
            bool daLiJeOstaloIstoProgram = false;

            foreach (DataRow red in dtZaProveru.Rows)
            {
                if (red["ID"].ToString().Trim() == proSifra.Trim())
                {
                    daLiJeOstaloIstoID = true;
                }
                if (red["program"].ToString().Trim() == proNaziv.Trim())
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
                            poruka[1] = "Šifra/naziv programa koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/naziv programa koju ste uneli već postoji u bazi!";
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
                            poruka[1] = "Šifra/naziv programa koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/naziv programa koju ste uneli već postoji u bazi!";
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
                        if (red["Program"].ToString() == proNaziv.Trim())
                        {
                            poruka[0] = "N";
                            poruka[1] = "Šifra/naziv programa koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/naziv programa koji ste uneli već postoji u bazi!";
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
                        if (red["Program"].ToString() == proNaziv.Trim())
                        {
                            poruka[0] = "N";
                            poruka[1] = "Šifra/naziv programa koju ste uneli već postoji u bazi!";
                            return poruka;
                            //lblObavestenje.Text = "Šifra/naziv programa koji ste uneli već postoji u bazi!";
                            //return;
                        }
                        
                    }
                }
            }

            try
            {
                string naredbaUpdate = "Update namena_sredstava_program set ID=@ID, Program=@Program, Svrha=@Svrha, Opis=@Opis, OdgovornoLice=@OdgovornoLice, Uneo=@Uneo where Sifra='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                komandaUpdate.Parameters.AddWithValue("@ID", proSifra.Trim());
                komandaUpdate.Parameters.AddWithValue("@Program", proNaziv.Trim());
                komandaUpdate.Parameters.AddWithValue("@Svrha", proSvrha.Trim());
                komandaUpdate.Parameters.AddWithValue("@Opis", proOpis.Trim());
                komandaUpdate.Parameters.AddWithValue("@OdgovornoLice", proOdgovornoLice.Trim());
                komandaUpdate.Parameters.AddWithValue("@Uneo", Korisnik);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili podatke o programu!";

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
                        lblObavestenje.Text = "Šifra programa koju ste uneli već postoji u bazi!";
                    }
                    if (red["Program"].ToString() == proNaziv.Trim())
                    {
                        poruka[0] = "N";
                        poruka[1] = "Naziv programa koju ste uneli već postoji u bazi!";
                        lblObavestenje.Text = "Naziv programa koji ste uneli već postoji u bazi!";
                        
                    }
                    
                    return poruka;
                }
            }

            string novaSifra = NovaSifra.VratiSifru("Sifra", "namena_sredstava_program", nazivPoslovnice, "PR");


            try
            {

                string naredbaInsert = "Insert into namena_sredstava_program (Sifra,ID,Program,Svrha,Opis,OdgovornoLice,Uneo) values (@Sifra,@ID,@Program,@Svrha,@Opis,@OdgovornoLice,@Uneo)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@ID", proSifra.Trim());
                komandaInsert.Parameters.AddWithValue("@Program", proNaziv.Trim());
                komandaInsert.Parameters.AddWithValue("@Svrha", proSvrha.Trim());
                komandaInsert.Parameters.AddWithValue("@Opis", proOpis.Trim());
                komandaInsert.Parameters.AddWithValue("@OdgovornoLice", proOdgovornoLice.Trim());
                komandaInsert.Parameters.AddWithValue("@Uneo", Korisnik);


                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste dodali podatke o programu!";

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