function storno(dok) {
    if (confirm('Da li ste sigurni da želite da stornirate dokument ' + dok + '?')) {
        var dataValue = "{dokument: '" + dok + "'}";

        $.ajax({
            type: "POST",
            url: "racunovodstvo_UR.aspx/storno",
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
    else {

    }

}