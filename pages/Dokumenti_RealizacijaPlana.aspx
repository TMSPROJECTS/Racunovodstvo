<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dokumenti_RealizacijaPlana.aspx.cs" Inherits="Dokumenti_RealizacijaPlana" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
</head>

   
<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form1" runat="server">
      <ol class="breadcrumb">
     	<li class="breadcrumb-item active">Budžet</li>
        <li class="breadcrumb-item active">Dokumenti</li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="finansijskiPlan.html">Finansijski plan</a></li>
        <li class="breadcrumb-item active">Realizacija plana</li>
      </ol>


           <div class="row">
              <div class="labela col-md-4">
                 <label for="datumOd">Za period od:</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input type="date" class="form-control" id="datumOd" name="datumOd">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="datumDo">do:</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input type="date" class="form-control" id="datumDo" name="datumDo">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="konto">Konto</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	  <input runat="server" class="form-control" id="konto" name="konto">
	             </div>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
                 <label for="program">Program</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select name="program" runat="server" id="program" class="form-control form-control-sm" onchange="prikazipa();">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>


            <div class="row">
              <div class="labela col-md-4">
                 <label for="programskaAktivnost">Programska aktivnost</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select name="programskaAktivnost" runat="server" id="programskaAktivnost" class="form-control form-control-sm" onchange="prikazifk();">
	             	
	             	</select>
	             </div>
	          </div>
          </div>

            <div class="row">
              <div class="labela col-md-4">
                 <label for="funkcionalnaKlasifikacija">Funkcionalna klasifikacija</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <select name="funkcionalnaKlasifikacija" runat="server" id="funkcionalnaKlasifikacija" class="form-control form-control-sm">
	             	 
	             	</select>
	             </div>
	          </div>
          </div>

          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	             <input runat="server" type="checkbox" id="check" name="planiranaKonta">&nbsp&nbsp <b>Prikaži konta za koja postoje planirana sredstva</b><br>
	          </div>
          </div>
          <div class="row">
              <div class="labela col-md-4">
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="saveButton">
	             	  <input type="button" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Prikaži" OnClick="prikaziT();">
	             	  <input type="button" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Kreiraj PDF" OnClick="kreirajPDF();">
	             	 <input onclick = "" class="btn btn-unos btn-xl js-scroll-trigger pages realizacija" value ="Export XLS">
	             </div>
	          </div>
          </div>
          
          <div class="separatorDiv"><h3>Realizacija plana</h3>
          <p><span name="idFinPlana">idPlana</span> - <span name="nazivFinPlana">nazivPlana</span></p></div>
          
       <div id="tabelaRealizacijaPlana" class="card mb-3">
	       <div class="card-header">
	          <div><p><b>Program: </b><span name="program"></span></p></div>
	          <div><p><b>Programska aktivnost: </b><span name="programskaAkt"></span></p></div>
	          <div><p><b>Funkcionalna klasifikacija: </b><span name="funKlasifikacija"></span></p></div>
	        </div>
	     <div class="card-body">
	          <div class="table-responsive">
	            <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
	              <thead>
	                <tr>
	                  <th>Konto</th>
	                  <th>Naziv</th>
	                  <th>Planirano</th>
	                  <th>Plaćeno</th>
	                  <th>Na čekanju</th>
	                  <th>Ukupno</th>
	                  <th>%</th>
	              </thead>
	              <tfoot>
	                <tr>
              		  <th>Konto</th>
	                  <th>Naziv</th>
	                  <th>Planirano</th>
	                  <th>Plaćeno</th>
	                  <th>Na čekanju</th>
	                  <th>Ukupno</th>
	                  <th>%</th>
	                </tr>
	              </tfoot>
	              <tbody>
	                <tr>
		             <td>111111</td>
	                 <td>blablabla</td>
	                 <td>2000.00</td>
	                 <td>100.00</td>
	                 <td>1900.00</td>
	                 <td>3900.00</td>
	                 <td>5%</td>
	                </tr>
	                
	              </tbody>
	            </table>
	          </div>
	        </div>
      	</div>
<asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign ="Center">
        <input runat="server" id="Text1" type="hidden"   />
               <!-- <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label>  -->
                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 
            </asp:Panel>
        </form>

        <script src="../js/RealizacijaPlana_PDF.js"></script>
        <script src="../js/RealizacijaPlana_PA.js"></script>
        <script src="../js/RealizacijaPlana_FK.js"></script>
        <script src="../js/RealizacijaPlana_Prikaz.js"></script>
       

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
