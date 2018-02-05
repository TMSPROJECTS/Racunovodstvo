function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }


    var dataValue = "{prodokument: '" + dok + "', proKonto: '" + document.getElementById("konto").value + "', proNamena: '" + document.getElementById("namena").value + "',proDobavljac: '" + document.getElementById("dobavljac").value + "', proTekuciRacun: '" + document.getElementById("takuciRacun").value + "', proSifPlacanja: '" + document.getElementById("sifPlacanja").value + "', proPozivNaBr: '" + document.getElementById("pozivNaBr").value + "', proVaziOd: '" + document.getElementById("vaziOd").value + "', proVaziDo: '" + document.getElementById("vaziDo").value + "', proIznos: '" + document.getElementById("iznos").value + "'}";

    $.ajax({
        type: "POST",
        url: "PomocniPodaci_Dokaznica_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('PomocniPodaci_Dokaznica.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}