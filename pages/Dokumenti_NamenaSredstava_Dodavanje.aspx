<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="Dokumenti_NamenaSredstava_Dodavanje.aspx.cs" Inherits="pages_Dokumenti_NamenaSredstava_Dodavanje" %>

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
     	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="zahtevZaSredstva.html">Zahtev za sredstva</a></li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="namenaSredstava.html">Namena sredstava</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="namenaSredstavaForma" action = "">
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
                 <label for="program">Program</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="program" id="program" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
	      </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="programskaAkt">Programska aktivnost</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="programskaAkt" id="programskaAkt" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
	      </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="projekat">Projekat</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="projekat" id="projekat" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
	      </div>	          
          <div class="row">
              <div class="labela col-md-4">
                 <label for="funkcija">Funkcija</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="funkcija" id="funkcija" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
	      </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="izvorFin">Izvor finansiranja</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="izvorFin" id="izvorFin" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="namena">* Namena</label>
              </div>
              <div class = "inputDiv col-md-8">
		              <div class="col-md-4">
		             	 <input runat="server" class="form-control" id="namena" name="namena" required>
		             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="iznos">* Iznos</label>
              </div>
              <div class = "inputDiv col-md-8">
		              <div class="col-md-4">
		             	 <input runat="server" class="form-control" id="iznos" name="iznos" required>
		             </div>
	          </div>
          </div>
          
           <div class="separatorDiv">Pravno lice</div>
           
           <div class="row">
              <div class="labela col-md-4">
                 <label for="sifDobavljaca">Dobavljač</label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">
		              <div class="col-md-4">
		             	 <input runat="server" class="form-control" id="sifDobavljaca" name="sifDobavljaca">
		             </div>
		              
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="tekRacunP">Tekući račun</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="tekRacunP" id="tekRacunP" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="brFakture">Broj fakture dobavljača</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	<input runat="server" class="form-control" id="brFakture" name="brFakture" required>
	             </div>
	          </div>
          </div>
          
          <div class="separatorDiv">Fizičko lice</div>
          
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="listaFizLica" id="listaFizLica" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="punoIme">Ime i prezime</label>
              </div>
              <div class = "inputDiv col-md-8">
		              <div class="col-md-4">
		             	 <input runat="server" class="form-control" id="punoIme" name="punoIme">
		             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="adresa">Adresa</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" class="form-control" id="adresa" name="adresa">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="mesto">Mesto</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" class="form-control" id="mesto" name="mesto">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="tekRačunF">Tekući račun</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" class="form-control" id="tekRačunF" name="tekRačunF">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="pozivNaBr">Poziv na broj</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" class="form-control" id="pozivNaBr" name="pozivNaBr">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select runat="server" name="listaAdvokata" id="listaAdvokata" class="form-control form-control-sm">
	             	 <option value = "" selected>-- Izaberite --</option>
	             	</select>
	             </div>
	          </div>
          </div>
	      <div class="row">
              <div class="labela col-md-4">
                 <label for="advokat">Advokat</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" class="form-control" id="advokat" name="advokat">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="saveButton">
                     <!-- <input type="submit" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Sačuvaj">-->
                      <input type="button" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Sačuvaj" OnClick="btnSave();">
	             	 <input type="button" onclick="reset()" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Poništi">
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="Dokumenti_ZahtevZaSredstva.aspx">Odustani</a>
	             </div>
	          </div>
          </div>
<asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

               <!-- <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label>  -->
                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>
        </form>

        <script src="../js/NamenaSredstava_Dodavanje.js"></script>
       

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