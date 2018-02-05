using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class pages_PomocniPodaci_ProgramskaAktivnost_Dodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument, string pro1, string pro2, string pro3, string pro4, string pro5, string pro6, string MM, string NN)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_PomocniPodaci_ProgramskaAktivnost_Dodavanje strana = new pages_PomocniPodaci_ProgramskaAktivnost_Dodavanje();
        string[] poruka = new string[2];
        poruka = strana.Sacuvaj(prodokument , pro1,pro2,pro3,pro4,pro5,pro6, MM, NN);
        return poruka;
    }

    //[System.Web.Services.WebMethod(true)]
    //public static string[] ovde(string prodo)
    //{
    //    //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
    //    pages_PomocniPodaci_ProgramskaAktivnost_Dodavanje strana = new pages_PomocniPodaci_ProgramskaAktivnost_Dodavanje();
    //    string[] poruka = new string[2];
    //    poruka = strana.rem(prodo);
    //    return poruka;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;



            if (Request.QueryString["SIFRA15"] == null)
            {
                divDok.Visible = false;

                if ((String)Session["papa"] == null || (String)Session["papa"] == "")
                {
                    Response.Redirect("navbar.aspx");
                    return;
                }

                string programcic = (String)Session["papa"];

                DataTable dtNazivPrograma = Upiti.Select2("Program", "namena_sredstava_program", "Sifra='" + programcic + "'", nazivPoslovnice);
                foreach (DataRow red in dtNazivPrograma.Rows)
                {
                    program.Value = red["Program"].ToString();
                }

            }
            else
            {
                divDok.Visible = true;
                //sifraPr.Disabled = true;

                DataTable dtIzmena = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "Sifra='" + Request.QueryString["SIFRA15"].Trim() + "'", nazivPoslovnice);

                

                foreach (DataRow red in dtIzmena.Rows)
                {

                    DataTable dtProgram = Upiti.Select2("program", "namena_sredstava_program", "Sifra='" + red["IDprograma"].ToString() + "'",nazivPoslovnice);

                    foreach (DataRow redd in dtProgram.Rows)
                    {
                        program.Value = redd["Program"].ToString();
                    }

                    dokument.Value = red["SIFRA"].ToString();
                    
                    naziv.Value = red["ProgramskaAktivnost"].ToString();
                }


               // < td >< input style = "width: 275px"class="form-control" disabled/> </td>
								//<td><input type = "button" class="btn btn-unos obrisi btn-xl js-scroll-trigger" value= "Obriši" onclick="deleteRow('dataTable')"/></td>

                System.Data.DataTable dtIzmena2 = Upiti.Select2("*", "programska_aktivnost_funkcionalna_klasifikacija", "IDpa='" + Request.QueryString["SIFRA15"].Trim() + "'", nazivPoslovnice);
                System.Data.DataTable dtFk = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
                int brojac = 0;
                foreach (System.Data.DataRow red in dtIzmena2.Rows)
                {

                    string IDfk = red["IDfk"].ToString();
                    string nazivFK = "";

                    foreach (System.Data.DataRow redic in dtFk.Rows)
                    {
                        if (IDfk == redic["Sifra"].ToString())
                        {
                            nazivFK = redic["Naziv"].ToString();
                            break;
                        }
                    }

                    Panel pnl2 = new Panel();
                    pnl2.ID = "pnTBX" + brojac;
                    pnl2.HorizontalAlign = HorizontalAlign.Left;
                    pnl2.Width = new Unit("100%");
                    pnl2.Height = new Unit("50px");

                    Panel pnl1 = new Panel();
                    pnl1.ID = "pnBTN" + brojac;
                    pnl1.HorizontalAlign = HorizontalAlign.Left;
                    pnl1.Width = new Unit("100%");
                    pnl1.Height = new Unit("50px");


                    TextBox tb = new TextBox();
                    tb.ID = "Atbx" + brojac;
                    tb.CssClass = "form-control";
                    tb.Text = nazivFK;
                    tb.Style.Add("width", "90%");

                    var btn = new HtmlButton();
                    btn.Attributes["class"] = "btn btn-unos btn-xl js-scroll-trigger pages2";
                    btn.Attributes["type"] = "button";
                    btn.ID = "Abtn" + brojac;
                    btn.InnerText = "Obriši";
                    btn.Attributes.Add("onclick", "remove('Abtn" + brojac + "')");
                    //Button btn = new Button();
                    //btn.ID = "Abtn" + brojac;
                    //btn.CssClass = "btn btn-unos btn-xl js-scroll-trigger pages";
                    //btn.Text = "Obriši";
                    // btn.onclick= "remove()";

                    pnl2.Controls.Add(tb);
                     pnl1.Controls.Add(btn);
                    panelFK1.Controls.Add(pnl2);
                    panelFK2.Controls.Add(pnl1 );

                    brojac++;
                }


                System.Data.DataTable dtIzmena3 = Upiti.Select2("*", "programska_aktivnost_izvor_finansiranja", "IDpa='" + Request.QueryString["SIFRA15"].Trim() + "'", nazivPoslovnice);
                System.Data.DataTable dtFk3 = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
                int brojac2 = 0;
                foreach (System.Data.DataRow red in dtIzmena3.Rows)
                {

                    string IDfk = red["IDif"].ToString();
                    string nazivFK = "";

                    foreach (System.Data.DataRow redic in dtFk3.Rows)
                    {
                        if (IDfk == redic["Sifra"].ToString())
                        {
                            nazivFK = redic["IzvorFinansiranja"].ToString();
                            break;
                        }
                    }

                    Panel pnl2 = new Panel();
                    pnl2.ID = "pnTBXZ" + brojac2;
                    pnl2.HorizontalAlign = HorizontalAlign.Left;
                    pnl2.Width = new Unit("100%");
                    pnl2.Height = new Unit("50px");

                    Panel pnl1 = new Panel();
                    pnl1.ID = "pnBTNZ" + brojac2;
                    pnl1.HorizontalAlign = HorizontalAlign.Left;
                    pnl1.Width = new Unit("100%");
                    pnl1.Height = new Unit("50px");


                    TextBox tb = new TextBox();
                    tb.ID = "BtbxZ" + brojac2;
                    tb.CssClass = "form-control";
                    tb.Text = nazivFK;
                    tb.Style.Add("width", "90%");


                    var btn = new HtmlButton();
                    btn.Attributes["class"] = "btn btn-unos btn-xl js-scroll-trigger pages2";
                    btn.Attributes["type"] = "button";
                    btn.ID = "BbtnZ" + brojac2;
                    btn.InnerText = "Obriši";
                    btn.Attributes.Add("onclick", "remove2('BbtnZ" + brojac2 + "')");

                    //Button btn = new Button();
                    //btn.ID = "BbtnZ" + brojac2;
                    //btn.CssClass = "btn btn-unos obrisi btn-xl js-scroll-trigger";
                    //btn.Text = "Obriši";


                    pnl2.Controls.Add(tb);
                    pnl1.Controls.Add(btn);
                    panelIF1.Controls.Add(pnl2);
                    panelIF2.Controls.Add(pnl1);

                    brojac2++;
                }

                System.Data.DataTable dtIzmena4 = Upiti.Select2("*", "programska_aktivnost_grupe_troskova", "IDpa='" + Request.QueryString["SIFRA15"].Trim() + "'", nazivPoslovnice);
                System.Data.DataTable dtFk4 = Upiti.Select2("*", "grupe_troskova", "ne", nazivPoslovnice);
                int brojac3 = 0;
                foreach (System.Data.DataRow red in dtIzmena4.Rows)
                {

                    string IDfk = red["IDgt"].ToString();
                    string nazivFK = "";

                    foreach (System.Data.DataRow redic in dtFk4.Rows)
                    {
                        if (IDfk == redic["Sifra"].ToString())
                        {
                            nazivFK = redic["Naziv"].ToString();
                            break;
                        }
                    }

                    Panel pnl2 = new Panel();
                    pnl2.ID = "pnTBXZY" + brojac3;
                    pnl2.HorizontalAlign = HorizontalAlign.Left;
                    pnl2.Width = new Unit("100%");
                    pnl2.Height = new Unit("50px");

                    Panel pnl1 = new Panel();
                    pnl1.ID = "pnBTNZY" + brojac3;
                    pnl1.HorizontalAlign = HorizontalAlign.Left;
                    pnl1.Width = new Unit("100%");
                    pnl1.Height = new Unit("50px");


                    TextBox tb = new TextBox();
                    tb.ID = "CtbxZY" + brojac3;
                    tb.CssClass = "form-control";
                    tb.Text = nazivFK;
                    tb.Style.Add("width", "90%");

                    var btn = new HtmlButton();
                    btn.Attributes["class"] = "btn btn-unos btn-xl js-scroll-trigger pages2";
                    btn.Attributes["type"] = "button";
                    btn.ID = "CbtnZY" + brojac3;
                    btn.InnerText = "Obriši";
                    btn.Attributes.Add("onclick", "remove3('CbtnZY" + brojac3 + "')");

                    //Button btn = new Button();
                    //btn.ID = "CbtnZY" + brojac2;
                    //btn.CssClass = "btn btn-unos obrisi btn-xl js-scroll-trigger";
                    //btn.Text = "Obriši";


                    pnl2.Controls.Add(tb);
                    pnl1.Controls.Add(btn);
                    panelGT1.Controls.Add(pnl2);
                    panelGT2.Controls.Add(pnl1);

                    brojac3++;
                }


            }

            System.Data.DataTable dtFunkc = Upiti.Select2("Naziv", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
            foreach (DataRow red in dtFunkc.Rows)
            {
                funkcija.Items.Add(red["Naziv"].ToString());
            }

            System.Data.DataTable dtIzvor = Upiti.Select2("IzvorFinansiranja", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
            foreach (DataRow red in dtIzvor.Rows)
            {
                izvoriFinansiranja.Items.Add(red["IzvorFinansiranja"].ToString());
            }

            System.Data.DataTable dtGrupa = Upiti.Select2("Naziv", "grupe_troskova", "ne", nazivPoslovnice);
            foreach (DataRow red in dtGrupa.Rows)
            {
                grupaTroskova.Items.Add(red["Naziv"].ToString());
            }

        }


    }
    //public string[] rem(string vrednost)
    //{
     




    //    //var brojac = eee.id.substring(8);
    //    //eee.style.display = 'none';
    //    ////eee.value = "obrisano";
    //    //eee.id = "obrisano3" + eee.id;

    //    //var iii = document.getElementById("BTBXXXZ[" + brojac + "]")
    //    //        iii.value = "";
    //    //iii.id = "obrisano4" + brojac;
    //    //iii.style.display = 'none';

    //    string[] poruka = new string[2];
    //    poruka[0] = "N";
    //    poruka[1] = vrednost;
    //    return poruka;
    //}

    public string[] Sacuvaj(string vrednost, string pro1, string pro2, string pro3, string pro4, string pro5, string pro6, string MM, string NN)
    {
        string[] poruka = new string[3];
        //poruka[0] = "N";
        //poruka[1] = "uspeh";
        //Session["sifraZaProgramskuAktivnost"] = Request.QueryString["SIFRA12"];

        //if ((String)Session["sifraZaProgramskuAktivnost"] == "" || (String)Session["sifraZaProgramskuAktivnost"] == null)
        //{
        //    Response.Redirect("/pages/navbar.aspx");
        //}

        //string SPZTR = (String)Session["sifraPartneraZaTekuciRacun"];

        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;

        string Korisnik = (String)Session["korisnickoIme"];
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));

        if ((String)Session["papa"] == null || (String)Session["papa"] == "")
        {
            poruka[0] = "N";
            poruka[1] = "Neuspešno čuvanje!";
            return poruka;
        }

        if (vrednost == "") // onda prvo mora insert  programske aktivnosti u bazu
        {
            string novaSifra = NovaSifra.VratiSifru("sifra", "namena_sredstava_programska_aktivnost", nazivPoslovnice, "PA");


            string naredbaInsert = "Insert into namena_sredstava_programska_aktivnost (Sifra,ProgramskaAktivnost,Uneo,IDprograma) values (@Sifra,@ProgramskaAktivnost,@Uneo,@IDprograma)";
            try
            {
                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@Sifra", novaSifra);
                komandaInsert.Parameters.AddWithValue("@ProgramskaAktivnost", NN);
                komandaInsert.Parameters.AddWithValue("@Uneo", Korisnik);
                komandaInsert.Parameters.AddWithValue("@IDprograma", (String)Session["papa"]);

                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
                return poruka;
            }

            vrednost = novaSifra;

        }
        else //ili update ako se menja ime
        {
            try
            {
                string naredbaUpdate = "Update namena_sredstava_programska_aktivnost set ProgramskaAktivnost='" + NN + "' where Sifra='" + vrednost + "'";
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();
            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
                return poruka;
            }
        }


        string[] rastaviPrvi1 = pro1.Split(new char[] { '#' });
        string[] rastaviPrvi2 = pro2.Split(new char[] { '#' });

        string[] rastaviDrugi1 = pro3.Split(new char[] { '#' });
        string[] rastaviDrugi2 = pro4.Split(new char[] { '#' });

        string[] rastaviTreci1 = pro5.Split(new char[] { '#' });
        string[] rastaviTreci2 = pro6.Split(new char[] { '#' });


      


        DataTable sveFK = Upiti.Select2("Sifra,Naziv", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
        DataTable sveIF = Upiti.Select2("Sifra,IzvorFinansiranja", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
        DataTable sveGT = Upiti.Select2("Sifra,Naziv", "grupe_troskova", "ne", nazivPoslovnice);

        string[] sviFK = new string[(rastaviPrvi1.Length - 1) + (rastaviPrvi2.Length - 1)];
        string[] sviIF = new string[(rastaviDrugi1.Length - 1) + (rastaviDrugi2.Length - 1)];
        string[] sviGT = new string[(rastaviTreci1.Length - 1) + (rastaviTreci2.Length - 1)];

        int brojacZaFK = 0;
        int brojacZaIF = 0;
        int brojacZaGT = 0;

        for (int i = 1; i < rastaviPrvi1.Length; i++)
        {
            foreach (DataRow red in sveFK.Rows)
            {
                if (red["Naziv"].ToString() == rastaviPrvi1[i].ToString())
                {
                    sviFK[brojacZaFK] = red["Sifra"].ToString();
                    brojacZaFK++;
                }
            }
        }
        for (int i = 1; i < rastaviPrvi2.Length; i++)
        {
            foreach (DataRow red in sveFK.Rows)
            {
                if (red["Naziv"].ToString() == rastaviPrvi2[i].ToString())
                {
                    sviFK[brojacZaFK] = red["Sifra"].ToString();
                    brojacZaFK++;
                }
            }
        }

        for (int i = 1; i < rastaviDrugi1.Length; i++)
        {
            foreach (DataRow red in sveIF.Rows)
            {
                if (red["IzvorFinansiranja"].ToString() == rastaviDrugi1[i].ToString())
                {
                    sviIF[brojacZaIF] = red["Sifra"].ToString();
                    brojacZaIF++;
                }
            }
        }

        for (int i = 1; i < rastaviDrugi2.Length; i++)
        {
            foreach (DataRow red in sveIF.Rows)
            {
                if (red["IzvorFinansiranja"].ToString() == rastaviDrugi2[i].ToString())
                {
                    sviIF[brojacZaIF] = red["Sifra"].ToString();
                    brojacZaIF++;
                }
            }
        }


        for (int i = 1; i < rastaviTreci1.Length; i++)
        {
            foreach (DataRow red in sveGT.Rows)
            {
                if (red["Naziv"].ToString() == rastaviTreci1[i].ToString())
                {
                    sviGT[brojacZaGT] = red["Sifra"].ToString();
                    brojacZaGT++;
                }
            }
        }
        for (int i = 1; i < rastaviTreci2.Length; i++)
        {
            foreach (DataRow red in sveGT.Rows)
            {
                if (red["Naziv"].ToString() == rastaviTreci2[i].ToString())
                {
                    sviGT[brojacZaGT] = red["Sifra"].ToString();
                    brojacZaGT++;
                }
            }
        }

        string naredbaDelete = "Delete from programska_aktivnost_funkcionalna_klasifikacija where IDpa='" + vrednost + "'";
        MySqlCommand komandaDelete = new MySqlCommand(naredbaDelete, konekcija);
        konekcija.Open();
        komandaDelete.ExecuteNonQuery();
        konekcija.Close();

        string naredbaDelete2 = "Delete from programska_aktivnost_izvor_finansiranja where IDpa='" + vrednost + "'";
        MySqlCommand komandaDelete2 = new MySqlCommand(naredbaDelete2, konekcija);
        konekcija.Open();
        komandaDelete2.ExecuteNonQuery();
        konekcija.Close();

        string naredbaDelete3 = "Delete from programska_aktivnost_grupe_troskova where IDpa='" + vrednost + "'";
        MySqlCommand komandaDelete3 = new MySqlCommand(naredbaDelete3, konekcija);
        konekcija.Open();
        komandaDelete3.ExecuteNonQuery();
        konekcija.Close();



        for (int i = 0; i < sviFK.Length; i++)
        {
            try
            { 
            DataTable dtKolikoIma = Upiti.Select2("IDpa", "programska_aktivnost_funkcionalna_klasifikacija", "IDpa='" + vrednost + "' and IDfk='" + sviFK[i] + "'",nazivPoslovnice);
            if (dtKolikoIma.Rows.Count == 0)
            {
               // string naredbaInsert = "IF NOT EXISTS (Select * from programska_aktivnost_funkcionalna_klasifikacija where IDpa='" + vrednost + "' and IDfk='" + sviFK[i] + "') BEGIN Insert into programska_aktivnost_funkcionalna_klasifikacija (IDpa,IDfk,Korisnik,Vreme) VALUES (@IDpa,@IDif,@Korisnik,@Vreme) END";
                //string naredbaInsert = "Insert into programska_aktivnost_funkcionalna_klasifikacija (IDpa,IDfk,Korisnik,Vreme) Select * from (Select " + vrednost + "," + sviFK[i] + "," + Korisnik + "," + DateTime.Now + ") as tmp WHERE NOT EXISTS(Select * from programska_aktivnost_funkcionalna_klasifikacija where IDpa='" + vrednost + "' and IDfk='" + sviFK[i] + "')";
                string naredbaInsert = "Insert into programska_aktivnost_funkcionalna_klasifikacija (IDpa,IDfk,Korisnik,Vreme) VALUES (@IDpa,@IDfk,@Korisnik,@Vreme)";
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                komandaInsert.Parameters.AddWithValue("@IDpa", vrednost);
                komandaInsert.Parameters.AddWithValue("@IDfk", sviFK[i]);
                komandaInsert.Parameters.AddWithValue("@Korisnik", Korisnik);
                komandaInsert.Parameters.AddWithValue("@Vreme", DateTime.Now);
                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();
            }

            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
                return poruka;
            }
        }

        for (int i = 0; i < sviIF.Length; i++)
        {
            try
            {
                DataTable dtKolikoIma = Upiti.Select2("IDpa", "programska_aktivnost_izvor_finansiranja", "IDpa='" + vrednost + "' and IDif='" + sviIF[i] + "'", nazivPoslovnice);
                if (dtKolikoIma.Rows.Count == 0)
                {
                    string naredbaInsert = "Insert into programska_aktivnost_izvor_finansiranja (IDpa,IDif) VALUES (@IDpa,@IDif)";
                    MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                    komandaInsert.Parameters.AddWithValue("@IDpa", vrednost);
                    komandaInsert.Parameters.AddWithValue("@IDif", sviIF[i]);
                    konekcija.Open();
                    komandaInsert.ExecuteNonQuery();
                    konekcija.Close();
                }
            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
                return poruka;
            }
        }

        for (int i = 0; i < sviGT.Length; i++)
        {
            try
            {
                DataTable dtKolikoIma = Upiti.Select2("IDpa", "programska_aktivnost_grupe_troskova", "IDpa='" + vrednost + "' and IDgt='" + sviGT[i] + "'", nazivPoslovnice);
                if (dtKolikoIma.Rows.Count == 0)
                {
                    string naredbaInsert = "Insert into programska_aktivnost_grupe_troskova (IDpa,IDgt) VALUES (@IDpa,@IDgt)";
                    MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                    komandaInsert.Parameters.AddWithValue("@IDpa", vrednost);
                    komandaInsert.Parameters.AddWithValue("@IDgt", sviGT[i]);
                    konekcija.Open();
                    komandaInsert.ExecuteNonQuery();
                    konekcija.Close();
                }
            }
            catch
            {
                konekcija.Close();
                poruka[0] = "N";
                poruka[1] = "Neuspešno konektovanje na bazu!";
                return poruka;
            }
        }

        

        poruka[0] = "D";
        poruka[1] = "Programska aktivnost uspešno sačuvana!";
        poruka[2] = (String)Session["papa"];

        return poruka;




    }




    protected void btn_Click(object sender, EventArgs e)
    {
        //TextBox1.Text = "upisao ovde";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Label1.Text = "ASd";
    }
}