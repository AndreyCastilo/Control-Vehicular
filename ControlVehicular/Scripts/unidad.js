$(document).ready(function () {
    uploadView();
    cargarTabla();
    onClickImg02();
    onClickImg03();
    onClickImg01();
});

function irMapa() {
  var cod =  $("#formEditarUnidad #Codigo").val();

    var url = "/Unidad/VerMapa?codigoUnidad=" + cod;
    window.location.href = url; 
    
}

function cargarTabla() {
    $.get("/Unidad/ObtenerTodas",
       function resultado(result) {

           var $table = $('#tablaUnidad');


           $table.bootstrapTable({
               data: result.registro,
           });

           $table.on('click-row.bs.table', function (e, row, $element) { Elemento(row.Codigo) });


       })
}

function Elemento(cod) {
    $.get("/Unidad/Elemento", { codigo: cod },
        function resultado(data) {
            $("#formEditarUnidad #Codigo").val(cod);
            $("#formEditarUnidad #Marca").val(data.Unidad.Marca)
            $("#formEditarUnidad #Placa").val(data.Unidad.Placa)
            $("#formEditarUnidad #Modelo").val(data.Unidad.Modelo)
            $("#formEditarUnidad #Anno").val(data.Unidad.Anno)
            $("#formEditarUnidad #UltimoAnnoRevision").val(data.Unidad.UltimoAnnoRevision)
            $("#formEditarUnidad #Capacidad").val(data.Unidad.Capacidad)
            $("#formEditarUnidad #Anno").val(data.Unidad.Anno)
            //$("#formEditarUnidad #URLFotografiaUnidad").val(data.Unidad.URLFotografiaUnidad)
            //$("#formEditarUnidad #URLRevisionTecnica").val(data.Unidad.URLRevisionTecnica)
            //$("#formEditarUnidad #URLTarjetaCirculacion").val(data.Unidad.URLTarjetaCirculacion)

            $("#img1").attr("src", data.urlFoto);
            $("#img2").attr("src", data.urlRevision);
            $("#img3").attr("src", data.urlTarjeta);

            $("#modalEditarUnidad").modal("show");
        })
}

let nuevaUnidad = () => {
        let form = $("#formAgregarUnidad");
        if (form.valid()) {
            formdata = new FormData(form[0]);
            $.ajax({
                url: '/Unidad/Agregar',
                data: formdata,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function (data, textStatus, jqXHR) {
                    // Callback code
                    // limpiar form...
                    mensajeOk("Exito!", "Unidad agregada correctamente!", function () {
                        $('#tablaUnidad').bootstrapTable("append", data.Unidad);
                        $('#modalAgregarUnidad').modal('toggle');
                    });
                }
            });
        }
}

let getresult = (data) => {
    console.log(JSON.stringify(data))
}

let editarUnidad = () => {
    let codigo = $("#formEditarUnidad #Codigo").val();
    let form = $("#formEditarUnidad");
        formdata = new FormData(form[0]);
        $.ajax({
            url: '/Unidad/Editar',
            data: formdata,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data, textStatus, jqXHR) {
                mensajeOk("Exito!", "Unidad editada correctamente!", function () {
                    var $table = $('#tablaUnidad');
                    $table.bootstrapTable('updateByUniqueId', {
                        id: codigo,
                        row: data.Unidad
                    });
                    // Callback code
                    // limpiar form...
                    //$('#modalAgregarUnidad').modal('toggle');
                    $("#formEditarUnidad #Marca").val("")
                    $("#formEditarUnidad #Placa").val("")
                    $("#formEditarUnidad #Modelo").val("")
                    $("#formEditarUnidad #Anno").val("")
                    $("#formEditarUnidad #UltimoAnnoRevision").val("")
                    $("#formEditarUnidad #Capacidad").val("")
                    $("#formEditarUnidad #Anno").val("")
                    $("#formEditarUnidad #URLFotografiaUnidad").val("")
                    $("#formEditarUnidad #URLRevisionTecnica").val("")
                    $("#formEditarUnidad #URLTarjetaCirculacion").val("")
                    $("#modalEditarUnidad").modal("hide");
                });
            }
        });

    /*
    var codigo = $("#formEditarUnidad #Codigo").val();
    $.post("/Unidad/Editar",
        $("#formEditarUnidad").serialize(),
        function resultado(data) {
            var $table = $('#tablaUnidad');
            $table.bootstrapTable('updateByUniqueId', {
                id: codigo,
                row: data.Unidad
            });


        })
        */
}

function Borrar() {
    var codigo = $("#formEditarUnidad #Codigo").val();
    $.post("/Unidad/Eliminar",
        { codigo: codigo },
        function resultado(data) {
            var $table = $('#tablaUnidad');
            $table.bootstrapTable('removeByUniqueId', codigo);
            $("#formEditarUnidad #Marca").val("")
            $("#formEditarUnidad #Placa").val("")
            $("#formEditarUnidad #Modelo").val("")
            $("#formEditarUnidad #Anno").val("")
            $("#formEditarUnidad #UltimoAnnoRevision").val("")
            $("#formEditarUnidad #Capacidad").val("")
            $("#formEditarUnidad #Anno").val("")
            $("#formEditarUnidad #URLFotografiaUnidad").val("")
            $("#formEditarUnidad #URLRevisionTecnica").val("")
            $("#formEditarUnidad #URLTarjetaCirculacion").val("")
            $("#modalEditarUnidad").modal("hide");

        });
}

// View Settings

let uploadView = () => {
    $(".file").attr("data-show-preview", "false");
    $(".file").fileinput({
        'showUpload': false,
        'showRemove': false,
        allowedFileExtensions: ['jpg', 'png', 'gif']
    })
}

let onClickImg01 = () => {
        abreModalImagen("img1",1)
}
let onClickImg02 = () => {
        abreModalImagen("img2", 2)
}
let onClickImg03 = () => {
        abreModalImagen("img3", 3)
}

// Modals de imagenes en editar
let abreModalImagen = (nombreImg, num) => {
    // Get the modal
    var modal = document.getElementById('modal_' + nombreImg);

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = document.getElementById(nombreImg);
    var modalImg = document.getElementById("img0"+num);
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

// ----------------------------