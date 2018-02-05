using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.WebControls;


public partial class pages_racunovodstvo_URStavkeDodavanje : System.Web.UI.Page
{
    [System.Web.Services.WebMethod(true)]
    public static string[] sacuvaj(int proID, string prodokument, string proTrosak, string proIznosBP, string proStopa, string proIznos, string proPlaceno, string proKonto, string proOpis)
    {
        //mora se kreirati objekat klase da bi se pozvala funkcija u STATIC metodi
        pages_racunovodstvo_URStavkeDodavanje strana = new pages_racunovodstvo_URStavkeDodavanje();
        string[] poruka = new string[2];
        poruka = strana.SacuvajUlazni(proID, prodokument, proTrosak, proIznosBP, proStopa, proIznos, proPlaceno, proKonto, proOpis);
        return poruka;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));

        if (Request.QueryString["ID"] == null)
        {
            divID.Visible = false;

            DataTable dtTabela = Upiti.Select2("*", "vrste_troskova", "ne", nazivPoslovnice);
            foreach (DataRow redP in dtTabela.Rows)
            {
                selTrosak.Items.Add(new ListItem(redP["Sifra"].ToString() + " - " + redP["Naziv"].ToString(), redP["Sifra"].ToString()));
            }

            dtTabela = Upiti.Select2("*", "konta", "ne", nazivPoslovnice);
            foreach (DataRow redP in dtTabela.Rows)
            {
                selKonto.Items.Add(new ListItem(redP["Sifra"].ToString() + " - " + redP["Naziv"].ToString(), redP["Sifra"].ToString()));
            }

            dtTabela = Upiti.Select2("*", "stope", "ne", nazivPoslovnice);
            foreach (DataRow redP in dtTabela.Rows)
            {
                selStopa.Items.Add(new ListItem(redP["Naziv"].ToString(), redP["Sifra"].ToString()));
            }
        }
        else
        {
            divID.Visible = true;
            DataTable dtPostojeci = Upiti.Select2("*", "ulazni_racuni_stavke", "ID = '" + Request.QueryString["ID"] + "' and Dokument = '" + Request.QueryString["SIFRA"] + "'", nazivPoslovnice);

            foreach (DataRow red in dtPostojeci.Rows)
            {
                idStavke.Value = red["ID"].ToString();

                DataTable dtTabela = Upiti.Select2("*", "vrste_troskova", "ne", nazivPoslovnice);
                foreach (DataRow redP in dtTabela.Rows)
                {
                    selTrosak.Items.Add(new ListItem(redP["Sifra"].ToString() + " - " + redP["Naziv"].ToString(), redP["Sifra"].ToString()));
                }
                selTrosak.Value = red["ID_vrsta_troska"].ToString();

                dtTabela = Upiti.Select2("*", "konta", "ne", nazivPoslovnice);
                foreach (DataRow redP in dtTabela.Rows)
                {
                    selKonto.Items.Add(new ListItem(redP["Sifra"].ToString() + " - " + redP["Naziv"].ToString(), redP["Sifra"].ToString()));
                }
                selKonto.Value = red["Konto"].ToString();

                dtTabela = Upiti.Select2("*", "stope", "ne", nazivPoslovnice);
                foreach (DataRow redP in dtTabela.Rows)
                {
                    selStopa.Items.Add(new ListItem(redP["Naziv"].ToString(), redP["Sifra"].ToString()));
                }
                selStopa.Value = red["Stopa"].ToString();

                iznosBP.Value = red["IznosBezPDV"].ToString();
                iznos.Value = red["Iznos"].ToString();
                opis.Value = red["Opis"].ToString();
                placeno.Value = red["Placeno"].ToString();
            }
        }
    }

    public string[] SacuvajUlazni(int proID, string proDokument, string proTrosak, string proIznosBP, string proStopa, string proIznos, string proPlaceno, string proKonto, string proOpis)
    {
        string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
        string nazivGodine = (String)Session["odabranaGodina"];
        nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
        //string SifraDok = Request.QueryString["SIFRA"];
        string[] poruka = new string[2];

        if (proID != 0)
        {

            string naredbaUpdate = "Update ulazni_racuni_stavke set ID_vrsta_troska=@Trosak, Stopa=@Stopa, Konto=@Konto, IznosBezPDV=@IznosBP, Iznos=@Iznos, Placeno=@Placeno, Opis=@Opis where Dokument='" + proDokument + "' and ID='" + proID + "'";

            try
            {
                MySqlCommand komandaUpdate = new MySqlCommand(naredbaUpdate, konekcija);

                komandaUpdate.Parameters.AddWithValue("@Trosak", proTrosak);
                komandaUpdate.Parameters.AddWithValue("@Stopa", proStopa);
                komandaUpdate.Parameters.AddWithValue("@Konto", proKonto);
                komandaUpdate.Parameters.AddWithValue("@IznosBP", proIznosBP);
                komandaUpdate.Parameters.AddWithValue("@Iznos", proIznos);
                komandaUpdate.Parameters.AddWithValue("@Placeno", proPlaceno);
                komandaUpdate.Parameters.AddWithValue("@Opis", proOpis);

                konekcija.Open();
                komandaUpdate.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste izmenili ulazni račun!";
            }
            catch (Exception ero)
            {
                konekcija.Close();
                poruka[0] = "N";
                //poruka[1] = ero.ToString();
                poruka[1] = "Greška prilikom izmene ulaznog računa!" + ero;
            }
            return poruka;
        }
        else
        {
            DataTable dtPokupiSifre = Upiti.Select2("max(ID) as ID", "ulazni_racuni_stavke", "ne", nazivPoslovnice);

            int poslednjaSifra = 1;
            int novaSifra = poslednjaSifra;

            if (dtPokupiSifre.Rows.Count == 1)
            {
                if (dtPokupiSifre.Rows[0]["ID"] == null || dtPokupiSifre.Rows[0]["ID"].ToString() == "")
                {
                    novaSifra = 1;
                }
                else
                {
                    poslednjaSifra = int.Parse(dtPokupiSifre.Rows[0]["ID"].ToString());
                    novaSifra = poslednjaSifra + 1;
                }
            }
    
            try
            {
                string naredbaInsert = "Insert into ulazni_racuni_stavke (ID, Dokument, Iznos, Opis, ID_vrsta_troska, Placeno, IznosBezPDV, Stopa, Konto) values (" + novaSifra + ", '" + proDokument + "', @Iznos, @Opis, @Trosak, @Placeno, @IznosBP, @Stopa, @Konto)";

                //MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivPoslovnice));
                MySqlCommand komandaInsert = new MySqlCommand(naredbaInsert, konekcija);

                komandaInsert.Parameters.AddWithValue("@Trosak", proTrosak);
                komandaInsert.Parameters.AddWithValue("@Stopa", proStopa);
                komandaInsert.Parameters.AddWithValue("@Konto", proKonto);
                komandaInsert.Parameters.AddWithValue("@IznosBP", proIznosBP);
                komandaInsert.Parameters.AddWithValue("@Iznos", proIznos);
                komandaInsert.Parameters.AddWithValue("@Placeno", proPlaceno);
                komandaInsert.Parameters.AddWithValue("@Opis", proOpis);

                konekcija.Open();
                komandaInsert.ExecuteNonQuery();
                konekcija.Close();

                poruka[0] = "D";
                poruka[1] = "Uspešno ste uneli stavku ulaznog računa!";
            }
            catch (Exception ero)
            {
                konekcija.Close();

                poruka[0] = "N";
                poruka[1] = "Greška prilikom unosa stavke ulaznog računa!";
            }
            return poruka;
        }
    }
}