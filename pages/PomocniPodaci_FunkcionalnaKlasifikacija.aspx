﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PomocniPodaci_FunkcionalnaKlasifikacija.aspx.cs" Inherits="pages_PomocniPodaci_FunkcionalnaKlasifikacija" %>

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
    <form id="form1" runat="server">
      <ol class="breadcrumb">
      	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Pomoćni podaci</li>
        <li class="breadcrumb-item active">Funkcionalna klasifikacija</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Lista funkcionalnih klasifikacija</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="PomocniPodaci_FunkcionalnaKlasifikacija_Dodavanje.aspx">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
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
                    <th>Šifra klasifikacije</th>
                  <th>Šifra</th>
                  <th>Naziv</th>
                  <th>Uneo</th>
                </tr>
              </thead>
              <tfoot>
                <tr>
                    <th>Šifra klasifikacije</th>
                  <th>Šifra</th>
                  <th>Naziv</th>
                  <th>Uneo</th>
                </tr>
              </tfoot>
              <tbody>
                 <%

                      string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                      string nazivGodine = (String)Session["odabranaGodina"];
                      nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                     System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);

                     foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                     {
                         Response.Write("<tr>");
                         

                         Response.Write("<td> <a class =pages href=# page_name=PomocniPodaci_FunkcionalnaKlasifikacija_Dodavanje.aspx?SIFRA14=" + red["Sifra"].ToString() + " >" + red["Sifra"].ToString() + "</a></td>");
                         Response.Write("<td>" + red["ID"].ToString() + "</td>");
                         Response.Write("<td>" + red["Naziv"].ToString() + "</td>");
                         Response.Write("<td>" + red["Uneo"].ToString() + "</td>");
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