function promeniTK(dobavljac)
{

    var dataValue = "{dobavljac: '" + dobavljac + "'}";

    $.ajax({
        type: "POST",
        url: "racunovodstvo_UR_Dodavanje.aspx/promeniTK",
        contentType: 'application/json; charset=utf-8',
        data: dataValue,
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown)
        {
            /*alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);*/
            alert("Greska prilikom generisanja povezanih tekucih racuna za odabranog dobavljaca! Ukoliko se ponavlja stalno, kontaktirajte administratora!");
        },
        success: function (result)
        {
            document.getElementById("tekr").innerHTML = "";
            document.getElementById("tekr").innerHTML = result.d[0];
            document.getElementById("ugovorDob").innerHTML = "";
            document.getElementById("ugovorDob").innerHTML = result.d[1];
        }
    });
}