<%@ Page Language="C#" AutoEventWireup="true" CodeFile="racunovodstvo_UR.aspx.cs" Inherits="pages_racunovodstvo_UR" %>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Ulazni računi</title>
  <!-- Bootstrap core CSS-->
  <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <!-- Custom fonts for this template-->
  <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <!-- Page level plugin CSS-->
  <link href="../vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet">  
</head>
    <body class="fixed-nav sticky-footer bg-dark" id="page-top">
        <ol class="breadcrumb">
      	<li class="breadcrumb-item active">Osnovni podaci</li>
        <li class="breadcrumb-item active">Komitenti</li>
        <li class="breadcrumb-item active">Poslovni partneri</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Ulazni računi</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="racunovodstvo_UR_Dodavanje.aspx">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
              <% 
                    if (Request.QueryString["spObavestenje"] != null)
                    {
                        Response.Write(Server.UrlDecode(Request.QueryString["spObavestenje"]));
                    }
                %>
          </span></div>
          <div class ="row enterButton">
            <a class="btn btn-primary btn-sm js-scroll-trigger pages" style="background-color:#64AF99; border-color:#64AF99;" onclick="kreirajZS();">Kreiraj zahtev za sredstva</a>
          </div>
        <div class="card-body">          
          <div class="table-responsive">                    
            <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
              <thead>
                <tr>
                  <tr>
                  <th>&nbsp;</th>
                  <th title='Storniraj'>S</th>
                  <th>K</th>
                  <th>Dokument</th>
                  <th>Stavke</th>
                  <th>Datum</th>
                  <th>Valuta</th>
                  <th>Dobavljac</th>
                  <th>Broj ulazne fakture</th>
                  <th>Opis</th>
                  <th>Iznos</th>
                  <th>Placeno</th>
                  <th>Ugovor dobavljaca</th>
                  <th>Korisnik</th>
                  <th>Poslednja izmena</th>
                 </tr>              
              </thead>
              <tfoot>
                  <tr>
                  <th>&nbsp;</th>
                  <th title='Storniraj'>S</th>
                  <th>K</th>
                  <th>Dokument</th>
                  <th>Stavke</th>
                  <th>Datum</th>
                  <th>Valuta</th>
                  <th>Dobavljac</th>
                  <th>Broj ulazne fakture</th>
                  <th>Opis</th>
                  <th>Iznos</th>
                  <th>Placeno</th>
                  <th>Ugovor dobavljaca</th>
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
                    System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "ulazni_racuni", "ne", nazivPoslovnice);

                    foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                    {
                        Response.Write("<tr>");
                        System.Data.DataTable dtProvera = Upiti.Select2("*", "zahtev_za_sredstva", "UlazniRacun LIKE '%" + red["Dokument"].ToString() + "%'", nazivPoslovnice);
                        if(dtProvera.Rows.Count == 0)
                        {
                            if (red["Storno"].ToString() == "N")
                            {
                                Response.Write("<td align=center><input type='checkbox' id='chk" + red["Dokument"].ToString() + "' name='chk" + red["Dokument"].ToString() + "' /></td>");
                            }
                            else
                            {
                                Response.Write("<td align=center>&nbsp;</td>");
                            }
                        }
                        else
                        {
                            Response.Write("<td align=center>&nbsp;</td>");
                        }

                        string storno = "";
                        if(red["Storno"].ToString() == "N")
                        {
                            storno = "<td><a id=" + red["Dokument"].ToString() + " href='#' title='Storniraj " + red["Dokument"].ToString() + "' onclick='storno(this.id);return false;'>N</a>";
                        }
                        else
                        {
                            storno = "<td bgcolor='#F13434'>D</td>";
                        }


                        Response.Write(storno);
                        Response.Write("<td>K</td>");
                        Response.Write("<td> <a class =pages href=# page_name=racunovodstvo_UR_Dodavanje.aspx?SIFRA=" + red["Dokument"].ToString() + " >" + red["Dokument"].ToString() + "</a></td>");
                        Response.Write("<td> <a class =pages href=# page_name=racunovodstvo_URStavke.aspx?SIFRA=" + red["Dokument"].ToString() + " >stavke</a></td>");
                        var dtDatum = DateTime.Parse(red["Datum"].ToString());
                        Response.Write("<td>" + dtDatum.ToString("dd.MM.yyyy.") + "</td>");
                        Response.Write("<td>" + red["Valuta"].ToString() + "</td>");
                        System.Data.DataTable dtDobavljac = Upiti.Select2("*", "poslovni_partneri",  "Sifra = '" + red["Valuta"].ToString() + "'", nazivPoslovnice);
                        if(dtDobavljac.Rows.Count > 0)
                        {
                            Response.Write("<td>" + dtDobavljac.Rows[0]["Naziv"] + "</td>");
                        }
                        else
                        {
                            Response.Write("<td>&nbsp;</td>");
                        }
                        Response.Write("<td>" + red["Broj_fakture"].ToString() + "</td>");
                        Response.Write("<td>" + red["Opis"].ToString() + "</td>");
                        System.Data.DataTable dtIznos = Upiti.Select2("sum(Iznos) as Iznos", "ulazni_racuni_stavke",  "Dokument = '" + red["Dokument"].ToString() + "'", nazivPoslovnice);
                        if(dtIznos.Rows.Count > 0)
                        {
                            Response.Write("<td>" + dtIznos.Rows[0]["Iznos"] + "</td>");
                        }
                        else
                        {
                            Response.Write("<td>0.00</td>");
                        }
                        System.Data.DataTable dtPlaceno = Upiti.Select2("sum(Placeno) as Placeno", "ulazni_racuni_stavke",  "Dokument = '" + red["Dokument"].ToString() + "'", nazivPoslovnice);
                        if(dtPlaceno.Rows.Count > 0)
                        {
                            Response.Write("<td>" + dtPlaceno.Rows[0]["Placeno"] + "</td>");
                        }
                        else
                        {
                            Response.Write("<td>0.00</td>");
                        }
                        Response.Write("<td>" + red["Ugovor"].ToString() + "</td>");
                        Response.Write("<td>" + red["Korisnik"].ToString() + "</td>");
                        Response.Write("<td>" + red["Poslednja_izmena"].ToString() + "</td>");
                        Response.Write("</tr>");
                    }

                %>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    <!-- Custom styles for this template-->
    <link href="../css/sb-admin.css" rel="stylesheet">
     <!-- Custom scripts for this page-->
    <script src="../js/sb-admin-datatables.min.js"></script>
    <script src="../js/racunovodstvo_UR_storno.js"></script>
    <script src="../js/racunovodstvo_UR_kreirajZS.js"></script>
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
