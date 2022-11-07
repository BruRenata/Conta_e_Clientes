namespace SolucaoAula18.Exercicio01.Classes
{
    public class ContaCorrente : Conta, ITributos
    {
        public ContaCorrente()
        {
            this.TipoConta = "Corrente";
        }

        public double CalcularTributo()
        {
            double tributos = saldo * 0.10;
            //10% de tributos
            return tributos;
        }

        public override void Sacar(double saque)
        {
            if (VerificarSaldo(saque))
            {
                saldo = saldo - saque;
            }
            //Ao selecionar sacar, solicitar o valor que gostaria sacar, 
            //alterar o objeto conforme regra de saque estiver correto, 
            //editar o mesmo e atualizar no conjunto/list conta. 
            //Informar ao usuário que o saque foi realizado com sucesso e Voltar ao menu conta.
        }
    }
}