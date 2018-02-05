function promeniPAK(program)
{

    var dataValue = "{program: '" + program + "'}";

    $.ajax({
        type: "POST",
        url: "racunovodstvo_UR_Dodavanje.aspx/promeniPAK",
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
            document.getElementById("selPogramAkt").innerHTML = "";
            document.getElementById("selPogramAkt").innerHTML = result.d;
        }
    });
}