<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="PomocniPodaci_Dokaznica_Dodavanje.aspx.cs" Inherits="pages_PomocniPodaci_Dokaznica_Dodavanje" %>

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
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="PomocniPodaci_Dokaznica.aspx">Dokaznica</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="dokaznicaForma" action = "">

             <div class="row">
              <div class="labela col-md-4">
                 <br />
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <br />
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
                 <label for="konto">* Konto</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="konto" name="konto" required>
	             </div>
	             
	          </div>
          </div>

          <div class="row">
              <div class="labela col-md-4">
                 <label for="namena">* Namena</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="namena" name="namena" required>
	             </div>
	             
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="dobavljac">* Dobavljač</label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">
		              <div class="col-md-4">
		             	 
                           <select runat ="server" name="dobavljac" id="dobavljac" class="form-control form-control-sm select2" style="width:100%"  onchange="racuni();" >
			             <option value = "" selected>-- Izaberite --</option></select>
		             </div>
		              <!-- <div class="col-md-4">
		             	 <input class="form-control" id="punoImeDobavljaca" name="punoImeDobavljaca" required>
		             </div> -->
	             </div>
	          </div>
          </div>
        <!--   <div class="row">
              <div class="labela col-md-4">
                 <label for="tekRacun">Tekući račun</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select name="tekRacun" id="tekRacun" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
          </div> -->

            <div class="row">
              <div class="labela col-md-4">
                 <label for="tekuciRacun">Tekući račun</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 

                       <select runat ="server" name="takuciRacun" id="takuciRacun" class="form-control form-control-sm" style="width:100%" >
			             <option value = "" selected>-- Izaberite --</option></select>
	             </div>
	             
	          </div>
          </div>

           <div class="row">
              <div class="labela col-md-4">
                 <label for="sifPlacanja">* Šifra plaćanja</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="sifPlacanja" name="sifPlacanja" required>
	             </div>
	             
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="pozivNaBr">* Poziv na broj</label></div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="pozivNaBr" name="pozivNaBr" required>
	             </div>
	             
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="vaziOd">* Važi od</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="date" class="form-control" id="vaziOd" name="vaziOd" required>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="vaziDo">* Važi do</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="date" class="form-control" id="vaziDo" name="vaziDo" required>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="iznos">* Iznos</label>
              </div>
              <div class = "inputDiv col-md-8">
		              <div class="col-md-4">
		             	 <input runat ="server" class="form-control" id="iznos" name="iznos" required>
		             </div>
	          </div>
          </div>
         
     	
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="saveButton">
	             	<!-- <input type="submit" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Sačuvaj">-->
                     <!--  <a runat="server" onclick ="btnSave_Click" class="btn btn-primary btn-xl js-scroll-trigger pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Sačuvaj</a>-->
                     <input type="button" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Sačuvaj" OnClick="btnSave();">
                        <input type="button" onclick="reset()" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Poništi">
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="PomocniPodaci_Dokaznica.aspx">Odustani</a>
	             </div>
	          </div>
          </div>
             <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

               <!-- <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label>  -->
                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>
        </form>

        <script src="../js/Dokaznica_Dodavanje.js"></script>
         <script src="../js/Dokaznica_TR.js"></script>

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
        document.getElementById("namenaSredstavaForma").reset();
    }
    </script>
        </form>
</body>
</html>
