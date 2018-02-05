function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }


    var dataValue = "{prodokument: '" + dok + "', proProgram: '" + document.getElementById("program").value + "', proProgramskaAkt: '" + document.getElementById("programskaAkt").value + "',proProjekat: '" + document.getElementById("projekat").value + "', proFunkcija: '" + document.getElementById("funkcija").value + "', proIzvorFin: '" + document.getElementById("izvorFin").value + "', proNamena: '" + document.getElementById("namena").value + "', proIznos: '" + document.getElementById("iznos").value + "', proSifDob: '" + document.getElementById("sifDobavljaca").value + "', proTR: '" + document.getElementById("tekRacunP").value + "', proBrFak: '" + document.getElementById("brFakture").value + "'}";

    $.ajax({
        type: "POST",
        url: "Dokumenti_NamenaSredstava_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('Dokumenti_NamenaSredstava.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}