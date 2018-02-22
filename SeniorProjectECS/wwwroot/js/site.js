//checks if member owes money for tuition assistance before deleteing and then warns user to take external action
function validateUpdate() {
    var isInactive = document.getElementById('IsInactive');
    var classPaid = document.getElementById('ClassPaid');
    var firstName = document.getElementById('FirstName').value;
    var lastName = document.getElementById('LastName').value;
    if (isInactive.checked && classPaid.checked) {
        alert("Contact HR, " + firstName + " " + lastName + " Owes class funding");
    }//end if
}//end validateUpdate function

//Pust NA in CDA blocks if person doesnot require CDA
function naCDA() {
    document.getElementById('CDAInProgress').checked = false;
    document.getElementById('CDAType').value = 'NA';
    document.getElementById('CDARenewalProcess').value = 'NA';
    document.getElementById('CDAExpiration').value = "";
}//end naCD function

//checks to make sure staffmember is deactivated before deleting
function validateDelete(url) {
    var isInactive = document.getElementById('IsInactive');
    if (!isInactive.checked) {
        alert("Staff member is not deactivated! Please deactivate and try again.");
    } else {
        if (confirm("This action cannot be undone, continue?")) {
            window.location.href = url;
        }
    }//end if/else
}//end validateDelete

//removes education from staffmember
function removeEducation(url, id) {
    window.location.href = url + "&EducationID=" + id;
}//end removeEducation function

function removePosition(url, id) {
    window.location.href = url + "&PositionID=" + id;
}

//---------------------------------------AJAX AutoFills----------------------------------
//gets DegreeAbrrev list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/StaffMember/GetDegreeAbrvList',
    data: 'degreeAbrvlist',
    success: function (data) {
        //console.log(data);
        $.each(data, function (index, v) {
            $.each(v, function (index, value) {
                //console.log(index, value);
                $('<option>').val(value).text(value).appendTo("#degreeSelect");
            })
        })
    },
});

//gets Degreelevel list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/StaffMember/GetDegreeLevelList',
    data: 'degreeLevellist',
    success: function (data) {
        //console.log(data);
        $.each(data, function (index, v) {
            $.each(v, function (index, value) {
                //console.log(index, value);
                $('<option>').val(value).text(value).appendTo("#degreeLevel");
            })
        })
    },
});

//gets DegreeType list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/staffmember/GetDegreeTypeList',
    data: 'degreeTypelist',
    success: function (data) {
        //console.log(data);
        $.each(data, function (index, v) {
            $.each(v, function (index, value) {
                //console.log(index, value);
                $('<option>').val(value).text(value).appendTo("#degreeType");
            })
        })
    },
});

//gets DegreeDetail list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/staffMember/GetDegreeDetailList',
    data: 'degreeDetaillist',
    success: function (data) {
        //console.log(data);
        $.each(data, function (index, v) {
            $.each(v, function (index, value) {
                //console.log(index, value);
                $('<option>').val(value).text(value).appendTo("#degreeDetail");
            })
        })
    },
});

//gets position list from database and puts in a select list
$.ajax({
    dataType: "json",
    url: '/staffMember/GetPositionList',
    data: 'position',
    success: function (data) {
        //console.log(data);
        $.each(data, function (index, v) {
            $.each(v, function (index, value) {
                //console.log(index, value);
                $('<option>').val(value).text(value).appendTo("#Position");
            })
        })
    },
});


//gets Centers list from database and puts in a select list (only in create function for right now)
$.ajax({
    dataType: "json",
    url: '/staffMember/GetCenterList',
    data: 'centers',
    success: function (data) {
        //console.log(data);
        $.each(data, function (index, value) {
            //console.log(value.name);
            $('<option>').val(value.name).text(value.name).appendTo("#CenterName");
            $('<option>').val(value.county).text(value.county).appendTo("#CenterCounty");
            $('<option>').val(value.region).text(value.region).appendTo("#CenterRegion");
        })
    },
});

//------------------------------------End AJAX AutoFills----------------------------------