using System;
using System.Diagnostics;

class Program
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


        int[] Original = new int[tamanho];
        int[] Copia = new int[tamanho];
        long[] temp = new long[10];
        long numMovimentacoes = 0;
        long numComparacoes = 0;

        PreencherVetor(Original, tipoPreenchimento);

        for (int j = 0; j < Original.Length; j++)
        {
            Copia[j] = Original[j];
        }

        for (int j = 0; j < Original.Length; j++)
        {
            Console.Write(Original[j] + " ");
        }


        Random random = new Random();
        Stopwatch stopwatch = new Stopwatch();

        for (int i = 0; i < temp.Length; i++)
        {
            long tempMov = 0;
            long tempComp = 0;

           

            if(tipoOrdenacao == 1)
            {
                stopwatch.Restart();
                Bolha(Copia, ref tempMov, ref tempComp);
                stopwatch.Stop();
            }
            else if(tipoOrdenacao == 2)
            {
                stopwatch.Restart();
                Insercao(Copia, ref tempMov, ref tempComp);
                stopwatch.Stop();
            }
            else if(tipoOrdenacao == 3)
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

        Insercao(Original);
        for (int j = 0; j < Original.Length; j++)
        {
            Console.Write(Original[j] + " ");
        }

    }

    static void PreencherVetor(int[] array, int tipoPreenchimento)
    {
        Random random = new Random();

        switch (tipoPreenchimento)
        {
            case 1: 
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = random.Next(0, 101);
                }
                break;
            case 2: 
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = i;
                }
                break;
            case 3: 
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array.Length - i;
                }
                break;
            default:
                Console.WriteLine("Tipo de preenchimento inválido!");
                break;
        }
    }

    static void Bolha(int[] array, ref long mov, ref long comp)
    {
        int n = array.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            for (int j = n - 1; j > i; j--)
            {
                if (array[j] < array[j - 1])
                {
                    int temp = array[j];
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                    mov += 3;
                }
                comp++;
            }
        }
    }

    static void Insercao(int[] array)
    {
        int n = array.Length;

        for (int i = 1; i < n; i++)
        {
            int tmp = array[i];
            int j = i - 1;
            while ((j >= 0) && (array[j] > tmp))
            {
                array[j + 1] = array[j];
                j--;
             
            }
            array[j + 1] = tmp;
         

        }
    }

    static void Insercao(int[] array, ref long mov, ref long comp)
    {
        int n = array.Length;

        for (int i = 1; i < n; i++)
        {
            int tmp = array[i];
            int j = i - 1;
            while ((j >= 0) && (array[j] > tmp))
            {
                array[j + 1] = array[j];
                j--;
                mov++;
            }
            array[j + 1] = tmp;
            mov += 2;
            comp+=2;

        }
    }

    static void OrdenacaoPorSelecao(int[] array, ref long mov, ref long comp)
    {
        int n = array.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            int menor = i;
            for (int j = (i + 1); j < n; j++)
            {
                if (array[menor] > array[j])
                {
                    menor = j;
                }
                comp++;
            }

            int temp = array[menor];
            array[menor] = array[i];
            array[i] = temp;
            mov += 3;
        }
    }
}
