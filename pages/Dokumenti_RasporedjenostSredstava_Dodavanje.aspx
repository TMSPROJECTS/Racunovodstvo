<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="Dokumenti_RasporedjenostSredstava_Dodavanje.aspx.cs" Inherits="pages_Dokumenti_RasporedjenostSredstava_Dodavanje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            -ms-flex: 0 0 33.333333%;
            flex: 0 0 33.333333%;
            max-width: 33.333333%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
</head>
<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form2" runat="server">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="Dokumenti_FinansijskiPlan.aspx">Finansijski plan</a></li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="Dokumenti_RasporedjenostSredstava.aspx">Raspoređenost planiranih sredstava</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="zahtevForma" action = "">
          <div class="row">
              <div class="labela col-md-4">
                 <label for="punNazivPlana">Plan</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="punNazivPlana" name="punNazivPlana" disabled>
	             </div>
	             
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="datumZahteva">* Zajednički naziv</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" type="text" class="form-control" id="datumZahteva" name="datumZahteva" onclick ="Izlistaj()" required>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="program">* Program</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="auto-style1">
                      <input runat ="server" type="text" class="form-control" id="program" name="program" disabled>
	             	
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="programskaAkt">* Programska aktivnost</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
                      <input runat ="server" type="text" class="form-control" id="programskaAkt" name="programskaAkt" disabled>
	             	
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="funkcija">* Funkcija</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
                      <input runat ="server" type="text" class="form-control" id="funkcija" name="funkcija" disabled>
	             	
	             	</select>
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="saveButton">
	             	<!-- <input id="prikaziDugme" type="submit" onclick = "" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Prikazi">-->
                      <!--<asp:Button ID="prikaziDugme" runat="server" class="btn btn-unos btn-xl js-scroll-trigger pages" Text="Sačuvaj" /> -->
	             </div>
	      
           </div>
	          </div>
          <div class="separatorDiv">Raspored sredstava</div>
          
           <div class="row">
              <div class="labela col-md-4">
                        <asp:Panel ID="Panel3" runat="server"></asp:Panel>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">
		              <div class="col-md-4">
                        <asp:Panel ID="Panel2" runat="server">
                    

                        </asp:Panel>
		             </div>
		              <div class="col-md-4">
		             </div>
	             </div>
	          </div>
           </div>
           
           <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="saveButton">
	             	<!--  <input type="submit" onclick = "" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Sačuvaj">-->
                        <input type="button" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Sačuvaj" OnClick="btnSave();">
                        <input type="button" onclick="reset()" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Poništi">
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="Dokumenti_FinansijskiPlan.aspx">Odustani</a>
	             </div>
	          </div>
          </div>
          <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">


                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

                            
            </asp:Panel>
        </form>

        <script src="../js/RasporedjenostSredstava_Dodavanje.js"></script>

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
