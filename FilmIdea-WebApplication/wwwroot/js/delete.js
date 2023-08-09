function showDeleteConfirmation() {
    var result = window.confirm("Are you sure you want to delete this item?");
    if (result) {
        return true;
    } else {
        return false;
    }
}