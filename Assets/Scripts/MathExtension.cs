using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathfExtension
{
    public static float Floor(float n, int precision)
    {
        float pn = Mathf.Pow(10, precision);
        return Mathf.Floor(n * pn) / pn;
    }

    public static float Ceil(float n, int precision)
    {
        float pn = Mathf.Pow(10, precision);
        return Mathf.Ceil(n * pn) / pn;

    }
}
