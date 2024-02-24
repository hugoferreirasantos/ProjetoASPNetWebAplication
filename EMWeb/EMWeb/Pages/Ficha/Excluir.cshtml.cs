using EMDomain;
using EMRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMWeb.Pages.Ficha
{
    public class ExcluirModel : PageModel
    {
        private RepositorioAluno _alunoRepository;

        public ExcluirModel(RepositorioAluno alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        [BindProperty]
        public Aluno Aluno { get; set; }

        public IActionResult OnGet(int? id)
        {
            Aluno = _alunoRepository.Get("MATRICULA = @p0", new object[] { id });

            if (Aluno == null)
            {
                return NotFound();
            }

            _alunoRepository.Remove(Aluno);

            return RedirectToPage("./Alunos");
        }
    }
}
