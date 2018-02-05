using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class pages_Dokumenti_NamenaSredstava_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string proProgram, string proProgramskaAkt, string proProjekat, string proFunkcija, string proIzvorFin, string proNamena, string proIznos, string proSifDob, string proTR, string proBrFak)
   {
   
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Dokumenti_NamenaSredstava_Dodavanje strana = new pages_Dokumenti_NamenaSredstava_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Cuvaj(prodokument,proProgram,proProgramskaAkt,proProjekat,proFunkcija,proIzvorFin,proNamena,proIznos,proSifDob,proTR,proBrFak);
        return poruka;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

            string vrednost = Request.QueryString["SIFRA4"];

            DataTable dtNamenaSredstvaFuncija = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
            DataTable dtNamenaSredstvaIzvorFinansiranja = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
            DataTable dtNamenaSredstvaProgram = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);
            DataTable dtNamenaSredstvaProgramskaAktivnost = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "ne", nazivPoslovnice);
            DataTable dtNamenaSredstvaProjekat = Upiti.Select2("*", "namena_sredstava_projekat", "ne", nazivPoslovnice);

            //funkcija.Items.Add("-- odaberite funkciju --");
            foreach (DataRow red in dtNamenaSredstvaFuncija.Rows)
            {
                funkcija.Items.Add(red["Naziv"].ToString());
            }
            if (funkcija.Items.Count == 2)
            {
                funkcija.SelectedIndex = 1;
            }

            //izvorFin.Items.Add("-- odaberite izvor finansiranja --");
            foreach (DataRow red in dtNamenaSredstvaIzvorFinansiranja.Rows)
            {
                izvorFin.Items.Add(red["IzvorFinansiranja"].ToString());
            }
            if (izvorFin.Items.Count == 2)
            {
                izvorFin.SelectedIndex = 1;
            }

            // program.Items.Add("-- odaberite program --");
            foreach (DataRow red in dtNamenaSredstvaProgram.Rows)
            {
                program.Items.Add(red["Program"].ToString());
            }
            if (program.Items.Count == 2)
            {
                program.SelectedIndex = 1;
            }

            //programskaAkt.Items.Add("-- odaberite programsku aktivnost --");
            foreach (DataRow red in dtNamenaSredstvaProgramskaAktivnost.Rows)
            {
                programskaAkt.Items.Add(red["ProgramskaAktivnost"].ToString());
            }
            if (programskaAkt.Items.Count == 2)
            {
                programskaAkt.SelectedIndex = 1;
            }

            //projekat.Items.Add("-- odaberite projekat --");
            foreach (DataRow red in dtNamenaSredstvaProjekat.Rows)
            {
                projekat.Items.Add(red["Projekat"].ToString());
            }
            if (projekat.Items.Count == 2)
            {
                projekat.SelectedIndex = 1;
            }

            ////////////////////////////////////////////

            if (Request.QueryString["SIFRA4"] == null)
            {
                divDok.Visible = false;
            }
            else
            {
                divDok.Visible = true;

                DataTable dtIzmena = Upiti.Select2("*", "namena_sredstava", "Dokument1='" + vrednost.Trim() + "'", nazivPoslovnice);

                foreach (DataRow red in dtIzmena.Rows)
                {

                    dokument.Value = vrednost.Trim();

                    if (red["IDprogram"].ToString().Trim() != "")
                    {
                        foreach (DataRow red2 in dtNamenaSredstvaProgram.Rows)
                        {
                            if (red2["Sifra"].ToString() == red["IDprogram"].ToString())
                            {
                                program.Value = red2["Program"].ToString();
                            } 
                        }
                    }

                    if (red["IDprogramskaAktivnost"].ToString().Trim() != "")
                    {
                        foreach (DataRow red2 in dtNamenaSredstvaProgramskaAktivnost.Rows)
                        {
                            if (red2["Sifra"].ToString() == red["IDprogramskaAktivnost"].ToString())
                            {
                                programskaAkt.Value = red2["ProgramskaAktivnost"].ToString();
                            }
                        }
                    }

                    //PROJEKAT

                    if (red["IDfunkcija"].ToString().Trim() != "")
                    {
                        foreach (DataRow red2 in dtNamenaSredstvaFuncija.Rows)
                        {
                            if (red2["Sifra"].ToString() == red["IDfunkcija"].ToString())
                            {
                                funkcija.Value = red2["Naziv"].ToString();
                            }
                        }
                    }

                    if (red["IDizvorFinansiranja"].ToString().Trim() != "")
                    {
                        foreach (DataRow red2 in dtNamenaSredstvaIzvorFinansiranja.Rows)
                        {
                            if (red2["Sifra"].ToString() == red["IDizvorFinansiranja"].ToString())
                            {
                                izvorFin.Value = red2["IzvorFinansiranja"].ToString();
                            }
                        }
                    }

                    namena.Value = red["Namena"].ToString();
                    iznos.Value = red["Iznos"].ToString();

                }

            }


            /////////////////////
        }
    }

    public string[] Cuvaj(string prodokument, string proProgram, string proProgramskaAkt, string proProjekat, string proFunkcija, string proIzvorFin, string proNamena, string proIznos, string proSifDob, string proTR, string proBrFak)
    {
        string[] poruka = new string[2];

        //poruka[0] = "N";
        //poruka[1] = "Greška prilikom izmene ulaznog računa!";
        // return poruka;

        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        string vrednost = prodokument; //(String)Session["sifraZS"];

        DataTable dtNamenaSredstvaFuncija = Upiti.Select2("*", "funkcionalna_klasifikacija", "Naziv='" + proFunkcija + "'", nazivPoslovnice);
        DataTable dtNamenaSredstvaIzvorFinansiranja = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "IzvorFinansiranja='" + proIzvorFin + "'", nazivPoslovnice);
        DataTable dtNamenaSredstvaProgram = Upiti.Select2("*", "namena_sredstava_program", "Program='" + proProgram + "'", nazivPoslovnice);
        DataTable dtNamenaSredstvaProgramskaAktivnost = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "ProgramskaAktivnost='" + proProgramskaAkt + "'", nazivPoslovnice);
        DataTable dtNamenaSredstvaProjekat = Upiti.Select2("*", "namena_sredstava_projekat", "Projekat='" + proProjekat+ "'", nazivPoslovnice);

        string idFuncija = "";
        string  idIzvorFinansiranja = "";
        string idProgram = "";
        string  idProgramskaAktivnost = "";
        string  idProjekat = "";

        if (proFunkcija == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali funkciju!";
            return poruka;
        }
        else
        {
            foreach (DataRow red in dtNamenaSredstvaFuncija.Rows)
            {
                idFuncija = red["Sifra"].ToString();
            }

        }

        if (proIzvorFin == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali izvor finansiranja!";
            return poruka;
        }
        else
        {
            foreach (DataRow red in dtNamenaSredstvaIzvorFinansiranja.Rows)
            {
                idIzvorFinansiranja = red["SIFRA"].ToString();
            }

        }

        if (proNamena == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali program!";
            return poruka;
        }
        else
        {
            foreach (DataRow red in dtNamenaSredstvaProgram.Rows)
            {
                idProgram = red["SIFRA"].ToString();
            }

        }

        if (proProgramskaAkt == "")
        {
            poruka[0] = "N";
            poruka[1] = "Niste odabrali programsku aktivnost!";
            return poruka;
        }
        else
        {
            foreach (DataRow red in dtNamenaSredstvaProgramskaAktivnost.Rows)
            {
                idProgramskaAktivnost = red["SIFRA"].ToString();
            }

        }

        if (proProjekat == "")
        {
            //poruka[0] = "N";
            //poruka[1] = "Niste odabrali projekat!";
            //return poruka;
        }
        else
        {
            foreach (DataRow red in dtNamenaSredstvaProjekat.Rows)
            {
                idProjekat = red["ID"].ToString();
            }

        }

        //poruka[0] = "N";
        //poruka[1] = vrednost;
        //return poruka;

        if (vrednost != "")
        {
            Session["sifraZS"] = "";
            //string naredbaUpdate = "Update zahtev_za_sredstva set Datum=@Datum, Broj=@Broj, Racun=@Racun, Napomena=@Napomena where SifraDokumenta='" + vrednost + "'";
            //MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
            //komandaUpdate.Parameters.AddWithValue("@Datum", proDatumZahteva);
            //komandaUpdate.Parameters.AddWithValue("@Broj", proBrojZahteva);
            //komandaUpdate.Parameters.AddWithValue("@Racun", proRacun);
            //komandaUpdate.Parameters.AddWithValue("@Napomena", proNapomena);

            //konekcija.Open();
            //komandaUpdate.ExecuteNonQuery();
            //konekcija.Close();

            string naredbaUpdate = "Update namena_sredstava set IDprogramskaAktivnost=@IDprogramskaAktivnost,IDprojekat=@IDprojekat,IDfunkcija=@IDfunkcija,IDizvorFinansiranja=@IDizvorFinansiranja,Namena=@Namena,Iznos=@Iznos,IDProgram=@IDProgram where Dokument1=@Dokument1";
            MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
            komandaUpdate.Parameters.AddWithValue("@IDprogramskaAktivnost", idProgramskaAktivnost);
            komandaUpdate.Parameters.AddWithValue("@IDprojekat", idProjekat);
            komandaUpdate.Parameters.AddWithValue("@IDfunkcija", idFuncija);
            komandaUpdate.Parameters.AddWithValue("@IDizvorFinansiranja", idIzvorFinansiranja);
            komandaUpdate.Parameters.AddWithValue("@Namena", proNamena);
            komandaUpdate.Parameters.AddWithValue("@Iznos", proIznos);
            komandaUpdate.Parameters.AddWithValue("@IDProgram", idProgram);
            komandaUpdate.Parameters.AddWithValue("@Dokument1", prodokument);
            konekcija.Open();
            komandaUpdate.ExecuteNonQuery();
            konekcija.Close();


        }
        else
        {

            string novaSifra = NovaSifra.VratiSifru("Dokument1", "namena_sredstava", nazivPoslovnice, "NS");

            string naredbaInsert = "Insert into namena_sredstava (Dokument1,Dokument,IDprogramskaAktivnost,IDprojekat,IDfunkcija,IDizvorFinansiranja,Namena,Iznos,IDProgram) values (@Dokument1,@Dokument,@IDprogramskaAktivnost,@IDprojekat,@IDfunkcija,@IDizvorFinansiranja,@Namena,@Iznos,@IDProgram)";

            //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
            MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
            komandaInsert.Parameters.AddWithValue("@Dokument1", novaSifra);
            komandaInsert.Parameters.AddWithValue("@Dokument", (String)Session["sifraZS"]);
            komandaInsert.Parameters.AddWithValue("@IDprogramskaAktivnost", idProgramskaAktivnost);
            komandaInsert.Parameters.AddWithValue("@IDprojekat", idProjekat);
            komandaInsert.Parameters.AddWithValue("@IDfunkcija", idFuncija);
            komandaInsert.Parameters.AddWithValue("@IDizvorFinansiranja", idIzvorFinansiranja);
            komandaInsert.Parameters.AddWithValue("@Namena", proNamena);
            komandaInsert.Parameters.AddWithValue("@Iznos", proIznos);
            komandaInsert.Parameters.AddWithValue("@IDProgram", idProgram);
            konekcija.Open();
            komandaInsert.ExecuteNonQuery();
            konekcija.Close();

            

        }



        poruka[0] = "D";
        poruka[1] = "Uspeh!";
        return poruka;




    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
    //    string nazivGodine = (String)Session["odabranaGodina"];
    //    nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
    //    MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
    //    string vrednost = Request.QueryString["SIFRA4"];

    //    DataTable dtNamenaSredstvaFuncija = Upiti.Select2("*", "namena_sredstava_funkcija", "Funkcija='" + funkcija.Value + "'", nazivPoslovnice);
    //    DataTable dtNamenaSredstvaIzvorFinansiranja = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "IzvorFinansiranja='" + izvorFin .Value + "'", nazivPoslovnice);
    //    DataTable dtNamenaSredstvaProgram = Upiti.Select2("*", "namena_sredstava_program", "Program='" + program.Value + "'", nazivPoslovnice);
    //    DataTable dtNamenaSredstvaProgramskaAktivnost = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "ProgramskaAktivnost='" + programskaAkt.Value + "'", nazivPoslovnice);
    //    DataTable dtNamenaSredstvaProjekat = Upiti.Select2("*", "namena_sredstava_projekat", "Projekat='" + projekat.Value + "'", nazivPoslovnice);

    //    int idFuncija = 0;
    //    int idIzvorFinansiranja = 0;
    //    int idProgram = 0;
    //    int idProgramskaAktivnost = 0;
    //    int idProjekat = 0;

    //    if (dtNamenaSredstvaFuncija .Rows.Count == 0)
    //    {
    //        lblObavestenje.Text = "Niste odabrali funkciju!";
    //        return;
    //    }
    //    else
    //    {
    //        foreach (DataRow red in dtNamenaSredstvaFuncija.Rows)
    //        {
    //            idFuncija = int.Parse(red["ID"].ToString());
    //        }
            
    //    }

    //    if (dtNamenaSredstvaIzvorFinansiranja.Rows.Count == 0)
    //    {
    //        lblObavestenje.Text = "Niste odabrali izvor finansiranja!";
    //        return;
    //    }
    //    else
    //    {
    //        foreach (DataRow red in dtNamenaSredstvaIzvorFinansiranja.Rows)
    //        {
    //            idIzvorFinansiranja = int.Parse(red["ID"].ToString());
    //        }

    //    }

    //    if (dtNamenaSredstvaProgram.Rows.Count == 0)
    //    {
    //        lblObavestenje.Text = "Niste odabrali program!";
    //        return;
    //    }
    //    else
    //    {
    //        foreach (DataRow red in dtNamenaSredstvaProgram.Rows)
    //        {
    //            idProgram  = int.Parse(red["ID"].ToString());
    //        }

    //    }

    //    if (dtNamenaSredstvaProgramskaAktivnost.Rows.Count == 0)
    //    {
    //        lblObavestenje.Text = "Niste odabrali programsku aktivnost!";
    //        return;
    //    }
    //    else
    //    {
    //        foreach (DataRow red in dtNamenaSredstvaProgramskaAktivnost.Rows)
    //        {
    //            idProgramskaAktivnost = int.Parse(red["ID"].ToString());
    //        }

    //    }

    //    if (dtNamenaSredstvaProjekat.Rows.Count == 0)
    //    {
    //        lblObavestenje.Text = "Niste odabrali projekat!";
    //        //return;  //!!!!
    //    }
    //    else
    //    {
    //        foreach (DataRow red in dtNamenaSredstvaProjekat.Rows)
    //        {
    //            idProjekat = int.Parse(red["ID"].ToString());
    //        }

    //    }


    //    string proDokument = dokument.Value;
    //    string proNamena = namena.Value;
    //    string proIznos = iznos.Value;




    //    lblObavestenje.Text = "";



    //    if (vrednost != null)
    //    {

    //        //string naredbaUpdate = "Update zahtev_za_sredstva set Datum=@Datum, Broj=@Broj, Racun=@Racun, Napomena=@Napomena where SifraDokumenta='" + vrednost + "'";
    //        //MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
    //        //komandaUpdate.Parameters.AddWithValue("@Datum", proDatumZahteva);
    //        //komandaUpdate.Parameters.AddWithValue("@Broj", proBrojZahteva);
    //        //komandaUpdate.Parameters.AddWithValue("@Racun", proRacun);
    //        //komandaUpdate.Parameters.AddWithValue("@Napomena", proNapomena);

    //        //konekcija.Open();
    //        //komandaUpdate.ExecuteNonQuery();
    //        //konekcija.Close();



    //    }
    //    else
    //    {

    //        //DataTable dtPokupiSifre = Upiti.Select2("SifraDokumenta", "zahtev_za_sredstva", "S='S' order by SifraDokumenta asc", nazivPoslovnice);

    //        //string poslednjaSifra = "";

    //        //foreach (DataRow red in dtPokupiSifre.Rows)
    //        //{

    //        //    poslednjaSifra = red["SifraDokumenta"].ToString();
    //        //}

    //        //int razdvojenaSifra = 0;


    //        //if (poslednjaSifra.Trim() == "")
    //        //{
    //        //    razdvojenaSifra = 1;
    //        //}
    //        //else
    //        //{
    //        //    razdvojenaSifra = int.Parse(poslednjaSifra.Remove(0, 2));
    //        //    razdvojenaSifra++;
    //        //}

    //        //int brojKaratreraSifra = razdvojenaSifra.ToString().Length;

    //        //int brojNulaKojeTrebaDodati = 7 - brojKaratreraSifra;

    //        //string novaSifra = "ZS";

    //        //for (int i = 0; i < brojNulaKojeTrebaDodati; i++)
    //        //{
    //        //    novaSifra += "0";
    //        //}

    //        //novaSifra += razdvojenaSifra.ToString();

    //        DataTable dtBroj = Upiti.Select2("ID", "namena_sredstava", "ne", nazivPoslovnice);

    //        int broj = dtBroj.Rows.Count + 1;

    //        string naredbaInsert = "Insert into namena_sredstava (ID,Dokument,IDprogramskaAktivnost,IDprojekat,IDfunkcija,IDizvorFinansiranja,Namena,Iznos) values (@ID,@Dokument,@IDprogramskaAktivnost,@IDprojekat,@IDfunkcija,@IDizvorFinansiranja,@Namena,@Iznos)";

    //        //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
    //        MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
    //        komandaInsert.Parameters.AddWithValue("@ID", broj);
    //        komandaInsert.Parameters.AddWithValue("@Dokument", proDokument);
    //        komandaInsert.Parameters.AddWithValue("@IDprogramskaAktivnost", idProgramskaAktivnost );
    //        komandaInsert.Parameters.AddWithValue("@IDprojekat", idProjekat);
    //        komandaInsert.Parameters.AddWithValue("@IDfunkcija", idFuncija);
    //        komandaInsert.Parameters.AddWithValue("@IDizvorFinansiranja", idIzvorFinansiranja);
    //        komandaInsert.Parameters.AddWithValue("@Namena", proNamena);
    //        komandaInsert.Parameters.AddWithValue("@Iznos", proIznos);
    //        komandaInsert.Parameters.AddWithValue("@Program", idProgram);
    //        konekcija.Open();
    //        komandaInsert.ExecuteNonQuery();
    //        konekcija.Close();

    //    }
    //}
}