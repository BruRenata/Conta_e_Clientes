using System;
using System.Linq;
namespace SolucaoAula18.Exercicio01.Classes
{
    public class Utilitarios
    {
        // No main, criar um menu onde teremos as seguinte opções:
        // 01-Dados das contas
        // 02-Dados dos clientes
        // 03-Total de dinheiro em caixa
        // 04-Total de tributos 
        // 05-Sair

        public static void MenuPricipal()
        {
            bool sair = false;
            while (true)
            {
                Console.WriteLine("Digite o numero da opção desejada");
                Console.WriteLine("01-Dados das contas.");
                Console.WriteLine("02-Dados dos clientes.");
                Console.WriteLine("03-Total de dinheiro em caixa.");
                Console.WriteLine("04-Total de tributos.");
                Console.WriteLine("05-Relatórios.");
                Console.WriteLine("06-Sair.");
                string resposta = Console.ReadLine();
                switch (resposta)
                {
                    case "1":
                        Console.Clear();
                        Utilitarios.MenuDadosConta();
                        break;
                    case "2":
                        Console.Clear();
                        Utilitarios.MenuCliente();
                        break;
                    case "3":
                        Console.Clear();
                        Utilitarios.TotalCaixa();
                        break;
                    case "4":
                        Console.Clear();
                        Utilitarios.TotalTributos();
                        break;
                    case "5":
                        Console.Clear();
                        CrudConta.Relatorios();
                        break;
                    case "6":
                        Console.Clear();
                        sair = true;
                        break;
                }

                if (sair)
                {
                    break;
                }
            }
        }

        public static void TotalCaixa()
        {
            double totalCaixa = 0;
            foreach (Conta conta in CrudConta.contas)
            {
                totalCaixa += conta.saldo;
                if (conta is ContaInvestimento)
                {
                    totalCaixa += (conta as ContaInvestimento).CalculaRendimento();
                }
                if (conta is ContaPoupanca)
                {
                    totalCaixa += (conta as ContaPoupanca).CalculaRendimento();
                }
            }
            Console.WriteLine($"Saldo Total em Caixa {totalCaixa}");
            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        public static void TotalTributos()
        {
            double totalTributos = 0;

            foreach (Conta conta in CrudConta.contas)
            {
                if (conta is ITributos)
                {
                    totalTributos += (conta as ITributos).CalcularTributo();
                }
            }
            Console.WriteLine($"Total de Tributos {totalTributos}");
            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }


        // Se selecionar a opção 1 mostrar o seguinte menu principal:
        // Adicionar conta
        // Editar conta
        // listar todas as contas
        // Consultar conta
        // Excluir conta
        // Voltar
        public static void MenuDadosConta()
        {
            bool sair = false;
            while (true)
            {
                Console.WriteLine("Digite o numero da opção desejada");
                Console.WriteLine("01-Adicionar conta.");
                Console.WriteLine("02-Editar conta.");
                Console.WriteLine("03-Listar todas as contas.");
                Console.WriteLine("04-Consultar conta e movimentar.");
                Console.WriteLine("05-Excluir conta.");
                Console.WriteLine("06-Voltar.");
                string resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":
                        Console.Clear();
                        CrudConta.AdicionarConta();
                        break;
                    case "2":
                        Console.Clear();
                        CrudConta.EditarConta();
                        break;
                    case "3":
                        Console.Clear();
                        CrudConta.ConsultarContas();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Digite o número da conta que deseja pesquisar:");
                        int numeroConta = int.Parse(Console.ReadLine());
                        CrudConta.ConsultarContas(numeroConta);
                        break;
                    case "5":
                        Console.Clear();
                        CrudConta.ExcluirConta();
                        break;
                    case "6":
                        Console.Clear();
                        sair = true;
                        break;
                }

                if (sair)
                {
                    break;
                }
            }
        }

        //Se selecionar a opção 1 do menu de contas, 
        //aparecer um menu solicitando se gostaria de adicionar 
        //uma conta poupança; 
        //uma conta investimento;
        //conta corrente;
        //voltar. 

        public static string MenuAdicionarConta()
        {
            Console.WriteLine("Digite o numero da opção desejada");
            Console.WriteLine("01-Adicionar conta Poupança.");
            Console.WriteLine("02-Adicionar conta Investimento");
            Console.WriteLine("03-Adicionar conta Corrente.");
            Console.WriteLine("04-Voltar.");
            return Console.ReadLine();
        }

        //Se selecionar a opção 5 mostrar o seguinte menu:
        //Grandes contas – Saldo maior que R$ 500.000
        //Correntistas
        //listar todas as contas
        //Consultar conta
        //Excluir conta
        //Voltar

        public static string MenuRelatorios()
        {
            Console.WriteLine("Digite o numero da opção desejada");
            Console.WriteLine("01-Grandes contas – Saldo maior que R$ 500.000");
            Console.WriteLine("02-Correntistas");
            Console.WriteLine("03-Listar todas as contas");
            Console.WriteLine("04-Consultar conta");
            Console.WriteLine("05-Excluir conta");
            Console.WriteLine("06-Voltar");
            return Console.ReadLine();
        }

        public static int RetornarNumeroInteiro(string msgSolicitacao, string msgErro)
        {
            string numeroStr = "";
            int numero = 0;
            while (true)
            {
                Console.WriteLine(msgSolicitacao);
                numeroStr = Console.ReadLine();

                if (!int.TryParse(numeroStr, out numero))
                {
                    Console.WriteLine(msgErro);
                }
                else
                {
                    break;
                }
            }

            return numero;
        }

        public static double RetornarNumeroDouble(string msgSolicitacao, string msgErro)
        {
            string numeroStr = "";
            double numero = 0;
            while (true)
            {
                Console.WriteLine(msgSolicitacao);
                numeroStr = Console.ReadLine();

                if (!double.TryParse(numeroStr, out numero))
                {
                    Console.WriteLine(msgErro);
                }
                else
                {
                    break;
                }
            }

            return numero;
        }

        //Apresentar um menu com as opções de sacar, depositar, saldo. 
        public static void MenuMovimentarConta(Conta conta)
        {
            bool sair = false;
            while (true)
            {
                Console.WriteLine($"Conta {conta.numero};\nAgencia: {conta.agencia};\nNome Corentista: {conta.numero}.");
                Console.WriteLine("Digite o numero da opção desejada");
                Console.WriteLine("01-Sacar.");
                Console.WriteLine("02-Depositar.");
                Console.WriteLine("03-Saldo");
                Console.WriteLine("04-Voltar.");
                string resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":
                        Console.Clear();
                        double saque = Utilitarios.RetornarNumeroDouble("Digite o Valor do Saque:", "Erro no número digitado, digite novamente.");
                        conta.Sacar(saque);

                        //Ao selecionar sacar, solicitar o valor que gostaria sacar, 
                        //alterar o objeto conforme regra de saque estiver correto, 
                        //editar o mesmo e atualizar no conjunto/list conta. 
                        //Informar ao usuário que o saque foi realizado com sucesso e Voltar ao menu conta. 

                        break;

                    case "2":
                        Console.Clear();
                        double deposito = Utilitarios.RetornarNumeroDouble("Digite o Valor do Deposito:", "Erro no número digitado, digite novamente.");
                        conta.Depositar(deposito);

                        //Ao selecionar deposito, solicitar o valor que gostaria depositar, 
                        //alterar o objeto, editar o mesmo e atualizar no conjunto/list conta. 
                        //Informar ao usuário que o deposito foi realizado com sucesso e Voltar ao menu conta. 
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine($" Saldo da conta {conta.numero} - {conta.agencia} é : {conta.saldo}");
                        //saldo
                        //Ao selecionar saldo, informar o saldo e Voltar ao menu conta.

                        break;
                    case "4":
                        Console.Clear();
                        sair = true;
                        break;

                }
                if (sair)
                {
                    break;
                }
            }

        }

        //Se selecionar a opção 2 (MENU PRINCIPAL) mostrar o seguinte menu principal:
        // Adicionar cliente
        // Editar cliente
        // listar todas os clientes
        // Consultar cliente
        // Excluir cliente
        // Voltar

        public static void MenuCliente()

        {
            bool sair = false;
            while (true)
            {
                Console.WriteLine("Digite o número da opção desejada.");
                Console.WriteLine("01- Adicionar Cliente.");
                Console.WriteLine("02- Editar Cliente.");
                Console.WriteLine("03- Listar todos os Clientes.");
                Console.WriteLine("04- Consultar Cliente por CPF.");
                Console.WriteLine("05- Excluir Cliente.");
                Console.WriteLine("06- Voltar");
                string resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":
                        Console.Clear();
                        CrudCliente.AdicionarCliente();
                        break;

                    case "2":
                        Console.Clear();
                        CrudCliente.EditarCliente();
                        break;

                    case "3":
                        Console.Clear();
                        CrudCliente.ListarTodosOsCliente();
                        break;

                    case "4":
                        Console.Clear();
                        CrudCliente.ConsultarCliente();
                        break;

                    case "5":
                        Console.Clear();
                        CrudCliente.ExcluirCliente();
                        break;

                    case "6":
                        Console.Clear();
                        sair = true;
                        break;
                }

                if (sair)
                {
                    break;
                }
            }

        }

    }
}
