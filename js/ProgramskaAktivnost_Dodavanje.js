function btnSave() {
    var dok = "";
    if (document.getElementById("dokument")) {
        dok = document.getElementById("dokument").value;
    }

    var dok2 = document.getElementById("program").value;
    var dok3 = document.getElementById("naziv").value;

    var vrednosttbx = " ";
    var vrednostTBXXX = " ";
    var vrednosttbxZ = " ";
    var vrednostTBXXXZ = " ";
    var vrednosttbxZY = " ";
    var vrednostTBXXXY = " ";

    $("input[id^='Atbx']").each(function () {

        vrednosttbx = vrednosttbx + "#" + this.value;
    });

    $("input[id^='ATBXXX']").each(function () {

        vrednostTBXXX = vrednostTBXXX + "#" + this.value;
    });

    $("input[id^='BtbxZ']").each(function () {

        vrednosttbxZ = vrednosttbxZ + "#" + this.value;
    });

    $("input[id^='BTBXXXZ']").each(function () {

        vrednostTBXXXZ = vrednostTBXXXZ + "#" + this.value;
    });
    
    $("input[id^='CtbxZY']").each(function () {

        vrednosttbxZY = vrednosttbxZY + "#" + this.value;
    });
    
    $("input[id^='CTBXXXY']").each(function () {

        vrednostTBXXXY = vrednostTBXXXY + "#" + this.value;
    });

    var dataValue = "{prodokument: '" + dok + "', pro1: '" + vrednosttbx + "', pro2: '" + vrednostTBXXX + "', pro3: '" + vrednosttbxZ + "', pro4: '" + vrednostTBXXXZ + "', pro5: '" + vrednosttbxZY + "', pro6: '" + vrednostTBXXXY + "', MM: '" + dok2 + "', NN: '" + dok3 + "'}";

    $.ajax({
        type: "POST",
        url: "PomocniPodaci_ProgramskaAktivnost_Dodavanje.aspx/sacuvaj",
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
                $('#main').load('PomocniPodaci_ProgramskaAktivnost.aspx?SIFRA12=' + result.d[2] + '&spObavestenje=' + encodeURIComponent(result.d[1]));
            }
        }
    });
}