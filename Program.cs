using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace att1_0
{
    class Program
    {
        static void Main(string[] args)
        {


            int[] Original = new int[10000];
            int[] Copia = new int[10000];
            long[] temp = new long[10];
            int nun_movimentacao = 0;
            int nun_comparacao = 0;

            Random random = new Random();
            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < Original.Length; i++)
            {
                Original[i] = random.Next(0, 101);
                Copia[i] = Original[i];
            }

           
                for (int i = 0; i < temp.Length; i++)
                {
                    int temp_mov = 0;
                    int temp_comp = 0;

                    stopwatch.Start();
                    Ordenar(Copia,ref temp_mov,ref temp_comp);
                    stopwatch.Stop();

                    temp[i] = stopwatch.ElapsedMilliseconds;

                   if(nun_comparacao < temp_comp)
                   {
                      nun_comparacao = temp_comp;
                   }


                   if(nun_movimentacao < temp_mov)
                    {
                      nun_movimentacao = temp_mov;
                    }

                for (int j = 0; j < Original.Length; j++)
                {
                    Copia[j] = Original[j];
                }

            }
            

            long totalTempo = 0;
            for(int i = 0; i < temp.Length;i++)
            {
                totalTempo += temp[i];
            }

            long mediaTempo = totalTempo / temp.Length;

            Console.WriteLine($"Tempo gasto médio: {mediaTempo} ms, número de movimentações = {nun_movimentacao}, número de comparações = {nun_comparacao}");
            Console.ReadLine();
        }

       

        static void Ordenar(int[] array, ref int mov, ref int comp)
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

}
