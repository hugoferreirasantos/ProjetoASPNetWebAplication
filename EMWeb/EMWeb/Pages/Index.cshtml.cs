using EMDomain;
using EMRepository;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EMWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RepositorioAluno _alunoRepositorio;
        public IEnumerable<Aluno> Alunos { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            RepositorioAluno alunoRepository)
        {
            _logger = logger;
            _alunoRepositorio = alunoRepository;
        }

        public void OnGet()
        {
            Alunos = _alunoRepositorio.GetAll();
        }
    }
}
