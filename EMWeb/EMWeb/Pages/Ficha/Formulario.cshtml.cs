using EMDomain;
using EMRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMWeb.Pages.Editar
{
    public class FormularioModel : PageModel
    {
        private RepositorioAluno _repositorioAluno;

        public FormularioModel(RepositorioAluno repositorioAluno)
        {
            _repositorioAluno = repositorioAluno;
        }

        [BindProperty]
        public Aluno Aluno { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Aluno = new Aluno();
            }
            else
            {
                Aluno = _repositorioAluno.Get("MATRICULA = @p0", new object[] { id });

                if(Aluno == null)
                {
                    return NotFound();
                }

            }

            return Page();


        }


        public async Task<IActionResult> OnPostAsync()
        {
            ValidacaoCamposAluno validacao = new ValidacaoCamposAluno();

            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Aluno.NASCIMENTO = DateTime.Parse(Request.Form["NASCIMENTO"]);

                if (Aluno.MATRICULA == 0)
                {
                    if (validacao.ValidaNomeNulo(Aluno) ||
                       validacao.ValidaDataNascimentoNaoExiste(Aluno) ||
                       validacao.ValidaSexoNulo(Aluno))
                    {
                        TempData["ValidationError"] = "Ocorreu um erro de Javascript, campos não estão validando as informações";
                    }
                    else
                    {
                        if (validacao.ValidaNomeQuantidadeForaPadrao(Aluno))
                        {
                            TempData["ValidationError"] = "Formato inválido, o nome deve ter pelo menos 3 caracteres";
                        }
                        else
                        {
                            if (Aluno.CPF == null)
                            {
                                if (validacao.ValidaDataNascimentoNaoExiste(Aluno))
                                {
                                    TempData["ValidationError"] = "Data no formato inválido, ajuste a data para o formato DD/MM/AAAA";
                                }
                                else
                                {
                                    _repositorioAluno.Add(Aluno);
                                    TempData["SuccessMessage"] = "Dados salvos com sucesso!";
                                }

                            }
                            else
                            {
                                ValidacaoCPF validaCPF = new ValidacaoCPF();

                                if (validaCPF.ValidarCPF(Aluno.CPF))
                                {
                                    if (validacao.ValidaDataNascimentoNaoExiste(Aluno))
                                    {
                                        TempData["ValidationError"] = "Data no formato inválido, ajuste a data para o formato DD/MM/AAAA";
                                    }
                                    else
                                    {
                                        _repositorioAluno.Add(Aluno);
                                        TempData["SuccessMessage"] = "Dados salvos com sucesso!";
                                    }


                                }
                                else
                                {
                                    TempData["ValidationError"] = "CPF inválido";
                                }


                            }
                        }
                        


                        

                    }
                }
                else
                {
                    if (validacao.ValidaNomeNulo(Aluno) ||
                       validacao.ValidaDataNascimentoNaoExiste(Aluno) ||
                       validacao.ValidaSexoNulo(Aluno))
                    {
                        TempData["ValidationError"] = "Ocorreu um erro de Javascript, campos não estão validando as informações";
                    }
                    else
                    {
                        if (validacao.ValidaNomeQuantidadeForaPadrao(Aluno))
                        {
                            TempData["ValidationError"] = "Formato inválido, o nome deve ter pelo menos 3 caracteres";
                        }
                        else
                        {
                            if (Aluno.CPF == null)
                            {
                                if (validacao.ValidaDataNascimentoNaoExiste(Aluno))
                                {
                                    TempData["ValidationError"] = "Data no formato inválido, ajuste a data para o formato DD/MM/AAAA";
                                }
                                else
                                {
                                    _repositorioAluno.Update(Aluno);
                                    TempData["SuccessMessage"] = "Dados Alterados salvos com sucesso!";
                                }

                            }
                            else
                            {
                                ValidacaoCPF validaCPF = new ValidacaoCPF();

                                if (validaCPF.ValidarCPF(Aluno.CPF))
                                {
                                    if (validacao.ValidaDataNascimentoNaoExiste(Aluno))
                                    {
                                        TempData["ValidationError"] = "Data no formato inválido, ajuste a data para o formato DD/MM/AAAA";
                                    }
                                    else
                                    {
                                        _repositorioAluno.Update(Aluno);
                                        TempData["SuccessMessage"] = "Dados Alterados salvos com sucesso!";
                                    }


                                }
                                else
                                {
                                    TempData["ValidationError"] = "CPF inválido";
                                }


                            }
                        }

                        

                    }


                }
            }
            catch (Exception ex)
            {
                TempData["ValidationError"] = $"Ocorreu um erro: {ex}";
            }



            return Page();
            }
        }




}

