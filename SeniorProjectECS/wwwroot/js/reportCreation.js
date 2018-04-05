function addTextField(fieldName) {
    var numInputs = $('#' + fieldName + 'List').children().length;
    $('<input>').attr({
        class: 'form-control',
        type: 'text',
        id: fieldName + '_' + numInputs + '_',
        name: fieldName + '[' + numInputs + ']'
    }).appendTo('#' + fieldName + 'List');
}

function addSelectField(fieldName) {
    var numInputs = $('#' + fieldName + 'List').children().length;
    $('<select>').attr({
        class: 'form-control',
        id: fieldName + '_' + numInputs + '_',
        name: fieldName + '[' + numInputs + ']'
    }).appendTo('#' + fieldName + 'List');

    var $options = $('#' + fieldName + '_0_ > option').clone();
    $('#' + fieldName + '_' + numInputs + '_').append($options);
}