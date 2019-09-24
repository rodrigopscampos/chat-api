using System;
using System.Collections.Generic;
using chat_api.Domain.Entities;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;
using MySql.Data.MySqlClient;

namespace chat_api.DAL
{
    public class RepositorioMySql : IRepositorio
    {
        string _connectionString;

        public RepositorioMySql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddMensagem(MensagemInput mensagem)
        {
            using (var conn = CriarNovaConexao())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "insert into mensagens (remetente, destinatario, texto, reservada) values (@remetente, @destinatario, @texto, @reservada)";
                cmd.Parameters.Add(new MySqlParameter("@remetente", mensagem.Remetente));
                cmd.Parameters.Add(new MySqlParameter("@destinatario", mensagem.Destinatario));
                cmd.Parameters.Add(new MySqlParameter("@texto", mensagem.Texto));
                cmd.Parameters.Add(new MySqlParameter("@reservada", mensagem.Reservada));

                cmd.ExecuteNonQuery();
            }
        }

        public bool AddUsuario(UsuarioInput usuario, out string erro)
        {
            try
            {
                using (var conn = CriarNovaConexao())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into usuarios (nome) values (@nome)";
                    cmd.Parameters.Add(new MySqlParameter("@nome", usuario.Nome));
                    cmd.ExecuteNonQuery();
                }

                erro = null;
                return true;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public IEnumerable<Mensagem> GetMensagens(int sequencial)
        {
            using (var conn = CriarNovaConexao())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select id, remetente, destinatario, texto, reservado from mensagens where id > @sequencial";
                cmd.Parameters.AddWithValue("@sequencial", sequencial);

                var resultado = new List<Mensagem>();
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var msg = new Mensagem();
                        msg.Id = Convert.ToInt32(dr["id"]);
                        msg.Rementente = dr["remetente"].ToString();
                        msg.Texto = dr["texto"].ToString();
                        msg.Destinatario = dr["destinatario"].ToString();
                        msg.Reservada = Convert.ToBoolean(dr["reservado"]);

                        resultado.Add(msg);
                    }
                    
                }

                return resultado;
            }
        }

        public IEnumerable<Usuario> GetUsuarios(int sequencial)
        {
            using (var conn = CriarNovaConexao())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "select id, nome from usuarios where id > @sequencial";
                cmd.Parameters.AddWithValue("@sequencial", sequencial);

                var resultado = new List<Usuario>();
                using (var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(dr["id"]);
                        usuario.Nome = dr["nome"].ToString();
                        resultado.Add(usuario);
                    }
                }

                return resultado;
            }
        }


        private MySqlConnection CriarNovaConexao()
        {
            var conn = new MySqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}