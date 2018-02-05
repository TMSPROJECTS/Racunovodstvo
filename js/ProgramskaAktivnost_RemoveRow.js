function remove(asd) {
    var dok = asd;

    var dugme = document.getElementById(asd);
    


    var brojac = dugme.id.substring(4);
    dugme.style.display = 'none';
    dugme.id = "obrisano10" + dugme.id;

    var iii = document.getElementById("Atbx" + brojac + "")
    iii.value = "";
    iii.id = "obrisano11" + brojac;
    iii.style.display = 'none';

}

function remove2(asd) {
    var dok = asd;

    var dugme = document.getElementById(asd);



    var brojac = dugme.id.substring(5);
    dugme.style.display = 'none';
    dugme.id = "obrisano12" + dugme.id;

    var iii = document.getElementById("BtbxZ" + brojac + "")
    iii.value = "";
    iii.id = "obrisano13" + brojac;
    iii.style.display = 'none';

}

function remove3(asd) {
    var dok = asd;

    var dugme = document.getElementById(asd);



    var brojac = dugme.id.substring(6);
    dugme.style.display = 'none';
    dugme.id = "obrisano14" + dugme.id;

    var iii = document.getElementById("CtbxZY" + brojac + "")
    iii.value = "";
    iii.id = "obrisano15" + brojac;
    iii.style.display = 'none';

}