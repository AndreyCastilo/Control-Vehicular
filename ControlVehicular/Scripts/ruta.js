$(document).ready(function () {
   cargarTabla();
});

function cargarTabla() {
    $.get("/Ruta/ObtenerTodas",
       function resultado(result) {

           var $table = $('#tablaRuta');


           $table.bootstrapTable({
               data: result.registro,
           });

           $table.on('click-row.bs.table', function (e, row, $element) { Elemento(row.Codigo) });


       })
}

function Elemento(cod) {
    $.get("/Ruta/Elemento", { codigo: cod },
        function resultado(data) {
            $("#formEditarRuta #Conductor").val(data.Ruta.Conductor)
            $("#formEditarRuta #Vehiculo").val(data.Ruta.Vehiculo)
            $("#formEditarRuta #Nombre").val(data.Ruta.Nombre)
            $("#formEditarRuta #Codigo").val(data.Ruta.Codigo)
           

            $("#modalEditarRuta").modal("show");
        })
}

let nuevaRuta = () => {
    if ($("#formAgregarRuta").valid()) {
        $.post("/Ruta/Agregar",
        $("#formAgregarRuta").serialize(),
       function (data) {
           if (data.Resultado) {
               
                   $('#tablaRuta').bootstrapTable("append", data.Ruta);
                   $('#modalAgregarRuta').modal('toggle');
              
           }
       });
    }
}

let editarRuta = () => {
    let codigo = $("#formEditarRuta #Codigo").val();
    $.post("/Ruta/Editar",
        $("#formEditarRuta").serialize(),
        function resultado(data) {
            var $table = $('#tablaRuta');
            $table.bootstrapTable('updateByUniqueId', {
                id: codigo,
                row: data.Ruta
            });
            $("#formEditarRuta #Conductor").val("")
            $("#formEditarRuta #Vehiculo").val("")
            $("#formEditarRuta #Nombre").val("")
            $("#modalEditarRuta").modal("hide");
        })
        
}