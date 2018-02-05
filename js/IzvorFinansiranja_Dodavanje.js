function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }


    var dataValue = "{prodokument: '" + dok + "', proSifra: '" + document.getElementById("sifra").value + "', proNaziv: '" + document.getElementById("naziv").value + "'}";

    $.ajax({
        type: "POST",
        url: "PomocniPodaci_IzvorFinansiranja_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('PomocniPodaci_IzvorFinansiranja.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}