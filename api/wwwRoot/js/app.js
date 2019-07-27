BASE_URL = 'http://localhost:5000'
URL_LOGIN = BASE_URL + '/login.html'

//serão carregadas do servidor no onload
URL_USUARIOS = ''
URL_MENSAGENS = ''
URL_MENSAGENS_POST = ''

_usuarios = []
_mensagens = []
_usuario_selecionado = {}

window.onload = function () {

    let location = localStorage.getItem('MY_LOCATION');
    if (location == null) {
        console.log(location)
        document.location = URL_LOGIN;
    }
    else {
        MY_ID = localStorage.getItem('MY_ID');
        URL_MENSAGENS = BASE_URL + '/mensagens?destinatario=' + MY_ID + '&seqnum=0';
        URL_MENSAGENS_POST = BASE_URL + '/mensagens';
        URL_USUARIOS = BASE_URL + '/usuarios';
        loadUsers();
        loadMessages();
    }
}

function loadUsers() {
    fetch(URL_USUARIOS)
        .then(function (response) {
            return response.json();
        }).then(function (usuarios) {
            usuarios
                .filter(u => u.id != MY_ID)
                .forEach(u => _usuarios[u.id] = u);
            templateUsuarios()
        }).catch(function (err) {
            console.error(err);
        });
}

function loadMessages() {
    return new Promise((resolve, reject) => {
        fetch(URL_MENSAGENS)
            .then(response => response.json())
            .then(msgs => {
                msgs.forEach(m => _mensagens[m.id] = m);
                resolve();
            })
    });
}

function onSendMessage() {
    const message = document.getElementById('mensagem-text').value;

    fetch(URL_MENSAGENS_POST, {
        headers: { "Content-Type": "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify({
            remetente: MY_ID,
            texto: message,
            destinatario: _usuario_selecionado.id
        })
    })
        .then(() => {
            document.getElementById('mensagem-text').value = "";
            loadMessages().then(() => renderMessages());
        });
}

//ao clicar em um item na lista de amigos
function onfriend(usuarioId) {
    _usuario_selecionado = _usuarios[usuarioId]
    renderMessages()
}

function renderMessages() {
    console.debug('render');
    messages = _mensagens.filter(m => m.destinatario == _usuario_selecionado.id || m.remetente == _usuario_selecionado.id);

    paragraphs = messages.map(m => {
        if (m.remetente == MY_ID) {
            return `<p class="talkimessage">${m.texto}</p>`
        }
        else {
            return `<p class="talkyoumessage">${m.texto}</p>`
        }
    });

    document.getElementById('talkmessages').innerHTML = paragraphs.join('\n');
    scrollDiv = document.getElementById('talkboxscroll');
    scrollDiv.scroll(0, scrollDiv.scrollHeight);
}


function templateUsuarios() {

    let template = "";

    _usuarios.forEach(usuario => {

        template += `<li class="personitem" onclick='onfriend(${usuario.id})'>
                        <div class="personitem__box">
                            <figure class="personitem__avatar">
                                <img class="personitem__avatar-image" src="image/svg/man0.svg" alt="foto">
                            </figure>
                        </div>

                        <div class="personitem__detail">
                            <div class="personitem__name">
                                <h4>${usuario.nome}</h4>
                            </div>
                            <div class="personitem__hour">
                                10:58
                            </div>
                        </div>
                    </li>`
    });

    lista_usuarios = document.getElementById('lista-usuarios');
    if (lista_usuarios != null) lista_usuarios.innerHTML = template
}

function logar() {

    event.preventDefault();
    let usuario = document.getElementById('login').value

    fetch(URL_USUARIOS, {
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
        localStorage.setItem("MY_ID", json.id);
        localStorage.setItem("MY_TOKEN", json.token);
        localStorage.setItem("MY_LOCATION", json.location);
        document.location = json.location + "/index.html";
    }).catch(function (err) {
        console.error(err);
    });

    document.getElementsByClassName("login-box")[0].style.display = "none";

    return false;
}