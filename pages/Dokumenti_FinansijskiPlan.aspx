<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dokumenti_FinansijskiPlan.aspx.cs" Inherits="pages_Dokumenti_FinansijskiPlan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Finansijski plan</title>
</head>


<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form2" runat="server">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item active">Finansijski plan</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Finansijski plan</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Dokumenti_FinansijskiPlan_Dodavanje.aspx">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
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
                  <th>Dokument</th>
                  <th>Sredstva</th>
                  <th>Plan</th>
                  <th>Naziv</th>
                  <th>Datum donošenja</th>
                  <th>Korisnik</th>
                  <th>Poslednja izmena</th>
                    </tr>
              </thead>
              <tfoot>
                <tr>
               	   <th>Dokument</th>
                  <th>Sredstva</th>
                  <th>Plan</th>
                  <th>Naziv</th>
                  <th>Datum donošenja</th>
                  <th>Korisnik</th>
                  <th>Poslednja izmena</th>
                </tr>
              </tfoot>
              <tbody>
                <%

                     string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                     string nazivGodine = (String)Session["odabranaGodina"];
                     nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                    System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "finansijski_plan", "Godina='" + nazivGodine + "'", nazivPoslovnice);

                    foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                    {
                        Response.Write("<tr>");

                        Response.Write("<td> <a class =pages href=# page_name=Dokumenti_FinansijskiPlan_Dodavanje.aspx?SIFRA5=" + red["SifraPlana"].ToString() + " >" + red["SifraPlana"].ToString() + "</a></td>");
                        //Response.Write("<td>" + red["Sredstva"].ToString() + "</td>");
                        Response.Write("<td> <a class =pages href=# page_name=Dokumenti_RasporedjenostSredstava.aspx?SIFRA6=" + red["SifraPlana"].ToString() + " >" + red["Sredstva"].ToString() + "</a></td>");
                        Response.Write("<td> <a class =pages href=# page_name=Dokumenti_RealizacijaPlana.aspx?SIFRAY=" + red["SifraPlana"].ToString() + " >" + red["Plan"].ToString() + "</a></td>");
                        //Response.Write("<td> <a class =pages href=# page_name=Komitenti_poslovniPartneri_Dodavanje.aspx?SIFRA=" + red["Sifra"].ToString() + " >" + red["Sifra"].ToString() + "</a></td>");
                        Response.Write("<td>" + red["Naziv"].ToString() + "</td>");
                        DateTime dtDatum = DateTime.Parse(red["Datum"].ToString());
                        Response.Write("<td>" + dtDatum.Day + "/" + dtDatum .Month + "/" + dtDatum.Year + "</td>");
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
