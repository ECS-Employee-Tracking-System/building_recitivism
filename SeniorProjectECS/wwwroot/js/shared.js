//used to cause default alert for deleteValidate class items
$(function () {
        $('.deleteValidate').click(function () {
            return window.confirm("You are about to delete this item. Are you sure?");
        });
});
function findAndReplace(string, target, replacement) {

    var i = 0, length = string.length;
    for (i; i < length; i++) {
        string = string.replace(target, replacement);
    }
    return string;
}