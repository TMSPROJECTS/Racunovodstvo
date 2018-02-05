<%@ Page Language="C#" AutoEventWireup="true" CodeFile="racunovodstvo_UR_Dodavanje.aspx.cs" Inherits="pages_racunovodstvo_UR_Dodavanje" %>

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
    <form id="form2" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Računovodstvo</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="racunovodstvo_UR.aspx">Ulazni računi</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="ur_dodavanje" action = "">
          <div class="row">
              <div class="labela col-md-4" style="padding-top: 20px;">
              </div>
              <div class = "inputDiv col-md-8" style="padding-top: 20px;">
	              <div class="col-md-4">
	             </div>
	          </div>        
          </div>
          <div runat="server" class="row" id="divDok">
              <div class="labela col-md-4">
                 <label for="dokument">Dokument</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="dokument" name="dokument" disabled="disabled" />
	             </div>
	             
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="datumZahteva">* Datum</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="date" class="form-control" id="datum" name="datum" value="2014-01-12" required>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="valuta">* Valuta</label>
              </div>
              <div class = "inputDiv col-md-8">
		              <div class="col-md-4">
		             	 <input runat ="server" class="form-control" id="valuta" name="Valuta" required>
		             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="ugovorDob">* Program</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="program" id="selProgram" onchange="promeniPAK(this.value)" class="form-control form-control-sm select2">            	 
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="selPogramAkt">* Programska aktivnost</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="programAkt" id="selPogramAkt" class="form-control form-control-sm select2">
                    
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="ugovorDob">* Funkcija</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="funkcija" id="selFunkcija" class="form-control form-control-sm select2">
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="ugovorDob">* Izvor finansiranja</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="izvorF" id="selIzvorF" class="form-control form-control-sm select2">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="ugovorDob">* Dobavljač</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	<select runat ="server" name="dob" id="selDob" onchange="promeniTK(this.value)" class="form-control form-control-sm select2" required="required">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="tekr">Tekući račun</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="tekr" id="tekr" class="form-control form-control-sm select2">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="brojFakture">Broj fakture</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="brojFakture" name="brojFakture" required="required" />
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="ugovorDob">Ugovor dobavljača</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat ="server" name="ugovorDob" id="ugovorDob" class="form-control form-control-sm select2">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>
     	 <div class="row">
              <div class="labela col-md-4">
                 <label for="napomena">Opis</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <textarea rows=3 runat ="server" class="form-control" id="napomena" name="napomena"></textarea>
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
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="racunovodstvo_UR.aspx">Odustani</a>
	             </div>
	          </div>
          </div>

             <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>

        </form>
    <script src="../js/racunovodstvo_UR_klik.js"></script>
    <script src="../js/racunovodstvo_UR_promeniTK.js"></script>
    <script src="../js/racunovodstvo_UR_promeniPrAkt.js"></script>
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
               placeholder: '--- Izaberite ---',
               width: '100%'
           });
       });

       

    function reset() {
        document.getElementById("ur_dodavanje").reset();
    }
    </script>
        </form>
</body>
</html>
