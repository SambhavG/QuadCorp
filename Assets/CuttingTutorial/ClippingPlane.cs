using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ClippingPlane : MonoBehaviour
{
    public Material material;

    // Update is called once per frame
    void Update()
    {
        Plane plane = new Plane(transform.up, transform.position);

        Vector4 PlaneVisualization = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
        material.SetVector("_Plane", PlaneVisualization);
    }
}
