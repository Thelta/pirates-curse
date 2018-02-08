using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNecessaryInfoMat : MonoBehaviour
{
    Material mat;

	// Use this for initialization
	void Start ()
	{
	    Renderer ren = GetComponent<Renderer>();
	    mat = ren.material;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    mat.SetFloat("_TrueTime", Time.timeSinceLevelLoad);
	}
}
