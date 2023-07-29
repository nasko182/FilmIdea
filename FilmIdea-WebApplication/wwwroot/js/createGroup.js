$(document).ready(function () {
    $("#UserSearch").on("input", function () {
        var searchText = $(this).val().toLowerCase();
        var usersSection = $("#usersSection");

        if (searchText.trim().length > 0) {
            usersSection.show();
        } else {
            usersSection.hide();
        }

        $("#Users option").each(function () {
            var userOption = $(this);
            var username = userOption[0].innerText.toLowerCase();

            if (username.includes(searchText)) {
                userOption.show();
            } else {
                userOption.hide();
            }
        });

        updateCreateButtonState();
    });

    $("#Users").on("change", function () {
        var selectedIds = $(this).val();
        var userListInput = $("#UserList");
        userListInput.val(JSON.stringify(selectedIds));

        updateCreateButtonState();
    });

    function updateCreateButtonState() {
        var selectedUsersCount = $("#Users").val()?.length ?? 0;
        var createButton = $("#createGroupButton");

        if (selectedUsersCount > 0) {
            createButton.prop("disabled", false);
        } else {
            createButton.prop("disabled", true);
        }
    }
});
