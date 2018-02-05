<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PomocniPodaci_PravoPregleda.aspx.cs" Inherits="pages_PomocniPodaci_PravoPregleda" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Finansijski plan - pravo pregleda troškova</title>
     <!-- Bootstrap core CSS-->
  <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <!-- Custom fonts for this template-->
  <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <!-- Page level plugin CSS-->
  <link href="../vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet">
  <!-- Custom styles for this template-->
  <link href="../css/sb-admin.css" rel="stylesheet">
</head>

    <div id="main">
<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form1" runat="server">
      <ol class="breadcrumb">
      	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Pomoćni podaci</li>
        <li class="breadcrumb-item active">Pravo pregleda troškova</li>
      </ol>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> Lista prava pregleda i unosa</div>
          <div class ="row enterButton">
          <a class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="pravoPregledaUnos.html">Novi unos</a></div>
        <div class="card-body">
          <div class="table-responsive">
            <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
              <thead>
                <tr>
                  <th>Korisnik</th>
                  <th>Grupa ili vrsta troška</th>
                </tr>
              </thead>
              <tfoot>
                <tr>
                  <th>Korisnik</th>
                  <th>Grupa ili vrsta troška</th>
                </tr>
              </tfoot>
              <tbody>
                 <%

                     string nazivPoslovnice = (String)Session["odabranaPoslovnicaBaza"];
                      string nazivGodine = (String)Session["odabranaGodina"];
                      nazivPoslovnice = nazivPoslovnice + "_" + nazivGodine;
                    System.Data.DataTable dtSviPodaci = Upiti.Select2("*", "grupe_troskova", "ne", nazivPoslovnice);

                    foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                    {
                        Response.Write("<tr>");

                        Response.Write("<td> <a class =pages href=# page_name=PomocniPodaci_GrupeTroskova_Dodavanje.aspx?SIFRA9=" + red["Sifra"].ToString() + " >" + red["Naziv"].ToString() + "</a></td>");
                        Response.Write("<td>" + red["Uneo"].ToString() + "</td>");

                        Response.Write("</tr>");
                    }

                 %>
              </tbody>
            </table>
          </div>
        </div>
      </div>
  <!-- Bootstrap core JavaScript-->
    <script src="../vendor/jquery/jquery.min.js"></script>
    <script src="../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- Core plugin JavaScript-->
    <script src="../vendor/jquery-easing/jquery.easing.min.js"></script>
    <!-- Page level plugin JavaScript-->
    <script src="../vendor/datatables/jquery.dataTables.js"></script>
    <script src="../vendor/datatables/dataTables.bootstrap4.js"></script>
    <!-- Custom scripts for all pages-->
    <script src="../js/sb-admin.min.js"></script>
    <!-- Custom scripts for this page-->
    <script src="../js/sb-admin-datatables.min.js"></script>
  <script>
    $(document).ready(function(){//this function load pages in the main div in html 
		$('.pages').click(function(){	
		var page_name = $(this).attr('page_name');
		$('#main').load(page_name);
		});
	});
    </script>
    </form>
</body>
</div>


</html>
