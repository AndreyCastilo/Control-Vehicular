$(function () {
    cargarTabla();



});

function triggerUpload(event, elem) {
    event.preventDefault;
    $(""+elem+"").click();
}

$('#modalAgregarConductor #formAgregarConductor #FileCedulaImg').bind('change', function () {
    var str = "";
    str = $(this).val();
    var url = str.split("\\");
    $("#formAgregarConductor #URLFotografiaCedula").val(url[2]);
    let filered = new FileReader();
    filered.onload = function (e) {
          $("#imagenPreview").attr("src",e.target.result)
    }

    filered.readAsDataURL(this.files[0]);
});


$('#modalAgregarConductor #formAgregarConductor #FileLicenciaImg').bind('change', function () {
    var str = "";
    str = $(this).val();
    var url = str.split("\\");
    $("#formAgregarConductor #URLFotografiaLicencia").val(url[2]);
});


$('#modalEditarConductor #formEditarConductor #FileCedulaImgEditar').bind('change', function () {
    
    var str = "";
    str = $(this).val();
    var url = str.split("\\");
    var imagenURL = url[2];
    if (imagenURL.length > 0) {
        $("#formEditarConductor #URLFotografiaCedula").val(imagenURL);
    }


});


$('#modalEditarConductor #formEditarConductor #FileLicenciaImgEditar').bind('change', function () {
    var str = "";
    str = $(this).val();
    var url = str.split("\\");
    var imagenURL = url[2];
    if (imagenURL.length > 0) {
        $("#formEditarConductor #URLFotografiaLicencia").val(imagenURL);
    }
});




function AbrilModalAgregarConductor() {
    limpiarFormAgregar();
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
    var form = $("#formAgregarConductor");
    $("#formAgregarConductor #Empresa").val(codigoEmpresa);
//    if ($("#formAgregarConductor").valid()) {
        formdata = new FormData(form[0]);
        $.ajax({
            url: '/Conductor/Guardar',
            data: formdata,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data, textStatus, jqXHR) {
                if (data.Resultado) {
                    mensajeOk("Exito!", "Conductor agregado correctamente!", function () {
                        $('#TablaConductores').bootstrapTable("append", data.Conductor);
                        limpiarFormAgregar();
                        $("#modalAgregarConductor").modal("hide");
                    })
                }
            }
        });
 //   }
}

function guardarEditarConductor() {
    var conductorId = $("#modalEditarConductor #formEditarConductor  #Codigo").val();
    var form = $("#modalEditarConductor #formEditarConductor");
   // if ($("#modalEditarConductor #formEditarConductor").valid()) {
   //     $("#formEditarConductor  #URLFotografiaCedula").prop("disabled", false);
   //     $("#formEditarConductor  #URLFotografiaLicencia").prop("disabled", false);
        formdata = new FormData(form[0]);
        $.ajax({
            url: '/Conductor/Editar',
            data: formdata,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data, textStatus, jqXHR) {
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
            }
        });
   // }

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
    $('#formAgregarConductor #FileCedulaImg').val("")
    $("#formAgregarConductor #FileLicenciaImg").val("");
}

function limpiarFormEditar() {
    $("#formEditarConductor  #Nombre").val("");
    $("#formEditarConductor  #TipoLicencia").val("");
    $("#formEditarConductor  #FechaVencimiento").val("");
    $("#formEditarConductor  #URLFotografiaCedula").val("");
    $("#formEditarConductor  #URLFotografiaLicencia").val("");
    $("#formEditarConductor  #Codigo").val("");
    $('#formEditarConductor #FileCedulaImg').val("")
    $("#formEditarConductor #FileLicenciaImg").val("");
}


function borrarConductor() {
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


function OnChangeEvent() {
    alert("cambio");

}

