function ToggleSidebar() {
    document.getElementById("sidebar").classList.toggle("toggleSidebar");
    document.getElementById("main").classList.toggle("toggleMain");
    document.getElementById("toggle1").classList.toggle("toggle1");
    document.getElementById("toggle2").classList.toggle("toggle2");
    document.getElementById("togglebox").classList.toggle("toggleboxHeight");
}

$(document).ready(() => {
    if ($(window).width() < 991) {
        ToggleSidebar();        
    }
    try {
        var element = document.getElementById("message");
        element.scrollTop = element.scrollHeight;
    }
    catch {

    }

    try {
        var messageCard = document.getElementById("messageCard");
        messageCard.scrollTop = messageCard.scrollHeight;
    }
    catch {

    }
});