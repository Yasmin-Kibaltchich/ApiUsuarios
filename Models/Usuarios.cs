using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

    public class ValidadorEmail
    {
    public static bool ValidarEmail(string email)
    {   
        Regex regex = new Regex(@"^[a-z0-9.+_-]+@[a-z0-9.-]+\.[a-z]{2,3}$");
        return regex.IsMatch(email);
       
    }
    }
        public void Update(string nome, string sobrenome, string email, DateTime datanascimento, int escolaridade_id)

    {
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;
        DataNascimento = datanascimento;
        Escolaridade_Id = escolaridade_id;
    }
        
    
    }

    public class Escolaridade 
    {
        public int Id {get; set;}
        public string Descricao {get; set;}

        
        
        public void Update(string descricao)
        {
             Descricao = descricao;
        }
    }
    

}