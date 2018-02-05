function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }


    var dataValue = "{prodokument: '" + dok + "', proSifraPrograma: '" + document.getElementById("sifraPr").value + "', proNaziv: '" + document.getElementById("naziv").value + "',proOdgovornoLice: '" + document.getElementById("odgLice").value + "', proSvrha: '" + document.getElementById("svrha").value + "', proOpis: '" + document.getElementById("opis").value + "'}";

    $.ajax({
        type: "POST",
        url: "PomocniPodaci_Program_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('PomocniPodaci_Program.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}