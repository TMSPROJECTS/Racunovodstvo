function btnSave()
{
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }

    var dataValue = "{prodokument: '" + dok + "', proImePrezime: '" + document.getElementById("inputImePrezime").value + "', proJMBG: '" + document.getElementById("inputJMBG").value + "',proMesto: '" + document.getElementById("inputMesto").value + "', proTelefon: '" + document.getElementById("inputTelefon").value + "', proFax: '" + document.getElementById("inputFax").value + "'}";

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