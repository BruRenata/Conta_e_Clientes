using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolucaoAula18.Exercicio01.Classes
{
    public class CrudConta
    {
        private static List<Conta> _contas;

        private static string pathCorrente = "ContaCorrente.txt";
        private static string pathInvestimento = "ContaInvestimento.txt";
        private static string pathPoupanca = "ContaPoupanca.txt";
        public static List<Conta> contas
        {
            get
            {
                if (_contas == null)
                {
                    _contas = new List<Conta>();

                    using (StreamReader leitor = new StreamReader(pathCorrente))
                    {
                        _contas.AddRange(JsonConvert.DeserializeObject<List<ContaCorrente>>(leitor.ReadToEnd()));
                    }

                    using (StreamReader leitor = new StreamReader(pathInvestimento))
                    {
                        _contas.AddRange(JsonConvert.DeserializeObject<List<ContaInvestimento>>(leitor.ReadToEnd()));
                    }

                    using (StreamReader leitor = new StreamReader(pathPoupanca))
                    {
                        _contas.AddRange(JsonConvert.DeserializeObject<List<ContaPoupanca>>(leitor.ReadToEnd()));
                    }

                }
                return _contas;
            }
            set
            {
                _contas = value;
            }
        }


        private static void SalvarArquivo()
        {

            using (StreamWriter escreve = new StreamWriter(pathCorrente))
            {
                escreve.Write(JsonConvert.SerializeObject(_contas.Where(C => C.TipoConta == "Corrente")));
            }

            using (StreamWriter escreve = new StreamWriter(pathInvestimento))
            {
                escreve.Write(JsonConvert.SerializeObject(_contas.Where(C => C.TipoConta == "Investimento")));
            }

            using (StreamWriter escreve = new StreamWriter(pathPoupanca))
            {
                escreve.Write(JsonConvert.SerializeObject(_contas.Where(C => C.TipoConta == "Poupança")));
            }
        }

        private static int UltimoID = 1;

        public static int GetID()
        {
            return UltimoID++;
        }

        //E teremos os seguintes métodos: 
        //adicionar;

        public static void AdicionarConta()
        {
            //solicitar os dados da conta ao usuário, 
            //após todos os dados solicitados o mesmo deverá ser adicionado ao conjuto/lista Conta. 
            //E voltar ao menu das contas.

            Conta conta = null;
            string tipoConta = Utilitarios.MenuAdicionarConta();
            switch (tipoConta)
            {
                case "1":
                    conta = new ContaPoupanca();
                    Console.Clear();
                    break;
                case "2":
                    conta = new ContaInvestimento();
                    Console.Clear();
                    break;
                case "3":
                    conta = new ContaCorrente();
                    Console.Clear();
                    break;
                case "4":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Opção Inválida.");
                    break;
            }

            if (conta != null)
            {
                Console.WriteLine("Digite o cpf do cliente:");
                string cpfConsulta = Console.ReadLine();
                Cliente cliente = CrudCliente.clientes.FirstOrDefault(C => C.cpf == cpfConsulta);
                if (cliente != null)
                {
                    conta.ID = CrudConta.GetID();
                    Console.WriteLine("Informações do Cliente");

                    conta.agencia = Utilitarios.RetornarNumeroInteiro("Digite a Agência:", "Formato Incorreto, tente novamente.");

                    conta.numero = Utilitarios.RetornarNumeroInteiro("Digite o Número da conta:", "Formato Incorreto, tente novamente.");

                    conta.correntista = cliente;

                    Console.WriteLine("Digite o valor de depósito inicial:");
                    conta.Depositar(double.Parse(Console.ReadLine()));

                    contas.Add(conta);
                    SalvarArquivo();
                }
                else
                {
                    Console.WriteLine($"Nenhum cliente cadastrado com o cpf: {cpfConsulta}.");
                }

                Console.WriteLine($"Digite qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
            }

            

        }

        //editar;
        public static void EditarConta()
        {
            //Console.WriteLine("Digite o número da conta que deseja editar:");
            int numeroDaConta = Utilitarios.RetornarNumeroInteiro("Digite o número da conta que deseja editar:", "Digite um número de conta válido");
            //int numeroDaConta = int.Parse(Console.ReadLine());

            foreach (Conta conta in contas.Where(C => C.numero == numeroDaConta))
            {
                Console.WriteLine("Informações Atuais:");
                Console.WriteLine($"Número da conta: {conta.numero}.");
                Console.WriteLine($"Agência: {conta.agencia}.");
                Console.WriteLine($"Correntista: {conta.correntista.nome} - CPF: {conta.correntista.cpf}.");

                Console.WriteLine("Novas Informações:");
                Console.WriteLine("Nova agência:");
                conta.agencia = int.Parse(Console.ReadLine());

                Console.WriteLine("CPF do Novo Correntista:");
                string cpfConsulta = Console.ReadLine();
                Cliente cliente = CrudCliente.clientes.FirstOrDefault(C => C.cpf == cpfConsulta);
                if (cliente != null)
                {
                    conta.correntista = cliente;
                    SalvarArquivo();
                }
                else
                {
                    Console.WriteLine($"Nenhum cliente cadastrado com o cpf: {cpfConsulta}.");
                }
            }

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        //excluir;
        public static void ExcluirConta()
        {
            //Ao selecionar a opção 5 solicitar o número da conta que gostaria de excluir 
            //após a ser digitada, excluir do conjunto/lista a conta informada, 
            //informar ao usuário conta excluída com sucesso e voltar ao menu das contas

            //Console.WriteLine("Digite o número da conta a ser removida:");
            //int numeroConta = int.Parse(Console.ReadLine());
            int numeroConta = Utilitarios.RetornarNumeroInteiro("Digite o número da conta a ser removida:", "Digite um número de conta válido");
            int numeroContasRemovidas = contas.RemoveAll(C => C.numero == numeroConta);
            Console.WriteLine($"Conta removida {numeroConta}");
            SalvarArquivo();

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        //consultar todos.
        public static void ConsultarContas()
        {
            ListarContas(contas, false);
        }

        //consultar por número de conta
        public static void ConsultarContas(int numeroConta)
        {
            if (contas.Any(C => C.numero == numeroConta))
            {
                ListarContas(contas.Where(C => C.numero == numeroConta), true);
                foreach (Conta conta in contas.Where(C => C.numero == numeroConta))
                {
                    Utilitarios.MenuMovimentarConta(conta);
                }

            }

        }

        private static void ListarContas(IEnumerable<Conta> contas, bool mostrarSaldo)
        {
            Console.WriteLine("Informações:\n");
            foreach (Conta conta in contas)
            {
                Console.WriteLine($"Número da Conta: {conta.numero}.");
                Console.WriteLine($"Agência: {conta.agencia}.");
                Console.WriteLine($"Correntista: {conta.correntista.nome} - {conta.correntista.cpf}.");
                Console.WriteLine($"Tipo Conta: {conta.TipoConta}");
                Console.WriteLine();

                if (mostrarSaldo)
                {
                    Console.WriteLine($"Saldo: {conta.saldo}.");
                }
            }

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        //Se selecionar a opção 5 mostrar o seguinte menu:
        //Grandes contas – Saldo maior que R$ 500.000
        //Correntistas
        //listar todas as contas
        //Consultar conta
        //Excluir conta
        //Voltar

        public static void Relatorios()
        {
            string tipoRelatorio = Utilitarios.MenuRelatorios();

            switch (tipoRelatorio)
            {
                case "1":
                    Console.Clear();
                    GrandesContas();
                    break;

                case "2":
                    Console.Clear();
                    TodasAsContas();
                    break;

                case "3":
                    Console.Clear();
                    ListarContas(contas, false);
                    break;

                case "4":
                    Console.Clear();
                    ConsultarContas();

                    break;

                case "5":
                    ExcluirConta();

                    break;

                case "6":
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Opção Invalida");
                    break;
            }

        }

        public static void GrandesContas()
        {
            Dictionary<string, Conta> GrandesContas = new Dictionary<string, Conta>();

            foreach (Conta conta in contas.Where(C => C.saldo >= 500000.00))
            {
                GrandesContas.Add(conta.correntista.cpf, conta);
            }

            foreach (KeyValuePair<string, Conta> dicionario in GrandesContas)
            {
                Console.WriteLine("Agencia\t|\tNúmero ");
                Console.WriteLine($"{dicionario.Value.agencia}\t|\t{dicionario.Value.numero}");
                Console.WriteLine();
            }

            Console.WriteLine($"Numero de contas encontradas {GrandesContas.Count}");

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        public static void TodasAsContas()
        {

            foreach (Conta conta in contas.OrderBy(C => C.correntista.nome))
            {
                Console.WriteLine("Correntista |Agencia | Número ");
                Console.WriteLine($"{conta.correntista.nome} | {conta.agencia} | {conta.numero}");
            }

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();

        }



    }
}