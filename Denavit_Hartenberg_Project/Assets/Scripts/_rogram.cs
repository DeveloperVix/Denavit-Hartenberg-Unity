using System;
using System.Numerics;

namespace cinematica_directa
{
    class Program
    {

        static void Main(string[] args)
        {
            double[] Matrizl = new double[16];  //matriz

            Console.WriteLine("Numero de eslabones");
            int n_eslabon = int.Parse(Console.ReadLine());
            for (int g=0; g < n_eslabon; g++) { 
                Console.WriteLine("valor de teta");
            double teta0 = double.Parse(Console.ReadLine());
            double teta = Math.PI * teta0 / 180.0;               //conversion a radianes
            Console.WriteLine("valor de a");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("valor de d");
            double d = double.Parse(Console.ReadLine());
            Console.WriteLine("valor de alfa");
            double alfa0 = double.Parse(Console.ReadLine());
            double alfa = Math.PI * alfa0 / 180.0;               //conversion a radianes

            double Cteta = Math.Round(Math.Cos(teta), 4);
            double Steta = Math.Round(Math.Sin(teta), 4);
            double Calfa = Math.Round(Math.Cos(alfa), 4);
            double Salfa = Math.Round(Math.Sin(alfa), 4);

            /* Matriz donde sustituyen datos
              cos(teta) -cos(alfa)*sin(teta) sin(alfa)*sin(teta) a*cos(teta)
              sin(teta) cos(alfa)*cos(teta) -sin(alfa)*cos(teta) a*sin(teta)
                 0           sin(alfa)            cos(alfa)           d
                 0              0                    0                1
*/

            //p0=Cteta
            double p1 = -Calfa * Steta;
            double p2 = Salfa * Steta;
            double p3 = a * Cteta;
            //p4=Steta
            double p5 = Calfa * Cteta;
            double p6 = -Salfa * Cteta;
            double p7 = a * Steta;
            // p8 = 0
            // p9 = Salfa
            // p10 = Calfa
            // p11 = d;
            // p12 a p14 =0 p15 =1

            //acomodo de las operaciones dentro del vector, cada 4 es una fila
            
            Matrizl[0] = Cteta;
            Matrizl[1] = p1;
            Matrizl[2] = p2;
            Matrizl[3] = p3;
            Matrizl[4] = Steta;
            Matrizl[5] = p5;
            Matrizl[6] = p6;
            Matrizl[7] = p7;
            Matrizl[8] = 0;
            Matrizl[9] = Salfa;
            Matrizl[10] = Calfa;
            Matrizl[11] = d;
            Matrizl[12] = 0;
            Matrizl[13] = 0;
            Matrizl[14] = 0;
            Matrizl[15] = 1;
            }
            Console.WriteLine("\nCinematica directa\n");
            Console.WriteLine("\n"+ Matrizl[0] + " " +Matrizl[1] + " " + Matrizl[2] + " " + Matrizl[3] + "\n"+
                              Matrizl[4] + " " + Matrizl[5] + " " + Matrizl[6] + " " + Matrizl[7] + "\n" +
                              Matrizl[8] + " " + Matrizl[9] + " " + Matrizl[10] + " " + Matrizl[11] + "\n" +
                              Matrizl[12] + " " + Matrizl[13] + " " + Matrizl[14] + " " + Matrizl[15] + "\n");
            
        }
    }
}
