$(document).ready(function () {
    $('#spelersTable').DataTable({
        "order": [[3, "desc"]],
        "scrollX": true,
        "paging": false,
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': [-3,-1]
        }],
          "language": {
              "sProcessing": "Bezig...",
              "sLengthMenu": "_MENU_ resultaten weergeven",
              "sZeroRecords": "Geen resultaten gevonden",
              "sInfo": "_START_ tot _END_ van _TOTAL_ resultaten",
              "sInfoEmpty": "Geen resultaten om weer te geven",
              "sInfoFiltered": " (gefilterd uit _MAX_ resultaten)",
              "sInfoPostFix": "",
              "sSearch": "Zoeken:",
              "sEmptyTable": "Geen resultaten aanwezig in de tabel",
              "sInfoThousands": ".",
              "sLoadingRecords": "Een moment geduld aub - bezig met laden...",
              "oPaginate": {
                  "sFirst": "Eerste",
                  "sLast": "Laatste",
                  "sNext": "Volgende",
                  "sPrevious": "Vorige"
              }
          }
    });
    $('#matchTable').DataTable({
        "order": [[0, "desc"]],
        "scrollX": true,
        "lengthMenu": [ 25, 50, 100 ],
        "pagingType": "numbers",
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': [-1, -6]
        }],
        "language": {
            "sProcessing": "Bezig...",
            "sLengthMenu": "_MENU_ resultaten weergeven",
            "sZeroRecords": "Geen resultaten gevonden",
            "sInfo": "_START_ tot _END_ van _TOTAL_ resultaten",
            "sInfoEmpty": "Geen resultaten om weer te geven",
            "sInfoFiltered": " (gefilterd uit _MAX_ resultaten)",
            "sInfoPostFix": "",
            "sSearch": "Zoeken:",
            "sEmptyTable": "Geen resultaten aanwezig in de tabel",
            "sInfoThousands": ".",
            "sLoadingRecords": "Een moment geduld aub - bezig met laden...",
            "oPaginate": {
                "sFirst": "Eerste",
                "sLast": "Laatste",
                "sNext": "Volgende",
                "sPrevious": "Vorige"
            }
        }
    });
    $('#seizoenTable').DataTable({
        "scrollX": true,
        "paging": false,
        "language": {
            "sProcessing": "Bezig...",
            "sLengthMenu": "_MENU_ resultaten weergeven",
            "sZeroRecords": "Geen resultaten gevonden",
            "sInfo": "_START_ tot _END_ van _TOTAL_ resultaten",
            "sInfoEmpty": "Geen resultaten om weer te geven",
            "sInfoFiltered": " (gefilterd uit _MAX_ resultaten)",
            "sInfoPostFix": "",
            "sSearch": "Zoeken:",
            "sEmptyTable": "Geen resultaten aanwezig in de tabel",
            "sInfoThousands": ".",
            "sLoadingRecords": "Een moment geduld aub - bezig met laden...",
            "oPaginate": {
                "sFirst": "Eerste",
                "sLast": "Laatste",
                "sNext": "Volgende",
                "sPrevious": "Vorige"
            }
        }
    });
});