$(document).ready(function () {
    var movies = $('#movieData').data('movies');
    var currentIndex = 0; // Declare and initialize the currentIndex

    var shouldReloadPage = false;
    function showMovie(index) {
        if (index < movies.length) {
            var movie = movies[index];
            $('.tinder-card-image').attr('src', movie.coverImageUrl);
            $('.tinder-card h3').text(movie.title);
            $('.tinder-card p').text(movie.description);
            shouldReloadPage = true;
        }
        else {
            // All movies swiped, show a message or redirect to a new page
            if (shouldReloadPage) {
                location.reload();
                shouldReloadPage = false;
            }
            //$('.tinder-card').html('<h3>No more movies available to swipe.</h3><h3>Do you want to start from the beginning?</h3 >');

            $('.tinder-buttons').hide();
        }
    }

    function showNextMovie() {
        currentIndex++;
        showMovie(currentIndex);
    }

    $('#dislike').on('click', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        var movieId = movies[currentIndex].id;

        $.post(addToUserPassedList, {
            movieId: movieId,
        })

        showNextMovie();
    });

    $('#like').on('click', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        var movieId = movies[currentIndex].id;

        $.post(addToUserWatchlistUrl, {
            movieId: movieId,
        })

        $.post(addToUserPassedList, {
            movieId: movieId,
        })

        showNextMovie();
    });

    // Show the first movie initially
    showMovie(currentIndex);
});
