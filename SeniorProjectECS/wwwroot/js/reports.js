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


//gets filter list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/Reports/GetFilterList',
    data: 'filterList',
    success: function (data) {
        $.each(data, function (index, value) {
            $('<option>').val(value.FilterID).text(value.FilterName).append('#filterSelect');
        })
    }
})

$.ajax({
    dataType: "json",
    url: '/Reports/GetFilterList',
    data: 'datalist',
    success: function (data) {
        $.each(data, function (index, value) {
            $('<option>').val(value.)
        })
    }
})

//populates all select lists for the filter page
$.ajax({
    dataType: "json",
    url: '/Reports/GetSelectLists',
    data: 'dataList',
    success: function (data) {
        console.log(data);
        var centerNames = [];
        var centerCounties = [];
        var centerRegions = [];
        var positions = [];
        var degreeLevels = [];
        var degreeTypes = [];
        var degreeDetails = [];
        var certs = [];
        $.each(data, function (index, value) {
            if (value.Name != undefined) {
                centerNames.push(value.Name);
            }
            if (value.County != undefined) {
                centerCounties.push(value.County);
            }
            if (value.Region != undefined) {
                centerRegions.push(value.Region);
            }
            if (value.DegreeLevel != undefined) {
                degreeLevels.push(value.DegreeLevel);
            }
            if (value.DegreeType != undefined) {
                degreeTypes.push(value.DegreeType);
            }
            if (value.DegreeDetail != undefined) {
                degreeDetails.push(value.DegreeDetail);
            }
            if (value.PositionTitle != undefined) {
                positions.push(value.PositionTitle);
            }
            if (value.CertName != undefined) {
                certs.push(value.CertName);
            }
        })
        centerNames = [...new Set(centerNames)];
        centerCounties = [...new Set(centerCounties)];
        centerRegions = [...new Set(centerRegions)];
        positions = [...new Set(positions)];
        degreeLevels = [...new Set(degreeLevels)];
        degreeTypes = [...new Set(degreeTypes)];
        degreeDetails = [...new Set(degreeDetails)];
        certs = [...new Set(certs)];

        $('<option>').val('').text('').appendTo("#CenterName_0_");
        $.each(centerNames, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#CenterName_0_");
        })

        $('<option>').val('').text('').appendTo("#CenterCounty_0_");
        $.each(centerCounties, function (index, value)  {
            $('<option>').val(value).text(value).appendTo("#CenterCounty_0_");
        })

        $('<option>').val('').text('').appendTo("#CenterRegion_0_");
        $.each(centerRegions, function (index, value) {
            
            $('<option>').val(value).text(value).appendTo("#CenterRegion_0_");
        })

        $('<option>').val('').text('').appendTo("#Position_0_");
        $.each(positions, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#Position_0_");
        })

        $('<option>').val('').text('').appendTo("#EducationLevel_0_");
        $.each(degreeLevels, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#EducationLevel_0_");
        })

        $('<option>').val('').text('').appendTo("#EducationType_0_");
        $.each(degreeTypes, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#EducationType_0_");
        })

        $('<option>').val('').text('').appendTo("#EducationDetail_0_");
        $.each(degreeDetails, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#EducationDetail_0_");
        })

        $('<option>').val('').text('').appendTo("#CertCompleted_0_");
        $.each(certs, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#CertCompleted_0_");
        })
    }
})