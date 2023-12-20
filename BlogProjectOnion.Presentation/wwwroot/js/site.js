// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ViewComments(id) {
    debugger;
    $.ajax({
        url: "Admin/Post/PostDetailComments/" + id,
        type: "GET",

        success: function (response) {

            $("#commentsView").html(response)

        }
    })
}


function ListComments(id) {

    $.ajax({
        url: "/Post/ListComments/" + id,
        type: "GET",

        success: function (response) {
            $("#form-Comment-List").html(""),
                $("#form-Comment-List").html(response)
        }
    })
}


function Sort() {
    debugger;
}

function ListArticles(id) {
    let _id = id;

    $.ajax({
        url: "/Profile/ListPost/" + _id,
        type: "GET",
        success: function (response) {
            $("#profil-Post-List").html("");
            $("#profil-Post-List").html(response);
        }

    });
}


function CommentOnThePost(id) {
    let commentText = $("#message").val();
    let appUserId = $("#userid").val();

    let comment = {
        id: id,
        comment: commentText,
        appuserid: appUserId,
    };

    $.ajax({
        url: "/Post/CommentPost",
        type: "POST",
        data: comment,
        success: function () {
            ListComments(id);
            $("#message").val("");
        },
        error: function (error) {
            $("#message").val("");
            console.error("Yorum gönderme hatası:", error);
        },
    });
}


function GetpostAndUserId(_postid, _userid) {
    let like = {
        postId: _postid,
        userId: _userid,
    };

    $.ajax({
        url: "/Post/AddLike",
        type: "POST",
        data: like,
        dataType: "json",
        success: function () {
            console.log("Başarılı");
        },
        error: function (error) {
            console.log("Başarısız");
        },
    });
}


