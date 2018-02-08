using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    float zDistance;

    void Start()
    {
        zDistance = playerTransform.position.z - transform.position.z;
    }

    void LateUpdate ()
	{
	    transform.position = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z - zDistance);
	}
}
