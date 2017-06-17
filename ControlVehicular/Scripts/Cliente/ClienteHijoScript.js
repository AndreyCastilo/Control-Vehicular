$(function () {
    cargarTabla();
});

function AbrirModalAgregarHijo() {
    $("#modalAgregarHijo").modal("show");
}

function AbrirModalEditarHijo(codigoHijo) {
    $.get("/ClienteHijo/Obtener", { codigo: codigoHijo },
        function resultado(data) {
            if (data.Resultado) {
                var codigoPadre = $("#padreId").html();
                $("#formEditarHijo  #PadreCliente").val(codigoPadre);
                $("#formEditarHijo  #Nombre").val(data.Hijo.Nombre);
                $("#formEditarHijo  #MostrarComo").val(data.Hijo.MostrarComo);
                $("#formEditarHijo  #Codigo").val(codigoHijo);
                $("#modalEditarHijo").modal("show");
            }
        });
}



function guardarHijo() {
    var codigoPadre = $("#padreId").html();
    $("#formAgregarHijo #PadreCliente").val(codigoPadre);
    var a = $("#formAgregarHijo #PadreCliente").val();
    if ($("#formAgregarHijo").valid()) {
        $.post("/ClienteHijo/Guardar",
        $("#formAgregarHijo").serialize(),
       function (data) {
           if (data.Resultado) {
               mensajeOk("Exito!", "Hijo agregado correctamente!", function () {
                   $('#TablaHijos').bootstrapTable("append", data.Hijo);
                   limpiarFormAgregar();
                   $("#modalAgregarHijo").modal("hide");
               })
           }
       });
    }
}


function guardarEditarHijo() {
    var conductorId = $("#formEditarHijo  #Codigo").val();
    $.post("/ClienteHijo/Editar",
       $("#formEditarHijo").serialize(),
       function resultado(data) {
           if (data.Resultado) {
               mensajeOk("Exito!", "Hijo editado correctamente!", function () {
                   $('#TablaHijos').bootstrapTable('updateByUniqueId', {
                       id: conductorId,
                       row: data.Hijo
                   });
                   limpiarFormEditar();
                   $("#modalEditarHijo").modal("hide");
               })
           }


       })
}


function cargarTabla() {
    var codigoPadre = $("#padreId").html();
    $.get("/ClienteHijo/GetHijosPadre", { id: codigoPadre },
   function resultado(result) {
       var $table = $('#TablaHijos');
       $table.bootstrapTable({
           data: result.Registro,
       });
       $table.on('click-row.bs.table', function (e, row, $element) { AbrirModalEditarHijo(row.Codigo) });
   })
}


function formatDate(value, row) {
    return dtConvFromJSON(value);
}




function limpiarFormAgregar() {
    $("#formAgregarHijo  #Nombre").val("");
    $("#formAgregarHijo  #MostrarComo").val("");
}

function limpiarFormEditar() {
    $("#formEditarHijo  #Nombre").val("");
    $("#formEditarHijo  #MostrarComo").val("");
    $("#formEditarHijo  #Codigo").val("");
}


function borrarCliente() {
    var codigo = $("#formEditarHijo #Codigo").val();
    $.post("/ClienteHijo/Remover",
        { codigo: codigo },
        function resultado(data) {
            if (data.Resultado) {
                mensajeOk("Exito!", "Hijo eliminado correctamente!", function () {
                    var $table = $('#TablaHijos');
                    $table.bootstrapTable('removeByUniqueId', codigo);
                    limpiarFormEditar();
                    $("#modalEditarHijo").modal("hide");
                })
            }

        });
}

