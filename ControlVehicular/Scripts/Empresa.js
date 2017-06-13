$(document).ready(function () {
  
    cargarTabla();
});


function cargarTabla() {
    $.get("/Empresa/ObtenerTodas",
       function resultado(result) {

           var $table = $('#tablaEmpresas');


           $table.bootstrapTable({
               data: result.registro,
           });

          // onclick();

       })
}

function Guardar() {

    $.post("/Empresa/Guardar",
        $("#formAgregarEmpresa").serialize(),
        function resultado(data) {

          

            $("#formAgregarEmpresa #Nombre").val("")
            $("#formAgregarEmpresa #Fisica").val("")
            $("#formAgregarEmpresa #Telefono").val("")
            $("#formAgregarEmpresa #Cedula").val("")


            $("#modalAgregar").modal("hide");
           
        })
}


function Elemento() {
    $.get("/Empresa/Elemento", { codigo: 3 },
        function resultado(data) {
            $("#formEditarEmpresa #Codigo").val(3);
            $("#formEditarEmpresa #Nombre").val(data.Empresa.Nombre)
            $("#formEditarEmpresa #Fisico").val(data.Empresa.Fisico)
            $("#formEditarEmpresa #Telefono").val(data.Empresa.Telefono)
            $("#formEditarEmpresa #Cedula").val(data.Empresa.Cedula)
       

            $("#modalEditar").modal("show");
        })
}


function Editar() {

    var codigo = $("#formEditarEmpresa #Codigo").val();
    $.post("/Empresa/Editar",
        $("#formEditarEmpresa").serialize(),
        function resultado(data) {
            alert("sdfsdf")
            $("#formsEditarEmpresa #Nombre").val("")
            $("#formEditarEmpresa #Fisica").val("")
            $("#formEditarEmpresa #Telefono").val("")
            $("#formEditarEmpresa #Cedula").val("")
            $("#modalEditar").modal("hide");
          
        })


}

function Borrar() {
    var codigo = $("#formEditarEmpresa #Codigo").val();
    $.post("/Empresa/Eliminar",
        { codigo: codigo },
        function resultado(data) {

            $("#formEditarEmpresa #Nombre").val("")
            $("#formEditarEmpresa #Fisica").val("")
            $("#formEditarEmpresa #Telefono").val("")
            $("#formEditarEmpresa #Cedula").val("")

            $("#modalEditar").modal("hide");


        });

}

