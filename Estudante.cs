using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace att2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escolha o tipo de ordenação:");
            Console.WriteLine("1 - Bolha");
            Console.WriteLine("2 - Inserção");
            Console.WriteLine("3 - Seleção");
            int tipoOrdenacao = int.Parse(Console.ReadLine());
            string TipoOrdenação = "";
            if (tipoOrdenacao == 1)
                TipoOrdenação = "Bolha";
            else if (tipoOrdenacao == 2)
                TipoOrdenação = "Inserção";
            else if (tipoOrdenacao == 3)
                TipoOrdenação = "Seleção";

            Console.WriteLine("Escolha o tamanho do vetor:");
            int tamanho = int.Parse(Console.ReadLine());

            Console.WriteLine("Escolha como o vetor será preenchido:");
            Console.WriteLine("1 - Aleatório");
            Console.WriteLine("2 - Ordenado");
            Console.WriteLine("3 - Reversamente Ordenado");
            int tipoPreenchimento = int.Parse(Console.ReadLine());
            string TipoPreenchimento = "";
            if (tipoPreenchimento == 1)
                TipoPreenchimento = "Aleatorio";
            else if (tipoPreenchimento == 2)
                TipoPreenchimento = "ordenação";
            else if (tipoPreenchimento == 3)
                TipoPreenchimento = "Reversamente Ordenado";


            Estudante[] Original = new Estudante[tamanho];
            Estudante[] Copia = new Estudante[tamanho];
            long[] temp = new long[10];
            long numMovimentacoes = 0;
            long numComparacoes = 0;

            PreencherVetor(Original, tipoPreenchimento);

            for (int j = 0; j < Original.Length; j++)
            {
                Copia[j] = Original[j];
            }

            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < temp.Length; i++)
            {
                long tempMov = 0;
                long tempComp = 0;

                if (tipoOrdenacao == 1)
                {
                    stopwatch.Restart();
                    Bolha(Copia, ref tempMov, ref tempComp);
                    stopwatch.Stop();
                }
                else if (tipoOrdenacao == 2)
                {
                    stopwatch.Restart();
                    Insercao(Copia, ref tempMov, ref tempComp);
                    stopwatch.Stop();
                }
                else if (tipoOrdenacao == 3)
                {
                    stopwatch.Restart();
                    OrdenacaoPorSelecao(Copia, ref tempMov, ref tempComp);
                    stopwatch.Stop();
                }

                temp[i] = stopwatch.ElapsedMilliseconds;

                if (numComparacoes < tempComp)
                {
                    numComparacoes = tempComp;
                }

                if (numMovimentacoes < tempMov)
                {
                    numMovimentacoes = tempMov;
                }

                for (int j = 0; j < Original.Length; j++)
                {
                    Copia[j] = Original[j];
                }
            }

            long totalTempo = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                totalTempo += temp[i];
            }

            long mediaTempo = totalTempo / temp.Length;

            Console.WriteLine($"Tipo de ordenação: {TipoOrdenação}");
            Console.WriteLine($"Tamanho do vetor: {tamanho}");
            Console.WriteLine($"Tipo de preenchimento: {TipoPreenchimento}");
            Console.WriteLine($"Tempo médio gasto: {mediaTempo} ms");
            Console.WriteLine($"Número de movimentações: {numMovimentacoes}");
            Console.WriteLine($"Número de comparações: {numComparacoes}");
            Console.ReadLine();
        }

        static void PreencherVetor(Estudante[] array, int tipoPreenchimento)
        {
            Random random = new Random();

            switch (tipoPreenchimento)
            {
                case 1:
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = GerarEstudanteAleatorio();
                    }
                    break;
                case 2:
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = new Estudante(i, GerarStringAleatoria(6), GerarStringAleatoria(8), random.Next(0, 101), i);
                    }
                    break;
                case 3:
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = new Estudante(array.Length - i, GerarStringAleatoria(6), GerarStringAleatoria(8), random.Next(0, 101), array.Length - i);
                    }
                    break;
                default:
                    Console.WriteLine("Tipo de preenchimento inválido!");
                    break;
            }
        }

        static Estudante GerarEstudanteAleatorio()
        {
            Random random = new Random();
            return new Estudante(random.Next(1000, 9999), GerarStringAleatoria(6), GerarStringAleatoria(8), random.Next(0, 101), random.Next(1, 11));
        }

        static string GerarStringAleatoria(int tamanho)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            char[] stringAleatoria = new char[tamanho];
            for (int i = 0; i < tamanho; i++)
            {
                stringAleatoria[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringAleatoria);
        }

        static void Bolha(Estudante[] array, ref long mov, ref long comp)
        {
            int n = array.Length;
            for (int i = 0; i < (n - 1); i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    if (array[j].Matricula < array[j - 1].Matricula)
                    {
                        Estudante temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
                        mov += 3;
                    }
                    comp++;
                }
            }
        }

        static void Insercao(Estudante[] array, ref long mov, ref long comp)
        {
            int n = array.Length;

            for (int i = 1; i < n; i++)
            {
                Estudante tmp = array[i];
                int j = i - 1;
                while ((j >= 0) && (array[j].Matricula > tmp.Matricula))
                {
                    array[j + 1] = array[j];
                    j--;
                    mov++;
                }
                array[j + 1] = tmp;
                mov += 2;
                comp += 2;
            }
        }

        static void OrdenacaoPorSelecao(Estudante[] array, ref long mov, ref long comp)
        {
            int n = array.Length;
            for (int i = 0; i < (n - 1); i++)
            {
                int menor = i;
                for (int j = (i + 1); j < n; j++)
                {
                    if (array[menor].Matricula > array[j].Matricula)
                    {
                        menor = j;
                    }
                    comp++;
                }

                Estudante temp = array[menor];
                array[menor] = array[i];
                array[i] = temp;
                mov += 3;
            }
        }
    }

    class Estudante
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Curso { get; set; }
        public int CoeficienteRendimento { get; set; }
        public int PeriodoAtual { get; set; }

        public Estudante(int matricula, string nome, string curso, int coeficienteRendimento, int periodoAtual)
        {
            Matricula = matricula;
            Nome = nome;
            Curso = curso;
            CoeficienteRendimento = coeficienteRendimento;
            PeriodoAtual = periodoAtual;
        }

    }
}
    

