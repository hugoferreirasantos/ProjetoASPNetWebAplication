using System;
using System.Text.RegularExpressions;

namespace EMDomain
{
    public class Aluno : IEntidade
    {

        public int MATRICULA { set; get; }
        public string NOME { set; get; }
        public string CPF { set; get; }
        public DateTime NASCIMENTO { set; get; }
        public EnumeradorSexo? SEXO { set; get; }


        public string NASCIMENTO_FORMATADO { get { return NASCIMENTO.ToString("dd/MM/yyyy"); } }
        public string CPF_FORMATADO { get { return CPF.FormatarCPF(); } }






    }

    }

public static class StringExtensions
{
    public static string FormatarCPF(this string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return "";

        var cpfNumeros = Regex.Replace(cpf, @"\D", "");

        if (cpfNumeros.Length != 11)
            return "";


        return $"{cpfNumeros.Substring(0, 3)}.{cpfNumeros.Substring(3, 3)}.{cpfNumeros.Substring(6, 3)}-{cpfNumeros.Substring(9, 2)}";
    }
}

