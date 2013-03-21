//$(document).ready(function () {
//    $('*[data-autocomplete-url]')
//        .each(function () {
//            $(this).autocomplete({
//                source: $(this).data("autocomplete-url")
//            });
//        });
//});

$(document).ready(function () {
    $('*[data-autocomplete-url]')
        .each(function () {
            $(this).autocomplete({
                source: $(this).data("autocomplete-url"),
                search: function () {
                    // custom minLength
                    var term = extractLast(this.value);
                    if (term.length < 1) {
                        return false;
                    }
                },
                focus: function () {
                    // prevent value inserted on focus
                    return false;
                },
                select: function (event, ui) {
                    var terms = split(this.value);
                    // remove the current input
                    terms.pop();
                    // add the selected item
                    terms.push(ui.item.value);
                    // add placeholder to get the comma-and-space at the end
                    terms.push("");
                    this.value = terms.join(",");
                    return false;
                }
            });
            function split(val) {
                return val.split(/,\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }
        });
});

