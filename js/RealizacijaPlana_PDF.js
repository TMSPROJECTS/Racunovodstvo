function kreirajPDF() {
    var dok = document.getElementById("Text1").value;
    var prog = document.getElementById("program").value;
    var progAkt = document.getElementById("programskaAktivnost").value;
    var funkKlas = document.getElementById("funkcionalnaKlasifikacija").value;
    var konto = document.getElementById("konto").value;
    var cekirano = "ne";

  
  
    var dataValue = "{prodokument: '" + dok + "',proProgram: '" + prog + "',proProgramskaAktivnost: '" + progAkt + "',proFunkcionalnaKlasifikacija: '" + funkKlas + "',proKonto: '" + konto + "',proCekirano: '" + cekirano + "'}";

    $.ajax({
        type: "POST",
        url: "Dokumenti_RealizacijaPlana.aspx/sacuvaj",
        contentType: 'application/json; charset=utf-8',
        data: dataValue,
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            /*alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);*/
            alert("Greska prilikom poziva upisa! Ukoliko se ponovlja stalno, kontaktirajte administratora!");
        },
        success: function (result) {
            if (result.d[0] == "N") {
                document.getElementById("spObavestenje").innerHTML = result.d[1];
            }
            else {
                $('#main').load('Dokumenti_RealizacijaPlana.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}