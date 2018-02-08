using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    Rigidbody objectRB;

    float yOffset;
	// Use this for initialization
	void Start ()
	{
	    objectRB = GetComponent<Rigidbody>();
	    yOffset = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    float y = CalculateTrianglePointHeight(transform.position);
	    objectRB.MovePosition(new Vector3(transform.position.x, y + yOffset, transform.position.z));
	}

    /*
        Much math isn't it. Well here is what it does.
        We can't use Sins.GetVertexHeight to get height info because it is used for vertex and a joint have a very big probability of not perpeticular on a vertice.
        First find which triangle the joint falls under. To do that we floor and ceil both x and z coordinates of joint's position. 
        p1 and p2 of triangle always will be (xFloor, y, zCeil) and (xCeil, y, zFloor). p3 will be closest one of (xFloor, y, zFloor) or (xCeil, y, zFloor)
        Then find which point on triangle joint falls under. Just find out of triangle's plane equation using triangle's normal then using these equation and x, z positions find out y position.
     */

    public static float CalculateTrianglePointHeight(Vector3 position)
    {
        float xFloor = MathfExtension.Floor(position.x, 1);
        float zFloor = MathfExtension.Floor(position.z, 1);
        float xCeil = MathfExtension.Ceil(position.x, 1);
        float zCeil = MathfExtension.Ceil(position.z, 1);

        Vector3 p3Floor = new Vector3(xFloor, position.y, zFloor);
        Vector3 p3Ceil = new Vector3(xCeil, position.z, zCeil);
        Vector3 p3 = Vector3.Distance(position, p3Floor) < Vector3.Distance(position, p3Ceil) ? p3Floor : p3Ceil;
        p3.y = Sins.GetVertexHeight(p3.x, p3.z);

        Vector3 p1 = new Vector3(xFloor, Sins.GetVertexHeight(xFloor, zCeil), zCeil);
        Vector3 p2 = new Vector3(xCeil, Sins.GetVertexHeight(xCeil, zFloor), zFloor);

        Vector3 triangleNormal = Vector3.Cross(p2 - p1, p3 - p1).normalized;
        float d = Vector3.Dot(p1, triangleNormal) * -1;

        if (Mathf.Approximately(triangleNormal.y, 0))
        {
            return p1.y;
        }

        return (triangleNormal.x * position.x + triangleNormal.z * position.z + d) / triangleNormal.y * -1;

    }
}
