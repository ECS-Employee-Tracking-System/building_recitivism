//used to cause default alert for deleteValidate class items
$(function () {
        $('.deleteValidate').click(function () {
            return window.confirm("You are about to delete this item. Are you sure?");
        });
    });