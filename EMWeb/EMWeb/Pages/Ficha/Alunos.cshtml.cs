using EMDomain;
using EMRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMWeb.Pages
{
    public class AlunosModel : PageModel
    {
        private readonly ILogger<AlunosModel> _logger;
        private readonly RepositorioAluno _alunoRepositorio;
        public IEnumerable<Aluno> Alunos { get; private set; }

        public AlunosModel(ILogger<AlunosModel> logger,
            RepositorioAluno alunoRepository)
        {
            _logger = logger;
            _alunoRepositorio = alunoRepository;
        }

        public void OnGet()
        {
            try
            {
                Alunos = _alunoRepositorio.GetAll();
            }
            catch(Exception ex)
            {
                TempData["ValidationError"] = $"Ocorre um erro: {ex}";
            }
            
        }

        public async Task<IActionResult> OnPostAsync(string opcaoBusca, string valorBusca)
        {
            try
            {
                if (opcaoBusca == null && valorBusca == null)
                {
                    Alunos = _alunoRepositorio.GetAll();
                }
                else
                {
                    switch (opcaoBusca)
                    {
                        case "matricula":
                            Alunos = _alunoRepositorio.GetAllGetByMatricula(valorBusca);
                            break;
                        case "nome":
                            Alunos = _alunoRepositorio.GetAllGetByNome(valorBusca.ToUpper());
                            break;
                        default:
                            Alunos = _alunoRepositorio.GetAll();
                            break;
                    }
                }
                

            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                {
                    Alunos = _alunoRepositorio.GetAll();
                }
                else
                {
                    TempData["ValidationError"] = $"Ocorreu um erro: {ex}";
                }
                

            }

            return Page();
        }


    }
}
