$(document).ready(function () {
    const addReviewButton = $("#addReviewButton");
    const addReviewForm = $("#addReviewForm");
    const editReviewButton = $("#editReviewButton");
    const editReviewForm = $("#editReviewForm");
    const addCommentButton = $("#addCommentButton");
    const addCommentForm = $("#addCommentForm");
    const editCommentButton = $("#editCommentButton");
    const editCommentForm = $("#editCommentForm");
    const addLikeButton = $("#addLikeButton");
    const addDislikeButton = $("#addDislikeButton");
    const deleteReviewButton = $("#deleteReviewButton");
    const deleteCommentButton = $("#deleteCommentButton");

    var movieId = $('#movieId').data('movie');
    var reviewId = $('#reviewId').data('review');
    var commentId = $('#commentId').data('comment');


    addReviewButton.on("click", function () {
        addReviewForm.toggle();
    });

    editReviewButton.on("click", function () {
        editReviewForm.toggle();
    });

    addCommentButton.click(function () {
        addCommentForm.toggle();
    });

    editCommentButton.click(function () {
        editCommentForm.toggle();
    });

    addLikeButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.post(addRemoveLike, {
            reviewId: reviewId,
            movieId: movieId
        })
        location.reload();
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

    deleteReviewButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.post(removeReview, {
            reviewId: reviewId
        })
        location.reload();
    });

    deleteCommentButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.post(removeComment, {
            commentId: commentId
        })
        location.reload();
    });
});