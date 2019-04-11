using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_api.Domain.Entities;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;

namespace chat_api.DAL
{
    public class RepositorioMySql : IRepositorio
    {
        private readonly ILogger<RepositorioMySql> _logger;
        private readonly string _connectionString;

        public RepositorioMySql(string connectionString, ILogger<RepositorioMySql> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public IEnumerable<Mensagem> GetMensagens(int destinatario, int seqnumInicio)
        {
            using (var conn = CriarConexao())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "select id, texto, destinatario from mensagens where destinatario = @destinatario and id > @seqnum";

                cmd.Parameters.Add(new MySqlParameter("@destinatario", destinatario));
                cmd.Parameters.Add(new MySqlParameter("@seqnum", seqnumInicio));

                using (var dr = cmd.ExecuteReader())
                {
                    var resultado = new List<Mensagem>();
                    while (dr.Read())
                    {
                        var m = new Mensagem();
                        m.Id = Convert.ToInt32(dr["id"]);
                        m.Destinatario = Convert.ToInt32(dr["destinatario"]);
                        m.Texto = Convert.ToString(dr["texto"]);
                        m.Rementente = destinatario;

                        resultado.Add(m);
                    }

                    return resultado;
                }
            }
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            using (var conn = CriarConexao())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "select id, nome from usuarios";

                using (var dr = cmd.ExecuteReader())
                {
                    var resultado = new List<Usuario>();
                    while (dr.Read())
                    {
                        var u = new Usuario();
                        u.Id = Convert.ToInt32(dr["id"]);
                        u.Nome = Convert.ToString(dr["nome"]);

                        resultado.Add(u);
                    }

                    return resultado;
                }
            }
        }

        public bool TryAddMensagem(MensagemInput mensagem)
        {
            try
            {
                using (var conn = CriarConexao())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "insert into mensagens (remetente, destinatario, texto) values (@remetente, @destinatario, @texto)";

                    cmd.Parameters.Add(new MySqlParameter("@remetente", mensagem.Remetente));
                    cmd.Parameters.Add(new MySqlParameter("@destinatario", mensagem.Destinatario));
                    cmd.Parameters.Add(new MySqlParameter("@texto", mensagem.Texto));

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "TryAddMensagem({mensagem})", mensagem);
                return false;
            }
        }

        public bool TryAddUsuario(UsuarioInput usuario, out int id)
        {
            try
            {
                using (var conn = CriarConexao())
                using (var tran = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "insert into usuarios (nome) values (@nome)";

                    cmd.Parameters.Add(new MySqlParameter("@nome", usuario.Nome));
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT LAST_INSERT_ID()";

                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        id = Convert.ToInt32(dr[0]);
                    }

                    tran.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "TryAddUsuario({usuario})");

                id = 0;
                return false;
            }
        }

        private MySqlConnection CriarConexao()
        {
            var conn = new MySqlConnection(_connectionString);
            conn.Open();

            return conn;
        }
    }
}