using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


public partial class pages_Dokumenti_RasporedjenostSredstava_Dodavanje : System.Web.UI.Page
{


    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(string prodokument,string pro, string proSV, string proFP)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_Dokumenti_RasporedjenostSredstava_Dodavanje strana = new pages_Dokumenti_RasporedjenostSredstava_Dodavanje();
        string[] poruka = new string[2];
        
        poruka = strana.Sacuvaj(prodokument,pro,proSV, proFP);
        return poruka;
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
  
        if (!IsPostBack)
        {
            string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
            string nazivGodine = (String)Session["odabranaGodina"];
            nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
       
            string vrednost = Request.QueryString["SIFRA7"];

            if (vrednost != null)
            {

                string[] rastavi = vrednost.Split(new char[] { ',' });

                string IDzajednickiNaziv = rastavi[0];
                string IDprogram2 = rastavi[1];
                string IDprogramskaAktivnost = rastavi[2];
                string IDfunkcija2 = rastavi[3];

                string zajednickiNaziv = rastavi[4];
                string program2 = rastavi[5];
                string programskaAktivnost = rastavi[6];
                string funkcija2 = rastavi[7];

                punNazivPlana.Value = zajednickiNaziv;
                datumZahteva.Value = zajednickiNaziv;
                program.Value = program2;
                programskaAkt.Value = programskaAktivnost;
                funkcija.Value = funkcija2;

                DataTable dtUzmiIDgrupeTroskova = Upiti.Select2("IDgt", "Programska_aktivnost_grupe_troskova", "IDpa='" + IDprogramskaAktivnost + "'", nazivPoslovnice);

                string idGT = "";

                foreach (DataRow red in dtUzmiIDgrupeTroskova.Rows)
                {
                    idGT = red["IDgt"].ToString();
                }

                DataTable dtUzmiSveVrsteTroskova = Upiti.Select2("*", "vrste_troskova", "IDgrupe='" + idGT + "'", nazivPoslovnice);

                DataTable dtUzmiVrednosti = Upiti.Select2("*", "rasporedjenost_po_vrstama", "ne", nazivPoslovnice);

                int brojac = 0;

                foreach (DataRow red in dtUzmiSveVrsteTroskova.Rows)
                {
                    Panel pnl = new Panel();
                    pnl.ID = "pnlLBL" + brojac;
                    pnl.HorizontalAlign = HorizontalAlign.Right;
                    pnl.Width = new Unit("100%");
                    pnl.Height = new Unit("50px");

                    Label lbl = new Label();
                    lbl.ID = "lblkk" + brojac;
                    lbl.Text = red["Konto"].ToString () + " " + red["Naziv"].ToString();

                    //////////////////////

                    TextBox lbl3 = new TextBox();
                    lbl3.ID = "lblSV" + brojac;
                    lbl3.Text = nazivGodine + "," + IDprogramskaAktivnost + "," + red["Sifra"].ToString() + "," + IDfunkcija2;
                    lbl3.Style.Add("display", "none");
                    //lbl3.Visible = false;

                    //////////////////////


                    pnl.Controls.Add(lbl);
                    pnl.Controls.Add(lbl3);
                    Panel3.Controls.Add(pnl);

                    Panel pnl2 = new Panel();
                    pnl2.ID = "pnTBX" + brojac;
                    pnl2.HorizontalAlign = HorizontalAlign.Left;
                    pnl2.Width = new Unit("100%");
                    pnl2.Height = new Unit("50px");

                    TextBox tb = new TextBox();
                    tb.ID = "tbx" + brojac;
                    tb.CssClass = "form-control";
      

                    foreach (DataRow ccc in dtUzmiVrednosti.Rows)
                    {
                        if (ccc["Godina"].ToString() == nazivGodine && ccc["FP"].ToString () == IDzajednickiNaziv && ccc["PA"].ToString() == IDprogramskaAktivnost && ccc["VT"].ToString() == red["Sifra"].ToString() && ccc["FK"].ToString() == IDfunkcija2)
                        {
                            tb.Text = ccc["Vrednost"].ToString();
                        }
                    }

                    pnl2.Controls.Add(tb);
                    Panel2.Controls.Add(pnl2);

                    brojac++;
                }


                


            }
            else
            {
                string FP = (String)Session["FP"];
                punNazivPlana.Value = FP;
            }

            ////////////////////////////////////////////////////

          
               
            



        }

       
        //////Control myControl1 = FindControl("tbx0");

        //////if (myControl1 != null)
        //////{
        //////    // Get control's parent.
        //////    //Control myControl2 = myControl1.Parent;
        //////    lblObavestenje.Text = "Parent of the text box is : " + myControl1.ID + "";
        //////}
        //////else
        //////{
        //////    lblObavestenje.Text = "NEMA!";// blObavestenje.Text = "Control not found";
        //////}
    }

    public string[] Sacuvaj(string prodokument, string pro, string proSV, string proFP)
    {
        string[] poruka = new string[2];
        if (proSV.Trim() == "")
        {
            poruka[0] = "N";
            poruka[1] = "Nema podataka za čuvanje!";
            return poruka;
        }

        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
       // string vrednost = Request.QueryString["SIFRA7"];
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));


        string[] sveZaUpis = proSV.Split(new char[] { '#' });

        DataTable dtProveriDaLiImaZapis = Upiti.Select2("*", "rasporedjenost_po_vrstama", "ne", nazivPoslovnice);

        string  idFP = "";

        DataTable dtUzmiFP = Upiti.Select2("SifraPlana", "Finansijski_Plan", "Naziv='" + proFP + "'",nazivPoslovnice );

        foreach (DataRow red in dtUzmiFP.Rows)
        {
            idFP = red["SifraPlana"].ToString();
        }

        string paZaUpd = "";
        string fkZaUpd = "";

        for (int i = 1; i < sveZaUpis.Length; i++)
        {
            string[] rastavi = sveZaUpis[i].Split(new char[] { ',' });
            string[] rastaviVrednosti = pro.Split(new char[] { '#' });

            string godina = rastavi[0];
            string PA = rastavi[1];
            string VT = rastavi[2];
            string FK = rastavi[3];

            paZaUpd = PA;
            fkZaUpd = FK;

            bool postoji = false;

            foreach (DataRow red in dtProveriDaLiImaZapis.Rows)
            {
                if (red["Godina"].ToString() == godina && red["FP"].ToString () == idFP.ToString () && red["PA"].ToString() == PA && red["VT"].ToString() == VT && red["FK"].ToString() == FK)
                {
                    postoji = true;
                    break;
                }
            }

            if (postoji == true)
            {
                try
                {
                    string naredbaUpdate = "Update rasporedjenost_po_vrstama set Vrednost=@Vrednost where Godina='" + godina + "' and FP='" + idFP.ToString () + "' and PA='" + PA + "' and VT='" + VT + "' and FK='" + FK + "'";
                    MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);
                    komandaUpdate.Parameters.AddWithValue("@Vrednost", rastaviVrednosti[i]);
                    konekcija.Open();
                    komandaUpdate.ExecuteNonQuery();
                    konekcija.Close();
                }
                catch
                {
                    konekcija.Close();
                    poruka[0] = "N";
                    poruka[1] = "Neuspešno konektovanje na bazu!";
                }
            }
            else
            {
                try
                {
                    string naredbaInsert = "Insert into rasporedjenost_po_vrstama (Godina,FP,PA,VT,FK,Vrednost) values (@Godina,@FP,@PA,@VT,@FK,@Vrednost)";
                    MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);
                    komandaInsert.Parameters.AddWithValue("@Godina", godina);
                    komandaInsert.Parameters.AddWithValue("@FP", idFP.ToString());
                    komandaInsert.Parameters.AddWithValue("@PA", PA);
                    komandaInsert.Parameters.AddWithValue("@VT", VT);
                    komandaInsert.Parameters.AddWithValue("@FK", FK);
                    komandaInsert.Parameters.AddWithValue("@Vrednost", rastaviVrednosti[i]);

                    konekcija.Open();
                    komandaInsert.ExecuteNonQuery();
                    konekcija.Close();
                }
                catch
                {
                    konekcija.Close();
                    poruka[0] = "N";
                    poruka[1] = "Neuspešno konektovanje na bazu!";
                }
            }

           
        }

        string Korisnik = (String)Session["korisnickoIme"];

        string naredbaUPD = "Update programska_aktivnost_funkcionalna_klasifikacija set Korisnik=@Korisnik,Vreme=@Vreme where  IDpa=@IDpa and IDfk=@IDfk";
        MySqlCommand komandaUPD = new MySqlCommand(naredbaUPD, konekcija);
        komandaUPD.Parameters.AddWithValue("@Korisnik", Korisnik);
        komandaUPD.Parameters.AddWithValue("@Vreme", DateTime.Now);
        komandaUPD.Parameters.AddWithValue("@IDpa", paZaUpd);
        komandaUPD.Parameters.AddWithValue("@IDfk", fkZaUpd);

        konekcija.Open();
        komandaUPD.ExecuteNonQuery();
        konekcija.Close ();



        poruka[0] = "D";
        poruka[1] = "Podaci su uspešno ažurirani!";
        return poruka;
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

   

}