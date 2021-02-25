using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectTest : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var outVector = new Vector2(5, 5);
        var normal = Vector2.down;
        
        Gizmos.DrawLine(Vector2.zero, outVector);
        Gizmos.DrawLine(outVector, Vector2.Perpendicular(normal) + outVector);

        var reflectVector = Vector2.Reflect(outVector, normal);
        Gizmos.DrawLine(outVector, reflectVector + outVector);

    }
}
