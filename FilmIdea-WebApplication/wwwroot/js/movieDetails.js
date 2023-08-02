$(document).ready(function () {
    const addReviewButton = $("#addReviewButton");
    const addReviewForm = $("#addReviewForm");
    const addCommentButton = $("#addCommentButton");
    const addCommentForm = $("#addCommentForm");
    const addLikeButton = $("#addLikeButton");
    const addDislikeButton = $("#addDislikeButton");
    var movieId = $('#movieId').data('movie');
    var reviewId = $('#reviewId').data('review');


    addReviewButton.on("click", function () {
        addReviewForm.toggle();
    });

    addCommentButton.click(function () {
        addCommentForm.toggle();
    });

    addLikeButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.post(addRemoveLike, {
            reviewId: reviewId,
            movieId: movieId
        })
        location.reload();
    });

    addDislikeButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.post(addRemoveDislike, {
            reviewId: reviewId,
            movieId: movieId
        })
        location.reload();

    });
});