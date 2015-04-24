function AleatorioNormalCuenta() {
    var min = 0, max = 0, num;
    var numAl = null;
    for (i = 0; i > 10; i++) {
        num = Math.round(Math.random() * (max - min + 1)) + min;
        numAl = numAl + num.toString(); 
    }

    var retornar = document.getElementById("AlNum").value = num;

}