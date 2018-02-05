function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }

    var dataValue = "{prodokument: '" + dok + "', proInputKlasifikacija: '" + document.getElementById("inputKlasifikacija").value + "', proInputNazivKlasifikacije: '" + document.getElementById("inputNazivKlasifikacije").value + "'}";

    $.ajax({
        type: "POST",
        url: "Komitenti_poslovniPartneri_KlasifikacijaAsortimanaDobavljaca_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('Komitenti_poslovniPartneri_KlasifikacijaAsortimanaDobavljaca.aspx?SIFRA=' + result.d[2] + '&spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}