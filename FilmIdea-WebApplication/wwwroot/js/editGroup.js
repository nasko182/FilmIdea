$(document).ready(function () {
    var currentIcon = $("#currentIcon").val();

    $("#Icon option[value='" + currentIcon + "']").prop("selected", true);

    const userList = document.getElementById("usersSection");
    const checkboxes = userList.querySelectorAll("input[type='checkbox']");
    const editGroupButton = document.getElementById("editGroupButton");
    const userSearch = $("#UserSearch");
    const usersSection = $("#usersSection");

    const selectedUserIds = [];

    function updateSelectedUsers() {
        selectedUserIds.length = 0;
        checkboxes.forEach(function (checkbox) {
            if (checkbox.checked) {
                selectedUserIds.push(parseInt(checkbox.value));
            }
        });

        editGroupButton.disabled = selectedUserIds.length === 0;
    }

    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener("change", function () {
            updateSelectedUsers();
        });
    });

    editGroupButton.addEventListener("click", function () {
        const userListInput = $("#UserList");
        userListInput.val(JSON.stringify(selectedUserIds));
        $("#editGroupForm").submit();
    });

    function filterUsers() {
        var searchText = userSearch.val().toLowerCase();

        if (searchText.trim().length > 0) {
            usersSection.show();
        } else {
            usersSection.hide();
        }

        checkboxes.forEach(function (checkbox) {
            var username = checkbox.parentElement.innerText.toLowerCase();

            if (username.includes(searchText)) {
                checkbox.parentElement.style.display = "block";
            } else {
                checkbox.parentElement.style.display = "none";
            }
        });

        updateSelectedUsers();
    }

    userSearch.on("input", function () {
        filterUsers();
    });

    filterUsers();
});
