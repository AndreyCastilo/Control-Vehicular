var codigoViejoClienteRuta;

$(function () {
    cargarTabla();
    setUpDropDownListHijos("#formAgregarClienteRuta");


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
           $table.on('click-row.bs.table', function (e, row, $element) {
               var b =e;
               var a= row;

               AbrilModalEditarClienteRuta(row.codigo)
           });
       }  
   })
}


$("#modalAgregarClienteRuta").on('hidden.bs.modal', function (e) {
    limpiarFormAgregar();
});

function GuardarClienteRuta() {
    var codigoClienteVal = $("#formAgregarClienteRuta #NombreHijoCliente").val();
    var codigoRutaVal = $("#formAgregarClienteRuta #NombreRuta").val();
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
                    $("#modalAgregarClienteRuta").modal("hide");
                })
            }
        }
    });
    //   }
}


function AbrilModalEditarClienteRuta(codigoClienteRuta) {
    $.get("/ClienteRuta/ObtenerRutaCliente", { id: codigoClienteRuta },
        function resultado(data) {
            if (data.Resultado) {
                $("#formEditarClienteRuta #NombreHijoCliente").empty();
                $("#formEditarClienteRuta #NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
                $("#modalEditarClienteRuta").modal("show");
                $("#modalEditarClienteRuta #formEditarClienteRuta #NombreRuta option[value=" + data.ClienteRuta.Ruta + "]").prop('selected', true).change();
                $("#modalEditarClienteRuta #formEditarClienteRuta #NombrePadreCliente option[value=" + data.ClienteRuta.PadreCliente + "]").prop('selected', true).change();
                setUpDropDownlistHijoEditar("#formEditarClienteRuta", data.ClienteRuta.HijoCliente, asignaHijoClienteValue);
                codigoViejoClienteRuta = data.ClienteRuta.HijoCliente + "-" + data.ClienteRuta.Ruta;
                
            }
        });
}

function guardarEditarClienteRuta() {
    var codigoClienteVal = $("#formEditarClienteRuta #NombreHijoCliente").val();
    var codigoRutaVal = $("#formEditarClienteRuta #NombreRuta").val();
    var idJunto = codigoClienteVal + "-" + codigoRutaVal;
    $.ajax({
        type: 'POST',
        url: '/ClienteRuta/Editar',
        dataType: 'json',
        data: { id: idJunto, idViejo: codigoViejoClienteRuta },
        success: function (data, textStatus, jqXHR) {
            if (data.Resultado) {
                var $table = $('#TablaRutasEmpresa');
                $table.bootstrapTable('removeByUniqueId', codigoViejoClienteRuta);
                mensajeOk("Exito!", "Ruta Cliente editada correctamente!", function () {
                    $('#TablaRutasEmpresa').bootstrapTable("append", data.ClienteRuta);
                    $("#modalEditarClienteRuta").modal("hide");
                })
            }
        }
    });
    //   }
}


    

function limpiarFormAgregar() {
    $("#formAgregarClienteRuta #NombreHijoCliente").val("");
    $("#formAgregarClienteRuta #NombreRuta").val("");
    $("#formAgregarClienteRuta #NombreHijoCliente").empty();
    $("#formAgregarClienteRuta #NombrePadreCliente").val("");
    $("#formAgregarClienteRuta #NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
}



function asignaHijoClienteValue(HijoCliente) {
    $("#modalEditarClienteRuta #formEditarClienteRuta #NombreHijoCliente option[value=" + HijoCliente + "]").prop('selected', true).change();
}

function setUpDropDownlistHijoEditar(selector,codigoCliente,callback) {
    $(selector + " " + "#NombreHijoCliente").empty();
    $.ajax({
        type: 'GET',
        url: '/ClienteRuta/actualizarDropdownListHijos',
        dataType: 'json',
        data: { id: $(selector + " " + "#NombrePadreCliente").val() },
        success: function (states) {
            $(selector + " " + "#NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
            $.each(states, function (i, state) {
                $(selector + " " + "#NombreHijoCliente").append('<option value="' + state.Value + '">' +
                     state.Value + "-" + state.Text + '</option>');
            });
            callback(codigoCliente);
        },
        error: function (ex) {
            $(selector + " " + "#NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
            //alert('Failed to retrieve states.' + ex);  
        }
    });
}


function setUpDropDownListHijos(selector) {
    $(selector + " " + "#NombrePadreCliente").change(function () {
        $(selector + " " + "#NombreHijoCliente").empty();
        $.ajax({  
            type: 'GET',  
            url: '/ClienteRuta/actualizarDropdownListHijos', 
            dataType: 'json',            
            data: { id: $(selector + " " + "#NombrePadreCliente").val() },
            success: function (states) {  
                $(selector +" "+"#NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
                $.each(states, function (i, state) {  
                    $(selector + " " +"#NombreHijoCliente").append('<option value="' + state.Value + '">' +
                         state.Value+"-"+state.Text + '</option>');
                });
            },  
            error: function (ex) {
                $(selector + " " + "#NombreHijoCliente").append('<option>' + "- Escoja un Hijo -" + '</option>');
            //alert('Failed to retrieve states.' + ex);  
        }  
    });  

});  
}
