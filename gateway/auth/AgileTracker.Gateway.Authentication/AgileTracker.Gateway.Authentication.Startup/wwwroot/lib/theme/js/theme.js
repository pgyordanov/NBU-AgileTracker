/*!------------------------------------------------------------------------------------------------------------------ *
 *                                                                                                                    *
 * [General Template Script]                                                                                          *
 *                                                                                                                    *
 * Project      Clarity                                                                                               *
 * Version      3.0                                                                                                   *
 * Description  Responsive Multipurpose Clean and Minimal Site Template                                               *
 *                                                                                                                    *
 * ------------------------------------------------------------------------------------------------------------------ */

/* ------------------------------------------------------------------------------------------------------------------ *
 *                                                                                                                    *
 * Functions                                                                                                          *
 *                                                                                                                    *
 * ------------------------------------------------------------------------------------------------------------------ */

/**
 * Extends Date
 * @returns {number}
 */
Date.prototype.daysInMonth = function() {
    return 33 - new Date(this.getFullYear(), this.getMonth(), 33).getDate();
};

/**
 * Returns the integral quotient
 *
 * @param val
 * @param by
 * @returns {number}
 */
function div(val, by){
    return (val - val % by) / by;
}

/**
 * Returns random number
 *
 * @param min
 * @param max
 * @returns {*}
 */
function getRandomNumber(min, max)  {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}




(function($, $2) {


    var app = {

        gridBreakpoints: {
            xs: {
                min: 0,
                max: 575
            },
            sm: {
                min: 576,
                max: 767
            },
            md: {
                min: 768,
                max: 991
            },
            lg: {
                min: 992,
                max: 1199
            },
            xl: {
                min: 1200,
                max: 100000
            }
        },

        gridYBreakpoints: {
            step: 50,
            prefix: 'height-'
        }

    };







    var $html           = $('html');
    var $body           = $('body');
    var $window         = $(window);
    var $document       = $(document);
    var $bodyHTML       = $('body, html');
    var $overlay        = $('.overlay');

    var $navbar         = $('.navbar');
    var $navbarCollapse = $('.navbar-collapse');

    var navbarHeight = 0;







    /* -------------------------------------------------------------------------------------------------------------- *
     * Additional Functions
     * -------------------------------------------------------------------------------------------------------------- */

    $.fn.removeClassByMask = function(mask) {
        return this.removeClass(function(index, cls) {
            var re = mask.replace(/\*/g, '\\S+');
            return (cls.match(new RegExp('\\b' + re + '', 'g')) || []).join(' ');
        });
    };






    /* -------------------------------------------------------------------------------------------------------------- *
     * Is Mobile
     * -------------------------------------------------------------------------------------------------------------- */

    var isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
    $html.addClass(isMobile ? 'mobile' : 'no-mobile');






    /* -------------------------------------------------------------------------------------------------------------- *
     * Page Scroll
     * -------------------------------------------------------------------------------------------------------------- */

    // TODO:
    //   1. Add top/bottom offsets

    var lastScrollTop = $window.scrollTop();

    var eventScroll = function() {

        // Function variables
        var scrollTop      = $window.scrollTop();
        var windowHeight   = $window.height();
        var documentHeight = $document.height();

        // Page top event
        if (scrollTop === 0) {
            $window.trigger('page:top');
        }

        // Page bottom event
        if (scrollTop + windowHeight >= documentHeight) {
            $window.trigger('page:bottom');
        }

        // Page scroll event
        $window.trigger('page:scroll', {
            percents:  Math.round(scrollTop / (documentHeight - windowHeight) * 100)
        });

        // Scroll direction events
        if (scrollTop > lastScrollTop) {

            $window.trigger('page:scroll-down');

            if (lastScrollTop === 0) {
                $window.trigger('page:top-leave');
            }

        } else {

            $window.trigger('page:scroll-up');

            if (lastScrollTop === (documentHeight - windowHeight)) {
                $window.trigger('page:bottom-leave');
            }
        }

        // Update last scroll position
        lastScrollTop = scrollTop;
    };

    $window.on('scroll', eventScroll);
    $window.on('page:top',          function(e) { $body.addClass('page-top');       });
    $window.on('page:top-leave',    function(e) { $body.removeClass('page-top');    });
    // $window.on('page:bottom',       function(e) { $body.addClass('page-bottom');    });
    // $window.on('page:bottom-leave', function(e) { $body.removeClass('page-bottom'); });

    eventScroll();

    // $window.on('page:scroll-down',  function(e)    { console.log(e.type); });
    // $window.on('page:scroll-up',    function(e)    { console.log(e.type); });
    // $window.on('page:top',          function(e)    { console.log(e.type); });
    // $window.on('page:bottom',       function(e)    { console.log(e.type); });
    // $window.on('page:top-leave',    function(e)    { console.log(e.type); });
    // $window.on('page:bottom-leave', function(e)    { console.log(e.type); });
    // $window.on('page:scroll',       function(e, p) { console.log(e.type + ' percents: ' + p.percents); });







    /* -------------------------------------------------------------------------------------------------------------- *
     * Grid
     * -------------------------------------------------------------------------------------------------------------- */

    function eventGridBreakpoints() {

        var width  = $window.width();
        var height = $window.height();

        for (var infix in app.gridBreakpoints) {

            if (!app.gridBreakpoints.hasOwnProperty(infix)) continue;

            if (width >= app.gridBreakpoints[infix]['min'] && width <= app.gridBreakpoints[infix]['max']) {
                $(window).trigger('grid:' + infix);
                break;
            }
        }

        var k  = div(height, app.gridYBreakpoints.step) + 1;
        var cl = app.gridYBreakpoints.prefix + (k * app.gridYBreakpoints.step);

        if ($body.hasClass(cl)) return;

        $body.removeClassByMask(app.gridYBreakpoints.prefix + '*');
        $body.addClass(cl);

    }

    // Add event listener
    $window.on('resize', eventGridBreakpoints);

    //
    // Grid Breakpoints Events Using:
    // ==============================

    // $window.on('grid:xs', function(e) { console.log(e.type); });
    // $window.on('grid:sm', function(e) { console.log(e.type); });
    // $window.on('grid:md', function(e) { console.log(e.type); });
    // $window.on('grid:lg', function(e) { console.log(e.type); });
    // $window.on('grid:xl', function(e) { console.log(e.type); });

    $window.on('grid:lg', function() {
        $html.removeClass('navbar-open');
    });





    /* -------------------------------------------------------------------------------------------------------------- *
     * Collapse
     * -------------------------------------------------------------------------------------------------------------- */

    $(document).on('show.bs.collapse', '.collapse', function () {
        $('[data-toggle="collapse"][data-target="#' + $(this).attr('id') + '"]').addClass('toggle-in');
    });

    $(document).on('hide.bs.collapse', '.collapse', function () {
        $('[data-toggle="collapse"][data-target="#' + $(this).attr('id') + '"]').removeClass('toggle-in');
    });






    /* -------------------------------------------------------------------------------------------------------------- *
     * Navigation
     * -------------------------------------------------------------------------------------------------------------- */

    /*
    $document
        .on('show.bs.collapse', '.navbar-collapse', function () {
            $body.addClass('fix-scroll');
        })
        .on('hide.bs.collapse', '.navbar-collapse', function () {
            $body.removeClass('fix-scroll');
        });

    var updateNavigation = function (){

        var width = $window.width();

        if ($navbar.hasClass('navbar-expand-lg') && width >= app.gridBreakpoints.lg.min ||
            $navbar.hasClass('navbar-expand-md') && width >= app.gridBreakpoints.md.min ||
            $navbar.hasClass('navbar-expand-sm') && width >= app.gridBreakpoints.sm.min) {
            $body.removeClass('fix-scroll');
        } else {
            if ($navbarCollapse.hasClass('show')) {
                $body.addClass('fix-scroll');
            }
        }

        if (!$navbarCollapse.hasClass('show')) navbarHeight = $navbar.height();
        $navbarCollapse.css('max-height', $window.height() - navbarHeight);
    };

    var resizeTimer;
    $(window).on('resize', function(e) {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(updateNavigation, 250);
    });

    updateNavigation();
    */





    /* -------------------------------------------------------------------------------------------------------------- *
     * Smooth Scroll
     * -------------------------------------------------------------------------------------------------------------- */

    $('.smooth-scroll:not([href="#"])').on('click', function(e) {

        e.preventDefault();

        var $this = $(this),
            target = $this.attr('href');

        if (target === 'undefined') return;

        var $target = $(target);
        if ($target.length === 0) return;

        $.scrollWindow($target.offset().top || 0);

    });

    $.scrollWindow = function(offset) {

        $overlay.fadeIn();
        $bodyHTML.animate({ scrollTop: offset }, 750);
        $overlay.delay(300).fadeOut();

        if ($html.hasClass('mobile')) {
            $('.aside').removeClass('aside-expand');
        }
    };





    /* -------------------------------------------------------------------------------------------------------------- *
     * Tooltips
     * -------------------------------------------------------------------------------------------------------------- */

    $(function () { $('[data-toggle="tooltip"]').tooltip(); });






    /* -------------------------------------------------------------------------------------------------------------- *
     * Appear
     * -------------------------------------------------------------------------------------------------------------- */

    $('.appear').appear();

    $body.on('appear', '.appear', function () {
        $(this).addClass('appear-in');
    });





    /* -------------------------------------------------------------------------------------------------------------- *
     * Animate Numbers
     * -------------------------------------------------------------------------------------------------------------- */

    var $animateNumber = $('.animate-number');
    if ($animateNumber.length > 0) $animateNumber.appear();

    $body.on('appear', '.animate-number', function () {
        $animateNumber.each(function () {
            var $this = $(this);
            if ($this.hasClass('animate-stop')) return;
            $this.animateNumber({ number: $this.attr('data-value') }, 1500);
            $this.addClass('animate-stop');
        });
    });





    /* -------------------------------------------------------------------------------------------------------------- *
     * OWLCarousel2
     * -------------------------------------------------------------------------------------------------------------- */

    function initOwlCarousel($carousel) {

        $carousel.owlCarousel('destroy');

        var owl_parameters = {
            dots: false,
            navText: [
                '<i class="icon ti-angle-left"></i>',
                '<i class="icon ti-angle-right"></i>'
            ]
        };

        var data_items = $carousel.data('owl-items') || 1,
            items_count = parseInt(data_items, 10);

        // Set to config
        owl_parameters['items'] = items_count;

        // Disable mouse drag
        if ($carousel.hasClass('owl-no-mousedrag')) owl_parameters['mouseDrag'] = false;
        // Show prev/next navigation
        if ($carousel.hasClass('owl-navigation')) owl_parameters['nav'] = true;
        // Show dots navigation
        if ($carousel.hasClass('owl-pagination')) owl_parameters['dots'] = true;
        // Loop
        if ($carousel.hasClass('owl-loop')) owl_parameters['loop'] = true;

        // Enable autoplay
        if ($carousel.hasClass('owl-autoplay')) {
            owl_parameters['loop'] = true;
            owl_parameters['autoplay'] = true;
            owl_parameters['autoplayTimeout'] = $carousel.data('owl-autoplay-timeout') || 5000;
        }

        // Responsive Items Count
        var data_items_responsive = $carousel.data('owl-items-responsive');
        if (typeof data_items_responsive !== 'undefined') {

            var arr = data_items_responsive.split(';'),
                responsive = {};

            responsive[1000] = { items: items_count };
            responsive[0] = { items: 1 };

            for (var i = 0, j = arr.length; i < j; i++) {

                var _arr = arr[i].split(':');
                if (typeof _arr[0] === 'undefined' || typeof _arr[1] === 'undefined') continue;

                var max_w = parseInt((_arr[0]).trim(), 10),
                    items_cnt = parseInt((_arr[1]).trim(), 10);

                responsive[max_w] = { items: items_cnt }
            }

            owl_parameters['responsive'] = responsive;
            owl_parameters['responsiveClass'] = true;
        }

        // Custom Animation
        var animate_in = $carousel.attr('data-owl-animate-in'),
            animate_out = $carousel.attr('data-owl-animate-out');

        if (typeof animate_in !== 'undefined') owl_parameters['animateIn'] = animate_in;
        if (typeof animate_out !== 'undefined') owl_parameters['animateOut'] = animate_out;

        // Initialize OWL Carousel
        $carousel.owlCarousel(owl_parameters);
    }

    $('.owl-carousel').each(function() {
        initOwlCarousel($(this));
    });

    //
    // Custom API
    //

    $(document).on('click', '[data-action="owl-prev"]', function() {
        $($(this).attr('data-target')).trigger('prev.owl.carousel');
    });

    $(document).on('click', '[data-action="owl-next"]', function() {
        $($(this).attr('data-target')).trigger('next.owl.carousel');
    });





    /* -------------------------------------------------------------------------------------------------------------- *
     * Magnific Popup
     * -------------------------------------------------------------------------------------------------------------- */

    $('.popup-image').magnificPopup({
        closeBtnInside: true,
        type          : 'image',
        mainClass     : 'mfp-fade',
        gallery       : {
            enabled: true
        }
    });

    $('.popup-iframe').magnificPopup({
        type      : 'iframe',
        mainClass : 'mfp-fade'
    });

    $('.popup-modal').magnificPopup({
        type      : 'inline',
        modal     : true,
        mainClass : 'mfp-fade'
    });

    $(document).on('click', '.popup-modal-dismiss', function (e) {
        e.preventDefault();
        $.magnificPopup.close();
    });





    /* -------------------------------------------------------------------------------------------------------------- *
     * Material Controls
     * -------------------------------------------------------------------------------------------------------------- */

    $('.md-form-control')
        .each(function() {
            var $this = $(this);
            if ($this.val() !== '') $this.parent().addClass('md-completed');
        })
        .on('focus', function() {
            $(this).parent().addClass('focus');
        })
        .on('blur', function() {

            var $this = $(this);
            var $parent = $this.parent();

            $parent.removeClass('focus');

            if ($(this).val() !== '') $parent.addClass('md-completed');
            else $parent.removeClass('md-completed');
        })
        .on('input, change', function() {

            var $parent = $(this).parent();

            if ($(this).val() !== '') $parent.addClass('md-completed');
            else $parent.removeClass('md-completed');
        });





    /* -------------------------------------------------------------------------------------------------------------- *
     * Bootstrap Dropdown Animation
     * -------------------------------------------------------------------------------------------------------------- */

    var $dropdown = $('.dropdown');

    $dropdown.on('show.bs.dropdown', function() {
        $(this).find('.dropdown-menu').first().stop(true, true).fadeIn('fast');
    });

    $dropdown.on('hide.bs.dropdown', function() {
        $(this).find('.dropdown-menu').first().stop(true, true).fadeOut('fast');
    });





    /* -------------------------------------------------------------------------------------------------------------- *
     * Togglers
     * -------------------------------------------------------------------------------------------------------------- */

    $(document).on('click', '[data-toggle="class"]', function(e) {

        e.preventDefault();

        // Current toggler element
        var $this = $(this);
        // Target element
        var tg    = $this.attr('data-target');
        // Class
        var cl    = $this.attr('data-class');
        // Element collection
        var $el;

        // Skip if required attributes not found
        if (typeof tg === 'undefined' || typeof cl === 'undefined') return;

        // Find elements
        $el = $(tg);

        // Return if elements not found
        if ($el.length === 0) return;

        // First element!!!
        if ($el.hasClass(cl)) {

            // Remove class from element
            $el.removeClass(cl);

            // Change togglers classes
            $('[data-target="' + tg + '"]').each(function() {

                var $_this = $(this);

                var togglerClassIn   = $_this.attr('data-toggler-class-in');
                var togglerClassOut  = $_this.attr('data-toggler-class-out');

                // Remove "in"-state class
                if (typeof togglerClassIn !== 'undefined') {
                    $_this.removeClass(togglerClassIn);
                }

                // Add "out"-state class
                if (typeof togglerClassOut !== 'undefined') {
                    $_this.addClass(togglerClassOut);
                }

            });

        } else {

            // Add class to element
            $el.addClass(cl);

            // Change togglers classes
            $('[data-target="' + tg + '"]').each(function() {

                var $_this = $(this);

                var togglerClassIn   = $_this.attr('data-toggler-class-in');
                var togglerClassOut  = $_this.attr('data-toggler-class-out');

                // Remove "out"-state classes
                if (typeof togglerClassOut !== 'undefined') {
                    $_this.removeClass(togglerClassOut);
                }

                // Add "in"-state classes
                if (typeof togglerClassIn !== 'undefined') {
                    $_this.addClass(togglerClassIn);
                }

            });
        }

    });





    /* -------------------------------------------------------------------------------------------------------------- *
     * Parallax Stellar
     * -------------------------------------------------------------------------------------------------------------- */

    if (!isMobile) {

        $2.stellar({
            responsive: true,
            verticalOffset: 0,
            horizontalOffset: 0,
            horizontalScrolling: false,
            hideDistantElements: false
        });
    }





    /* -------------------------------------------------------------------------------------------------------------- *
     * Preloader
     * -------------------------------------------------------------------------------------------------------------- */

    $(window).on('load', function() {

        $html.addClass('loaded');

    });






    /* -------------------------------------------------------------------------------------------------------------- *
     * Search
     * -------------------------------------------------------------------------------------------------------------- */






})(jQuery, jQuery2);
