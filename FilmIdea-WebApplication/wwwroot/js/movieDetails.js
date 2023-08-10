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
    const addLikeButton = $(".addLikeButton");
    const addDislikeButton = $(".addDislikeButton");
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
        var deletedReview = $(this).closest(".review");
        deletedReview.addClass("hidden");
        
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
        var deletedComment = $(this).closest(".comment");
        deletedComment.addClass("hidden");
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
            
        }else{

        }
    });



    addLikeButton.click(function () {
        var reviewId = $(this).data("review-id");
        var likes = document.querySelector(`.likes[data-reviewid="${reviewId}"]`);

        fetch('/Movie/AddRemoveLike?reviewId=' + encodeURIComponent(reviewId), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                RequestVerificationToken: csrfToken
            },
            body: reviewId
            
        })
            .then(response => response.json())
            .then(data => {
                likes.textContent = data.likes;
                location.reload();
            })
            .catch(error => {
                console.error('Fetch error:', error);
            });
    });

    addDislikeButton.click(function () {
        var reviewId = $(this).data("review-id");
        var dislikes = document.querySelector(`.likes[data-reviewid="${reviewId}"]`);

        fetch('/Movie/AddRemoveDislike?reviewId=' + encodeURIComponent(reviewId), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                RequestVerificationToken: csrfToken
            },
            body: reviewId

        })
            .then(response => response.json())
            .then(data => {
                dislikes.textContent = data.dislikes;
                location.reload();
            })
            .catch(error => {
                console.error('Fetch error:', error);
            });
    });
});