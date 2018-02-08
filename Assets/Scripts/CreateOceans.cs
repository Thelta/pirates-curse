using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOceans : MonoBehaviour 
{
	public Transform ship;
	public GameObject ocean;
	GameObject oceanTree;
	GameObject firstOcean;
	float nextZ;
	public GameObject[] terrains;
	public GameObject[] rocks;
	// Use this for initialization
	void Start () 
	{
		oceanTree = new GameObject();
		for(int i = 0; i < 8; i++)
		{
			CreateOcean(ship.position.z + 10 * i - 5);
		}
		nextZ = 65;
		firstOcean = oceanTree.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(ship.transform.position.z - firstOcean.transform.position.z > 10)
		{
			DestroyImmediate(firstOcean);

			CreateOcean(nextZ);
			firstOcean = oceanTree.transform.GetChild(0).gameObject;
			nextZ += 10;
		}
	}

	void CreateOcean(float z)
	{
		GameObject newOcean = GameObject.Instantiate(ocean, 
			new Vector3(0, 0, z), Quaternion.identity);
		newOcean.transform.parent = oceanTree.transform;
		GameObject firstTerrain = GameObject.Instantiate(terrains[Random.Range(0, terrains.Length)],
			new Vector3(-15, -0.25f, z - 5), Quaternion.identity);
		GameObject secondTerrain = GameObject.Instantiate(terrains[Random.Range(0, terrains.Length)],
			new Vector3(5, -0.25f, z - 5), Quaternion.identity);
		firstTerrain.transform.parent = newOcean.transform;
		secondTerrain.transform.parent = newOcean.transform;
		int rockNo = oceanTree.transform.childCount < 3 ? 0 : Random.Range(0, 4);
		
		for(int i = 0; i < rockNo; i++)
		{
			Vector2 randomPos = Random.insideUnitCircle * 5;
			Vector3 rockPos = new Vector3(newOcean.transform.position.x + randomPos.x, -0.25f, newOcean.transform.position.z + randomPos.y);
			GameObject newRock = GameObject.Instantiate(rocks[Random.Range(0, rocks.Length)], rockPos, Quaternion.identity);
			newRock.transform.parent = newOcean.transform;	
		}

		 newOcean.transform.parent = oceanTree.transform;
	}
}
