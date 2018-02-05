<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="PomocniPodaci_FunkcionalnaKlasifikacija_Dodavanje.aspx.cs" Inherits="pages_PomocniPodaci_FunkcionalnaKlasifikacija_Dodavanje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content
</head>

<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form1" runat="server">
      <ol class="breadcrumb">
      	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Pomoćni podaci</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="PomocniPodaci_FunkcionalnaKlasifikacija.aspx">Funkcionalna klasifikacija</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="izvorForma" action = "">

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
                 <label for="sifra">* Šifra</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="sifra" name="sifra" required>
	             </div>
	             
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="naziv">* Naziv</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" class="form-control" id="naziv" name="naziv" required>
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
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="PomocniPodaci_FunkcionalnaKlasifikacija.aspx">Odustani</a>
	             </div>
	          </div>
          </div>

           <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

               <!-- <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label>  -->
                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>
        </form>

        <script src="../js/FunkcionalnaKlasifikacija_Dodavanje.js"></script>

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

    function reset() {
        document.getElementById("namenaSredstavaForma").reset();
    }
    </script>
        </form>
</body>
</html>
