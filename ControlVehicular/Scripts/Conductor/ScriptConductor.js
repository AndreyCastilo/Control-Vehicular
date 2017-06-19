$(function () {
    cargarTabla();
    onClickImg01();
    onClickImg02();
    onClickImg04();
    onClickImg03();

});

function triggerUpload(event, elem) {
    event.preventDefault;
    $(""+elem+"").click();
}

$('#modalAgregarConductor #formAgregarConductor #FileCedulaImg').bind('change', function () {
    let filered = new FileReader();
    filered.onload = function (e) {
        var algo = e.target.result;
        $("#imagenPreviewCedula").attr("src", e.target.result)
    }

    var a = this.files[0];
    filered.readAsDataURL(this.files[0]);
});


$('#modalAgregarConductor #formAgregarConductor #FileLicenciaImg').bind('change', function () {
    let filered = new FileReader();
    filered.onload = function (e) {
        $("#imagenPreviewLicencia").attr("src", e.target.result)
    }

    filered.readAsDataURL(this.files[0]);
});


$('#modalEditarConductor #formEditarConductor #FileCedulaImgEditar').bind('change', function () {
    
    var str = "";
    str = $(this).val();
    var url = str.split("\\");
    var imagenURL = url[2];
    if (imagenURL.length > 0) {
        let filered = new FileReader();
        filered.onload = function (e) {
            $("#imagenPreviewCedulaEditar").attr("src", e.target.result)
        }

        filered.readAsDataURL(this.files[0]);
    }


});


$('#modalEditarConductor #formEditarConductor #FileLicenciaImgEditar').bind('change', function () {
    var str = "";
    str = $(this).val();
    var url = str.split("\\");
    var imagenURL = url[2];
    if (imagenURL.length > 0) {
        let filered = new FileReader();
        filered.onload = function (e) {
            $("#imagenPreviewLicenciaEditar").attr("src", e.target.result)
        }
        filered.readAsDataURL(this.files[0]);
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
                $("#formEditarConductor  #Codigo").val(codigoConductor);         
                $("#imagenPreviewCedulaEditar").attr("src", data.urlCedula);
                $("#imagenPreviewLicenciaEditar").attr("src", data.urlLicencia);

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

    return (value !=null)? dtConvFromJSON(value) : value;
}




function limpiarFormAgregar() {
    $("#formAgregarConductor  #Nombre").val("");
    $("#formAgregarConductor  #TipoLicencia").val("");
    $("#formAgregarConductor  #FechaVencimiento").val("");
    $('#formAgregarConductor #FileCedulaImg').val("")
    $("#formAgregarConductor #FileLicenciaImg").val("");
    $("#imagenPreviewCedula").attr("src","")
    $("#imagenPreviewLicencia").attr("src","")
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


let onClickImg01 = () => {
    abreModalImagen("imagenPreviewCedulaEditar", 1)
}
let onClickImg02 = () => {
    abreModalImagen("imagenPreviewLicenciaEditar", 2)
}

let onClickImg03 = () => {
    abreModalImagen("imagenPreviewCedula", 3)
}
let onClickImg04 = () => {
    abreModalImagen("imagenPreviewLicencia", 4)
}

// Modals de imagenes en editar
let abreModalImagen = (nombreImg, num) => {
    // Get the modal
    var modal = document.getElementById('modal_' + nombreImg);
    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = document.getElementById(nombreImg);
    var modalImg = document.getElementById("img0" + num);
    var captionText = document.getElementById("caption");
    img.onclick = function () {
        modal.style.display = "block";
        modalImg.src = this.src;
        captionText.innerHTML = this.alt;
    }

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close-image")[0];

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }
}

