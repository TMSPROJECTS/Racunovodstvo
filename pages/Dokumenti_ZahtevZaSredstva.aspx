<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dokumenti_ZahtevZaSredstva.aspx.cs" Inherits="pages_Dokumenti_ZahtevZaSredstva" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Zahtev za sredstva</title>
  
</head>

<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form2" runat="server">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item active">Zahtev za sredstva</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Zahtevi za transfer sredstava</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Dokumenti_zahtevZaSredstva_Dodavanje.aspx">Novi unos</a></div>
        <div class="card-body">
          <div class="table-responsive">
            <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
              <thead>
                <tr>
                  <th title='Storniraj'>S</th>
                  <th>L</th>
                  <th>Virmani</th>
                  <th>Dokument</th>
                  <th>Datum</th>
                  <th>Broj</th>
                  <th>Račun</th>
                  <th>Iznos</th>
                  <th>Plaćeno</th>
                  <th>Korisnik</th>
                  <th>Poslednja izmena</th>
                </tr>
              </thead>
              <tfoot>
                <tr>
               	  <th title='Storniraj'>S</th>
                  <th>L</th>
                  <th>Virmani</th>
                  <th>Dokument</th>
                  <th>Datum</th>
                  <th>Broj</th>
                  <th>Račun</th>
                  <th>Iznos</th>
                  <th>Plaćeno</th>
                  <th>Korisnik</th>
                  <th>Poslednja izmena</th>
                </tr>
              </tfoot>
              <tbody>
                
                   <%
                       //<a class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a>
                       string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                       string nazivGodine = (String)Session["odabranaGodina"];
                       nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                       System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "zahtev_za_sredstva", "ne", nazivPoslovnice);

                       foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                       {
                           Response.Write("<tr>");

                           string storno = "";
                           if(red["Storno"].ToString() == "N")
                           {
                               storno = "<td><a id=" + red["SifraDokumenta"].ToString() + " href='#' title='Storniraj " + red["SifraDokumenta"].ToString() + "' onclick='storno(this.id);return false;'>N</a>";
                           }
                           else
                           {
                               storno = "<td bgcolor='#F13434'>D</td>";
                           }
                           Response.Write("<td>L</td>");
                           Response.Write("<td>V</td>");
                           //Response.Write("<td><a class = &quot;pages&quot; href=Komitenti_poslovniPartneri_Dodavanje.aspx?SIFRA=" + red["Sifra"].ToString() + " page_name=&quot; Komitenti_poslovniPartneri_Dodavanje.aspx &quot;>" + red["Sifra"].ToString() + "</a></td>");
                           Response.Write("<td> <a class =pages href=# page_name=Dokumenti_ZahtevZaSredstva_Dodavanje.aspx?SIFRA3=" + red["SifraDokumenta"].ToString() + " >" + red["SifraDokumenta"].ToString() + "</a></td>");
                           DateTime dtDatum = DateTime .Parse ( red["Datum"].ToString());
                           Response.Write("<td>" + dtDatum.Day + "/" + dtDatum.Month + "/" + dtDatum .Year + "</td>");
                           Response.Write("<td>" + red["Broj"].ToString() + "</td>");
                           Response.Write("<td>" + red["Racun"].ToString() + "</td>");                           
                           System.Data.DataTable dtIznos = Upiti.Select2("sum(Iznos) as Iznos", "namena_sredstava",  "Dokument = '" + red["SifraDokumenta"].ToString() + "'", nazivPoslovnice);
                           if(dtIznos.Rows.Count > 0)
                           {
                               Response.Write("<td> <a class =pages href=# page_name=Dokumenti_NamenaSredstava.aspx?SIFRA8=" + red["SifraDokumenta"].ToString() + " >" + dtIznos.Rows[0]["Iznos"].ToString() + "</a></td>");
                           }
                           else
                           {
                               Response.Write("<td>0.00</td>");
                           }
                           System.Data.DataTable dtPlaceno = Upiti.Select2("sum(Placeno) as Placeno", "namena_sredstava",  "Dokument = '" + red["SifraDokumenta"].ToString() + "'", nazivPoslovnice);
                           if(dtIznos.Rows.Count > 0)
                           {
                               Response.Write("<td> <a class =pages href=# page_name=Dokumenti_NamenaSredstava.aspx?SIFRA8=" + red["SifraDokumenta"].ToString() + " >" + dtPlaceno.Rows[0]["Placeno"].ToString() + "</a></td>");
                           }
                           else
                           {
                               Response.Write("<td>0.00</td>");
                           }
                           Response.Write("<td>" + red["Korisnik"].ToString() + "</td>");
                           Response.Write("<td>" + red["PoslednjaIzmena"].ToString() + "</td>");

                           Response.Write("</tr>");
                       }

                       %>
                
              </tbody>
            </table>
          </div>
        </div>
      </div>

            <script src="../js/sb-admin-datatables.min.js"></script>
            <script src="../js/ZahtevZaSredstva_Storno.js"></script>
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