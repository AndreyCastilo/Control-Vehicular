﻿$(document).ready(function () {
    enviaCoordenadas()
    clientesEnRuta()
})

function clientesEnRuta() {
    $.get("/Home/ClientesEnRuta", { codigoUnidad:1},
        function resultado(result) {

            var $table = $('#tablaViajes');


            $table.bootstrapTable({
                data: result.Clientes,
            });

           // $table.on('click-row.bs.table', function (e, row, $element) { Elemento(row.Codigo) });


        })

}

function enviaCoordenadas() {
    setTimeout(function () {
        
        navigator.geolocation.getCurrentPosition(function (position) {
            let pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            var marker = new google.maps.Marker({
                position: pos,
                map: map,
                title: 'Posición Actual'
            });

     
         
            $.post('/Home/ActualizaCoordenadas/', {
                codigoUnidad: 1, lat: pos.lat, lon: pos.lng
            }, function resultado(result) {
            
                enviaCoordenadas()
            })
        })
    }, 1000*10)
}


var map;
var actual;


var directionsDisplay;
var directionsService;
function initMap() {
    directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer();

    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 15
    });
    directionsDisplay.setMap(map);
    var infoWindow = new google.maps.InfoWindow({ map: map });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            actual = pos;

            map.setCenter(pos);

            var marker = new google.maps.Marker({
                position: pos,
                map: map,
                title: 'Posición Actual'
            });

        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
                          'Error: The Geolocation service failed.' :
                          'Error: Your browser doesn\'t support geolocation.');
}

//CALCULA RUTA
function calcRoute() {
    console.log(actual.lat, actual.lng);
    var request = {
        origin: { lat: actual.lat, lng: actual.lng },
        destination: { lat: 9.971157, lng: -84.129138 },
        travelMode: google.maps.DirectionsTravelMode.DRIVING
    };
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        }
    });

}



function Button1_onclick() {
    calcRoute();
}