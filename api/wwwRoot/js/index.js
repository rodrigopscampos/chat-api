_usuarios = []
_mensagens = []
_usuario_selecionado = {}
_messages_seqnum = 0
_refresh_interval_ms = 500

window.onload = function () {

    let myId = getMyId()
    if (myId == null) {
        document.location.assign('/login.html');
        return;
    }

    let username = sessionStorage.getItem("MY_NAME");
    document.getElementById('div_name').innerHTML = "Bem-Vindo " + username;

    loadUsers();
    loadMessages();

    start_autorefresh()
}

function getMyId() {
    return sessionStorage.getItem('MY_ID');
}

function start_autorefresh() {
    setInterval(function () {
        loadUsers()
        loadMessages()

    }, _refresh_interval_ms)
}

function loadUsers() {
    fetch('/usuarios?usuarioId=' + getMyId())
        .then(function (response) {
            return response.json();
        }).then(function (usuarios) {

            let atualHash = 0;
            let novoHash = 0;

            if (_usuarios.length > 0)
                atualHash = _usuarios.map(u => u.id).reduce((a, b) => a + b)

            if (usuarios.length > 0)
                novoHash = usuarios.map(u => u.id).reduce((a, b) => a + b)

            if (atualHash != novoHash) {
                
                //bug: se um usuário desloga, ele não some da lista

                usuarios.forEach(u => _usuarios[u.id] = u);
                templateUsuarios()
            }
        }).catch(function (err) {
            console.error(err);
        });
}

function loadMessages() {
    return new Promise((resolve, reject) => {
        fetch('/mensagens?destinatario=' + getMyId() + '&seqnum=' + _messages_seqnum)
            .then(response => response.json())
            .then(msgs => {

                if (msgs.length > 0) {
                    msgs.forEach(m => _mensagens[m.id] = m);

                    if (_mensagens.length > 0) {
                        _messages_seqnum = _mensagens
                            .map(v => v.id)
                            .reduce((a, b) => Math.max(a, b))
                    }

                    renderMessages();
                }


                resolve();
            })
    });
}

function onSendMessage() {
    const message = document.getElementById('mensagem-text').value;

    fetch('/mensagens', {
        headers: { "Content-Type": "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify({
            remetente: getMyId(),
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
    iniciarMensagem()
    _usuario_selecionado = _usuarios[usuarioId]
    renderMessages()

    let friends = document.getElementsByClassName('personitem');
    for (let index = 0; index < friends.length; index++) {
        friends[index].classList.remove('pensonitem__selected')
    }
    
    document.getElementById('friend_' + usuarioId).classList.add('pensonitem__selected')
    document.getElementById('friend_name').innerHTML = _usuario_selecionado.nome
    document.getElementById('friend_iniciais').innerHTML = _usuario_selecionado.iniciais
}

function renderMessages() {

    messages = _mensagens.filter(m =>
        m.destinatario == _usuario_selecionado.id
        || m.remetente == _usuario_selecionado.id);

    paragraphs = messages.map(m => {
        if (m.remetente == getMyId()) {
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

        //<img class="personitem__avatar-image" src="image/svg/man0.svg" alt="foto">
        template += `<li class="personitem" id='friend_${usuario.id}' onclick='onfriend(${usuario.id})'>
                        <div class="personitem__box">
                            <figure class="personitem__avatar">
                                <span class=personitem__avatar-span>${usuario.iniciais}</span>
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

function onMensagemTextKeyPress() {
    
    let keyEnter = 13;
    var key = window.event.keyCode;

    if (key === keyEnter) {
        
        onSendMessage();
    }
};