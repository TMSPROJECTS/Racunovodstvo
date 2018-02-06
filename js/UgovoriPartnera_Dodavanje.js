function btnSave() {
    var dok = ""; 
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }

    var dataValue = "{prodokument: '" + dok + "', proDDLkorisnik: '" + document.getElementById("ddlKorisnik").value + "', proBrUgovora: '" + document.getElementById("brUgovora").value + "',proDatumOd: '" + document.getElementById("datumOd").value + "', proDatumDo: '" + document.getElementById("datumDo").value + "', proIznosUgovora: '" + document.getElementById("iznosUgovora").value + "', proPreostaliIznos: '" + document.getElementById("preostaliIznos").value + "' , proOpis: '" + document.getElementById("opis").value + "'}";

    $.ajax({
        type: "POST",
        url: "Komitenti_ugovoriPartnera_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('Komitenti_ugovoriPartnera.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}