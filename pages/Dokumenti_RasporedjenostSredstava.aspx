<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dokumenti_RasporedjenostSredstava.aspx.cs" Inherits="pages_Dokumenti_RasporedjenostSredstava" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Rasporedjenost sredstava</title>

</head>


  
<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form2" runat="server">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Dokumenti</li>
         <li class="breadcrumb-item"><a class = "pages" href="#" page_name="Dokumenti_FinansijskiPlan.aspx">Finansijski plan</a></li>
        <li class="breadcrumb-item active">Rasporedjenost sredstava</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Raspoređenost planiranih sredstava</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Dokumenti_RasporedjenostSredstava_Dodavanje.aspx">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
              <% 
                    if (Request.QueryString["spObavestenje"] != null)
                    {
                        Response.Write(Server.UrlDecode(Request.QueryString["spObavestenje"]));
                    }
                %>
          </span> </div>
        <div class="card-body">
          <div class="table-responsive">
            <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
              <thead>
                <tr>
                  <th>B</th>
                  <th>Zajednički naziv</th>
                  <th>Konta</th>
                  <th>Konta</th>
                  <th>Program</th>
                  <th>Programska aktivnost</th>
                  <th>Funkcija</th>
                  <th>Korisnik</th>
                  <th>Poslednja izmena</th>
              </thead>
              <tfoot>
                <tr>
               	  <th>B</th>
                  <th>Zajednički naziv</th>
                  <th>Konta</th>
                  <th>Konta</th>
                  <th>Program</th>
                  <th>Programska aktivnost</th>
                  <th>Funkcija</th>
                  <th>Korisnik</th>
                  <th>Poslednja izmena</th>
                </tr>
              </tfoot>
              <tbody>
               <%

                    string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                      string nazivGodine = (String)Session["odabranaGodina"];
                      nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                   string vrednost = Request.QueryString["SIFRA6"];

                   if (vrednost == "" || vrednost == null)
                   {
                       try
                       {
                           vrednost = (String)Session["povratna"];
                           Session["povratna"] = null;
                       }
                       catch
                       {

                       }
                   }

                   Session["FP"] = vrednost;

                   System.Data.DataTable dtFunkcija = Upiti.Select2("*", "namena_sredstava_funkcija", "ne", nazivPoslovnice);
                   System.Data.DataTable dtIzvorFinansiranja = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
                   System.Data.DataTable dtProgram = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);
                   System.Data.DataTable dtProgramskaAktivnost = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "ne", nazivPoslovnice);
                   System.Data.DataTable dtProgramProjekat = Upiti.Select2("*", "namena_sredstava_projekat", "ne", nazivPoslovnice);
                   System.Data.DataTable dtFunkcionalnaKlasifikacija = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
                   System.Data.DataTable dtProgramskaAktivnostFunkcionalnaKlasifikacija = Upiti.Select2("*", "programska_aktivnost_funkcionalna_klasifikacija", "ne", nazivPoslovnice);
                   System.Data.DataTable dtFinPlan = Upiti.Select2("*", "finansijski_plan", "ne", nazivPoslovnice);
                   string zajednickiNaziv = "";

                   foreach (System.Data.DataRow red in dtFinPlan.Rows)
                   {
                       if (red["SifraPlana"].ToString() == vrednost)
                       {
                           zajednickiNaziv = red["Naziv"].ToString();
                           break;
                       }
                   }


                   foreach (System.Data.DataRow red in dtProgramskaAktivnost.Rows)
                   {
                       string sifraPA = red["Sifra"].ToString();
                       string sifraPR = red["IDprograma"].ToString();
                       string nazivProgramskeAktivnosti = red["ProgramskaAktivnost"].ToString();
                       string nazivPrograma = "";
                       string funkcija = "";

                       foreach (System.Data.DataRow redic in dtProgram.Rows)
                       {
                           if (redic["Sifra"].ToString() == sifraPR)
                           {
                               nazivPrograma = redic["Program"].ToString();
                               break;
                           }
                       }

                       foreach (System.Data.DataRow redara in dtProgramskaAktivnostFunkcionalnaKlasifikacija.Rows)
                       {
                           if (redara["IDpa"].ToString() == sifraPA)
                           {

                               foreach (System.Data.DataRow redicic in dtFunkcionalnaKlasifikacija.Rows)
                               {
                                   if (redicic["Sifra"].ToString() == redara["IDfk"].ToString())
                                   {
                                       funkcija = redicic["Naziv"].ToString();


                                       //upis

                                       Response.Write("<tr>");
                                       Response.Write("<td>B</td>");
                                       Response.Write("<td>" + zajednickiNaziv + "</td>");
                                       //Response.Write("<td>izmeni</td>");
                                       Response.Write("<td> <a class =pages href=# page_name=Dokumenti_RasporedjenostSredstava_Dodavanje.aspx?SIFRA7=" + vrednost  + "," + sifraPR + "," + sifraPA + "," + redara["IDfk"].ToString () + "," + System.Web.HttpUtility.UrlEncode(zajednickiNaziv) + "," + System.Web.HttpUtility.UrlEncode(nazivPrograma) + "," + System.Web.HttpUtility.UrlEncode(nazivProgramskeAktivnosti) + "," + System.Web.HttpUtility.UrlEncode(funkcija) + " >izmeni</a></td>");
                                       Response.Write("<td>osveži</td>");
                                       Response.Write("<td>" + nazivPrograma + "</td>");
                                       Response.Write("<td>" + nazivProgramskeAktivnosti  + "</td>");
                                       Response.Write("<td>" + funkcija + "</td>");
                                       Response.Write("<td>" + redara["Korisnik"].ToString () + "</td>");
                                       Response.Write("<td>" + redara["Vreme"].ToString () +"</td>");

                                       break;

                                   }


                               }


                           }
                       }

                   }

                   //<a class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a>
                   //string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                   //string nazivGodine = (String)Session["odabranaGodina"];
                   //nazivPoslovnice = "nazivKlijenta";
                   //string vrednost = Request.QueryString["SIFRA6"];

                   //Session["FP"] = vrednost;

                   //System.Data.DataTable dtFunkcija = Upiti.Select2("*", "namena_sredstava_funkcija", "ne", nazivPoslovnice);
                   //System.Data.DataTable dtIzvorFinansiranja = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
                   //System.Data.DataTable dtProgram = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);
                   //System.Data.DataTable dtProgramskaAktivnost = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "ne", nazivPoslovnice);
                   //System.Data.DataTable dtProgramProjekat = Upiti.Select2("*", "namena_sredstava_projekat", "ne", nazivPoslovnice);

                   //System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "rasporedjenost_sredstava", "SifraPlana='" + vrednost + "'", nazivPoslovnice);

                   //foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                   //{
                   //    Response.Write("<tr>");

                   //    Response.Write("<td>" + red["B"].ToString () + "</td>");
                   //    Response.Write("<td>" + red["ZajednickiNaziv"].ToString() + "</td>");
                   //    Response.Write("<td>" + red["Konta"].ToString () + "</td>"); 
                   //    Response.Write("<td> <a class =pages href=# page_name=Dokumenti_RasporedjenostSredstava_Dodavanje.aspx?SIFRA7=" + vrednost  + "," + red["program"].ToString () + "," + red["ProgramskaAktivnost"].ToString () + "," + red["Funkcija"].ToString () + "," + red["ZajednickiNaziv"].ToString () + " >" + red["Konta2"].ToString() + "</a></td>");
                   //   // Response.Write("<td>" + red["Konta2"].ToString () + "</td>"); 

                   //    if (red["program"].ToString().Trim() == "" || red["program"].ToString().Trim() == "0")
                   //    {
                   //        Response.Write("<td>" + " " + "</td>");
                   //    }
                   //    else
                   //    {
                   //        foreach (System.Data.DataRow redic in dtProgram.Rows)
                   //        {


                   //            if (redic["ID"].ToString() == red["program"].ToString())
                   //            {
                   //                Response.Write("<td>" + redic["Program"].ToString() + "</td>");
                   //                break;
                   //            }
                   //        }
                   //    }

                   //    if (red["programskaAktivnost"].ToString().Trim() == "" || red["programskaAktivnost"].ToString().Trim() == "0")
                   //    {
                   //        Response.Write("<td>" + " " + "</td>");
                   //    }
                   //    else
                   //    {
                   //        foreach (System.Data.DataRow redic in dtProgramskaAktivnost.Rows)
                   //        {
                   //            if (redic["ID"].ToString() == red["programskaAktivnost"].ToString())
                   //            {
                   //                Response.Write("<td>" + redic["ProgramskaAktivnost"].ToString() + "</td>");
                   //                break;
                   //            }
                   //        }
                   //    }

                   //    //if (red["IDprojekat"].ToString().Trim() == "" || red["IDprojekat"].ToString().Trim() == "0")
                   //    //{
                   //    //    Response.Write("<td>" + " " + "</td>");
                   //    //}
                   //    //else
                   //    //{
                   //    //    foreach (System.Data.DataRow redic in dtProgramProjekat.Rows)
                   //    //    {
                   //    //        if (redic["ID"].ToString() == red["IDprojekat"].ToString())
                   //    //        {
                   //    //            Response.Write("<td>" + redic["Projekat"].ToString() + "</td>");
                   //    //            break;
                   //    //        }
                   //    //    }
                   //    //}

                   //    if (red["funkcija"].ToString().Trim() == "" || red["funkcija"].ToString().Trim() == "0")
                   //    {
                   //        Response.Write("<td>" + " " + "</td>");
                   //    }
                   //    else
                   //    {
                   //        foreach (System.Data.DataRow redic in dtFunkcija.Rows)
                   //        {
                   //            if (redic["ID"].ToString() == red["funkcija"].ToString())
                   //            {
                   //                Response.Write("<td>" + redic["Funkcija"].ToString() + "</td>");
                   //                break;
                   //            }
                   //        }
                   //    }

                   //    Response.Write("<td>" + red["Korisnik"].ToString () + "</td>"); 
                   //    Response.Write("<td>" + red["PoslednjaIzmena"].ToString () + "</td>"); 



                   //    Response.Write("</tr>");
                   //}

                       %>
                
              </tbody>
            </table>
          </div>
        </div>
      </div>

     <script src="../js/sb-admin-datatables.min.js"></script>
  <script>
      $(document).ready(function () {//this function load pages in the main div in html 
          var clicked = false;
          $('.pages').click(function () {
              if (!clicked) {
                  clicked = true;
                  var page_name = $(this).attr('page_name');
                  $("#main").empty().off("*");
                  $('#main').load(page_name);
              }
          });
      });

    </script>
            </form> 
</body>

</html>