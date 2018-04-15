//gets filter list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/Reports/GetFilterList',
    data: 'dataList',
    success: function (data) {
        $.each(data, function (index, value) {
            //console.log(index);
           // console.log(value);
            $('#filterSelect').append($('<option>', {
                value: index,
                text: value
            }))
        })
    }
});

//populates all select lists for the filter page
$.ajax({
    dataType: "json",
    url: '/Reports/GetSelectLists',
    data: 'dataList',
    success: function (data) {
        //console.log(data);
        var centerNames = [];
        var centerCounties = [];
        var centerRegions = [];
        var positions = [];
        var degreeAbrvs = [];
        var degreeLevels = [];
        var degreeTypes = [];
        var degreeDetails = [];
        var certs = [];
        $.each(data, function (index, value) {
            if (value.CenterName != undefined) {
                centerNames.push(value.CenterName);
            }
            if (value.CenterCounty != undefined) {
                centerCounties.push(value.CenterCounty);
            }
            if (value.CenterRegion != undefined) {
                centerRegions.push(value.CenterRegion);
            }
            if (value.EducationAbrv != undefined) {
                degreeAbrvs.push(value.EducationAbrv);
            }
            if (value.EducationLevel != undefined) {
                degreeLevels.push(value.EducationLevel);
            }
            if (value.EducationType != undefined) {
                degreeTypes.push(value.EducationType);
            }
            if (value.EducationDetail != undefined) {
                degreeDetails.push(value.EducationDetail);
            }
            if (value.Position != undefined) {
                positions.push(value.Position);
            }
            if (value.CertCompleted != undefined) {
                certs.push(value.CertCompleted);
            }
        })
        centerNames = [...new Set(centerNames)];
        centerCounties = [...new Set(centerCounties)];
        centerRegions = [...new Set(centerRegions)];
        positions = [...new Set(positions)];
        degreeAbrvs = [...new Set(degreeAbrvs)];
        degreeLevels = [...new Set(degreeLevels)];
        degreeTypes = [...new Set(degreeTypes)];
        degreeDetails = [...new Set(degreeDetails)];
        certs = [...new Set(certs)];

        $('<option>').val('').text('').appendTo("#CenterName_0_");
        $.each(centerNames, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#CenterName_0_");
        })

        $('<option>').val('').text('').appendTo("#CenterCounty_0_");
        $.each(centerCounties, function (index, value) {
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

        $('<option>').val('').text('').appendTo("#EducationAbrv_0_");
        $.each(degreeAbrvs, function (index, value) {
            $('<option>').val(value).text(value).appendTo("#EducationAbrv_0_");
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
});

//gets Certifications list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/cert/GetCertificationList',
    data: 'certifications',
    success: function (data) {
        $.each(data, function (index, value) {
            $('<option>').val(value.CertificationID).text(value.CertName).appendTo("#certSelect");
        })
    }
})
//fills the autoslect box for the user to be able to select an available center
$.ajax({
    dataType: "json",
    url: '/Center/GetAllCenters',
    data: 'centers',
    success: function (data) {
        centArray = [];
        $('<option>').val('').text('').appendTo("#centSelect");
        $.each(data, function (index, value) {
            var centersSelect = new Object();
            centersSelect.centerID = value.centerID;
            centersSelect.name = value.name;
            centersSelect.county = value.county;
            centersSelect.region = value.region;
            centArray.push(centersSelect);
            $('<option>').val(value.centerID).text(value.name + ', ' + value.county + ', ' + value.region).appendTo("#centSelect");
            return centArray;
        })
    }
});
