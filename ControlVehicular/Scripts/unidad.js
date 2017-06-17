$(document).ready(function () {

    cargarTabla();
});

function cargarTabla() {
    $.get("/Unidad/ObtenerTodas",
       function resultado(result) {

           var $table = $('#tablaUnidad');


           $table.bootstrapTable({
               data: result.registro,
           });

           $table.on('click-row.bs.table', function (e, row, $element) { Elemento(row.Codigo) });


       })
}

function Elemento(cod) {
    $.get("/Unidad/Elemento", { codigo: cod },
        function resultado(data) {
            $("#formEditarUnidad #Codigo").val(cod);
            $("#formEditarUnidad #Marca").val(data.Unidad.Marca)
            $("#formEditarUnidad #Placa").val(data.Unidad.Placa)
            $("#formEditarUnidad #Modelo").val(data.Unidad.Modelo)
            $("#formEditarUnidad #Anno").val(data.Unidad.Anno)
            $("#formEditarUnidad #UltimoAnnoRevision").val(data.Unidad.UltimoAnnoRevision)
            $("#formEditarUnidad #Capacidad").val(data.Unidad.Capacidad)
            $("#formEditarUnidad #Anno").val(data.Unidad.Anno)
            $("#formEditarUnidad #URLFotografiaUnidad").val(data.Unidad.URLFotografiaUnidad)
            $("#formEditarUnidad #URLRevisionTecnica").val(data.Unidad.URLRevisionTecnica)
            $("#formEditarUnidad #URLTarjetaCirculacion").val(data.Unidad.URLTarjetaCirculacion)
            $("#modalEditarUnidad").modal("show");
        })
}

let nuevaUnidad = () => {
    $.post("/Unidad/Agregar",
        $("#formAgregarUnidad").serialize(),
        (data) => {
            $("#modalAgregarUnidad").modal("hide")
            alert(data.Unidad.Codigo)
        })
}

let editarUnidad= () => {

    var codigo = $("#formEditarUnidad #Codigo").val();
    $.post("/Unidad/Editar",
        $("#formEditarUnidad").serialize(),
        function resultado(data) {
            var $table = $('#tablaUnidad');
            $table.bootstrapTable('updateByUniqueId', {
                id: codigo,
                row: data.Unidad
            });

            $("#formEditarUnidad #Marca").val("")
            $("#formEditarUnidad #Placa").val("")
            $("#formEditarUnidad #Modelo").val("")
            $("#formEditarUnidad #Anno").val("")
            $("#formEditarUnidad #UltimoAnnoRevision").val("")
            $("#formEditarUnidad #Capacidad").val("")
            $("#formEditarUnidad #Anno").val("")
            $("#formEditarUnidad #URLFotografiaUnidad").val("")
            $("#formEditarUnidad #URLRevisionTecnica").val("")
            $("#formEditarUnidad #URLTarjetaCirculacion").val("")
            $("#modalEditarUnidad").modal("hide");


        })
}

function Borrar() {
    var codigo = $("#formEditarUnidad #Codigo").val();
    $.post("/Unidad/Eliminar",
        { codigo: codigo },
        function resultado(data) {
            var $table = $('#tablaUnidad');
            $table.bootstrapTable('removeByUniqueId', codigo);
            $("#formEditarUnidad #Marca").val("")
            $("#formEditarUnidad #Placa").val("")
            $("#formEditarUnidad #Modelo").val("")
            $("#formEditarUnidad #Anno").val("")
            $("#formEditarUnidad #UltimoAnnoRevision").val("")
            $("#formEditarUnidad #Capacidad").val("")
            $("#formEditarUnidad #Anno").val("")
            $("#formEditarUnidad #URLFotografiaUnidad").val("")
            $("#formEditarUnidad #URLRevisionTecnica").val("")
            $("#formEditarUnidad #URLTarjetaCirculacion").val("")
            $("#modalEditarUnidad").modal("hide");

        });
}