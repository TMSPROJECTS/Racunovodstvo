<%@ Page Language="C#" AutoEventWireup="true" CodeFile="navbar.aspx.cs" Inherits="pages_navbar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Finansijski plan</title>
  <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <link href="../css/sb-admin.css" rel="stylesheet">
  <link href="../vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet">
    <link href="../css/select2.min.css" rel="stylesheet">
    <script src="../js/select2.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />

</head>

  <body class="fixed-nav sticky-footer bg-dark" id="page-top">
      <form id="form2" runat="server">

          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
<asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="100000"></asp:Timer>
              </ContentTemplate>
          </asp:UpdatePanel>
          

 <nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top" id="mainNav">
      <div class="navbar-brand"><a class="navbar-brand pages" href="#" page_name="homePage.aspx">
     <asp:Label ID="lblJedinica" runat="server" Text="Label"></asp:Label></a></div>
      <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarResponsive">
      <ul class="navbar-nav navbar-sidenav" id="exampleAccordion">
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Osnovni podaci">
          <a class="nav-link nav-link-collapse collapsed" data-toggle="collapse" href="#collapseMulti" data-parent="#exampleAccordion">
            <i class="fa fa-fw fa-sitemap"></i>
            <span class="nav-link-text">Osnovni podaci</span>
          </a>
          <ul class="sidenav-second-level collapse" id="collapseMulti">
            <li>
               <a class="nav-link-collapse collapsed" data-toggle="collapse" href="#collapseMulti2">
              <i class="fa fa-handshake-o"></i>
              <span>Komitenti</span></a>
              <ul class="sidenav-third-level collapse" id="collapseMulti2">
                <li>
                  <a href="#" class="pages" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a>
                </li>
                <li>
                	<a class="nav-link-collapse collapsed" data-toggle="collapse" href="#collapseMulti3">
                	<i class="fa fa-check-square-o"></i>
                	<span>Ugovori</span></a>
              		<ul class="sidenav-third-level collapse" id="collapseMulti3">
              			<li>
              				<a href="#" class="pages" page_name="Komitenti_ugovoriPartnera.aspx?SPECIFICNO=ne">Ugovori partnera</a>
              			</li>
              		</ul>
                </li>
              </ul>
            </li>
            <li>
               <a class="nav-link-collapse collapsed" data-toggle="collapse" href="#collapseMulti4">
              <i class="fa fa-suitcase"></i>
              <span>Preduzeće</span></a>
              <ul class="sidenav-third-level collapse" id="collapseMulti4">
                <li>
                  <a href="#" class="pages" page_name="Preduzece_podaci.aspx">Podaci o preduzeću</a>
                </li>
              </ul>
            </li>
          </ul>
        </li>
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Budžet">
          <a class="nav-link nav-link-collapse collapsed" data-toggle="collapse" href="#budzet" data-parent="#exampleAccordion">
            <i class="fa fa-briefcase"></i>
            <span class="nav-link-text">Budžet</span>
          </a>
          <ul class="sidenav-second-level collapse" id="budzet">
            <li>
              <a class="nav-link-collapse collapsed" data-toggle="collapse" href="#dokumenti">
              <i class="fa fa-folder-open-o"></i>
              <span>Dokumenti</span></a>
              <ul class="sidenav-third-level collapse" id="dokumenti">
                <li>
                  <a href="#" class="pages" page_name="Dokumenti_ZahtevZaSredstva.aspx">Zahtev za sredstva</a>
                  <a href="#" class="pages" page_name="Dokumenti_FinansijskiPlan.aspx">Finansijski plan</a>
                </li>
              </ul>
            </li>
            <li>
              <a class="nav-link-collapse collapsed" data-toggle="collapse" href="#pomocniPodaci">
              <i class="fa fa-newspaper-o"></i>
              <span>Pomoćni podaci</span></a>
          		<ul class="sidenav-third-level collapse" id="pomocniPodaci">
          			<li>
          				<a href="#" class="pages" page_name="PomocniPodaci_GrupeTroskova.aspx">Grupe troškova</a>
          				<a  href="#" class="pages" page_name="PomocniPodaci_VrsteTroskova.aspx">Vrste troškova</a>
          				<a  href="#" class="pages" page_name="PomocniPodaci_Dokaznica.aspx">Dokaznica</a>
          				<a  href="#" class="pages" page_name="PomocniPodaci_Program.aspx">Program</a>
          				<a  href="#" class="pages" page_name="PomocniPodaci_IzvorFinansiranja.aspx">Izvor finansiranja</a>
          				<a  href="#" class="pages" page_name="PomocniPodaci_FunkcionalnaKlasifikacija.aspx">Funkcionalna klasifikacija</a>
                          
          			</li>
          		</ul>
            </li>
          </ul>
        </li>
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Računovodstvo">
          <a class="nav-link nav-link-collapse collapsed" data-toggle="collapse" href="#racunovodstvo" data-parent="#exampleAccordion">
            <i class="fa fa-line-chart"></i>
            <span class="nav-link-text">Računovodstvo</span>
          </a>
          <ul class="sidenav-second-level collapse" id="racunovodstvo">
            <li>
              <a class="nav-link-collapse collapsed" data-toggle="collapse" href="#dokumentiR">
              <i class="fa fa-folder-open-o"></i>
              <span>Dokumenti</span></a>
              <ul class="sidenav-third-level collapse" id="dokumentiR">
                <li>
                  <a  href="#" class="pages" page_name="racunovodstvo_UR.aspx">Ulazni računi</a>
                  <a href="#">Izlazni računi</a>
                </li>
              </ul>
            </li>
            <li>
              <a class="nav-link-collapse collapsed" data-toggle="collapse" href="#knjizenje">
              <i class= "fa fa-pencil-square-o"></i>
              <span>Knjiženje</span></a>
          		<ul class="sidenav-third-level collapse" id="knjizenje">
          			<li>
          				<a href="#">Ručno knjiženje</a>
          				<a href="#">Knjiženje perioda</a>
          			</li>
          		</ul>
            </li>
             <li>
              <a class="nav-link-collapse collapsed" data-toggle="collapse" href="#izvestaji">
              <i class="fa fa-sticky-note-o"></i>
              <span>Izveštaji</span></a>
          		<ul class="sidenav-third-level collapse" id="izvestaji">
          			<li>
          				<a href="#">Upit glavne knjige</a>
          				<a href="#">Upit knjiženja</a>
          				<a href="#">Bilans prometa</a>
          				<a href="#">IOS</a>
          			</li>
          		</ul>
            </li>
          </ul>
        </li>
      </ul>
      <ul class="navbar-nav sidenav-toggler">
        <li class="nav-item">
          <a class="nav-link text-center" id="sidenavToggler">
            <i class="fa fa-fw fa-angle-left"></i>
          </a>
        </li>
      </ul>
      <ul class="navbar-nav ml-auto">
        <li class="nav-item">
          <a class="nav-link" data-toggle="modal" data-target="#modalLogOut">
            <i class="fa fa-fw fa-sign-out"></i>Izlogujte se</a>
        </li>
      </ul>
    </div>
  </nav>
  <div class="content-wrapper">
    <div class="container-fluid"  id="main">
    </div>
    <!-- /.container-fluid-->
    <!-- /.content-wrapper-->
    <footer class="sticky-footer">
      <div class="container">
        
      </div>
    </footer>
        <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
      <i class="fa fa-angle-up"></i>
    </a>
    <!-- Logout Modal-->
    <div class="modal fade" id="modalLogOut" tabindex="-1" role="dialog" aria-labelledby="logOutModal" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="logOutModal">Želite li da se izlogujete?</h5>
            <button class="close" type="button" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">×</span>
            </button>
          </div>
          <div class="modal-body">Kliknite "Izloguj se" ukoliko želite da se izlogujete.</div>
          <div class="modal-footer">
            <button class="btn btn-secondary" type="button" data-dismiss="modal">Odustani</button>
           <!-- <a class="btn btn-primary" href="/opstina/login.aspx">Izloguj se</a>-->
            <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Izloguj se" OnClick="logout_click" />
          </div>
        </div>
      </div>
    </div>
   <script src="../vendor/jquery/jquery.min.js"></script>
    <script src="../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- Core plugin JavaScript-->
    <script src="../vendor/jquery-easing/jquery.easing.min.js"></script>
    <!-- Custom scripts for all pages-->
    <script src="../js/sb-admin.min.js"></script>
    <script src="../js/sb-admin.js"></script>
    <!-- Custom scripts for this page-->
    <!-- Toggle between fixed and static navbar-->
    <script src="../vendor/datatables/jquery.dataTables.js"></script>
    <script src="../vendor/datatables/dataTables.bootstrap4.js"></script>
  
    <script src="../js/creative.min.js"></script>
    <script src="../vendor/scrollreveal/scrollreveal.min.js"></script>
    <script src="../vendor/magnific-popup/jquery.magnific-popup.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    <script>
    $('#toggleNavPosition').click(function() {
      $('body').toggleClass('fixed-nav');
      $('nav').toggleClass('fixed-top static-top');
    });

    </script>
    <!-- Toggle between dark and light navbar-->
    <script>
    $('#toggleNavColor').click(function() {
      $('nav').toggleClass('navbar-dark navbar-light');
      $('nav').toggleClass('bg-dark bg-light');
      $('body').toggleClass('bg-dark bg-light');
    });
    
    $(document).ready(function () {//this function load pages in the main div in html 
        $("#main").load("homePage.aspx")
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
  </div>
          </form>
</body>

</html>
