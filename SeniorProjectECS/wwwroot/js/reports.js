function addFirstNameField() {
    var numInputs = $('#FirstNameList').children().length;
    $('<input>').attr({
        class: 'form-control',
        type: 'text',
        id: 'FirstName_' + numInputs + '_',
        name: 'FirstName[' + numInputs + ']'
    }).appendTo('#FirstNameList');
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
        })
        centerNames = [...new Set(centerNames)];
        centerCounties = [...new Set(centerCounties)];
        centerRegions = [...new Set(centerRegions)];
        positions = [...new Set(positions)];
        degreeLevels = [...new Set(degreeLevels)];
        degreeTypes = [...new Set(degreeTypes)];
        degreeDetails = [...new Set(degreeDetails)];

        $.each(centerNames, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#centerNames");
        })
        $.each(centerCounties, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#centerCounties");
        })
        $.each(centerRegions, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#centerRegions");
        })
        $.each(positions, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#positions");
        })
        $.each(degreeLevels, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#degreeLevels");
        })
        $.each(degreeTypes, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#degreeTypes");
        })
        $.each(degreeDetails, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#degreeDetails");
        })
    }
})