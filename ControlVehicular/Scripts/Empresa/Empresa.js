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

           $table.on('click-row.bs.table', function (e, row, $element) { Elemento(row.Codigo) });

         
       })
}


function formatoFisica(value) {
    return (value) ? 'Si' : 'No';
}

function Guardar() {

    $.post("/Empresa/Guardar",
        $("#formAgregarEmpresa").serialize(),
        function resultado(data) {

            console.log(data.Empresa)
            $('#tablaEmpresas').bootstrapTable("append", data.Empresa);

            $("#formAgregarEmpresa #Nombre").val("")
            $("#formAgregarEmpresa #Fisica").val("")
            $("#formAgregarEmpresa #Telefono").val("")
            $("#formAgregarEmpresa #Cedula").val("")


            $("#modalAgregarEmpresa").modal("hide");
           
        })
}


function Elemento(cod) {
    $.get("/Empresa/Elemento", { codigo: cod },
        function resultado(data) {
            $("#formEditarEmpresa #Codigo").val(cod);
            $("#formEditarEmpresa #Nombre").val(data.Empresa.Nombre)
            $("#formEditarEmpresa #Fisico").val(data.Empresa.Fisico)
            $("#formEditarEmpresa #Telefono").val(data.Empresa.Telefono)
            $("#formEditarEmpresa #Cedula").val(data.Empresa.Cedula)
            $("#modalEditarEmpresa").modal("show");
        })
}


function Editar() {

    var codigo = $("#formEditarEmpresa #Codigo").val();
    $.post("/Empresa/Editar",
        $("#formEditarEmpresa").serialize(),
        function resultado(data) {
            var $table = $('#tablaEmpresas');
            $table.bootstrapTable('updateByUniqueId', {
                id: codigo,
                row: data.Empresa
            });

            $("#formsEditarEmpresa #Nombre").val("")
            $("#formEditarEmpresa #Fisica").val("")
            $("#formEditarEmpresa #Telefono").val("")
            $("#formEditarEmpresa #Cedula").val("")
            $("#modalEditarEmpresa").modal("hide");
          
        })


}

function Borrar() {
    var codigo = $("#formEditarEmpresa #Codigo").val();
    $.post("/Empresa/Eliminar",
        { codigo: codigo },
        function resultado(data) {
            var $table = $('#tablaEmpresas');
            $table.bootstrapTable('removeByUniqueId', codigo);
            $("#formEditarEmpresa #Nombre").val("")
            $("#formEditarEmpresa #Fisica").val("")
            $("#formEditarEmpresa #Telefono").val("")
            $("#formEditarEmpresa #Cedula").val("")

            $("#modalEditarEmpresa").modal("hide");


        });
}

