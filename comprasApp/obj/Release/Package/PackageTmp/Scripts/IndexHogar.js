$(document).ready(function () {
    $("#loadingOverlay").hide();

    $("#btnProcess").click(function () {
        const fileNameInput = document.getElementById("fileName");
        if (!fileNameInput.validity.valid) {
            swal("Verifique Campos", "Por favor indique un nombre para la exportación.", "error");
        }
        else {
            const publicUrl = `${window.location.hostname}:${window.location.port}`;
            var fileName = $("#fileName").val();
            var conditions = getConditions();
            var dataToSend = {
                fileName: fileName,
                publicUrl: publicUrl,
                conditions: conditions
            };
            console.log('URL completa:', publicUrl);
            $("#loadingOverlay").show();
            $.ajax({
                url: '/Maison/ExportToExcelMaison',
                type: 'POST',
                data: dataToSend,
                success: function (data) {
                    var a = document.createElement('a');
                    a.href = "http://" + data.tempFile;
                    a.download = data.fileName; // Nombre con el que se descargará el archivo
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a);
                    $("#loadingOverlay").hide();
                    swal("Exportación", "Archivo descargado correctamente", "success");
                },
                error: function (error) {
                    $("#loadingOverlay").hide();
                    swal("Error al procesar la solicitud", error, "error");
                }
            });
        }
    });
    function getConditions() {
        var listMarcas = document.getElementById('listMarcas');
        var listTiendas = document.getElementById('listTiendas');
        var listProveedores = document.getElementById('listProveedores');

        var listRubros = document.getElementById('listRubros');

        var elementosMarcas = obtenerElementosSeleccionadosDeSelect(listMarcas);
        var elementosTiendas = obtenerElementosSeleccionadosDeSelect(listTiendas);
        var elementosProveedores = obtenerElementosSeleccionadosDeSelect(listProveedores);
    
        var elementosRubros = obtenerElementosSeleccionadosDeSelect(listRubros);

        var filtroMarcas = "";
        var filtroTiendas = "";
        var filtroProveedores = "";
    
        var filtroRubros = "";

        if (elementosMarcas.length > 0) {
            filtroMarcas = " AND ("
            elementosMarcas.forEach((element) => filtroMarcas += "compras.dbo.NombreMarcas.CC_LIBELLE='" + element + "' or ");
            filtroMarcas = filtroMarcas.slice(0, -3) + ')';
        }
        if (elementosTiendas.length > 0) {
            filtroTiendas = " AND ("
            elementosTiendas.forEach((element) => filtroTiendas += "compras.dbo.NombreSucursales.ET_LIBELLE='" + element + "' or ");
            filtroTiendas = filtroTiendas.slice(0, -3) + ')';
        }
        if (elementosProveedores.length > 0) {
            filtroProveedores = " AND ("
            elementosProveedores.forEach((element) => filtroProveedores += "compras.dbo.DatoProveedores.T_LIBELLE='" + element + "' or ");
            filtroProveedores = filtroProveedores.slice(0, -3) + ')';
        }
 
        if (elementosRubros.length > 0) {
            filtroRubros = " AND ("
            elementosRubros.forEach((element) => filtroRubros += "compras.dbo.Rubros.YX_LIBELLE='" + element + "' or ");
            filtroRubros = filtroRubros.slice(0, -3) + ')';
        }


        return filtroMarcas + filtroTiendas + filtroProveedores  + filtroRubros;
    }

    function obtenerElementosSeleccionadosDeSelect(select) {
        var elementosSeleccionados = [];
        for (var i = 0; i < select.options.length; i++) {
            if (select.options[i].selected) {
                elementosSeleccionados.push(select.options[i].text);
            }
        }
        return elementosSeleccionados;
    }
    function clearListSelection(listId) {
        var list = document.getElementById(listId);

        // Desmarcar todos los elementos
        for (var i = 0; i < list.options.length; i++) {
            list.options[i].selected = false;
        }
    }

    document.getElementById('cleanMarca').addEventListener('click', function () {
        clearListSelection('listMarcas');
    });

    document.getElementById('cleanTienda').addEventListener('click', function () {
        clearListSelection('listTiendas');
    });

    document.getElementById('cleanProveedor').addEventListener('click', function () {
        clearListSelection('listProveedores');
    });

 

    document.getElementById('cleanRubro').addEventListener('click', function () {
        clearListSelection('listRubros');
    });

});