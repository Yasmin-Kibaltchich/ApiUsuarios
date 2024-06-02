using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiUsuarios.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUsuarios.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController
    {
        private readonly UsuariosDbContext _db;

        public UsuariosController(UsuariosDbContext db)
        {
            _db = db;
        }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return (IActionResult)_db.Usuarios.ToListAsync();

    }

    [HttpGet("{id}")]
    public IActionResult GetUsuarios(int id)
    {
        var usuario =  _db.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
        
        if(usuario == null)
        {
            return NotFound();
        }
        return new OkResult();
    }

    

    }
}
