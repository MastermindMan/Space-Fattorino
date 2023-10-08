using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMathStuff
{
    public static class Integrals
    {
        //può tornare utile:
        //https://numerics.mathdotnet.com/Integration

        static double Integral(float a, float b, float c, float intervalStart, float intervalEnd, int partizioni)
        {

            float xp, y, s, result = 0, g = (intervalEnd - intervalStart) / partizioni;
            for (int i = 0; i < partizioni; i++)
            {
                xp = intervalStart + g;
                y = (a * Mathf.Pow(xp, 2)) + (b * xp) + c;
                s = g * y;
                result += s;
            }
            return result;
        }

    }

}
