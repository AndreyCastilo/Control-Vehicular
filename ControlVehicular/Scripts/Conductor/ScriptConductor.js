$(function () {
    cargarTabla();
});

function AbrilModalAgregarConductor() {
    $("#modalAgregarConductor").modal("show");
}

function AbrilModalEditarConductor(codigoConductor) {
    $.get("/Conductor/Obtener", { codigo: codigoConductor },
        function resultado(data) {
            if (data.Resultado) {

                var codigoEmpresa = $("#EmpresaId").html();
                $("#formEditarConductor  #Empresa").val(codigoEmpresa);
                $("#formEditarConductor  #Nombre").val(data.Conductor.Nombre);
                $("#formEditarConductor  #TipoLicencia").val(data.Conductor.TipoLicencia);
                $("#formEditarConductor  #FechaVencimiento").val(dtConvFromJSON(data.Conductor.FechaVencimiento));
                $("#formEditarConductor  #URLFotografiaCedula").val(data.Conductor.URLFotografiaCedula);
                $("#formEditarConductor  #URLFotografiaLicencia").val(data.Conductor.URLFotografiaLicencia);
                $("#formEditarConductor  #Codigo").val(codigoConductor);
                $("#modalEditarConductor").modal("show");
            }
        });
}



function guardarConductor() {
    var codigoEmpresa = $("#EmpresaId").html();
    $("#formAgregarConductor #Empresa").val(codigoEmpresa);
    var a = $("#formAgregarConductor #Empresa").val();
    if ($("#formAgregarConductor").valid()) { 
        $.post("/Conductor/Guardar",
        $("#formAgregarConductor").serialize(),
       function (data) {
           if (data.Resultado) {
               mensajeOk("Exito!", "Conductor agregado correctamente!", function () {               
                   $('#TablaConductores').bootstrapTable("append", data.Conductor);
                   limpiarFormAgregar();
                   $("#modalAgregarConductor").modal("hide");
               })
           }
       });
    }
}


function guardarEditarConductor() {
   var conductorId= $("#formEditarConductor  #Codigo").val();
    $.post("/Conductor/Editar",
       $("#formEditarConductor").serialize(),
       function resultado(data) {
           if (data.Resultado) {
               mensajeOk("Exito!", "Conductor editado correctamente!", function () {
                   $('#TablaConductores').bootstrapTable('updateByUniqueId', {
                       id: conductorId,
                       row: data.Conductor
                   });
                   limpiarFormEditar();
                   $("#modalEditarConductor").modal("hide");
               })
           }


       })
}


function cargarTabla() {
    var codigoEmpresa = $("#EmpresaId").html();
    $.get("/Conductor/GetConductoresEmpresa", { id : codigoEmpresa },
   function resultado(result) {
       var $table = $('#TablaConductores');
       $table.bootstrapTable({
           data: result.Registro,   
       });
       $table.on('click-row.bs.table', function (e, row, $element) {AbrilModalEditarConductor(row.Codigo) });
   })
}


function formatDate(value, row) {
    return dtConvFromJSON(value);
}




function limpiarFormAgregar() {
    $("#formAgregarConductor  #Nombre").val("");
    $("#formAgregarConductor  #TipoLicencia").val("");
    $("#formAgregarConductor  #FechaVencimiento").val("");
    $("#formAgregarConductor  #URLFotografiaCedula").val("");
    $("#formAgregarConductor  #URLFotografiaLicencia").val("");
}

function limpiarFormEditar() {
    $("#formEditarConductor  #Nombre").val("");
    $("#formEditarConductor  #TipoLicencia").val("");
    $("#formEditarConductor  #FechaVencimiento").val("");
    $("#formEditarConductor  #URLFotografiaCedula").val("");
    $("#formEditarConductor  #URLFotografiaLicencia").val("");
    $("#formEditarConductor  #Codigo").val("");
}


function Borrar() {
    var codigo = $("#formEditarConductor #Codigo").val();
    $.post("/Conductor/Remover",
        { codigo: codigo },
        function resultado(data) {
            if (data.Resultado) {
                mensajeOk("Exito!", "Conductor eliminado correctamente!", function () {
                    var $table = $('#TablaConductores');
                    $table.bootstrapTable('removeByUniqueId', codigo);
                    limpiarFormEditar();
                    $("#modalEditarConductor").modal("hide");
                })
            }
        
        });
}

