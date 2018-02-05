<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Komitenti_ugovoriPartnera.aspx.cs" Inherits="pages_ugovoriPartnera" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
</head>
<body class="fixed-nav sticky-footer bg-dark" id="page-top">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Osnovni podaci</li>
        <li class="breadcrumb-item active">Komitenti</li>
        <li class="breadcrumb-item active">Ugovori</li>
        <li class="breadcrumb-item active">Ugovori partnera</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Lista ugovora partnera</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Komitenti_ugovoriPartnera_Dodavanje.aspx">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
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
              <thead >
                <tr>
                  <th>Sifra ugovora</th>
                  <th>Broj ugovora</th>
                  <th>Datum ugovora</th>
                  <th>Vazi do</th>
                  <th>Iznos ugovora</th>
                  <th>Preostali iznos ugovora</th>
                  <th>Opis</th>
                  <th>Poslovni partner</th>
                  <th>Datum unosa ugovora</th>
                </tr>
              </thead>
              <tfoot>
                <tr>
                  <th>Sifra ugovora</th>
                  <th>Broj ugovora</th>
                  <th>Datum ugovora</th>
                  <th>Vazi do</th>
                  <th>Iznos ugovora</th>
                  <th>Preostali iznos ugovora</th>
                  <th>Opis</th>
                  <th>Poslovni partner</th>
                  <th>Datum unosa ugovora</th>
                </tr>
              </tfoot>
              <tbody>
                
                  <%
                      // string zaIscitavanje = Request.QueryString["SPECIFICNO"];
                       string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                      string nazivGodine = (String)Session["odabranaGodina"];
                      nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                      string spec = "I";

                      spec = Request.QueryString["SPECIFICNO"];

                      System.Data.DataTable dtSviPodaci;

                      try
                      {
                          if (spec.Length == 9)
                          {
                              dtSviPodaci = Upiti.Select2("*", "ugovori_partnera", "Korisnik='" + spec + "'", nazivPoslovnice);
                          }
                          else
                          {
                              dtSviPodaci = Upiti.Select2("*", "ugovori_partnera", "ne", nazivPoslovnice);
                          }


                      }
                      catch
                      {
                          dtSviPodaci = Upiti.Select2("*", "ugovori_partnera", "ne", nazivPoslovnice);
                      }



                      //if (zaIscitavanje == null)
                      //{
                      //     dtSviPodaci = Upiti.Select2("*", "ugovori_partnera", "ne", nazivPoslovnice);
                      //}

                      //if (zaIscitavanje == "ne")
                      //{
                      //    dtSviPodaci = Upiti.Select2("*", "ugovori_partnera", "ne", nazivPoslovnice);
                      //}
                      //else
                      //{
                      //    dtSviPodaci = Upiti.Select2("*", "ugovori_partnera", "Korisnik='" + zaIscitavanje + "'", nazivPoslovnice);
                      //}

                      foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                      {
                          Response.Write("<tr>");
                          //<a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Komitenti_ugovoriPartnera_Dodavanje.aspx">Novi unos</a>
                          Response.Write("<td> <a class =pages href=# page_name=Komitenti_ugovoriPartnera_Dodavanje.aspx?SIFRA2=" + red["SifraUgovora"].ToString() + " >" + red["SifraUgovora"].ToString() + "</a></td>");
                          //Response.Write("<td>" + red["SifraUgovora"].ToString() + "</td>");
                          Response.Write("<td>" + red["BrojUgovora"].ToString() + "</td>");
                          DateTime dtDatumUgovora = DateTime.Parse(red["DatumUgovora"].ToString());
                          string dan2 = dtDatumUgovora.Day.ToString();
                          string mesec2 = dtDatumUgovora.Month.ToString();
                          string godina2 = dtDatumUgovora.Year.ToString();
                           if (dan2.Length == 1)
                          {
                              dan2 = "0" + dan2;
                          }
                            if (mesec2.Length == 1)
                          {
                              mesec2 = "0" + mesec2;
                          }
                          Response.Write("<td>" +dan2 + "." + mesec2 + "." + godina2 + ".</td>");
                          DateTime dtVaziDo = DateTime.Parse(red["VaziDo"].ToString());
                          string dan1 = dtVaziDo.Day.ToString();
                          string mesec1 = dtVaziDo.Month.ToString();
                          string godina1 = dtVaziDo.Year.ToString();
                           if (dan1.Length == 1)
                          {
                              dan1 = "0" + dan1;
                          }
                            if (mesec1.Length == 1)
                          {
                              mesec1 = "0" + mesec1;
                          }
                          Response.Write("<td>" + dan1  + "." + mesec1+ "." + godina1 + ".</td>");
                          Response.Write("<td>" + red["IznosUgovora"].ToString() + "</td>");
                          Response.Write("<td>" + red["PreostaliIznos"].ToString() + "</td>");
                          Response.Write("<td>" + red["Opis"].ToString() + "</td>");

                          string poslovniPartner = red["IDpartnera"].ToString();

                          System.Data.DataTable dtPP = Upiti.Select2("ImePrezime", "Poslovni_partneri", "Sifra='" + poslovniPartner + "'", nazivPoslovnice);

                          string pp = "";
                          foreach (System.Data.DataRow redic in dtPP.Rows)
                          {
                              pp = redic["ImePrezime"].ToString();
                          }

                          Response.Write("<td>" + pp + "</td>");
                          DateTime dtUnos = DateTime.Parse(red["DatumUnosa"].ToString());

                          string dan = dtUnos.Day.ToString();
                          string mesec = dtUnos.Month.ToString();
                          string godina = dtUnos.Year.ToString();
                          string sat = dtUnos.Hour.ToString();
                          string minut = dtUnos.Minute.ToString();
                          string sekunda = dtUnos.Second.ToString();

                          if (mesec.Length == 1)
                          {
                              mesec = "0" + mesec;
                          }
                          if (dan.Length == 1)
                          {
                              dan = "0" + dan;
                          }
                          if (sat.Length == 1)
                          {
                              sat = "0" + sat;
                          }
                          if (minut.Length == 1)
                          {
                              minut  = "0" + minut;
                          }
                          if (sekunda.Length == 1)
                          {
                              sekunda = "0" + sekunda;
                          }

                          Response.Write("<td>" + dan + "." + mesec + "." + godina + ".  " + sat + ":" + minut + ":" + sekunda   + "</td>");

                          Response.Write("</tr>");
                      }

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
