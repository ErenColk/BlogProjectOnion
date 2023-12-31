

function addFollow(id) {
    debugger
    console.log("addFollowfunction");
    $.ajax({
        url:"/Profile/AddFollow/"+id,
        type: "POST",
        success: function () {
            console.log("Başarılı")
        },
        error: function (error) {
            console.log("Başarısız")
        }
    })
}



function removeFollow(id) {
    $.ajax({
        url: "/Profile/RemoveFollow/" + id,
        type: "POST",
        success: function () {
            console.log("Başarılı")
        },
        error: function (error) {
            console.log("Başarısız")
        }
    })
}



function toggleFollowState(id,element) {

    var followed = element.getAttribute('data-followed') === 'true';

    if (followed) {
        addFollow(id)
        element.innerHTML = `Takip Edildi`
        element.style.backgroundColor = 'green'; 
        element.style.color = 'white'; 
        element.setAttribute('data-followed', 'false');
    }
    else {
        removeFollow(id)
      
        element.innerHTML = `+Takip Et`
        element.style.backgroundColor = 'white';
        element.style.color = '#2c2e2c'; 
        element.setAttribute('data-followed', 'true');
    }


}