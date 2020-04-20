

$(document).ready(function () {
    $('#myTable').DataTable({
        orderCellsTop: true,
        stateSave: true,
        dom: 'Bfrtip',
        buttons: [
            'colvis'
        ],
        initComplete: function (table) {
            // This is for the search bar option 
            this.api().columns('.search-filter').every(function (i) {
                var column = this;
                var target = table.aoHeader[1][column.index()].cell;
                var searchable = $('<input class="form-control form-control-sm" type="text"/>')
                    .appendTo(target)
                    .on('keyup change clear', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val || '', true, false)
                            .draw();
                    });
            });

            // This is for the dropdown fitlers 
            this.api().columns('.dropdown-filter').every(function (i) {
                var column = this;
                var target = table.aoHeader[1][column.index()].cell;
                var select = $('<select class="form-control form-control-sm"><options value="></options></select>')
                    .appendTo(target)
                    .on('keyup change clear', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                //get filter state of current column
                var sSearch = table.aoPreSearchCols[i].sSearch.replace(/(\^|\$)/g, "");

                // filter out html string and get unique options for the column
                var uniqueData = column.data().unique().reduce(function (reducer, v) {
                    //if the string has any html elements then they are filterd out
                    if (v.match(/(\^|\$)/)) {
                        var input = $(v).find('input');
                        if ($input.length == 1) {
                            v = input.val();
                        }
                        else {
                            v = $(v).text();
                        }
                    }

                    if (v && reducer.indexOf(v) == -1) {
                        reducer.push(v.trim());
                    }
                    return reducer;
                }, []).sort();

                uniqueData.forEach(function (d, j) {
                    isSelect = "";
                    if (sSearch && sSearch === d) {
                        isSelect = "selected";
                    }
                    select.append('<option ' + isSelect + ' value="' + d + '">' + d + '</option>')
                });
            })

            this.api().columns('.daterange-filter').every(function () {
                var column = this;
                var target = table.aoHeader[1][column.index()].cell;
                var div = $('<div class="input-group-sm" style="margin:0 !important; min-width:150px !important; width:100%;"><div class="input-group-append"></div></div>').appendTo(target);

                var datepicker = $('<input placeholder="Date Range Filter" class="dt-rg-picker form-control"/>').prependTo(div).flatpickr({
                    mode: "range",
                    allowInput: true,
                    dateFormat: 'n/j/Y',
                    onChange: function (range) {
                        if (range.length == 0 || range.length == 2) {
                            table.oInstance.api().draw();
                        }
                    }
                });
                $('<button class="btn btn-secondary" type="button">Clear</button>').appendTo(div.find('div')).click(datepicker.clear);
                $.fn.dataTable.ext.search.push(
                    function (settings, data) {
                        var range = datepicker.selectedDates;
                        var dateStringRegex = data[column.index()].match(/([0-9]*\/){2}[0-9]*/g) || [''];
                        var compTime = new Date(dateStringRegex[0]).getTime();
                        return range.length == 0 || (range.length >= 2 && !isNaN(compTime) && range[0].getTime() <= compTime && range[1].getTime() >= compTime);
                    }
                );
            });

            this.api().columns('.hideColumn').visible(false);
        }
    });
});