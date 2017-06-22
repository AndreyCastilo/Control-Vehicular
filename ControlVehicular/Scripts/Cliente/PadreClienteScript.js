$(function () {
    cargarTabla();
});

function limpiaFormularioAgregarCliente() {
    $("#formAgregarCliente #Nombre").val("");
    $("#formAgregarCliente #MostrarComo").val("");
    $("#formAgregarCliente #Direccion").val("");
    $("#formAgregarCliente #Empresa").val("");

}

function limpiaFormularioEditarCliente() {
    $("#formEditarCliente #Nombre").val("");
    $("#formEditarCliente #MostrarComo").val("");
    $("#formEditarCliente #Direccion").val("");
    $("#formEditarCliente #Empresa").val("");

}

function AbrirModalAgregarCliente() {
    limpiaFormularioAgregarCliente();
    $("#modalAgregarCliente").modal("show");
}

function agregarCliente() {
    if ($("#formAgregarCliente").valid()) {
        $.post("/PadreCliente/Guardar",
        $("#formAgregarCliente").serialize(),
       function (data) {
           if (data.Resultado) {
               mensajeOk("Exito!", "Cliente agregado correctamente!", function () {
                   $('#tablaPadreCliente').bootstrapTable("append", data.PadreCliente);
                   $("#modalAgregarCliente").modal("hide");
               })
           }
       });
    }
}

function AbrilModalEditarCliente(codigoCliente) {
    $.get("/PadreCliente/Obtener", { codigo: codigoCliente },
        function resultado(data) {
            if (data.Resultado) {
                $("#verHijos").attr("href", "/ClienteHijo/PadreHijo/" + data.Cliente.Codigo + "/");
                $("#verRutasHijos").attr("href", "/ClienteRuta/RutaClientesEmpresa/" + data.Cliente.Codigo + "/");

                //
                $("#formEditarCliente #Nombre").val(data.Cliente.Nombre);
                $("#formEditarCliente #MostrarComo").val(data.Cliente.MostrarComo);
                $("#formEditarCliente #Direccion").val(data.Cliente.Direccion);
                $("#formEditarCliente #Empresa").val(data.Cliente.Empresa);
                $("#formEditarCliente #Codigo").val(codigoCliente);
                $("#modalEditarCliente").modal("show");
            }
        });
}

function guardarEditarCliente() {
    var idCliente = $("#formEditarCliente #Codigo").val();
    $.post("/PadreCliente/Editar",
       $("#formEditarCliente").serialize(),
       function resultado(data) {
           if (data.Resultado) {
               mensajeOk("Exito!", "Cliente editado correctamente!", function () {
                   $('#tablaPadreCliente').bootstrapTable('updateByUniqueId', {
                       id: idCliente,
                       row: data.PadreCliente
                   });
                   limpiaFormularioEditarCliente();
                   $("#modalEditarCliente").modal("hide");
               })
           }


       })
}

function cargarTabla() {
    // no sé si mostrar los de solo una empresa o todos
    $.get("/PadreCliente/GetClientes",
   function resultado(result) {
       var $table = $('#tablaPadreCliente');
       $table.bootstrapTable({
           data: result.Registro,
       });
       $table.on('click-row.bs.table', function (e, row, $element) { AbrilModalEditarCliente(row.Codigo) });
   })
}

function borrarCliente() {
    var codigo = $("#formEditarConductor #Codigo").val();
    $.post("/PadreCliente/Remover",
        { codigo: codigo },
        function resultado(data) {
            if (data.Resultado) {
                mensajeOk("Exito!", "Cliente eliminado correctamente!", function () {
                    var $table = $('#tablaPadreCliente');
                    $table.bootstrapTable('removeByUniqueId', codigo);
                    limpiarFormEditar();
                    $("#modalEditarCliente").modal("hide");
                })
            }

        });
}