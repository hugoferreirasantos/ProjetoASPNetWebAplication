

using System;

namespace EMDomain
{
    public interface IEntidade
    {

        int MATRICULA { set; get; }
        string NOME { set; get; }

        string CPF { set; get; }
        DateTime NASCIMENTO { set; get; }
        EnumeradorSexo? SEXO { set; get; }
    }
}
