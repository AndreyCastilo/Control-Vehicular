/*
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
*/
function mensajeOk(titulo,texto,funcion) {
    iziToast.success({
        title: titulo,
        message: texto,
        onOpen: function (instance, toast, closedBy) {
            funcion();
        }
    });
}

function mensajeSinEmpresa() {
    iziToast.warning({
        title: 'Nota',
        message: 'No ha seleccionado ninguna empresa.',
        position: 'topRight',
    });
}


function toDate(selector) {
    var from = $(selector).val().split("/");
    return new Date(from[2], from[1]-1, from[0]).toISOString();
}

function dtConvFromJSON(data) {

    if (data == null) return '';
    if (data.includes("Date")) {
        let r = /\/Date\(([0-9]+)\)\//gi
        let matches = data.match(r);
        if (matches == null) return '1/1/1950';
        let result = matches.toString().substring(6, 19);
        let epochMilliseconds = result.replace(
        /^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/,
        '$1');
        let b = new Date(parseInt(epochMilliseconds));
        let c = new Date(b.toString());
        let curr_date = c.getDate();
        let curr_month = c.getMonth() + 1;
        let curr_year = c.getFullYear();
        let curr_h = c.getHours();
        let curr_m = c.getMinutes();
        let curr_s = c.getSeconds();
        let curr_offset = c.getTimezoneOffset() / 60
        let d = ((curr_date < 10) ? '0' + curr_date : curr_date) + '/' + ((curr_month < 10) ? '0' + curr_month.toString() : curr_month.toString()) + '/' + curr_year;
        return d;
    }
    else {
        return data;
    }
   
}

function validatorDateFormat() {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || Date.parseExact(value, 'd/M/yyyy') !== null;
    };
}