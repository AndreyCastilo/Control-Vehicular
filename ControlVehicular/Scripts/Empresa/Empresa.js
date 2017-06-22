﻿$(document).ready(function () {
  
    cargarTabla();
    empresaSeleccionada();
    //empresaSeleccionadaConEstilo();
});


function cargarTabla() {
    let codigo = $("#ConEmpresa").val()
    $.get("/Empresa/ObtenerTodas",
       function resultado(result) {

           var $table = $('#tablaEmpresas');


           $table.bootstrapTable({
               data: result.registro,
               onPostBody: function (data) {
                   alert("gola")
               }
           });

           $table.on('click-row.bs.table', function (e, row, $element) {
               Elemento(row.Codigo)
           });

           
           /*Para cambiar a success la empresa seleccionada
           var rows = $("#tablaEmpresas > tbody > tr");

           for (i = 0; i < rows.length; i++) {
               var uniqueid = rows[0].attributes[1].value;
               if (uniqueid == codigo) {
                   var ff = rows[i];
                   //ff.addClass("success");
                   break;
               }
           }
           */
           
         
       })
}

function empresaSeleccionada() {
    if ( $("#ConEmpresa").val() == 0 ) {
        mensajeSinEmpresa();
    }
}

function empresaSeleccionadaConEstilo(table) {
    let codigo = $("#ConEmpresa").val()
    if (codigo != 0) {
        let row = table.bootstrapTable('getRowByUniqueId', codigo);
    }
}

function formatoFisica(value) {
    return (value) ? 'Si' : 'No';
}

function Guardar() {

    $.post("/Empresa/Guardar",
        $("#formAgregarEmpresa").serialize(),
        function resultado(data) {

            mensajeOk("Exito!", "Empresa agregada correctamente!", function () {
                console.log(data.Empresa)
                $('#tablaEmpresas').bootstrapTable("append", data.Empresa);

                $("#formAgregarEmpresa #Nombre").val("")
                $("#formAgregarEmpresa #Fisica").val("")
                $("#formAgregarEmpresa #Telefono").val("")
                $("#formAgregarEmpresa #Cedula").val("")


                $("#modalAgregarEmpresa").modal("hide");
            });
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
            mensajeOk("Exito!", "Empresa editada correctamente!", function () {
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
            });
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

let seleccionarEmpresa = () => {
    let codigo = $("#formEditarEmpresa #Codigo").val()
    $.post("/Empresa/SeleccionarEmpresa",
        { codigo: codigo },
        (data) => {
            if (data.result) {
                $('#modalEditarEmpresa').modal('hide');
                window.location.href = "/Home/Index";
            }
        })
}
