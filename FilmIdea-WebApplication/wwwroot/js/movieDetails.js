$(document).ready(function () {
    var movieId = $('#movieId').data('movie');

    const addReviewButton = $("#addReviewButton");
    const addReviewForm = $("#addReviewForm");
    const editReviewButton = $(".editReviewButton");
    const editReviewForm = $(".editReviewForm");
    const addCommentButton = $(".addCommentButton");
    const addCommentForm = $(".addCommentForm");
    const editCommentButton = $(".editCommentButton");
    const editCommentForm = $(".editCommentForm");
    const addLikeButton = $("#addLikeButton");
    const addDislikeButton = $("#addDislikeButton");
    const deleteReviewButton = $(".deleteReviewButton");
    const deleteCommentButton = $(".deleteCommentButton");



    addReviewButton.on("click", function () {
        addReviewForm.toggle();
    });

    editReviewButton.on("click", function () {
        var reviewId = $(this).data("review-id");

        editReviewForm.hide();
        $(".editReviewForm[data-review-id='" + reviewId + "']").show();
    });

    deleteReviewButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        var reviewId = $(this).data("review-id");

        if (confirm("Are you sure you want to delete this review?")) {
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
        } else {

        }
    });

    addCommentButton.click(function () {
        var reviewId = $(this).data("review-id");

        addCommentForm.hide();
        $(".addCommentForm[data-review-id='" + reviewId + "']").show();
    });

    editCommentButton.click(function () {
        var commentId = $(this).data("comment-id");

        editCommentForm.hide();
        $(".editCommentForm[data-comment-id='" + commentId + "']").show();
    });

    deleteCommentButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        var commentId = $(this).data("comment-id");

        if (confirm("Are you sure you want to delete this comment?")) {
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
        }else{

        }
    });

    addLikeButton.click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        $.ajax({
            type: 'POST',
            url: addRemoveLike,
            headers: {
                RequestVerificationToken: csrfToken
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
                RequestVerificationToken: csrfToken
            },
            data: {
                reviewId: reviewId,
                movieId: movieId
            }
        });
        location.reload();
    });

    document.getElementById("showMore").addEventListener("click", function () {
        document.getElementById("bioMore").style.display = "inline";
        document.getElementById("showMore").style.display = "none";
        document.getElementById("showLess").style.display = "inline";
    });

    document.getElementById("showLess").addEventListener("click", function () {
        document.getElementById("bioMore").style.display = "none";
        document.getElementById("showMore").style.display = "inline";
        document.getElementById("showLess").style.display = "none";
    });
});