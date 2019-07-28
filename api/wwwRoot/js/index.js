_usuarios = []
_mensagens = []
_usuario_selecionado = {}

window.onload = function () {

    MY_ID = sessionStorage.getItem('MY_ID');

    if(MY_ID == null)
    {
        document.location.href = '/login.html';
        return;
    }
    
    loadUsers();
    loadMessages();
}

function loadUsers() {
    fetch('/usuarios?usuarioId=' + MY_ID)
        .then(function (response) {
            return response.json();
        }).then(function (usuarios) {
            usuarios.forEach(u => _usuarios[u.id] = u);
            templateUsuarios()
        }).catch(function (err) {
            console.error(err);
        });
}

function loadMessages() {
    return new Promise((resolve, reject) => {
        let seqNum = sessionStorage.getItem('MY_MSG_SEQNUM');
        if(seqNum == null) seqNum = 0;

        fetch('/mensagens?destinatario=' + MY_ID + '&seqnum=' + seqNum)
            .then(response => response.json())
            .then(msgs => {
                msgs.forEach(m => _mensagens[m.id] = m);

                let maxSeqNum = _mensagens[0].id;

                 _mensagens.forEach(m => {
                     if (m.id > maxSeqNum) maxSeqNum = m.id;
                 });

                 sessionStorage.setItem('MY_MSG_SEQNUM', maxSeqNum);

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
    messages = _mensagens.filter(m =>
           m.destinatario == _usuario_selecionado.id 
        || m.remetente == _usuario_selecionado.id);

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
