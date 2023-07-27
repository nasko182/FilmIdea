$(document).ready(function () {
    $('.rate-button').on('click', function (e) {
        e.preventDefault();
        var movieId = $(this).data('movie');
        var ratingButton = $(this);

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
        var ratingValue = $('#ratingValue').val();
        var movieId = $('#movieId').val();

        $.post('@Url.Action("AddRating", "Movie")', {
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
