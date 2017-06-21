$(function () {

    cargarTabla();
});


function cargarTabla() {
    var codigoPadre = $("#padreId").html();
    $.get("/ClienteHijo/GetHijosPadre", { id: codigoPadre },
   function resultado(result) {
       var $table = $('#TablaHijos');
       $table.bootstrapTable({
           data: result.Registro,
       });
       $table.on('click-row.bs.table', function (e, row, $element) { ActualizarMapaRuta(row.Codigo) });
   })
}

function ActualizarMapaRuta(codigoHijo) {
    var lat;
    var lng;
    $.get("/PadreRuta/GetClienteRuta/", { id: codigoHijo },
        function (result) {
            if (result.Resultado) {
                lat = result.Latitud;
                lng = result.Longitud;
                calcRoute(lat, lng);
            }
        });
}