document.getElementById("back")
    .addEventListener('click', voltar)

function voltar() {
    //if(window.screen.width < 992){
    if (document.body.clientWidth < 992) {
        document.getElementById("rightcol").style.left = "100%"
    }
}