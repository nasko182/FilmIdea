const showMore = document.getElementById("showMore");
if (showMore != null) {
    showMore.addEventListener("click", function () {
        document.getElementById("bioMore").style.display = "inline";
        document.getElementById("showMore").style.display = "none";
        document.getElementById("showLess").style.display = "inline";
    });

    document.getElementById("showLess").addEventListener("click", function () {
        document.getElementById("bioMore").style.display = "none";
        document.getElementById("showMore").style.display = "inline";
        document.getElementById("showLess").style.display = "none";
    });
}


if (isAdmin == 'True') {
    document.getElementById("addPhotoBtn").addEventListener("click", function () {
        document.getElementById("addPhoto").classList.toggle("hidden");
    });

    document.getElementById("addVideoBtn").addEventListener("click", function () {
        document.getElementById("addVideo").classList.toggle("hidden");
    });
}