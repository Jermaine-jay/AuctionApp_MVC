$(document).ready(function () {
    var countdownElements = $('#countdown');
    var closedcountdownElement = $('#ClosedcountdownElement');
    var isTimerActive = true;

    countdownElements.each(function () {
        var countdownElement = $(this);
        var countdownTime = 3 * 60 * 60;

        function updateCountdown() {
            var hours = Math.floor(countdownTime / 3600);
            var minutes = Math.floor((countdownTime % 3600) / 60);
            var seconds = countdownTime % 60;

            countdownElement.text(hours + ':' + (minutes < 10 ? '0' : '') + minutes + ':' + (seconds < 10 ? '0' : '') + seconds);
            countdownTime--;

        if (countdownTime < 0) {
                clearInterval(timer);
                closedcountdownElement.text('Time expired');

    
            $.ajax({
                url: '/Admin/SaveStatus'+ ProductId,
                method: 'POST',
                data: {IsSold: !IsSold },
                success: function () {},
                error: function () {}
                });
            }
        }

    updateCountdown();

    var timer = setInterval(updateCountdown, 1000);});
});