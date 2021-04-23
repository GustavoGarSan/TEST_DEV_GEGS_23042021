$(document).ready(function () {
    autorizarAPI();
    rellenarTabla();
});

function autorizarAPI() {
    try {
        var respuesta;
        var settings = {
            "url": "https://api.toka.com.mx/candidato/api/login/authenticate",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json",
                "Cookie": "__cfduid=dde11cb7f8d82faeab90e135a5efa75161619067600"
            },
            "data": JSON.stringify({
                "Username": "ucand0021",
                "Password": "yNDVARG80sr@dDPc2yCT!"
            }),
        };

        $.ajax(settings).done(function (response) {
            conseguirDatos(response.Data);
        });
    } catch (error) {
        console.log(error);
    }
}

function conseguirDatos(token) {
    try {
        var token = token;
        var settings = {
            "url": "https://api.toka.com.mx/candidato/api/customers",
            "method": "GET",
            "timeout": 0,
            "headers": {
                "Authorization": "Bearer " + token,
                "Cookie": "__cfduid=dde11cb7f8d82faeab90e135a5efa75161619067600"
            },
        };

        $.ajax(settings).done(function (response) {
            // rellenarTabla(response);
        });

    } catch (error) {
        console.log(error);
    }
}

async function rellenarTabla() {
    const archivo = '/Scripts/empleados.json';
    console.log(archivo);
    const response = await fetch(archivo);
    const datos = await response.json();
    const { empleados } = datos;
    console.log(empleados);
    var trHTML = '';
    $.each(empleados, function(index, item){
        trHTML += '<tr><td>' + item.id + '</td><td>' + item.nombre + '</td><td>' + item.puesto + '</td></tr>';
    });
    $('#cuerpo-tabla').append(trHTML);
    $('#tabla').DataTable( {
        "pagingType": "full_numbers",
        "pageLength": 25,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        dom: 'Bfrtip',
        buttons: [
            'csv', 'excel', 'pdf', 'print'
        ]
    } );
}