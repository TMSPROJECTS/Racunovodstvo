<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="Preduzece_podaci.aspx.cs" Inherits="pages_poslovneJediniceUnos" %>

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
        <li class="breadcrumb-item"><!--<a class = "pages" href="#" page_name="poslovniPartneri.html">Poslovni partneri</a>-->Preduzeće</li>
        <li class="breadcrumb-item active">Podaci o preduzeću</li>
      </ol>
        <form action = "">
        <!--  <div class="row">
              <div class="labela col-md-4">
                 <label for="sifra">* Šifra</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="sifra" name="sifra" disabled>
	             </div>
	          </div>
          </div> -->
          <div class="row">
              <div class="labela col-md-4">
                 <label for="poslovnoIme">* Poslovno ime</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="poslovnoIme" name="poslovnoIme" required>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="skrPoslovnoIme">Skraćeno poslovno ime</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="skrPoslovnoIme" name="skrPoslovnoIme">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="mesto">Mesto</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="mesto" name="mesto" >
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="adresa">Adresa</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="adresa" name="adresa">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="drzava">Država</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	<input class="form-control" runat ="server" id="drzava" name="drzava">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="email">Email</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="email" name="email" type="email" aria-describedby="emailHelp">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="telefon">Telefon</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="telefon" name="telefon">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="fax">Fax</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="fax" name="fax">
	             </div>
	          </div>
          </div>
          
          <div class="separatorDiv">Poslovni podaci</div>
          
          <div class="row">
              <div class="labela col-md-4">
                 <label for="datumOsnivanja">Datum osnivanja</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input type="date" runat ="server" class="form-control" id="datumOsnivanja" name="datumOsnivanja">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="pib">PIB</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="pib" name="pib">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="registarskiBr">Registarski broj</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="registarskiBr" name="registarskiBr">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="maticniBr">Matični broj</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="maticniBr" name="maticniBr">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="sifraDel">Šifra delatnosti</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="sifraDel" name="sifraDel">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="vrstaDel">Vrsta delatnosti</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" runat ="server" id="vrstaDel" name="vrstaDel">
	             </div>
	          </div>
          </div>
          
          <div class="separatorDiv">Podaci o kontakt osobama</div>
          
          <div class="row">
              <div class="labela col-md-4">
                 <label for="direktor">Direktor</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	  <input class="form-control"  id="direktor" runat ="server" name="direktor">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="telDirektora">Telefon direktora</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control"  id="telDirektora" runat ="server" name="telDirektora">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="finOsoba">Finansije (kontakt osoba)</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	<input class="form-control"  id="finOsoba" runat ="server" name="finOsoba">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="finTel">Telefon finansije</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control"  id="finTel" runat ="server" name="finTel">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="komercijalaOsoba">Komercijala (kontakt osoba)</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control"  id="komercijalaOsoba" runat ="server" name="komercijalaOsoba">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="komercijalaTel">Telefon komercijala</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control"  id="komercijalaTel" runat ="server" name="komercijalaTel">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="saveButton">
	             	 <!-- <input type="submit" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Sačuvaj">-->
                      <asp:Button ID="btnSave" runat="server" class="btn btn-unos btn-xl js-scroll-trigger pages2" Text="Sačuvaj" OnClick="btnSave_Click" />
                       <input type="button" onclick="reset()" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Poništi">
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="navbar.aspx">Odustani</a>
	             </div>
	          </div>
          </div>

            <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

                <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label> 

            </asp:Panel>
        </form>

  
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
