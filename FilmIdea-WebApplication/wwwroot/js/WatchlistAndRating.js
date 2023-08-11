$(document).ready(function () {
    $('#rateModal').on('show.bs.modal', function () {
        $('#ratingValue').val('');
        $('#submitRating').prop('disabled', true);
    });

    $('#ratingValue').on('change', function () {
        var rating = $(this).val();
        $('#submitRating').prop('disabled', rating === '');
    });

    $('.rate-button').on('click', function (e) {
        e.preventDefault();
        var movieId = $(this).data('movie');

        $('#rateModal').modal('show');

        $('#movieId').val(movieId);
    });

    $('.watchlist-button').on('click', function (e) {
        e.preventDefault();
        var movieId = $(this).data('movie');
        $.ajax({
            url: addToUserWatchlistUrl,
            type: 'POST',
            data: {
                movieId: movieId,
            },
            headers: {
                RequestVerificationToken: csrfToken,
            },
            success: function (data) {
                $('#rateModal').modal('hide');
                location.reload();
            },
        });
    });

    $('.added-button').on('click', function (e) {
        e.preventDefault();
        var movieId = $(this).data('movie');
        $.ajax({
            url: removeFromUserWatchlistUrl,
            type: 'POST',
            data: {
                movieId: movieId,
            },
            headers: {
                RequestVerificationToken: csrfToken,
            },
            success: function (data) {
                $('#rateModal').modal('hide');
                hasMovieInWatchlist = false;
                $(this).data('has-movie-in-watchlist', hasMovieInWatchlist);
                location.reload();
            },
        });
    });

    $('#submitRating').on('click', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        var ratingValue = parseInt($('#ratingValue').val());
        var movieId = parseInt($('#movieId').val());

        $.ajax({
            url: addRatingUrl,
            type: 'POST',
            data: {
                movieId: movieId,
                ratingValue: ratingValue,
            },
            headers: {
                RequestVerificationToken: csrfToken,
            },
            success: function (data) {
                var ratingButton = $('.rate-button[data-movie="' + movieId + '"]');
                ratingButton.html('<span>★ ' + ratingValue + '/10</span>');
                $('#rateModal').modal('hide');
            },
        });

    });

    $('#closeModal').on('click', function (e) {
        $('#rateModal').modal('hide');
    });

    $(".movie-card").hover(
        function () {
            $(this).find(".card-footer").fadeIn(200);
        },
        function () {
            $(this).find(".card-footer").fadeOut(200);
        }
    );

});