function SacuvajPoslovnePartnere() {
    var dok = "";
    if (document.getElementById("inputImePrezime")) {
        dok = document.getElementById("inputImePrezime").value;
    }

    var dataValue = "{prodokument: '" + dok + "'}";

    $.ajax({
        type: "POST",
        url: "Komitenti_poslovniPartneri_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('Komitenti_poslovniPartneri.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}