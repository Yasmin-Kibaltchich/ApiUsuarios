using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiUsuarios.Data;
using ApiUsuarios.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiUsuarios.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosDbContext _db;

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
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuarios(Usuarios usuario)
        {
             
           var novoUsuario = _db.Usuarios.Add(usuario);
           await _db.SaveChangesAsync();

           return CreatedAtAction(nameof(GetUsuarios), new {id = usuario.Id}, usuario);
        }  
        

        [HttpPut("{id}")]
        public async Task <IActionResult> PutUsuarios(int id, Usuarios input)
        {
            var usuarioAtualizado = await _db.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
            if(usuarioAtualizado == null)
            {
                return NotFound();
            }

            usuarioAtualizado.Update(input.Nome, input.Sobrenome, input.Email, input.DataNascimento, input.Escolaridade_Id);
            _db.Usuarios.Update(usuarioAtualizado);
            await _db.SaveChangesAsync();
            return Ok();
            
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
            return Ok();
        }

    }
}