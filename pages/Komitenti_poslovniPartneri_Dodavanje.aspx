<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="Komitenti_poslovniPartneri_Dodavanje.aspx.cs" Inherits="pages_Komitenti_poslovniPartneri_Dodavanje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  
</head>
<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form2" runat="server">

      <ol class="breadcrumb">
        <li class="breadcrumb-item"> Osnovni Podaci</li>
        <li class="breadcrumb-item"><a runat ="server" id="linkPoslovni" class = "pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Poslovni partneri</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="reset" action = "">
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
                 <label for="lblImePrezime">* Ime i prezime</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="inputImePrezime" name="inputImePrezime" required>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="lblJMBG">* JMBG</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="inputJMBG" name="inputJMBG"  required>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="lblMesto">Mesto</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="inputMesto" name="inputMesto">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="lblTelefon">Telefon</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="inputTelefon" name="inputTelefon">
	             </div>
	          </div>
          </div>
            <div class="row">
              <div class="labela col-md-4">
                 <label for="lblFax">Fax</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="inputFax" name="inputFax">
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
                      <!--  <input type="button" onclick="" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Brisanje">-->
	             	 <input type="button" onclick="reset()" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Poništi">
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="Komitenti_poslovniPartneri.aspx">Odustani</a>
                       
	             </div>
                 
	          </div>
          </div>

            <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

               <!-- <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label>  -->
                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>
        </form>

        <script src="../js/komitenti_PP_sacuvaj.js"></script>

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
        document.getElementById("form2").reset();
    }
    </script>
        </form>
</body>
</html>
