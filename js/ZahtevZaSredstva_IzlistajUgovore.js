function izlistajUgovor() {
    var dok = "";
    if (document.getElementById("racun").value == "") {
        document.getElementById("ugovorDob").options.length = 0;

        var x = document.getElementById("ugovorDob");
        var option = document.createElement("option");
        option.text = "--Izaberite--";
        x.add(option);
        return;
    }
    else {
        dok = document.getElementById("racun").value;
    }


    var dataValue = "{prodokument: '" + dok + "'}";

    $.ajax({
        type: "POST",
        url: "Dokumenti_ZahtevZaSredstva_Dodavanje.aspx/s2",
        contentType: 'application/json; charset=utf-8',
        data: dataValue,
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            /*alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);*/
            alert("Greška!");
        },
        success: function (result) {
            if (result.d[0] == "N") {
               
            }
            else {
                var rez = result.d[1];

                if (rez == "") {
                    document.getElementById("ugovorDob").options.length = 0;

                    var x = document.getElementById("ugovorDob");
                    var option = document.createElement("option");
                    option.text = "--Izaberite--";
                    x.add(option);
                }
                else {
                    document.getElementById("ugovorDob").options.length = 0;

                    var x = document.getElementById("ugovorDob");
                    var option = document.createElement("option");
                    option.text = "--Izaberite--";
                    x.add(option);
                    var x = document.getElementById("ugovorDob");
                    var option = document.createElement("option");
                    option.text = rez;
                    x.add(option);
                    document.getElementById("ugovorDob").selectedIndex = "1";
                }

               

            }
        }
    });
}