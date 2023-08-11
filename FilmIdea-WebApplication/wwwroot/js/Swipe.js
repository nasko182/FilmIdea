$(document).ready(function () {
    const likeBtns = document.querySelectorAll(".like");
    const dislikeBtns = document.querySelectorAll(".dislike");
    var movies = $('#movieData').data('movies');
    var currentIndex = 0; 

    var shouldReloadPage = false;
    function showMovie(index) {
        if (index < movies.length) {
            var movie = movies[index];
            var rating = parseFloat(movie.rating);
            var formattedRating = (Math.abs(rating - Math.round(rating, 2)) > 0.001) ? rating.toFixed(1) : rating.toFixed(0);
            if (isNaN(parseFloat(formattedRating))) {
                formattedRating = 0;
            }
            var totalMinutes = movie.duration;
            var hours = Math.floor(totalMinutes / 60) + 'h';
            var minutes = (totalMinutes % 60) + 'm';

            var title = movie.title + " (" + movie.releaseYear + ")";

            $('.tinder-card-image').attr('src', movie.coverImageUrl);
            $('.tinder-card-rating').text(formattedRating);
            $('.tinder-card-duration').text(hours +" "+ minutes);
            $('.card-title').text(title);
            $('.tinder-card-description').text(movie.description);
            const trailerBtn = document.getElementById('tinder-card-trailer');
            trailerBtn.setAttribute("href", movie.trailerUrl);
            $("#cardFooter").addClass("hidden");
            $("#tinderButtons").removeClass("hidden");
            shouldReloadPage = true;
        }
        else {
            if (shouldReloadPage) {
                location.reload();
                shouldReloadPage = false;
            }
            
            $('.tinder-buttons').hide();
        }
    }

    function showNextMovie() {
        currentIndex++;
        showMovie(currentIndex);
    }
    dislikeBtns.forEach(function (dislikeBtn) {
        dislikeBtn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();

            var movieId = movies[currentIndex].id;

            $.ajax({
                type: 'POST',
                url: addToUserPassedList,
                headers: {
                    'RequestVerificationToken': csrfToken
                },
                data: {
                    movieId: movieId,
                }
            });

            showNextMovie();
        });
    });

    likeBtns.forEach(function (likeBtn) {
        likeBtn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();

            var movieId = movies[currentIndex].id;

            $.ajax({
                type: 'POST',
                url: addToUserWatchlistUrl,
                headers: {
                    'RequestVerificationToken': csrfToken
                },
                data: {
                    movieId: movieId,
                }
            });

            $.ajax({
                type: 'POST',
                url: addToUserPassedList,
                headers: {
                    'RequestVerificationToken': csrfToken
                },
                data: {
                    movieId: movieId,
                }
            });

            showNextMovie();
        });
    });
    $("#more").click(function () {
        $("#cardFooter").toggleClass("hidden");
        $("#tinderButtons").toggleClass("hidden");
    });

    showMovie(currentIndex);
});
