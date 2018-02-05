function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }


    var dataValue = "{prodokument: '" + dok + "', proBrojZahteva: '" + document.getElementById("brojZahteva").value + "', proDatumZahteva: '" + document.getElementById("datumZahteva").value + "',proRacun: '" + document.getElementById("racun").value + "', proUgovorDob: '" + document.getElementById("ugovorDob").value + "', proNapomena: '" + document.getElementById("napomena").value + "'}";

    $.ajax({
        type: "POST",
        url: "Dokumenti_ZahtevZaSredstva_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('Dokumenti_ZahtevZaSredstva.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}