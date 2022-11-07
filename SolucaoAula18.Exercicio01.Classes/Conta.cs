using System;
namespace SolucaoAula18.Exercicio01.Classes
{
    public abstract class Conta
    {
        //Conta tem 
        //agencia 
        //numero
        //corentista (Cliente)
        //saldo 
        //Quantidade de Contas (id)
        public int agencia { get; set; }
        public int numero { get; set; }
        public Cliente correntista { get; set; }
        public double saldo { get; set; }
        public int ID { get; set; }

        public string TipoConta { get; set; }

        public virtual void Sacar(double saque)
        {
            if (VerificarSaldo(saque))
            {
                saldo = saldo - saque;
            }
        }
        public bool VerificarSaldo(double saque)
        {
            if (saque <= saldo)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Saldo Insuficiente.");
                return false;
            }

        }

        public void Depositar(double deposito)
        {
            if (deposito != 0 && deposito > 0)
            {
                saldo = saldo + deposito;
            }
            else
            {
                Console.WriteLine("Deposito não pode ser negativo.");
            }
        }

        public int QuantidadeContaAtual()
        {
            return CrudConta.contas.Count;
        }

    }
}