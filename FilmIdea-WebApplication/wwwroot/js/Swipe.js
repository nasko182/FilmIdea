$(document).ready(function () {
    var movies = $('#movieData').data('movies');
    var currentIndex = 0; // Declare and initialize the currentIndex

    function showMovie(index) {
        if (index < movies.length) {
            var movie = movies[index];
            $('.tinder-card-image').attr('src', movie.coverImageUrl);
            $('.tinder-card h3').text(movie.title);
            $('.tinder-card p').text(movie.description);
        }
        else {
            // All movies swiped, show a message or redirect to a new page
            $('.tinder-card').html('<h3>No more movies to swipe!</h3>');
            $('.tinder-buttons').hide();
        }
    }

    function showNextMovie() {
        currentIndex++;
        showMovie(currentIndex);
    }

    $('#left').on('click', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        console.log('Dislike');
        $.post(addToUserWatchlistUrl, {
            movieId: movieId,
        })
            .done(function (data) {
                $('#rateModal').modal('hide');
                location.reload();
            });

        showNextMovie();
    });

    $('#right').on('click', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        console.log('Like');

        showNextMovie();
    });

    // Show the first movie initially
    showMovie(currentIndex);
});
