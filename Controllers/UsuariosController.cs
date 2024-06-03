using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiUsuarios.Data;
using ApiUsuarios.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;

namespace ApiUsuarios.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosDbContext _db;
        private static readonly List<Escolaridade > EscolaridadesValidas = new List<Escolaridade>
        {
            new Escolaridade { Id = 1, Descricao = "Infantil" },
            new Escolaridade { Id = 2, Descricao = "Fundamental" },
            new Escolaridade { Id = 3, Descricao = "Médio" },
            new Escolaridade { Id = 4, Descricao = "Superior" }
        };

        public UsuariosController(UsuariosDbContext db)
        {
            _db = db;
        }

      
        [HttpGet]
        public IActionResult GetLista()
        {
            var getLista = _db.Usuarios.ToList();
            return Ok(getLista);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarios(int id)
        {
            var usuario = await _db.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
            
            if(usuario == null)
            {
                return NotFound("Não encontrado!");
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuarios(Usuarios usuario)
        {
           
           if (!ValidarEmail(usuario.Email))
           {
                return BadRequest("Email Inválido");
           }

           if (!EscolaridadesValidas.Any(e => e.Descricao == usuario.Escolaridade))
           {
                return BadRequest("Escolaridade inválida! Use 1 para nível infantil, 2 para nível fundamental, 3 para nível médio e 4 para nível superior");
           }

           
           if(usuario.DataNascimento > DateTime.Now)
           {
                return BadRequest("Data de nascimento não pode ser maior do que a data atual");
           }  
           
           var novoUsuario = _db.Usuarios.Add(usuario);
           await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsuarios), new {id = usuario.Id}, usuario );
        }


        public static bool ValidarEmail(string email)
        {
            Regex regex = new Regex(@"^(([\w\-\.]+)@(\w+)(\.[a-z]{2,}){1,})|((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])@(\w+)(\.[a-z]{2,}){1,})");
            return regex.IsMatch(email);
        }

    
        [HttpPut("{id}")]
        public async Task <IActionResult> PutUsuarios(int id, Usuarios input)
        {
            if (!ValidarEmail(input.Email))
           {
                return BadRequest("Email Inválido");
           }
            var usuarioAtualizado = await _db.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
            if(usuarioAtualizado == null)
            {
                return NotFound("Não encontrado");
            }

            usuarioAtualizado.Update(input.Nome, input.Sobrenome, input.Email, input.DataNascimento, input.Escolaridade_Id);
            _db.Usuarios.Update(usuarioAtualizado);
            await _db.SaveChangesAsync();
            return Ok("Usuário atualizado com sucesso!");
            
        }
        

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuarios(int id, Usuarios usuario)
        {
            var usuarioDeletado = _db.Usuarios.Where(u => u.Id == id);
            if(usuarioDeletado == null)
            {
                return NotFound();
            }
            _db.Usuarios.Remove(usuario);
            _db.SaveChanges();
            return Ok("Usuário deletado com sucesso!");
        }

    }
}