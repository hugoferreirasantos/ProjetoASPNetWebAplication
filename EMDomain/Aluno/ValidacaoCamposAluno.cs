using System;

namespace EMDomain
{
    public class ValidacaoCamposAluno
    {
        
        public bool ValidaNomeNulo(Aluno nome)
        {

            if (nome.NOME == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidaNomeQuantidadeForaPadrao(Aluno nome)
        { 
            if(nome.NOME.Length <= 2 || nome.NOME.Length > 100 || nome.NOME == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidaDataNascimentoNaoExiste(Aluno dataNascimento)
        {
            DateTime data;

            if(dataNascimento.NASCIMENTO.ToString("yyyyMMdd").Length <= 7 || 
                dataNascimento.NASCIMENTO.ToString("yyyyMMdd").Length > 8 ||
                !DateTime.TryParse(dataNascimento.NASCIMENTO.ToString(), out data) ||
                  dataNascimento.NASCIMENTO == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidaSexoNulo(Aluno sexo)
        {
            if(sexo.SEXO == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
}
