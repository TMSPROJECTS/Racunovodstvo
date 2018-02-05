function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }


    var dataValue = "{prodokument: '" + dok + "', proInputGodina: '" + document.getElementById("godina").value + "', proInputNaziv: '" + document.getElementById("inputNaziv").value + "', proDatumZahteva: '" + document.getElementById("datumZahteva").value + "'}";

    $.ajax({
        type: "POST",
        url: "Dokumenti_FinansijskiPlan_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('Dokumenti_FinansijskiPlan.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}