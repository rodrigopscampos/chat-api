/*
create table usuarios
(
	id int primary key auto_increment,
    nome varchar(200) not null
);

create table mensagens
(
	id int primary key auto_increment,
    remetente int,
    destinatario int,
    texto varchar(10000)
);
*/

insert into usuarios (nome) values ('Rafael');
insert into usuarios (nome) values ('João');
insert into usuarios (nome) values ('José');
insert into usuarios (nome) values ('Leonardo');