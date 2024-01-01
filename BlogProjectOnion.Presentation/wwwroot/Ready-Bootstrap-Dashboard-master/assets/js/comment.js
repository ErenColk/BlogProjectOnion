function ViewComments(id) {

    $.ajax({
        url: '/admin/post/PostDetailComments/' + id,
        type: "GET",

        success: function (response) {
            $("#CommentsView").html(response)
        },
        error: function () {

            console.log("Hata");
        }


    })


}