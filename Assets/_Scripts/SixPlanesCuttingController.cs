using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SixPlanesCuttingController : MonoBehaviour {

    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject plane4;
    public GameObject plane5;
    public GameObject plane6;
    public Renderer rend;

    private Vector3 normal1;
    private Vector3 position1;
    private Vector3 normal2;
    private Vector3 position2;
    private Vector3 normal3;
    private Vector3 position3;
    private Vector3 position4;
    private Vector3 normal4;
    private Vector3 position5;
    private Vector3 normal5;
    private Vector3 position6;
    private Vector3 normal6;


    private MaterialPropertyBlock m_MaterialPropertyBlock;

    private void Awake()
    {
        m_MaterialPropertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        UpdateShaderProperties();
    }

#if UNITY_EDITOR
    private void Update()
    {
        UpdateShaderProperties();
    }
#endif

    public void UpdateShaderProperties()
    {
        if (plane1) {
            normal1 = plane1.transform.TransformVector(new Vector3(0, 0, -1));
            position1 = plane1.transform.position;
        } else {
            normal1 = -Vector3.forward;
            position1 = new Vector3(0f, 0f, -1000f);
        }

        if (plane2) {
            normal2 = plane2.transform.TransformVector(new Vector3(0, 0, -1));
            position2 = plane2.transform.position;
        } else {
            normal2 = -Vector3.forward;
            position2 = new Vector3(0f, 0f, 1000f);
        }

        if (plane3) {
            normal3 = plane3.transform.TransformVector(new Vector3(0, 0, -1));
            position3 = plane3.transform.position;
        } else {
            normal3 = -Vector3.forward;
            position3 = new Vector3(0f, 0f, 1000f);
        }

        if (plane4) {
            normal4 = plane4.transform.TransformVector(new Vector3(0, 0, -1));
            position4 = plane4.transform.position;
        } else {
            normal4 = -Vector3.forward;
            position4 = new Vector3(0f, 0f, 1000f);
        }

        if (plane5) {
            normal5 = plane5.transform.TransformVector(new Vector3(0, 0, -1));
            position5 = plane5.transform.position;
        } else {
            normal5 = -Vector3.forward;
            position5 = new Vector3(0f, 0f, 1000f);
        }

        if (plane6) {
            normal6 = plane6.transform.TransformVector(new Vector3(0, 0, -1));
            position6 = plane6.transform.position;
        } else {
            normal6 = -Vector3.forward;
            position6 = new Vector3(0f, 0f, 1000f);
        }

        

        m_MaterialPropertyBlock.SetVector("Vector3_Plane1_Normal", normal1);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane1_Position", position1);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane2_Normal", normal2);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane2_Position", position2);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane3_Normal", normal3);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane3_Position", position3);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane4_Normal", normal4);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane4_Position", position4);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane5_Normal", normal5);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane5_Position", position5);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane6_Normal", normal6);
        m_MaterialPropertyBlock.SetVector("Vector3_Plane6_Position", position6);

        rend.SetPropertyBlock(m_MaterialPropertyBlock);
    }
}
