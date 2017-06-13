
function mensajeOk(titulo, texto, funcion) {
    swal({
        title: titulo,
        text: texto,
        type: "success",
        confirmButtonText: 'Ok',
    },
          function (isConfirm) {
              funcion();

          });
}
