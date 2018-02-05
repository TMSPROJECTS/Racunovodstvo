<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Komitenti_poslovniPartneri.aspx.cs" Inherits="pages_poslovniPartneri" %>

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
        <form id="poslovniPartneri" runat="server">
        <ol class="breadcrumb">
      	<li class="breadcrumb-item active">Osnovni podaci</li>
        <li class="breadcrumb-item active">Komitenti</li>
        <li class="breadcrumb-item active">Poslovni partneri</li>
      </ol>

      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Lista poslovnih partnera</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Komitenti_poslovniPartneri_Dodavanje.aspx">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
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
                  <th>Z</th>
                  <th>U</th>
                  <th>K</th>
                  <th>Šifra partnera</th>
                  <th>Ime i prezime</th>
                  <th>JMBG</th>
                  <th>Mesto</th>
                  <th>Telefon</th>
                  <th>Fax</th>
                </tr>
              </thead>
              <tfoot>
                <tr>
                  <th>Z</th>
                  <th>U</th>
                  <th>K</th>
                  <th>Šifra partnera</th>
                  <th>Ime i prezime</th>
                  <th>JMBG</th>
                  <th>Mesto</th>
                  <th>Telefon</th>
                  <th>Fax</th>
                </tr>
              </tfoot>
              <tbody>
                
                  <%
                      //<a class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a>
                      string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                      string nazivGodine = (String)Session["odabranaGodina"];
                      nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                      System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "poslovni_partneri", "ne", nazivPoslovnice);

                      foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                      {
                          Response.Write("<tr>");

                          Response.Write("<td> <a class =pages href=# page_name=Komitenti_poslovniPartneri_TR.aspx?SIFRA=" + red["Sifra"].ToString() + " >Z</a></td>");
                          Response.Write("<td> <a class =pages href=# page_name=Komitenti_ugovoriPartnera.aspx?SPECIFICNO=" + red["Sifra"].ToString() + " >U</a></td>");
                          Response.Write("<td> <a class =pages href=# page_name=Komitenti_poslovniPartneri_KlasifikacijaAsortimanaDobavljaca.aspx?SIFRA=" + red["Sifra"].ToString() + " >K</a></td>");
                          //Response.Write("<td><a class = &quot;pages&quot; href=Komitenti_poslovniPartneri_Dodavanje.aspx?SIFRA=" + red["Sifra"].ToString() + " page_name=&quot; Komitenti_poslovniPartneri_Dodavanje.aspx &quot;>" + red["Sifra"].ToString() + "</a></td>");
                          Response.Write("<td> <a class =pages href=# page_name=Komitenti_poslovniPartneri_Dodavanje.aspx?SIFRA=" + red["Sifra"].ToString() + " >" + red["Sifra"].ToString() + "</a></td>");


                          Response.Write("<td>" + red["ImePrezime"].ToString() + "</td>");
                          Response.Write("<td>" + red["JMBG"].ToString() + "</td>");
                          Response.Write("<td>" + red["Mesto"].ToString() + "</td>");
                          Response.Write("<td>" + red["Telefon"].ToString() + "</td>");
                          Response.Write("<td>" + red["Fax"].ToString() + "</td>");

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
