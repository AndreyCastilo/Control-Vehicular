$(function () {
    cargarTabla();
    setUpDropDownListHijos();

});


function cargarTabla() {
    var codigoEmpresa = $("#EmpresaId").html();
    $.get("/ClienteRuta/Listar", { id: codigoEmpresa },
   function resultado(result) {
       if (result.Resultado) {
           var $table = $('#TablaRutasEmpresa');
           $table.bootstrapTable({
               data: result.Registro,
           });
           $table.on('click-row.bs.table', function (e, row, $element) { AbrilModalEditarClienteRuta(row.Codigo) });
       }
   
   })
}

function GuardarClienteRuta() {
    var codigoClienteVal = $("#NombreHijoCliente").val();
    var codigoRutaVal = $("#NombreRuta").val();
    var idJunto = codigoClienteVal + "-" + codigoRutaVal;
    $.ajax({
        type: 'POST',
        url: '/ClienteRuta/AgregarRutaCliente',
        dataType: 'json',
        data: { id: idJunto },
        success: function (data, textStatus, jqXHR) {
            if (data.Resultado) {
                mensajeOk("Exito!", "Conductor agregado correctamente!", function () {
                    $('#TablaRutasEmpresa').bootstrapTable("append", data.ClienteRuta);
                    limpiarFormAgregar();
                    $("#modalAgregarClienteRuta").modal("hide");
                })
            }
        }
    });
    //   }
}
function limpiarFormAgregar() {
     $("#NombreHijoCliente").val("");
     $("#NombreRuta").val("");
     $("#NombrePadreCliente").val("");
}



function setUpDropDownListHijos() {
    $("#NombrePadreCliente").change(function () {
        $("#NombreHijoCliente").empty();
        $.ajax({  
            type: 'GET',  
            url: '/ClienteRuta/actualizarDropdownListHijos', 
            dataType: 'json',            
            data: { id: $("#NombrePadreCliente").val() },
            success: function (states) {  
                $("#NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
                $.each(states, function (i, state) {  
                    $("#NombreHijoCliente").append('<option value="' + state.Value + '">' +
                         state.Text + '</option>');                                                                                                  
                });  
            },  
            error: function (ex) {
                $("#NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
            //alert('Failed to retrieve states.' + ex);  
        }  
    });  

});  
}