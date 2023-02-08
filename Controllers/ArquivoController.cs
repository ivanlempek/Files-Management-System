using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NOVAteste.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using NOVAteste.Data;

namespace NOVAteste.Controllers
{
    public class ArquivoController : Controller
    {
        DataContext _dataContext;

        public ArquivoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            /*return View(await _dataContext.Arquivos.ToListAsync());*/
            var arquivos = _dataContext.Arquivo.ToList();
            return View(arquivos);
        }
   
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _dataContext.Arquivo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Status")] IList<IFormFile> arquivos, string descString, string statusString )
        {
            IFormFile imagemCarregada = arquivos.FirstOrDefault();

            if (imagemCarregada != null)
            {
                MemoryStream ms = new MemoryStream();
                imagemCarregada.OpenReadStream().CopyTo(ms);


                Arquivo arqui = new Arquivo()
                {
                    Nome = imagemCarregada.FileName,
                    Descricao = descString,
                    Status = statusString,
                    Dados = ms.ToArray(),
                };

                string caminhoSalvar = "\\arquivos\\";
                string novoNome = Guid.NewGuid().ToString() + "_" + imagemCarregada.FileName;

                if (!Directory.Exists(caminhoSalvar))
                {
                    Directory.CreateDirectory(caminhoSalvar);
                }
                using (var stream = System.IO.File.Create(caminhoSalvar + novoNome))
                {
                    imagemCarregada.CopyToAsync(stream); 
                }

                _dataContext.Add(arqui);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            }
            return View(arquivos);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arquivos = await _dataContext.Arquivo.FindAsync(id);
            if (arquivos == null)
            {
                return NotFound();
            }
            return View(arquivos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Status")] Arquivo arquivos)
        {

            if (id != arquivos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(arquivos);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(arquivos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(arquivos);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _dataContext.Arquivo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _dataContext.Arquivo.FindAsync(id);
            _dataContext.Arquivo.Remove(categories);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesExists(int id)
        {
            return _dataContext.Arquivo.Any(e => e.Id == id);
        }

        public IActionResult Visualizar(int id)
        {
            var arquivosBanco = _dataContext.Arquivo.FirstOrDefault(a => a.Id == id);

            return File(arquivosBanco.Dados, arquivosBanco.Descricao);
        }
        public FileResult Download(int id)
        {
            if (id == 0)
                return null;

            var fileResult = _dataContext.Arquivo.FirstOrDefault(a => a.Id == id);

            //Se o conteúdo está armazenado no banco de dados:
            return File(fileResult.Dados, System.Net.Mime.MediaTypeNames.Application.Octet, fileResult.Nome);

            //Se conteúdo está no arquivo armazenado em diretório:
            /*return File(File.ReadAllBytes(fileResult.EnderecoAnexo), System.Net.Mime.MediaTypeNames.Application.Octet, fileResult.NomeAnexo);*/
        }
    }
}
