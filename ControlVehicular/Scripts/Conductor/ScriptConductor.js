$(function () {

});

function AbrilModalAgregarConductor() {
    $("#modalAgregarConductor").modal("show");
}

function guardarConductor() {
    if ($("#formAgregarConductor").valid()) { 
        $.post("/Conductor/Guardar",
        $("#formAgregarConductor").serialize(),
       function (data) {
           if (data.Resultado) {
               mensajeOk("Exito!", "Conductor agregado correctamente!", function () {
                   $("#modalAgregarConductor").modal("hide");
               })
           }
       });
    }
}

function listarConductores() {






}