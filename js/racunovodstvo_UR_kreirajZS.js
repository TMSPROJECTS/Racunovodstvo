function kreirajZS(dok)
{
    var dataValue = "{dokumenti: '";
    var brojac = 0;
    $('input:checkbox[id^="chk"]:checked').each(function () {
        dataValue += $(this).attr("id") + ',';
        brojac++;
    });
    dataValue += "'}";
    dataValue = dataValue.replace(",'}", "'}");
    if (brojac > 0)
    {
        if (confirm('Da li ste sigurni da želite da kreirate "Zahtev za sredstva" na osnovu selektovanih dokumenata?'))
        {
            $.ajax({
                type: "POST",
                url: "racunovodstvo_UR.aspx/kreirajZS",
                contentType: 'application/json; charset=utf-8',
                data: dataValue,
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    /*alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);*/
                    alert("Greska prilikom poziva upisa! Ukoliko se ponovlja stalno, kontaktirajte administratora!");
                },
                success: function (result) {
                    if (result.d[0] == "N") {
                        $('#main').load('racunovodstvo_UR.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
                    }
                    else {
                        $('#main').load('racunovodstvo_UR.aspx?spObavestenje=' + encodeURIComponent(result.d[1]));
                    }
                }
            });
        }
    }
    else
    {
        alert("Niste odabrali nijedan ulazni račun!");
        $('#main').load('racunovodstvo_UR.aspx');
    }    
}