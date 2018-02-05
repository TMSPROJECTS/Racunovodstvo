<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Komitenti_poslovniPartneri_TR.aspx.cs" Inherits="pages_Komitenti_poslovniPartneri_TR" %>

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
        <li class="breadcrumb-item active"><a class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Ugovori partnera</a></li>
        <li class="breadcrumb-item active">Tekući računi</li>
      </ol>

      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Lista tekućih računa</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Komitenti_poslovniPartneri_TR_Dodavanje.aspx?SIFRA=<%Response.Write(Request.QueryString["SIFRA"]);%>">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
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
                    <th>Šifra računa</th>
                  <th>Račun</th>
                  <th>Poslednja izmena</th>
                </tr>
              </thead>
              <tfoot>
                <tr>
                    <th>Šifra računa</th>
                  <th>Račun</th>
                  <th>Poslednja izmena</th>
                </tr>
              </tfoot>
              <tbody>
                
                  <%
                      //<a class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a>
                      string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                      string nazivGodine = (String)Session["odabranaGodina"];
                      nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                      string vrednost = Request.QueryString["SIFRA"];

                      //if (vrednost == "" || vrednost == null)
                      //{
                      //    vrednost =  (String)Session["povratnaSesija1"];
                      //}

                      System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "tekuci_racun", "IDpartnera='" + vrednost + "'" , nazivPoslovnice);

                      //if (Request.QueryString["SIFRA"] != "" && Request.QueryString["SIFRA"] != null)
                      //{
                      Session["sifraPartneraZaTekuciRacun"] = Request.QueryString["SIFRA"];
                      //}
                      //else
                      //{
                      //    Session["sifraPartneraZaTekuciRacun"] = (String)Session["povratnaSesija2"];
                      //}


                      foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                      {
                          Response.Write("<tr>");

                          Response.Write("<td> <a class =pages href=# page_name=Komitenti_poslovniPartneri_TR_Dodavanje.aspx?SIFRA=" + vrednost + "&SIFRA20=" + red["ID"].ToString() + " >" + red["ID"].ToString() + "</a></td>");
                          Response.Write("<td>" + red["Racun"].ToString() +  "</td>");
                          DateTime dtDatumUgovora = DateTime.Parse(red["PoslednjaIzmena"].ToString());
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
                          Response.Write("<td>" + dan2 + "." + mesec2 + "." + godina2 + ".</td>");


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
