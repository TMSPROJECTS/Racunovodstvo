<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="Komitenti_ugovoriPartnera_Dodavanje.aspx.cs" Inherits="pages_Komitenti_ugovoriPartnera_Dodavanje" %>

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
        <li class="breadcrumb-item active">Osnovni podaci</li>
        <li class="breadcrumb-item active">Ugovori</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="Komitenti_ugovoriPartnera.aspx">Ugovori partnera</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form action = "">
         <!-- <div class="row">
              <div class="labela col-md-4">
                 <label for="oznaka">Oznaka</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input class="form-control" id="oznaka" name="oznaka" disabled>
	             </div>
	             
	          </div>
          </div>-->

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
                 <label for="korisnik">* Korisnik</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select name="ddlKorisnik" id="ddlKorisnik" class="form-control form-control-sm select2" runat ="server" required>
	             	</select>
	             </div>
	             
	          </div>
          </div>

             
         <!--  <div class="row">
              <div class="labela col-md-4">
                 <label for="sifPartnera">Partner</label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">
		              <div class="col-md-4">
		             	 <input class="form-control" id="sifPartnera" name="sifPartnera">
		             </div>
		              <div class="col-md-4">
		             	 <input class="form-control" id="punoImePartnera" name="punoImePartnera">
		             </div>
	             </div>
	          </div>
          </div>-->
          <div class="row">
              <div class="labela col-md-4">
                 <label for="brUgovora">* Broj ugovora</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" class="form-control" id="brUgovora" name="brUgovora">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="datumOd">* Datum ugovora</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="date" class="form-control" id="datumOd" name="datumOd">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="datumDo">* Važi do</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="date" class="form-control" id="datumDo" name="datumDo">
	             </div>
	          </div>
          </div>
          
          <div class="separatorDiv">Finansijski podaci ugovora</div>
          
          <div class="row">
              <div class="labela col-md-4">
                 <label for="iznosUgovora">* Iznos ugovora</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="text" class="form-control" id="iznosUgovora" style="text-align:right" name="iznosUgovora">
	             </div>
	          </div>
          </div>
         <!-- <div class="row">
              <div class="labela col-md-4">
                 <label for="ucesce">Iznos učešća</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="number" class="form-control" id="ucesce" name="ucesce">
	             </div>
	          </div>
          </div> -->
          
          <div class="row">
              <div class="labela col-md-4">
                 <label for="preostaliIznos">Preostali iznos ugovora</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat="server" type="text" class="currency form-control" id="preostaliIznos" style="text-align:right" name="preostaliIznos">
	             </div>
	          </div>
          </div>
          
          <div class="separatorDiv">Ostali podaci</div>
          
          <div class="row">
              <div class="labela col-md-4">
                 <label for="opis">Opis</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <textarea runat ="server" rows="5" cols="100" class="form-control" id="opis" name="opis"></textarea>
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
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="Komitenti_ugovoriPartnera.aspx?SPECIFICNO=ne">Odustani</a>
	             </div>
	          </div>
          </div>

               <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">

               <!-- <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label>  -->
                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>
        </form>

        <script src="../js/UgovoriPartnera_Dodavanje.js"></script>

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
