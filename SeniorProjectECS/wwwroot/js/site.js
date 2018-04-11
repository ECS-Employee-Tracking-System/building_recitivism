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

//removed position from staffmember
function removePosition(url, id) {
    window.location.href = url + "&PositionID=" + id;
}

//remove completed certification from staffmember
function removeCertification(url, id) {
    window.location.href = url + "&StaffMemberID=" + id;
}

//remove certification from position
function removeCert(url, id) {
    window.location.href = url + "&PositionID=" + id;
}

