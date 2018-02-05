function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }

    var dataValue = "{prodokument: '" + dok + "', proDatum: '" + document.getElementById("datum").value + "', proValuta: '" + document.getElementById("valuta").value + "',proProgram: '" + document.getElementById("selProgram").value + "', proProgramskAktivnost: '" + document.getElementById("selPogramAkt").value + "', proFunkcija: '" + document.getElementById("selFunkcija").value + "', proIzvorF: '" + document.getElementById("selIzvorF").value + "', proPartner: '" + document.getElementById("selDob").value + "', proBrojF: '" + document.getElementById("brojFakture").value + "', proUgovor: '" + document.getElementById("ugovorDob").value + "', proOpis: '" + document.getElementById("napomena").value + "'}";

    $.ajax({
        type: "POST",
        url: "racunovodstvo_UR_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('racunovodstvo_UR.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}