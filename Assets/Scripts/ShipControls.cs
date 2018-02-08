using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipControls : MonoBehaviour
{
    public float rotSpeed;
    public float speed;

    Rigidbody objectRB;
    int currentOceanTiles;

	// Use this for initialization
	void Start ()
	{
	    objectRB = GetComponent<Rigidbody>();
	    currentOceanTiles = 0;
	}

    void Update()
    {
        int passedOceanTiles = Mathf.FloorToInt(transform.position.z) / 10;

        if (passedOceanTiles > 0 && currentOceanTiles != passedOceanTiles)
        {
            print("bump");
            speed += (speed / 10);
            currentOceanTiles = passedOceanTiles;
        }
    }

    // Update is called once per frame
	void FixedUpdate ()
	{
	    float horizInput = Input.GetAxis("Horizontal");
        objectRB.AddTorque(new Vector3(0, horizInput * rotSpeed, 0));
	    objectRB.AddForce(transform.forward.normalized * speed - objectRB.velocity);
    }

    private void OnCollisionEnter()
    {
        SceneManager.LoadScene(1);
    }
}
