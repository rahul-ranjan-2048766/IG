function ToggleAdminSideBar() {
    document.getElementById("adminSidebar").classList.toggle("toggleAdminSideBar");
    document.getElementById("adminMain").classList.toggle("toggleAdminMain");
    document.getElementById("cover").classList.toggle("coverToggle");
    document.getElementById("line_1").classList.toggle("line_1");
    document.getElementById("line_2").classList.toggle("line_2");
}


$(document).ready(() => {
    if ($(window).width() < 991) {
        ToggleAdminSideBar();
    }
    var element = document.getElementById("messageFrame");
    element.scrollTop = element.scrollHeight;

    var elements = document.getElementsByClassName("commentArea");
    for (var i = 0; i < elements.length; i++) {
        elements[i].scrollTop = elements[i].scrollHeight;
    }
});




