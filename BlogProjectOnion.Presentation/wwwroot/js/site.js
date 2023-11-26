// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ViewComments(id) {
    debugger
    console.log('Yorumları görüntüle:', id);
    $.ajax({
        url:"Admin/Post/PostDetailComments/" + id,
        type: "GET",

        success: function (response) {

            $("#commentsView").html(response)

        }


    })


}