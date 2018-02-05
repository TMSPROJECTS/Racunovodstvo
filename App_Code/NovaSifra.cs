using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


/// <summary>
/// Summary description for NovaSifra
/// </summary>
public class NovaSifra
{
    public NovaSifra()
    {
      
    }

    public static string VratiSifru(string kolona, string tabela, string nazivPoslovnice,string simbol)
    {

       
        DataTable dtPokupiSifre = Upiti.Select2("max("+ kolona + ") as maksimalno", tabela, "ne", nazivPoslovnice);

        string poslednjaSifra = "";


        if (dtPokupiSifre.Rows[0]["maksimalno"] != null && dtPokupiSifre.Rows[0]["maksimalno"].ToString() != "")
        {

            poslednjaSifra = dtPokupiSifre.Rows[0]["maksimalno"].ToString();
        }

       

        int razdvojenaSifra = 0;


        if (poslednjaSifra.Trim() == "")
        {
            razdvojenaSifra = 1;
        }
        else
        {
            razdvojenaSifra = int.Parse(poslednjaSifra.Remove(0, 2));
            razdvojenaSifra++;
        }

        int brojKaratreraSifra = razdvojenaSifra.ToString().Length;

        int brojNulaKojeTrebaDodati = 7 - brojKaratreraSifra;

        string novaSifra = simbol;

        for (int i = 0; i < brojNulaKojeTrebaDodati; i++)
        {
            novaSifra += "0";
        }

        novaSifra += razdvojenaSifra.ToString();

        return novaSifra;

    }
}