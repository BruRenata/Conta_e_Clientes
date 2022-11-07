using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolucaoAula18.Exercicio01.Classes
{
    public class CrudCliente

    {
        private static List<Cliente> _clientes;

        private static string path = "Clientes.txt";
        public static List<Cliente> clientes
        {
            get
            {
                if(_clientes == null)
                {
                    using (StreamReader leitor = new StreamReader(path)) 
                    {
                        _clientes = JsonConvert.DeserializeObject<List<Cliente>>(leitor.ReadToEnd());
                    }
                }
                return _clientes;
            }
            set
            {
                _clientes = value;
            }

        }


        private static int UltimoID = 1;

        public static int GetID()
        {
            return UltimoID++;
        }



        // Excluir cliente
        // Voltar

        // Adicionar cliente
        public static void AdicionarCliente()
        {
            //Se selecionar a opção 1 do solicitar os dados do cliente ao usuário, 
            //após todos os dados solicitados o mesmo deverá ser adicionado ao conjunto/lista cliente. 
            //E voltar ao menu das clientes. 

            Cliente clientenovo = new Cliente();
            Console.Write("Digite o nome do Cliente:");
            clientenovo.nome = Console.ReadLine();
            Console.Write("Digite CPF:");
            clientenovo.cpf = Console.ReadLine();
            Console.Write("Digite RG:");
            clientenovo.rg = Console.ReadLine();
            Console.Write("Digite Endereço. \nRua, Nº, Bairro, Cidade/Estado.");
            clientenovo.endereco = Console.ReadLine();
            clientes.Add(clientenovo);
            Console.WriteLine($"Cliente {clientenovo.nome} cadastrado");
            SalvarArquivo();
            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();

        }

        private static void SalvarArquivo()
        {
            using (StreamWriter escreve = new StreamWriter(path))
            {
                escreve.Write(JsonConvert.SerializeObject(_clientes));
            }
        }

        // Editar cliente
        public static void EditarCliente()
        {
            Console.WriteLine("Digite o CPF da cliente que deseja editar:");
            string numeroDoCPF = Console.ReadLine();

            if (clientes.Any(C => C.cpf == numeroDoCPF))
            {
                foreach (Cliente cliente in clientes.Where(C => C.cpf == numeroDoCPF))
                {
                    Console.WriteLine($"Alterando Informações do Cliente {cliente.cpf}");
                    Console.Write("Digite novo Nome: ");
                    cliente.nome = Console.ReadLine();
                    Console.Write("Digite novo número do RG: ");
                    cliente.rg = Console.ReadLine();
                    Console.Write("Digite novo endereço:\nRua, Nº, Bairro, Cidade/Estado.");
                    cliente.endereco = Console.ReadLine();
                }
                Console.WriteLine($"Cliente de CPF {numeroDoCPF} alterado.");

                SalvarArquivo();
            }

            else
            {
                Console.WriteLine("CPF não encontrado");
            }

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        //Listar todos os clientes
        public static void ListarTodosOsCliente()
        {
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine();
                Console.WriteLine($"Nome:{cliente.nome} ");
                Console.WriteLine($"CPF: {cliente.cpf} ");
                Console.WriteLine($"RG: {cliente.rg} ");
                Console.WriteLine($"Endereço:{cliente.endereco}.");
            }
            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        // Consultar cliente
        public static void ConsultarCliente()
        {
            Console.Write("Digite o CPF do Cliente que deseja Consultar:");
            string CPFconsulta = Console.ReadLine();
            Cliente cliente = clientes.FirstOrDefault(c => c.cpf == CPFconsulta);

            if (cliente != null)
            {
                Console.WriteLine($"Nome:{cliente.nome} ");
                Console.WriteLine($"Número do CPF: {cliente.cpf} ");
                Console.WriteLine($"Número do RG: {cliente.rg} ");
                Console.WriteLine($"Endereço:{cliente.endereco}.");
            }
            else
            {
                Console.WriteLine($"CPF {cliente.cpf} não encontrado.");
            }

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();

        }

        // Excluir cliente
        public static void ExcluirCliente()
        {
            Console.Write("Digite o CPF do Cliente que deseja Excluir:");
            string CPFconsulta = Console.ReadLine();
            if (!string.IsNullOrEmpty(CPFconsulta) && clientes.Any(C => C.cpf == CPFconsulta))
            {
                clientes.RemoveAll(c => c.cpf == CPFconsulta);
                Console.WriteLine($"Cliente de CPF {CPFconsulta} removido");
            }
            else
            {
                Console.WriteLine($"CPF {CPFconsulta} não encontrado.");
            }

            Console.WriteLine($"Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();

            SalvarArquivo();
        }


        //cliente tem 
        //nome
        //cpf
        //rg
        //endereço

        //E teremos os seguintes métodos: 
        //adicionar;
        //editar;
        //excluir;
        //consultar por número de conta e consultar todos.
    }
}