(function ($) {
    $.fn.responsiveRate = function () {
        var $stars;
        var $container = $(this);
        var ratingId = $container.attr('id');

        for (var i = 0; i < 5; i++) {
            $container.append(`<i class="py-2 px-1 rate-popover" data-index="${i}" data-html="true" data-placement="top"></i>`);
        }
        $container.append(`<div style="display:none;" id="${ratingId}_rating">0</div>`);

        $stars = $container.children();

        if ($container.hasClass('rating-faces')) {
            $stars.addClass('far fa-meh-blank');
        } else if ($container.hasClass('empty-stars')) {
            $stars.addClass('far fa-star');
        } else {
            $stars.addClass('fas fa-star');
        }

        $stars.on('mouseover', function () {
            var index = $(this).attr('data-index');
            markStarsAsActive(index);
        });

        function markStarsAsActive(index) {
            unmarkActive();
            $('#' + ratingId + '_rating').empty();
            var rating = parseInt(index) + 1;
            $('#' + ratingId + '_rating').html(rating);

            for (var i = 0; i <= index; i++) {

                if ($container.hasClass('rating-faces')) {
                    $($stars.get(i)).removeClass('fa-meh-blank');
                    $($stars.get(i)).addClass('live');

                    switch (index) {
                        case '0':
                            $($stars.get(i)).addClass('fa-angry');
                            break;
                        case '1':
                            $($stars.get(i)).addClass('fa-frown');
                            break;
                        case '2':
                            $($stars.get(i)).addClass('fa-meh');
                            break;
                        case '3':
                            $($stars.get(i)).addClass('fa-smile');
                            break;
                        case '4':
                            $($stars.get(i)).addClass('fa-laugh');
                            break;
                    }

                } else if ($container.hasClass('empty-stars')) {
                    $($stars.get(i)).addClass('fas');
                    switch (index) {
                        case '0':
                            $($stars.get(i)).addClass('oneStar');
                            break;
                        case '1':
                            $($stars.get(i)).addClass('twoStars');
                            break;
                        case '2':
                            $($stars.get(i)).addClass('threeStars');
                            break;
                        case '3':
                            $($stars.get(i)).addClass('fourStars');
                            break;
                        case '4':
                            $($stars.get(i)).addClass('fiveStars');
                            break;
                    }
                } else {
                    $($stars.get(i)).addClass('amber-text');

                }
            }
        }

        function unmarkActive() {
            $stars.parent().hasClass('rating-faces') ? $stars.addClass('fa-meh-blank') : $stars;
            $container.hasClass('empty-stars') ? $stars.removeClass('fas') : $container;
            $stars.removeClass('fa-angry fa-frown fa-meh fa-smile fa-laugh live oneStar twoStars threeStars fourStars fiveStars amber-text');
        }
    }

    $.fn.unresponsiveRate = function (rating) {
        var $stars;
        var $container = $(this);

        for (var i = 0; i < 5; i++) {
            $container.append(`<i class="py-2 px-1 rate-popover" data-index="${i}" data-html="true" data-placement="top"></i>`);
        }

        $stars = $container.children();

        if ($container.hasClass('rating-faces')) {
            $stars.addClass('far fa-meh-blank');
        } else if ($container.hasClass('empty-stars-unresponsive')) {
            $stars.addClass('far fa-star');
        } else {
            $stars.addClass('fas fa-star');
        }

        for (var i = 0; i < rating; i++) {

            if ($container.hasClass('rating-faces')) {
                $($stars.get(i)).removeClass('fa-meh-blank');
                $($stars.get(i)).addClass('live');

                switch (rating) {
                    case '1':
                        $($stars.get(i)).addClass('fa-angry');
                        break;
                    case '2':
                        $($stars.get(i)).addClass('fa-frown');
                        break;
                    case '3':
                        $($stars.get(i)).addClass('fa-meh');
                        break;
                    case '4':
                        $($stars.get(i)).addClass('fa-smile');
                        break;
                    case '5':
                        $($stars.get(i)).addClass('fa-laugh');
                        break;
                }

            } else if ($container.hasClass('empty-stars-unresponsive')) {
                $($stars.get(i)).addClass('fas');
                switch (rating) {
                    case '1':
                        $($stars.get(i)).addClass('oneStar');
                        break;
                    case '2':
                        $($stars.get(i)).addClass('twoStars');
                        break;
                    case '3':
                        $($stars.get(i)).addClass('threeStars');
                        break;
                    case '4':
                        $($stars.get(i)).addClass('fourStars');
                        break;
                    case '5':
                        $($stars.get(i)).addClass('fiveStars');
                        break;
                }
            } else {
                $($stars.get(i)).addClass('amber-text');
            }
        }
    }
})(jQuery);