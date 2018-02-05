<%@ Page Language="C#" AutoEventWireup="true" CodeFile="racunovodstvo_URStavke.aspx.cs" Inherits="pages_racunovodstvo_URStavke" %>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Ulazni računi stavke</title>
  <!-- Bootstrap core CSS-->
  <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <!-- Custom fonts for this template-->
  <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <!-- Page level plugin CSS-->
  <link href="../vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet">
  <!-- Custom styles for this template-->
  <link href="../css/sb-admin.css" rel="stylesheet">
</head>

<body class="fixed-nav sticky-footer bg-dark" id="page-top">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Računovodstvo</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item active"><a class = "pages" href="#" page_name="racunovodstvo_UR.aspx">Ulazni računi</a></li>
        <li class="breadcrumb-item active">Ulazni računi - stavke</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Stavke za ulazni račun <B><% Response.Write(Request.QueryString["SIFRA"]); %></B></div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="racunovodstvo_URStavkeDodavanje.aspx?SIFRA=<% Response.Write(Request.QueryString["SIFRA"]); %>" >Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
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
                  <tr>
                  <th>ID</th>
                  <th>Trošak</th>
                  <th>Iznos bez PDV - a</th>
                  <th>Stopa</th>
                  <th>Iznos</th>
                  <th>Plaćeno</th>
                  <th>Konto</th>
                  <th>Opis</th>                  
                 </tr>              
              </thead>
              <tfoot>
                  <tr>
                  <th>ID</th>
                  <th>Trošak</th>
                  <th>Iznos bez PDV - a</th>
                  <th>Stopa</th>
                  <th>Iznos</th>
                  <th>Plaćeno</th>
                  <th>Konto</th>
                  <th>Opis</th>     
                 </tr>                  
              </tfoot>
              <tbody>
                <%
                    //<a class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a>
                    string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                    string nazivGodine = (String)Session["odabranaGodina"];
                    nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                    System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "ulazni_racuni_stavke", "Dokument = '" + Server.UrlDecode(Request.QueryString["SIFRA"]) + "'", nazivPoslovnice);

                    foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                    {
                        Response.Write("<td> <a class=pages href=# page_name=racunovodstvo_URStavkeDodavanje.aspx?ID=" + red["ID"].ToString() + "&SIFRA=" + red["Dokument"].ToString() + " >"+ red["ID"].ToString() +"</a></td>");

                        System.Data.DataTable dtTrosak = Upiti.Select2("*", "vrste_troskova",  "Sifra = '" + red["ID_vrsta_troska"].ToString() + "'", nazivPoslovnice);
                        if(dtTrosak.Rows.Count > 0)
                        {
                            Response.Write("<td>" + dtTrosak.Rows[0]["Sifra"] + " - " + dtTrosak.Rows[0]["Naziv"] + "</td>");
                        }
                        else
                        {
                            Response.Write("<td>&nbsp;</td>");
                        }

                        Response.Write("<td>" + red["IznosBezPDV"].ToString() + "</td>");

                        System.Data.DataTable dtStopa = Upiti.Select2("*", "stope",  "Sifra = '" + red["Stopa"].ToString() + "'", nazivPoslovnice);
                        if(dtStopa.Rows.Count > 0)
                        {
                            Response.Write("<td>" + dtStopa.Rows[0]["Naziv"] + "</td>");
                        }
                        else
                        {
                            Response.Write("<td>&nbsp;</td>");
                        }
                        Response.Write("<td>" + red["Iznos"].ToString() + "</td>");
                        Response.Write("<td>" + red["Placeno"].ToString() + "</td>");
                        System.Data.DataTable dtKonto = Upiti.Select2("*", "konta",  "Sifra = '" + red["Konto"].ToString() + "'", nazivPoslovnice);
                        if(dtKonto.Rows.Count > 0)
                        {
                            Response.Write("<td>" + dtKonto.Rows[0]["Sifra"] + " - " + dtKonto.Rows[0]["Naziv"] + "</td>");
                        }
                        else
                        {
                            Response.Write("<td>&nbsp;</td>");
                        }
                        Response.Write("<td>" + red["Opis"].ToString() + "</td>");
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

</body>
</html>
