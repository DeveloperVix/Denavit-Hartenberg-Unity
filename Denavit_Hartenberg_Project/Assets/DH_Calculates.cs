using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class MatrizStructure
{
    public double[] Matrizl = new double[16];
}

public class DH_Calculates : MonoBehaviour
{
    private static DH_Calculates instance;
    public static DH_Calculates Instance { get => instance; }
    public List<Link> allLinks;

    public List<MatrizStructure> finalMatriz;

    public double[] result = new double[16];


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void CalculateMatrix()
    {
        finalMatriz = new List<MatrizStructure>();
        MatrizStructure tempMatirz = new MatrizStructure();


        int n_eslabon = allLinks.Count;
        for (int g = 0; g < n_eslabon; g++)
        {
            double teta0 = allLinks[g].teta;
            double teta = Math.PI * teta0 / 180.0;               //conversion a radianes

            double a = allLinks[g].a;

            double d = allLinks[g].d;

            double alfa0 = allLinks[g].alfa;
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
            tempMatirz.Matrizl[0] = Cteta;
            tempMatirz.Matrizl[1] = p1;
            tempMatirz.Matrizl[2] = p2;
            tempMatirz.Matrizl[3] = p3;
            tempMatirz.Matrizl[4] = Steta;
            tempMatirz.Matrizl[5] = p5;
            tempMatirz.Matrizl[6] = p6;
            tempMatirz.Matrizl[7] = p7;
            tempMatirz.Matrizl[8] = 0;
            tempMatirz.Matrizl[9] = Salfa;
            tempMatirz.Matrizl[10] = Calfa;
            tempMatirz.Matrizl[11] = d;
            tempMatirz.Matrizl[12] = 0;
            tempMatirz.Matrizl[13] = 0;
            tempMatirz.Matrizl[14] = 0;
            tempMatirz.Matrizl[15] = 1;

            finalMatriz.Add(tempMatirz);

            //print("\nCinematica directa\n");
            /*print("\n" + finalMatriz[g].Matrizl[0] + " " + finalMatriz[g].Matrizl[1] + " " + finalMatriz[g].Matrizl[2] + " " + finalMatriz[g].Matrizl[3] + "\n" +
                              finalMatriz[g].Matrizl[4] + " " + finalMatriz[g].Matrizl[5] + " " + finalMatriz[g].Matrizl[6] + " " + finalMatriz[g].Matrizl[7] + "\n" +
                             finalMatriz[g].Matrizl[8] + " " + finalMatriz[g].Matrizl[9] + " " + finalMatriz[g].Matrizl[10] + " " + finalMatriz[g].Matrizl[11] + "\n" +
                              finalMatriz[g].Matrizl[12] + " " + finalMatriz[g].Matrizl[13] + " " + finalMatriz[g].Matrizl[14] + " " + finalMatriz[g].Matrizl[15] + "\n");*/
        }

        int currentMatriz = 0;
        int nextMatriz = 1;
        if (finalMatriz.Count > 1)
        {
            for (int i = 0; i < finalMatriz.Count-1; i++)
            {
                #region fila 1 de la matriz
                result[0] = ((finalMatriz[currentMatriz].Matrizl[0] * finalMatriz[nextMatriz].Matrizl[0]) + (finalMatriz[currentMatriz].Matrizl[1] * finalMatriz[nextMatriz].Matrizl[4]) +
                (finalMatriz[currentMatriz].Matrizl[2] * finalMatriz[nextMatriz].Matrizl[8]) + (finalMatriz[currentMatriz].Matrizl[3] * finalMatriz[nextMatriz].Matrizl[12]));

                result[1] = ((finalMatriz[currentMatriz].Matrizl[0] * finalMatriz[nextMatriz].Matrizl[1]) + (finalMatriz[currentMatriz].Matrizl[1] * finalMatriz[nextMatriz].Matrizl[5]) +
                (finalMatriz[currentMatriz].Matrizl[2] * finalMatriz[nextMatriz].Matrizl[9]) + (finalMatriz[currentMatriz].Matrizl[3] * finalMatriz[nextMatriz].Matrizl[13]));

                result[2] = ((finalMatriz[currentMatriz].Matrizl[0] * finalMatriz[nextMatriz].Matrizl[2]) + (finalMatriz[currentMatriz].Matrizl[1] * finalMatriz[nextMatriz].Matrizl[6]) +
                (finalMatriz[currentMatriz].Matrizl[2] * finalMatriz[nextMatriz].Matrizl[10]) + (finalMatriz[currentMatriz].Matrizl[3] * finalMatriz[nextMatriz].Matrizl[14]));

                result[3] = ((finalMatriz[currentMatriz].Matrizl[0] * finalMatriz[nextMatriz].Matrizl[3]) + (finalMatriz[currentMatriz].Matrizl[1] * finalMatriz[nextMatriz].Matrizl[7]) +
                (finalMatriz[currentMatriz].Matrizl[2] * finalMatriz[nextMatriz].Matrizl[11]) + (finalMatriz[currentMatriz].Matrizl[3] * finalMatriz[nextMatriz].Matrizl[15]));
                #endregion

                #region fila 2 de la matriz
                result[4] = ((finalMatriz[currentMatriz].Matrizl[4] * finalMatriz[nextMatriz].Matrizl[0]) + (finalMatriz[currentMatriz].Matrizl[5] * finalMatriz[nextMatriz].Matrizl[4]) +
                (finalMatriz[currentMatriz].Matrizl[6] * finalMatriz[nextMatriz].Matrizl[8]) + (finalMatriz[currentMatriz].Matrizl[7] * finalMatriz[nextMatriz].Matrizl[12]));

                result[5] = ((finalMatriz[currentMatriz].Matrizl[4] * finalMatriz[nextMatriz].Matrizl[1]) + (finalMatriz[currentMatriz].Matrizl[5] * finalMatriz[nextMatriz].Matrizl[5]) +
                (finalMatriz[currentMatriz].Matrizl[6] * finalMatriz[nextMatriz].Matrizl[9]) + (finalMatriz[currentMatriz].Matrizl[7] * finalMatriz[nextMatriz].Matrizl[13]));

                result[6] = ((finalMatriz[currentMatriz].Matrizl[4] * finalMatriz[nextMatriz].Matrizl[2]) + (finalMatriz[currentMatriz].Matrizl[5] * finalMatriz[nextMatriz].Matrizl[6]) +
                (finalMatriz[currentMatriz].Matrizl[6] * finalMatriz[nextMatriz].Matrizl[10]) + (finalMatriz[currentMatriz].Matrizl[7] * finalMatriz[nextMatriz].Matrizl[14]));

                result[7] = ((finalMatriz[currentMatriz].Matrizl[4] * finalMatriz[nextMatriz].Matrizl[3]) + (finalMatriz[currentMatriz].Matrizl[5] * finalMatriz[nextMatriz].Matrizl[7]) +
                (finalMatriz[currentMatriz].Matrizl[6] * finalMatriz[nextMatriz].Matrizl[11]) + (finalMatriz[currentMatriz].Matrizl[7] * finalMatriz[nextMatriz].Matrizl[15]));
                #endregion

                #region fila 3 de la matriz
                result[8] = ((finalMatriz[currentMatriz].Matrizl[8] * finalMatriz[nextMatriz].Matrizl[0]) + (finalMatriz[currentMatriz].Matrizl[9] * finalMatriz[nextMatriz].Matrizl[4]) +
                (finalMatriz[currentMatriz].Matrizl[10] * finalMatriz[nextMatriz].Matrizl[8]) + (finalMatriz[currentMatriz].Matrizl[11] * finalMatriz[nextMatriz].Matrizl[12]));

                result[9] = ((finalMatriz[currentMatriz].Matrizl[8] * finalMatriz[nextMatriz].Matrizl[1]) + (finalMatriz[currentMatriz].Matrizl[9] * finalMatriz[nextMatriz].Matrizl[5]) +
                (finalMatriz[currentMatriz].Matrizl[10] * finalMatriz[nextMatriz].Matrizl[9]) + (finalMatriz[currentMatriz].Matrizl[11] * finalMatriz[nextMatriz].Matrizl[13]));

                result[10] = ((finalMatriz[currentMatriz].Matrizl[8] * finalMatriz[nextMatriz].Matrizl[2]) + (finalMatriz[currentMatriz].Matrizl[9] * finalMatriz[nextMatriz].Matrizl[6]) +
                (finalMatriz[currentMatriz].Matrizl[10] * finalMatriz[nextMatriz].Matrizl[10]) + (finalMatriz[currentMatriz].Matrizl[11] * finalMatriz[nextMatriz].Matrizl[14]));

                result[11] = ((finalMatriz[currentMatriz].Matrizl[8] * finalMatriz[nextMatriz].Matrizl[3]) + (finalMatriz[currentMatriz].Matrizl[9] * finalMatriz[nextMatriz].Matrizl[7]) +
                (finalMatriz[currentMatriz].Matrizl[10] * finalMatriz[nextMatriz].Matrizl[11]) + (finalMatriz[currentMatriz].Matrizl[11] * finalMatriz[nextMatriz].Matrizl[15]));
                #endregion

                #region fila 4 de la matriz
                result[12] = ((finalMatriz[currentMatriz].Matrizl[12] * finalMatriz[nextMatriz].Matrizl[0]) + (finalMatriz[currentMatriz].Matrizl[13] * finalMatriz[nextMatriz].Matrizl[4]) +
                (finalMatriz[currentMatriz].Matrizl[14] * finalMatriz[nextMatriz].Matrizl[8]) + (finalMatriz[currentMatriz].Matrizl[15] * finalMatriz[nextMatriz].Matrizl[12]));

                result[13] = ((finalMatriz[currentMatriz].Matrizl[12] * finalMatriz[nextMatriz].Matrizl[1]) + (finalMatriz[currentMatriz].Matrizl[13] * finalMatriz[nextMatriz].Matrizl[5]) +
                (finalMatriz[currentMatriz].Matrizl[14] * finalMatriz[nextMatriz].Matrizl[9]) + (finalMatriz[currentMatriz].Matrizl[15] * finalMatriz[nextMatriz].Matrizl[13]));

                result[14] = ((finalMatriz[currentMatriz].Matrizl[12] * finalMatriz[nextMatriz].Matrizl[2]) + (finalMatriz[currentMatriz].Matrizl[13] * finalMatriz[nextMatriz].Matrizl[6]) +
                (finalMatriz[currentMatriz].Matrizl[14] * finalMatriz[nextMatriz].Matrizl[10]) + (finalMatriz[currentMatriz].Matrizl[15] * finalMatriz[nextMatriz].Matrizl[14]));

                result[15] = ((finalMatriz[currentMatriz].Matrizl[12] * finalMatriz[nextMatriz].Matrizl[3]) + (finalMatriz[currentMatriz].Matrizl[13] * finalMatriz[nextMatriz].Matrizl[7]) +
                (finalMatriz[currentMatriz].Matrizl[14] * finalMatriz[nextMatriz].Matrizl[11]) + (finalMatriz[currentMatriz].Matrizl[15] * finalMatriz[nextMatriz].Matrizl[15]));
                #endregion

                finalMatriz[currentMatriz].Matrizl = result;
                currentMatriz++;
                nextMatriz++;
            }
        }
        else
        {
            result = finalMatriz[0].Matrizl;
        }


        print("\nCinematica directa\n");
        print("\n" + result[0] + " " + result[1] + " " + result[2] + " " + result[3] + "\n" +
                          result[4] + " " + result[5] + " " + result[6] + " " + result[7] + "\n" +
                         result[8] + " " + result[9] + " " + result[10] + " " + result[11] + "\n" +
                          result[12] + " " + result[13] + " " + result[14] + " " + result[15] + "\n");
    }
}
