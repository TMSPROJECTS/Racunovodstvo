<%@ Page Language="C#" AutoEventWireup="true" CodeFile="racunovodstvo_URStavkeDodavanje.aspx.cs" Inherits="pages_racunovodstvo_URStavkeDodavanje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <!-- Bootstrap core CSS-->
  <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <!-- Custom fonts for this template-->
  <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <!-- Custom styles for this template-->
  <link href="../css/sb-admin.css" rel="stylesheet">
</head>


<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="ur_stavke_dodavanje" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Računovodstvo</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item">Ulazni računi</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="racunovodstvo_URStavke.aspx?SIFRA=<%Response.Write(Request.QueryString["SIFRA"]);%>">Stavke</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="zahtevForma" action = "">
          <div class="row">
              <div class="labela col-md-4" style="padding-top: 20px;">
              </div>
              <div class = "inputDiv col-md-8" style="padding-top: 20px;">
	              <div class="col-md-4">
	             </div>
	          </div>        
          </div>
          <div runat="server" class="row" id="divID">
              <div class="labela col-md-4">
                 <label for="idStavke">ID</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">                     
	             	 <input runat ="server" class="form-control" id="idStavke" name="idStavke" disabled="disabled" />
	             </div>
	             
	          </div>
          </div>          
          <div class="row">
              <div class="labela col-md-4">
                 <label for="trosak">Trošak</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
                      <input type="hidden" class="form-control" id="dokument" name="dokument" value="<%Response.Write(Request.QueryString["SIFRA"]);%>" />
	             	 <select runat ="server" name="trosak" id="selTrosak" class="form-control form-control-sm select2">            	 
	             	</select>
	             </div>
	          </div>
          </div>              
          <div class="row">
              <div class="labela col-md-4">
                 <label for="iznosBP">* Iznos bez PDV - a</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="iznosBP" name="iznosBP" required="required" />
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="stopa">* PDV</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="stopa" id="selStopa" class="form-control form-control-sm select2">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>      
          <div class="row">
              <div class="labela col-md-4">
                 <label for="iznos">* Iznos sa PDV - om</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="iznos" name="iznos">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="placeno">* Plaćeno</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="placeno" name="placeno">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="konto">Konto</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="konto" id="selKonto" class="form-control form-control-sm select2">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="opis">Opis</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="opis" name="opis">
	             </div>
	          </div>
          </div>      
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="saveButton">
	             	 <input type="button" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Sačuvaj" OnClick="btnSave();">
	             	 <input type="button" onclick="reset()" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Poništi">
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="racunovodstvo_URStavke.aspx?SIFRA=<%Response.Write(Request.QueryString["SIFRA"]);%>">Odustani</a>
	             </div>
	          </div>
          </div>

             <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>

        </form>
    <script src="../js/racunovodstvo_UR_Stavke_Save.js"></script>
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
            $('.select2').select2({
                allowClear: true,
                placeholder: '--- Izaberite ---'
            });
        });
        

    function reset() {
        document.getElementById("ur_stavke_dodavanje").reset();
    }
    </script>
        </form>
</body>

</html>
