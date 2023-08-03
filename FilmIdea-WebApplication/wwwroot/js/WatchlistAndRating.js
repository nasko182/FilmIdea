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

        $.post(addToUserWatchlistUrl, {
            movieId: movieId,
        })
            .done(function (data) {
                $('#rateModal').modal('hide');
                location.reload();
            });
    });

    $('.added-button').on('click', function (e) {
        e.preventDefault();
        var movieId = $(this).data('movie');

        $.post(removeFromUserWatchlistUrl, {
            movieId: movieId,
        })
            .done(function (data) {
                $('#rateModal').modal('hide');
                hasMovieInWatchlist = false;
                $(this).data('has-movie-in-watchlist', hasMovieInWatchlist);
                location.reload();
            });
    });

    $('#submitRating').on('click', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        var ratingValue = parseInt($('#ratingValue').val());
        var movieId = parseInt($('#movieId').val());

        $.post(addRatingUrl, {
            movieId: movieId,
            ratingValue: ratingValue,
        })
            .done(function (data) {
                var ratingButton = $('.rate-button[data-movie="' + movieId + '"]');
                ratingButton.html('<span>★ ' + ratingValue + '/10</span>');
                $('#rateModal').modal('hide');
            })
            .fail(function (error) {
                alert(error.message);
            });
    });

    $('#closeModal').on('click', function (e) {
        $('#rateModal').modal('hide');
    });

});
