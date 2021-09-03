using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace HoraExtra.Models
{
    public class Funcionario : Beneficio
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string RG { get; set; }
        public string Endereco { get; set; }
        public float Salario { get; set; }

        [NotMapped]
        public string SalarioMascara { get; set; }


        public Funcionario(string nome, string telefone, string rg, string endereco, float salario)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.RG = rg;
            this.Endereco = endereco;
            this.Salario = salario;

        }

        public Funcionario()
        {
        }

        public float HoraExtra(int hora)
        {
            float porcentagem = 0.05f;

            for(var i = 1; i < hora; i++)
            {
                porcentagem += 0.05f;
            }

            float extra = this.Salario * porcentagem;
            Salario += extra;
            return Salario;
            
        }
    }
}
