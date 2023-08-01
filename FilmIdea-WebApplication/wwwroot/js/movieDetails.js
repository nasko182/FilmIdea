// JavaScript to toggle the visibility of the Add review form
$(document).ready(function () {
    const addReviewButton = $("#addReviewButton");
    const addReviewForm = $("#addReviewForm");

    addReviewButton.on("click", function () {
        addReviewForm.toggle();
    });
});