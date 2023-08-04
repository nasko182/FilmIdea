$(document).ready(function () {
    var movieId = $('#movieId').data('movie');
    var commentId = $('#commentId').data('comment');
    var reviewId = $('#reviewId').data('review');

    const addReviewButton = $("#addReviewButton");
    const addReviewForm = $("#addReviewForm");
    const editReviewButton = $("#editReviewButton-");
    const editReviewForm = $("#editReviewForm-");
    const addCommentButton = $("#addCommentButton");
    const addCommentForm = $("#addCommentForm");
    const editCommentButton = $("#editCommentButton");
    const editCommentForm = $("#editCommentForm");
    const addLikeButton = $("#addLikeButton");
    const addDislikeButton = $("#addDislikeButton");
    const deleteReviewButton = $("#deleteReviewButton");
    const deleteCommentButton = $("#deleteCommentButton");



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

        $.ajax({
            type: 'POST',
            url: addRemoveLike,
            headers: {
                'RequestVerificationToken': csrfToken
            },
            data: {
                reviewId: reviewId,
                movieId: movieId
            }
        });
        location.reload();
    });

    addDislikeButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.ajax({
            type: 'POST',
            url: addRemoveDislike,
            headers: {
                'RequestVerificationToken': csrfToken
            },
            data: {
                reviewId: reviewId,
                movieId: movieId
            }
        });
        location.reload();
    });

    deleteReviewButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.ajax({
            type: 'POST',
            url: removeReview,
            headers: {
                'RequestVerificationToken': csrfToken
            },
            data: {
               reviewId: reviewId
            }
        });
        location.reload();
    });

    deleteCommentButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.ajax({
            type: 'POST',
            url: removeComment,
            headers: {
                'RequestVerificationToken': csrfToken
            },
            data: {
                commentId: commentId
            }
        });
        location.reload();
        
    });
});