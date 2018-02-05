function prikazifk() {


    var dataValue = "{prodokument: '" + document.getElementById("program").value + "',prodokument2: '" + document.getElementById("programskaAktivnost").value + "'}";

    $.ajax({
        type: "POST",
        url: "Dokumenti_RealizacijaPlana.aspx/izlistajFK",
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
                 alert ( result.d[1]);
               

                document.getElementById("funkcionalnaKlasifikacija").options.length = 0;
                var x = document.getElementById("funkcionalnaKlasifikacija");
                var option = document.createElement("option");
                option.text = "Sve funkcionalne klasifikacije";
                x.add(option);
            }
            else {
                alert(result.d[1]);
                //$('#main').load('PomocniPodaci_Dokaznica_Dodavanje.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
                var rezultat = result.d[1];

                document.getElementById("spObavestenje").value = rezultat;

                if (rezultat == "" || rezultat == null) {
                    

                    document.getElementById("funkcionalnaKlasifikacija").options.length = 0;
                    var x = document.getElementById("funkcionalnaKlasifikacija");
                    var option = document.createElement("option");
                    option.text = "Sve funkcionalne klasifikacije";
                    x.add(option);

                    return;
                }




                var res = rezultat.split("#");

                document.getElementById("funkcionalnaKlasifikacija").options.length = 0;



                document.getElementById("funkcionalnaKlasifikacija").options.length = 0;
                var x = document.getElementById("funkcionalnaKlasifikacija");
                var option = document.createElement("option");
                option.text = "Sve funkcionalne klasifikacije";
                x.add(option);

                for (i = 0; i < res.length; i++) {
                    var x = document.getElementById("funkcionalnaKlasifikacija");
                    var option = document.createElement("option");
                    option.text = res[i];
                    x.add(option);
                }

            }
        }
    });
}