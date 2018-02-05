using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using MySql.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for ProveriSesiju
/// </summary>
public class ProveriSesiju
{
    public ProveriSesiju()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool ProveriAktivnuSesiju(string NazKor, string aktivnaSes)
    {
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath());
     

     
            string brojKorisnika = "Select * from Korisnici where Naziv='" + NazKor + "' and Sesija='" + aktivnaSes + "'";
            MySqlCommand komandaBrojKorisnika = new MySqlCommand(brojKorisnika, konekcija);
            MySqlDataAdapter adapter = new MySqlDataAdapter(komandaBrojKorisnika);
            DataTable dtRezultat = new DataTable();
            adapter.Fill(dtRezultat);

            if (dtRezultat.Rows.Count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        



    }
}