using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWaterPhysics : MonoBehaviour
{
	public GameObject jointPrefab;
    public Vector3 jointPosOffset;
    public int xPointCount;
    public int zPointCount;

	Rigidbody objectRB;
	BoxCollider objectCollider;
    GameObject joints;

    // Use this for initialization
    void Start ()
	{
	    objectCollider = GetComponent<BoxCollider>();
        Vector3[] corners = new Vector3[4];
        Vector3[] points = new Vector3[xPointCount * zPointCount];
	    Vector3 colliderSize = objectCollider.bounds.size;

	    objectRB = GetComponent<Rigidbody>();

	    corners[0] = objectCollider.bounds.min;
	    for (int z = 1, i = 0; z <= zPointCount; z++)
	    {
	        for (int x = 1; x <= xPointCount; x++, i++)
	        {
	            points[i] = corners[0] +
	                        new Vector3(colliderSize.x / xPointCount * x, 0, colliderSize.z / zPointCount * z);
	        }
	    }

	    joints = new GameObject("joints");
        //joints.transform.SetParent(transform);
	    joints.transform.position = transform.position + jointPosOffset;
	    MoveJoints mj = joints.AddComponent<MoveJoints>();
	    mj.effectedObjectTransform = transform;

	    foreach (var point in points)
	    {
	        Vector3 preStartPos = point + jointPosOffset;
	        float yPhysicsOffset = WaterPhysics.CalculateTrianglePointHeight(preStartPos);
	        preStartPos.y += yPhysicsOffset;

	        GameObject joint = Instantiate(jointPrefab, preStartPos, Quaternion.identity);

	        SpringJoint sj = joint.GetComponent<SpringJoint>();
	        sj.connectedAnchor = transform.InverseTransformPoint(point);
            sj.connectedBody = objectRB;
	        sj.maxDistance = Vector3.Distance(point, joint.transform.position);
            joint.transform.SetParent(joints.transform);
            
	    }

	    objectRB.maxAngularVelocity = 2f;
    }

    /*void FixedUpdate()
    {
        objectRB.MovePosition(transform.position + new Vector3(-0.01f, 0, 0));
        joints.transform.position = transform.position + jointPosOffset;
    }*/
}
