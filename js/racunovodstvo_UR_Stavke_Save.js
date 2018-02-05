function btnSave()
{
    var id = 0;
    if (document.getElementById("idStavke"))
    {
        id = document.getElementById("idStavke").value;
    }

    var dataValue = "{proID: '" + id + "',prodokument: '" + document.getElementById("dokument").value + "', proTrosak: '" + document.getElementById("selTrosak").value + "', proIznosBP: '" + document.getElementById("iznosBP").value + "',proStopa: '" + document.getElementById("selStopa").value + "', proIznos: '" + document.getElementById("iznos").value + "', proPlaceno: '" + document.getElementById("placeno").value + "', proKonto: '" + document.getElementById("selKonto").value + "', proOpis: '" + document.getElementById("opis").value + "'}";

    $.ajax({
        type: "POST",
        url: "racunovodstvo_URStavkeDodavanje.aspx/sacuvaj",
        contentType: 'application/json; charset=utf-8',
        data: dataValue,
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown)
        {
            /*alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);*/
            alert("Greska prilikom poziva upisa! Ukoliko se ponovlja stalno, kontaktirajte administratora!");
        },
        success: function (result)
        {
            if (result.d[0] == "N")
            {
                document.getElementById("spObavestenje").innerHTML = result.d[1];
            }
            else
            {
                $('#main').load("racunovodstvo_URStavke.aspx?SIFRA=" + document.getElementById("dokument").value + "&spObavestenje=" + encodeURIComponent(result.d[1]));
            }
        }
    });
}