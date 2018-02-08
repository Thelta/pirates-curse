using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sins
{
	public static float SinZ(float x, float t, bool round = true, bool height = true)
	{
		if(round)
		{
			x = Mathf.Round(x);
		}
		
		return (0.06f * (Mathf.Sin(x * 3.5f + t * 0.35f) + Mathf.Sin(x * 4.8f + t * 1.05f) + Mathf.Sin(x * 7.3f + t * 0.45f)));
	}
	
	public static float SinX(float x, float t, bool round = true, bool height = true)
	{
		if(round)
		{
			x = Mathf.Round(x);
		}
		
		return (0.06f * (Mathf.Sin(x * 4.0f + t * 0.5f) + Mathf.Sin(x * 6.8f + t * 0.75f) + Mathf.Sin(x * 11.3f + t * 0.2f)));
	}

    public static float GetVertexHeight(float x, float z)
    {
        return SinZ(z, Time.timeSinceLevelLoad, false, false) + SinX(x, Time.timeSinceLevelLoad, false, false);
    }
}
