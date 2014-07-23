//loading the DOM into jQuery
$(document).ready(function () {
    //where we put our code
    $('.likes').on('click', function () {
        //when we click run this code
        //getting the id from data-id in our likes div
        var id = $(this).data('id');
        //put likes in a variable
        var likesDiv = $(this);
        //make a request to add a like on a specific post
        $.get('/Home/Like/' + id, function (data) {
            //repalce the html of the like div that was click with a string returned by our GET
            likesDiv.html(data);
        });
    });

    //adding a comment, bind a submit event to the addComment forms
    $('.addComment form').on('submit', function (event) {
        //stop the form from submitting normally
        event.preventDefault();
        //put this into variable
        var theForm = $(this);
        //AJAX post use serialize command to make a string of data
        $.post('/Home/addComment', $(this).serialize(), function (data) {
            theForm.parent().prepend(data);
        });
    });
});