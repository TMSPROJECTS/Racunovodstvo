<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dokumenti_NamenaSredstava.aspx.cs" Inherits="pages_Dokumenti_NamenaSredstava" %>

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
    <form id="form2" runat="server">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="Dokumenti_ZahtevZaSredstva.aspx">Zahtev za sredstva</a></li>
        <li class="breadcrumb-item active">Namena sredstava</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Lista sredstava za namenu</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Dokumenti_NamenaSredstava_Dodavanje.aspx">Novi unos</a>&nbsp;&nbsp;&nbsp;<span ID="spObavestenje" style="color:cadetblue; font-weight: bold; font-size: 18px;">
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
                  <th>Program</th>
                  <th>Programska aktivnost</th>
                  <th>Projekat</th>
                  <th>Funkcija</th>
                  <th>Konto</th>
                  <th>Namena</th>
                  <th>Poslovnica</th>
                  <th>Mesto troška</th>
                  <th>Dobavljač</th>
                  <th>Fizičko lice</th>
                  <th>Iznos</th>
                  <th>Plaćeno</th>
                  <th>Korisnik</th>
                  
                </tr>
              </thead>
              <tfoot>
                <tr>
                    <th>Dokument</th>
               	  <th>Program</th>
                  <th>Programska aktivnost</th>
                  <th>Projekat</th>
                  <th>Funkcija</th>
                  <th>Konto</th>
                  <th>Namena</th>
                  <th>Poslovnica</th>
                  <th>Mesto troška</th>
                  <th>Dobavljač</th>
                  <th>Fizičko lice</th>
                  <th>Iznos</th>
                  <th>Plaćeno</th>
                  <th>Korisnik</th>
                  
                </tr>
              </tfoot>
              <tbody>
                <%
                    //<a class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a>
                    string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                      string nazivGodine = (String)Session["odabranaGodina"];
                      nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                    string vrednost = Request.QueryString["SIFRA8"];

                     Session["sifraZS"] = Request.QueryString["SIFRA8"];

                    System.Data.DataTable dtFunkcija = Upiti.Select2("*", "funkcionalna_klasifikacija", "ne", nazivPoslovnice);
                    System.Data.DataTable dtIzvorFinansiranja = Upiti.Select2("*", "namena_sredstava_izvor_finansiranja", "ne", nazivPoslovnice);
                    System.Data.DataTable dtProgram = Upiti.Select2("*", "namena_sredstava_program", "ne", nazivPoslovnice);
                    System.Data.DataTable dtProgramskaAktivnost = Upiti.Select2("*", "namena_sredstava_programska_aktivnost", "ne", nazivPoslovnice);
                    System.Data.DataTable dtProgramProjekat = Upiti.Select2("*", "namena_sredstava_projekat", "ne", nazivPoslovnice);

                    System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "namena_sredstava", "Dokument='" + vrednost + "'", nazivPoslovnice);

                    foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                    {
                        Response.Write("<tr>");
                         Response.Write("<td> <a class =pages href=# page_name=Dokumenti_NamenaSredstava_Dodavanje.aspx?SIFRA4=" + red["Dokument1"].ToString() + " >" + red["Dokument1"].ToString() + "</a></td>");
                        //Response.Write("<td> <a class =pages href=# page_name=Dokumenti_NamenaSredstava_Dodavanje.aspx?SIFRA4=" + red["Dokument1"].ToString() + " >" + red["Dokument1"].ToString() + "</a></td>");

                        if (red["IDprogram"].ToString().Trim() == "" || red["IDprogram"].ToString().Trim() == "0")
                        {
                            Response.Write("<td>" + " " + "</td>");
                        }
                        else
                        {
                            foreach (System.Data.DataRow redic in dtProgram.Rows)
                            {


                                if (redic["SIFRA"].ToString() == red["IDprogram"].ToString())
                                {
                                    Response.Write("<td>" + redic["Program"].ToString() + "</td>");
                                    break;
                                }
                            }
                        }

                        if (red["IDprogramskaAktivnost"].ToString().Trim() == "" || red["IDprogramskaAktivnost"].ToString().Trim() == "0")
                        {
                            Response.Write("<td>" + " " + "</td>");
                        }
                        else
                        {
                            foreach (System.Data.DataRow redic in dtProgramskaAktivnost.Rows)
                            {
                               
                                if (redic["Sifra"].ToString() == red["IDprogramskaAktivnost"].ToString())
                                {
                                    Response.Write("<td>" + redic["ProgramskaAktivnost"].ToString() + "</td>");
                                    break;
                                }
                            }
                        }

                        if (red["IDprojekat"].ToString().Trim() == "" || red["IDprojekat"].ToString().Trim() == "0")
                        {
                            Response.Write("<td>" + " " + "</td>");
                        }
                        else
                        {
                            foreach (System.Data.DataRow redic in dtProgramProjekat.Rows)
                            {
                                if (redic["ID"].ToString() == red["IDprojekat"].ToString())
                                {
                                    Response.Write("<td>" + redic["Projekat"].ToString() + "</td>");
                                    break;
                                }
                            }
                        }

                        if (red["IDfunkcija"].ToString().Trim() == "" || red["IDfunkcija"].ToString().Trim() == "0")
                        {
                            Response.Write("<td>" + " " + "</td>");
                        }
                        else
                        {
                            foreach (System.Data.DataRow redic in dtFunkcija.Rows)
                            {
                                if (redic["Sifra"].ToString() == red["IDfunkcija"].ToString())
                                {
                                    Response.Write("<td>" + redic["Naziv"].ToString() + "</td>");
                                    break;
                                }
                            }
                        }
                        Response.Write("<td>" + " " + "</td>"); //konto
                        Response.Write("<td>" + red["Namena"].ToString() + "</td>");
                        Response.Write("<td>" + " " + "</td>"); //poslovnica
                        ////Response.Write("<td><a class = &quot;pages&quot; href=Komitenti_poslovniPartneri_Dodavanje.aspx?SIFRA=" + red["Sifra"].ToString() + " page_name=&quot; Komitenti_poslovniPartneri_Dodavanje.aspx &quot;>" + red["Sifra"].ToString() + "</a></td>");
                        //Response.Write("<td> <a class =pages href=# page_name=Dokumenti_ZahtevZaSredstva_Dodavanje.aspx?SIFRA3=" + red["SifraDokumenta"].ToString() + " >" + red["SifraDokumenta"].ToString() + "</a></td>");
                        Response.Write("<td>" + " " + "</td>"); //mesto troskova
                        Response.Write("<td>" + " " + "</td>"); //dobavljac
                        Response.Write("<td>" + " " + "</td>"); //fizicko lice

                        if(red["Iznos"].ToString().Trim() == "" || red["Iznos"].ToString().Trim() == "0")
                        {
                            Response.Write("<td>0.00</td>");
                        }
                        else
                        {
                            Response.Write("<td>" + red["Iznos"].ToString() + "</td>"); //iznos
                        }
                      
                        if(red["Placeno"].ToString().Trim() == "" || red["Placeno"].ToString().Trim() == "0")
                        {
                            Response.Write("<td>0.00</td>");
                        }
                        else
                        {
                            Response.Write("<td>" + red["Placeno"].ToString() + "</td>"); //placeno
                        }
                        Response.Write("<td>" + " " + "</td>"); // korisnik


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