using EMDomain;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EMRepository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
        public override void Add(Aluno entity)
        {
            string insertSQL = "INSERT INTO TBALUNO(NOME,CPF,SEXO,DTNASCIMENTO) VALUES(@Nome, @Cpf, @Sexo, @Nascimento)";

            using (FbCommand cmd = new FbCommand(insertSQL, banco.connection))
            {
                cmd.Parameters.AddWithValue("@Nome", entity.NOME.ToUpper());
                if (entity.CPF != null)
                {
                    string cpf = entity.CPF.Replace(".", "").Replace("-", "");
                    cmd.Parameters.AddWithValue("@Cpf", cpf);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cpf", DBNull.Value);
                }
                string nascimentoFormatado = entity.NASCIMENTO.ToString("yyyyMMdd");
                int nascimentoAsInt = int.Parse(nascimentoFormatado);
                cmd.Parameters.AddWithValue("@Nascimento", nascimentoAsInt);
                cmd.Parameters.AddWithValue("@Sexo", entity.SEXO);

                cmd.ExecuteNonQuery();

            }

        }

        public override void Remove(Aluno entity)
        {
            string removeSQL = "DELETE FROM TBALUNO WHERE MATRICULA = @Matricula";

            using (FbCommand cmd = new FbCommand(removeSQL, banco.connection))
            {
                cmd.Parameters.AddWithValue("@Matricula", entity.MATRICULA);
                cmd.ExecuteNonQuery();
            }

        }

        public override void Update(Aluno entity)
        {
            string updateSQL = "UPDATE TBALUNO SET NOME = @Nome, CPF = @Cpf, SEXO = @Sexo, DTNASCIMENTO = @Nascimento WHERE MATRICULA = @Matricula";

            using (FbCommand cmd = new FbCommand(updateSQL, banco.connection))
            {
                cmd.Parameters.AddWithValue("@Nome", entity.NOME.ToUpper());
                if (entity.CPF != null)
                {
                    string cpf = entity.CPF.Replace(".", "").Replace("-", "");
                    cmd.Parameters.AddWithValue("@Cpf", cpf);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cpf", DBNull.Value);
                }
                string nascimentoFormatado = entity.NASCIMENTO.ToString("yyyyMMdd");
                int nascimentoAsInt = int.Parse(nascimentoFormatado);
                cmd.Parameters.AddWithValue("@Nascimento", nascimentoAsInt);


                cmd.Parameters.AddWithValue("@Sexo", entity.SEXO);
                cmd.Parameters.AddWithValue("@Matricula", entity.MATRICULA);

                cmd.ExecuteNonQuery();
            }

        }

        public override IEnumerable<Aluno> GetAll()
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO,CPF FROM TBALUNO A ORDER BY NOME";

            List<Aluno> alunos = new List<Aluno>();

            using (FbCommand cmd = new FbCommand(selectSQL, banco.connection))
            {
                using (FbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Aluno aluno = new Aluno()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            SEXO = (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("DTNASCIMENTO")))
                        {
                            string dtnascimentoString = reader.GetString(reader.GetOrdinal("DTNASCIMENTO"));
                            aluno.NASCIMENTO = DateTime.ParseExact(dtnascimentoString, "yyyyMMdd", CultureInfo.InvariantCulture);
                        }

                        alunos.Add(aluno);
                    }
                }
            }

            return alunos;
        }

        public override Aluno Get(string predicate, params object[] parameters)
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO,CPF FROM TBALUNO A WHERE " + predicate;

            Aluno aluno = null;

            using (FbCommand cmd = new FbCommand(selectSQL, banco.connection))
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@p" + i, parameters[i]);
                }

                using (FbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string dataComoString = reader.GetInt32(reader.GetOrdinal("DTNASCIMENTO")).ToString();
                        aluno = new Aluno()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            SEXO = (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("DTNASCIMENTO")))
                        {
                            string dtnascimentoString = reader.GetString(reader.GetOrdinal("DTNASCIMENTO"));
                            aluno.NASCIMENTO = DateTime.ParseExact(dtnascimentoString, "yyyyMMdd", CultureInfo.InvariantCulture);
                        }
                    }
                }
            }

            return aluno;
        }

        public IEnumerable<Aluno> GetAllGetByMatricula(string matricula)
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO ,CPF  FROM TBALUNO A WHERE CAST(MATRICULA AS VARCHAR(20)) LIKE @matricula ORDER BY NOME";

            List<Aluno> alunos = new List<Aluno>();

            using (FbCommand cmd = new FbCommand(selectSQL, banco.connection))
            {
                cmd.Parameters.AddWithValue("@matricula", "%" + matricula + "%");

                using (FbDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Aluno aluno = new Aluno()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            SEXO = (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("DTNASCIMENTO")))
                        {
                            string dtnascimentoString = reader.GetString(reader.GetOrdinal("DTNASCIMENTO"));
                            aluno.NASCIMENTO = DateTime.ParseExact(dtnascimentoString, "yyyyMMdd", CultureInfo.InvariantCulture);
                        }

                        alunos.Add(aluno);
                    }
                }
            }

            return alunos;
        }

        public IEnumerable<Aluno> GetAllGetByNome(string nome)
        {
            string selectSQL = "SELECT MATRICULA,NOME,SEXO,DTNASCIMENTO ,CPF  FROM TBALUNO A WHERE CAST(NOME AS VARCHAR(100)) LIKE @nome ORDER BY NOME";

            List<Aluno> alunos = new List<Aluno>();

            using (FbCommand cmd = new FbCommand(selectSQL, banco.connection))
            {
                cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");

                using (FbDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Aluno aluno = new Aluno()
                        {
                            MATRICULA = reader.GetInt32(reader.GetOrdinal("MATRICULA")),
                            NOME = reader.IsDBNull(reader.GetOrdinal("NOME")) ? null : reader.GetString(reader.GetOrdinal("NOME")),
                            CPF = reader.IsDBNull(reader.GetOrdinal("CPF")) ? null : reader.GetString(reader.GetOrdinal("CPF")),
                            SEXO = (EnumeradorSexo)reader.GetInt32(reader.GetOrdinal("SEXO"))
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("DTNASCIMENTO")))
                        {
                            string dtnascimentoString = reader.GetString(reader.GetOrdinal("DTNASCIMENTO"));
                            aluno.NASCIMENTO = DateTime.ParseExact(dtnascimentoString, "yyyyMMdd", CultureInfo.InvariantCulture);
                        }

                        alunos.Add(aluno);
                    }
                }
            }

            return alunos;
        }

    }
}
