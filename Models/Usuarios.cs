using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUsuarios.Models
{
    public class Usuarios
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Sobrenome {get; set;}
        public string Email {get; set;}
        public DateTime DataNascimento {get; set;}
        public int Escolaridade_Id {get; set;}
        public int HistoricoEscolar_Id {get; set;}
    }

    public class Escolaridade 
    {
        public int Id {get; set;}
        public string Descricao {get; set;}
        
    }
}