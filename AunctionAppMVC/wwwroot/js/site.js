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


function toggleAnchorLinks() {
    const anchorLinks = document.querySelector('.anchor-links');
    const displayStyle = anchorLinks.style.display;
    if (displayStyle === 'none') {
        anchorLinks.style.display = 'block';
    } else {
        anchorLinks.style.display = 'none';
    }
}


const imageToChange = document.getElementById('imageToChange');
const fileInput = document.getElementById('fileInput');
const form = document.getElementById('form-file');

imageToChange.addEventListener('click', () => {
    fileInput.click();
});

fileInput.addEventListener('change', () => {
    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = async (event) => {
            // Preview the selected image
            //imageToChange.src = reader.result;
            imageToChange.src = event.target.result;
        form.append(fileInput.files[0])
        };
        reader.readAsDataURL(fileInput.files[0]);
    }
});
