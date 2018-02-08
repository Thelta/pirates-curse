using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoints : MonoBehaviour
{
    public Transform effectedObjectTransform;
    Rigidbody[] springRBs;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(effectedObjectTransform.position.x, transform.position.y,
            effectedObjectTransform.position.z);
        transform.eulerAngles = new Vector3(0, effectedObjectTransform.eulerAngles.y, 0);
    }
}
