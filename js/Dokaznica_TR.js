function racuni() {

    
    var dataValue = "{prodokument: '" + document.getElementById("dobavljac").value + "'}";

    $.ajax({
        type: "POST",
        url: "PomocniPodaci_Dokaznica_Dodavanje.aspx/rcn",
        contentType: 'application/json; charset=utf-8',
        data: dataValue,
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            /*alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);*/
            alert("Greska!");
        },
        success: function (result) {
            if (result.d[0] == "N") {
                //document.getElementById("spObavestenje").innerHTML = result.d[1];
            }
            else {
                //$('#main').load('PomocniPodaci_Dokaznica_Dodavanje.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
                var rezultat = result.d[1];

                if (rezultat == "" || rezultat == null) {
                    document.getElementById("takuciRacun").options.length = 0;
                    var x = document.getElementById("takuciRacun");
                    var option = document.createElement("option");
                    option.text = "--Izaberite--";
                    x.add(option);

                    return;
                }
             
                   
                

                var res = rezultat.split("#");

                document.getElementById("takuciRacun").options.length = 0;

                var x = document.getElementById("takuciRacun");
                var option = document.createElement("option");
                option.text = "--Izaberite--";
                x.add(option);

                for (i = 0; i < res.length; i++) {
                    var x = document.getElementById("takuciRacun");
                    var option = document.createElement("option");
                    option.text = res[i];
                    x.add(option);
                }
                
            }
        }
    });
}