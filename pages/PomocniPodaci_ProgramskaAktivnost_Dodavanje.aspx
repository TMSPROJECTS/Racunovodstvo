<%@ Page  EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="PomocniPodaci_ProgramskaAktivnost_Dodavanje.aspx.cs" Inherits="pages_PomocniPodaci_ProgramskaAktivnost_Dodavanje" %>

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
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="PomocniPodaci_Program.aspx">Program</a></li>
        <li class="breadcrumb-item"><a class = "pages" href="#" page_name="PomocniPodaci_ProgramskaAktivnost.aspx?SIFRA12=<%Response.Write(Request.QueryString["SIFRA12"]);%>">Programska aktivnost</a></li>
        <li class="breadcrumb-item active">Unos</li>
      </ol>
        <form id="programskaAktivnostForma" action = "">

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
	             	 <input runat="server" class="form-control" id="program" name="program" disabled>
	             </div>
	             
	          </div>
          </div>
         
           <div class="row">
              <div class="labela col-md-4">
                 <label for="naziv">* Naziv</label>
              </div>
              <div class = "inputDiv col-md-8">
	              <div class="col-md-4">
	             	 <input runat ="server" class="form-control" id="naziv" name="naziv" required>
	             </div>
	             
	          </div>
          </div>

 <!------------------------ -->
            <div class="separatorDiv">Funkcionalna klasifikacija</div>
          <div class="row">
               <div class="labela col-md-4">
                 <label for="klasifikacija"></label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">

                      <table style="width:100%">
                          <tr style ="width:100%">
                              <td style="width:50%"><asp:Panel ID="panelFK1" runat="server" Width ="100%">


                            </asp:Panel></td>
                              <td style="width:50%"> <asp:Panel ID="panelFK2" runat="server" Width="100%">


                            </asp:Panel></td>

                          </tr>

                      </table>

                      <TABLE id="dataTableXXX" width="100%">
							<tr>
								
							</tr>
						</TABLE>
                        
		              <div class="col-md-4">
		             </div>
		              <div class="col-md-4">
			             
		             </div>
	             </div>
	          </div>
	          </div>
             <div class="row">
               <div class="labela col-md-4">
                 <label for="klasifikacija"></label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">

                        <table style="width:100%">
                          <tr style ="width:100%">
                              <td style="width:50%"><asp:Panel ID="pnlFKK1" runat="server" Width ="100%">

                                  <select runat ="server" name="funkcija" id="funkcija" class="form-control form-control-sm" style="width:90%" >
			             <option value = "" selected>-- Izaberite --</option></select>
                            </asp:Panel></td>
                              <td style="width:50%"> <asp:Panel ID="pnlFKK2" runat="server" Width="100%">

                                  <input type="button" class="btn btn-unos btn-xl js-scroll-trigger" value="Dodaj funkciju" onclick="addRow(dataTableXXX)"/>
                            </asp:Panel></td>

                          </tr>

                      </table>

                      

		              <div class="col-md-4">
		             	 
		             </div>
		              <div class="col-md-4">
		             	
		             </div>
	             </div>
	          </div>
	          </div>

       <!------------------------ -->       
	       <div class="separatorDiv">Izvori finansiranja</div>
	       
	       <div class="row">
               <div class="labela col-md-4">
                 <label for="izvoriFinansiranja"></label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">

                      <table style="width:100%">
                          <tr style ="width:100%">
                              <td style="width:50%"><asp:Panel ID="panelIF1" runat="server" Width ="100%">


                            </asp:Panel></td>
                              <td style="width:50%"> <asp:Panel ID="panelIF2" runat="server" Width="100%">


                            </asp:Panel></td>

                          </tr>

                      </table>

                      <TABLE id="dataTableXXX1" width="100%">
							<tr>
								
							</tr>
						</TABLE>

		              <div class="col-md-4">
		             	
		             </div>
		              <div class="col-md-4">
		             </div>
	             </div>
	          </div>
	          </div>
             <div class="row">
               <div class="labela col-md-4">
                 <label for="izvoriFinansiranja"></label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">

                      <table style="width:100%">
                          <tr style ="width:100%">
                              <td style="width:50%"><asp:Panel ID="pnlIFF1" runat="server" Width ="100%">

                                  <select runat ="server" name="izvoriFinansiranja" id="izvoriFinansiranja" class="form-control form-control-sm" style="width:90%" >
			             <option value = "" selected>-- Izaberite --</option></select>
                            </asp:Panel></td>
                              <td style="width:50%"> <asp:Panel ID="pnlIFF2" runat="server" Width="100%">

                                  <input type="button" class="btn btn-unos btn-xl js-scroll-trigger" value="Dodaj izvor" onclick="addRow2(dataTableXXX1)"/>
                            </asp:Panel></td>

                          </tr>

                      </table>

		              <div class="col-md-4">
		             	 
		             </div>
		              <div class="col-md-4">
		             	 
		             </div>
	             </div>
	          </div>
	          </div>
	          
	           <div class="separatorDiv">Grupa troškova</div>
	       
	       <div class="row">
               <div class="labela col-md-4">
                 <label for="grupaTroskova"></label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">

                      <table style="width:100%">
                          <tr style ="width:100%">
                              <td style="width:50%"><asp:Panel ID="panelGT1" runat="server" Width ="100%">


                            </asp:Panel></td>
                              <td style="width:50%"> <asp:Panel ID="panelGT2" runat="server" Width="100%">


                            </asp:Panel></td>

                          </tr>

                      </table>

                      <TABLE id="dataTableXXX2" width="100%">
							<tr>
								
							</tr>
						</TABLE>

		              <div class="col-md-4">
		             </div>
		              <div class="col-md-4">
			             
		             </div>
	             </div>
	          </div>
	          </div>
             <div class="row">
               <div class="labela col-md-4">
                 <label for="grupaTroskova"></label>
              </div>
              <div class = "inputDiv col-md-8">
              	<div class="inputRow row">


                      <table style="width:100%">
                          <tr style ="width:100%">
                              <td style="width:50%"><asp:Panel ID="panelGTT1" runat="server" Width ="100%">

                                  <select runat ="server" name="grupaTroskova" id="grupaTroskova" class="form-control form-control-sm" style="width:90%" >
			             <option value = "" selected>-- Izaberite --</option></select>
                            </asp:Panel></td>
                              <td style="width:50%"> <asp:Panel ID="panelGTT2" runat="server" Width="100%">

                                  <input type="button" class="btn btn-unos btn-xl js-scroll-trigger" value="Dodaj grupu" onclick="addRow3(dataTableXXX2)"/>
                            </asp:Panel></td>

                          </tr>

                      </table>


		              <div class="col-md-4">
		             	
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
	             	  <input type="button" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Sačuvaj" OnClick="btnSave();">
	             	 <!--<input type="submit" id="btnSave" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Sačuvaj">-->
	             	<!-- <input type="button" onclick="" class="btn btn-unos btn-xl js-scroll-trigger pages" value ="Brisanje"> -->
	             	 <input type="button" onclick="reset()" class="btn btn-unos btn-xl js-scroll-trigger pages2" value ="Poništi">
	             	 <a class="btn btn-unos btn-xl js-scroll-trigger pages" href="#" page_name="PomocniPodaci_ProgramskaAktivnost.aspx?SIFRA12=<%Response.Write(Request.QueryString["SIFRA12"]);%>">Odustani</a>
	             </div>
	          </div>
          </div>
       <asp:Panel ID="Panel2" runat="server" Width="100%" HorizontalAlign ="Center">

               <!-- <asp:Label ID="lblObavestenje" runat="server" ForeColor="Red"></asp:Label>  -->
                <span ID="spObavestenje" style="color:cadetblue; font-weight: bold;"></span> 

            </asp:Panel>
        </form>

        <script src="../js/ProgramskaAktivnost_Dodavanje.js"></script>
        <script src="../js/ProgramskaAktivnost_RemoveRow.js"></script>
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
    <script language="javascript">
        var y = 0;

        function addRow3(dataTableXXX2) {

            var table = document.getElementById("dataTableXXX2");
            var selektovano = document.getElementById("grupaTroskova");

            if (selektovano.value.trim() == "" || selektovano.value == "-- Izaberite --") {
                return;
            }

            var prodji = 1;
            //proveri da li postoji
            $("input[id^='CTBXXXY']").each(function () {

                if (this.value == selektovano.value) {
                    prodji = 0;

                }
            });

            $("input[id^='CtbxZY']").each(function () {

                if (this.value == selektovano.value) {
                    prodji = 0;
                }
            });

            if (prodji == 0) {
                return;
            }


            y++;

            var rowCount = table.rows.length;
            var row = table.insertRow(rowCount);


            var cell1 = row.insertCell(0);
            cell1.style.width = "50%";
            var element1 = document.createElement("input");
            element1.id = "CTBXXXY[" + y + "]";
            element1.name = "CTBXXXY[" + y + "]";
            element1.disabled = true;
            element1.className = "form-control";
            element1.value = selektovano.value;
            element1.style.width = "90%";
            cell1.appendChild(element1);


            var cell2 = row.insertCell(1);
            cell2.style.width = "50%";
            var element2 = document.createElement("input");
            element2.type = "button";
            element2.value = "Obriši";
            element2.className = "btn btn-unos obrisi btn-xl js-scroll-trigger";
            element2.id = "CBTNXXXY" + y;
            element2.name = "CBTNXXXY" + y;
            element2.onclick = function () {
                var eee = this;

                var brojac = eee.id.substring(8);
                eee.style.display = 'none';
                //eee.value = "obrisano";
                eee.id = "obrisano5" + eee.id;

                var iii = document.getElementById("CTBXXXY[" + brojac + "]")
                iii.value = "";
                iii.id = "obrisano6" + brojac;
                iii.style.display = 'none';
            };

            cell2.appendChild(element2);

            //document.getElementById(element2.id).setAttribute("onclick" , "parent().parent().deleteRow('"+ tableID +"')")


        }

  


        var z = 0;

        function addRow2(dataTableXXX1) {

            var table = document.getElementById("dataTableXXX1");
            var selektovano = document.getElementById("izvoriFinansiranja");

            if (selektovano.value.trim() == "" || selektovano.value == "-- Izaberite --") {
                return;
            }

            var prodji = 1;
            //proveri da li postoji
            $("input[id^='BTBXXXZ']").each(function () {

                if (this.value == selektovano.value) {
                    prodji = 0;

                }
            });

            $("input[id^='BtbxZ']").each(function () {

                if (this.value == selektovano.value) {
                    prodji = 0;
                }
            });

            if (prodji == 0) {
                return;
            }


           z++;

            var rowCount = table.rows.length;
            var row = table.insertRow(rowCount);


            var cell1 = row.insertCell(0);
            cell1.style.width = "50%";
            var element1 = document.createElement("input");
            element1.id = "BTBXXXZ[" + z + "]";
            element1.name = "BTBXXXZ[" + z + "]";
            element1.disabled = true;
            element1.className = "form-control";
            element1.value = selektovano.value;
            element1.style.width = "90%";
            cell1.appendChild(element1);


            var cell2 = row.insertCell(1);
            cell2.style.width = "50%";
            var element2 = document.createElement("input");
            element2.type = "button";
            element2.value = "Obriši";
            element2.className = "btn btn-unos obrisi btn-xl js-scroll-trigger";
            element2.id = "BBTNXXXZ" + z;
            element2.name = "BBTNXXXZ" + z;
            element2.onclick = function () {
                var eee = this;

                var brojac = eee.id.substring(8);
                eee.style.display = 'none';
                //eee.value = "obrisano";
                eee.id = "obrisano3" + eee.id;

                var iii = document.getElementById("BTBXXXZ[" + brojac + "]")
                iii.value = "";
                iii.id = "obrisano4" + brojac;
                iii.style.display = 'none';
            };
            cell2.appendChild(element2);

            //document.getElementById(element2.id).setAttribute("onclick" , "parent().parent().deleteRow('"+ tableID +"')")


        }



        var i = 0;
    	function addRow(dataTableXXX) {
			
			var table = document.getElementById("dataTableXXX");
			var selektovano = document.getElementById("funkcija");

			if (selektovano.value.trim() == "" || selektovano.value == "-- Izaberite --")
			{
			    return;
			}
			
			var prodji = 1;
    	    //proveri da li postoji
			$("input[id^='ATBXXX']").each(function () {

			    if (this.value == selektovano.value)
			    {
			        prodji = 0;
			      
			    }
			});

			$("input[id^='Atbx']").each(function () {
                
			    if (this.value == selektovano.value) {
			        prodji = 0;
			    }
			});

			if (prodji == 0)
			{
			    return;
			}


			i++;

			var rowCount = table.rows.length;
			var row = table.insertRow(rowCount);


			var cell1 = row.insertCell(0);
			cell1.style.width = "50%";
			var element1 = document.createElement("input");
			element1.id = "ATBXXX[" + i + "]";
			element1.name="ATBXXX[" + i + "]";
			element1.disabled = true;
			element1.className = "form-control";
			element1.value = selektovano.value;
			element1.style.width = "90%";
			cell1.appendChild(element1);


			var cell2 = row.insertCell(1);
			cell2.style.width = "50%";
			var element2 = document.createElement("input");
			element2.type = "button";
			element2.value = "Obriši";
			element2.className = "btn btn-unos obrisi btn-xl js-scroll-trigger";
			element2.id = "ABTNXXX" + i;
			element2.name = "ABTNXXX" + i;
			element2.onclick = function () {
			    var eee = this;

			    var brojac = eee.id.substring(7);
			    eee.style.display = 'none';
			    //eee.value = "obrisano";
			    eee.id = "obrisano" + eee.id;

			    var iii = document.getElementById("ATBXXX[" + brojac + "]")
			    iii.value = "";
			    iii.id = "obrisano2" + brojac;
			    iii.style.display = 'none';
			};

			//element2.addEventListener("onclick", deleteRow2("ABTNXXX" + i));
			cell2.appendChild(element2);

			//document.getElementById(element2.id).setAttribute("onclick" , "parent().parent().deleteRow('"+ tableID +"')")

			//document.getElementById("ABTNXXX" + i).onclick = deleteRow2("ABTNXXX" + i);

		}
		function deleteRow(tableID) {
			debugger;
			try {
			var table = document.getElementById(tableID);
			var rowCount = table.rows.length;

			for(var i=0; i<rowCount; i++) {
				var row = table.rows[i];
				var inputField = row.cells[0].childNodes[0];
				if(null != inputField) {
					table.deleteRow(i);
					rowCount--;
					i--;
				}


			}
			}catch(e) {
				alert(e);
			}
		}

		//function deleteRow2(ime) {
		//    try {
		        

		//        if (ime.substring(0, 4) == "Abtn") {


		//            alert("DAa");
		//        }
		//        else {

		//            alert("NEe");
		//        }

		    

		//        if (ime.substring(0, 7) == "ABTNXXX") {
		//            var broj = ime.substring(7, 8);
		//            var deo = ime.substring(0, 7);

		//            alert("DA");
		//        }
		//        else {

		//            alert("NE");
		//        }
		     

		       
		//    } catch (e) {
		//        alert(e);
		//    }
		//}

	
	</script>
     </form>
</body>


</html>
