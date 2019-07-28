window.onload = function () {

   
}

function logar() {

    event.preventDefault();
    let usuario = document.getElementById('login').value

    fetch('/usuarios', {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: "POST",
        body: JSON.stringify({ nome: usuario })
    }
    ).then(function (response) {
        return response.json();
    }).then(function (json) {
        sessionStorage.setItem("MY_ID", json.id);
        document.location.assign("/index.html"); 
    }).catch(function (err) {
        console.error(err);
    });

    document.getElementsByClassName("login-box")[0].style.display = "none";

    return false;
}