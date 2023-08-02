$(document).ready(function () {
    const addReviewButton = $("#addReviewButton");
    const addReviewForm = $("#addReviewForm");
    const addCommentButton = $("#addCommentButton");
    const addCommentForm = $("#addCommentForm");

    addReviewButton.on("click", function () {
        addReviewForm.toggle();
    });

    addReviewForm.on("submit", function () {
        location.reload();
    });
    addCommentButton.click(function () {
        addCommentForm.toggle();
    });

    //addCommentForm.on("submit", function () {
    //    location.reload();
    //});
});