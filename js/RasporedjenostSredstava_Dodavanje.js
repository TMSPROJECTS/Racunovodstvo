function btnSave() {
    var dok = "1";
    //if (document.getElementById("dokument")) {
    //    dok = document.getElementById("dokument").value;
    //}

    var vrednostt = " ";
    var vrednostl = " ";

    $("input[id^='tbx']").each(function () {

        vrednostt = vrednostt + "#" + this.value;
    });

    $("input[id^='lblSV']").each(function () {

        vrednostl = vrednostl + "#" + this.value;// span this.innerHTML;
    });

    alert(vrednostl);

    var dataValue = "{prodokument: '" + dok + "', pro: '" + vrednostt + "', proSV: '" + vrednostl + "', proFP: '" + document.getElementById("punNazivPlana").value + "'}";

    $.ajax({
        type: "POST",
        url: "Dokumenti_RasporedjenostSredstava_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('Dokumenti_RasporedjenostSredstava.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}