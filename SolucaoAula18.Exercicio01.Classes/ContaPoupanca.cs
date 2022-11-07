namespace SolucaoAula18.Exercicio01.Classes
{
    public class ContaPoupanca : Conta
    {
        public ContaPoupanca()
        {
            this.TipoConta = "Poupança";
        }

        public double CalculaRendimento()
        {
            double rendimento = saldo * 0.005;
            return rendimento;
            //A conta poupança rende 0,5%
        }

        public override void Sacar(double saque)
        {
            if (VerificarSaldo(saque))
            {
                saldo = saldo - saque;
            }

        }


    }
}