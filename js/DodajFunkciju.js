function btnSaveDF() {
    var dok = "";
    if (document.getElementById("funkcija")) {
        dok = document.getElementById("funkcija").value;
    }

    var dataValue = "{prodokument: '" + dok + "'}";

    $.ajax({
        type: "POST",
        url: "PomocniPodaci_ProgramskaAktivnost_Dodavanje.aspx/sacuvajDF",
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
                document.getElementById("spObavestenje").innerHTML = result.d[1];
            }
        }
    });
}