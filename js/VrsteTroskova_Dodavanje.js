function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }

    
    var dataValue = "{prodokument: '" + dok + "', proNaziv: '" + document.getElementById("naziv").value + "', proJedinicaMere: '" + document.getElementById("jedinicaMere").value + "',proGrupa: '" + document.getElementById("grupa").value + "', proKonto: '" + document.getElementById("konto").value + "', proSifraPlacanja: '" + document.getElementById("sifraPlacanja").value + "', proPozivNaBroj: '" + document.getElementById("pozivNaBroj").value + "'}";

    $.ajax({
        type: "POST",
        url: "PomocniPodaci_VrsteTroskova_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('PomocniPodaci_VrsteTroskova.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}