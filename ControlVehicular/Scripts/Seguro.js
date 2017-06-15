$(document).ready(function () {

    cargarTabla();
});


function cargarTabla() {
    $.get("/Seguros/ObtenerTodas",
       function resultado(result) {

           var $table = $('#tablaSeguros');


           $table.bootstrapTable({
               data: result.registro,
           });

           // onclick();

       })
}

function Guardar() {

    $.post("/Seguros/Guardar",
        $("#formAgregarSeguro").serialize(),
        function resultado(data) {



            $("#formAgregarSeguro #Empresa").val("")
            $("#formAgregarSeguro #Nombre").val("")
            $("#formAgregarSeguro #Tipo").val("")
            $("#formAgregarSeguro #Detalle").val("")


            $("#modalAgregar").modal("hide");

        })
}


function Elemento() {
    $.get("/Seguros/Elemento", { codigo: 2 },
        function resultado(data) {
            $("#formEditarSeguro #Codigo").val(2);
            $("#formEditarSeguro #Nombre").val(data.Seguro.Nombre)
            $("#formEditarSeguro #Empresa").val(data.Seguro.Empresa)
            $("#formEditarSeguro #Tipo").val(data.Seguro.Tipo)
            $("#formEditarSeguro #Detalle").val(data.Seguro.Detalle)


            $("#modalEditar").modal("show");
        })
}


function Editar() {

    var codigo = $("#formEditarSeguro #Codigo").val();
    $.post("/Seguros/Editar",
        $("#formEditarSeguro").serialize(),
        function resultado(data) {
            alert("Se edito")
            $("#formsEditarSeguro #Nombre").val("")
            $("#formEditarSeguro #Empresa").val("")
            $("#formEditarSeguro #Tipo").val("")
            $("#formEditarSeguro #Detalle").val("")
            $("#modalEditar").modal("hide");

        })


}

function Borrar() {
    var codigo = $("#formEditarSeguro #Codigo").val();
    $.post("/Seguros/Eliminar",
        { codigo: codigo },
        function resultado(data) {

            $("#formEditarEmpresa #Nombre").val("")
            $("#formEditarEmpresa #Empresa").val("")
            $("#formEditarEmpresa #Tipo").val("")
            $("#formEditarEmpresa #Detalle").val("")

            $("#modalEditar").modal("hide");


        });

}

