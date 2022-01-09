using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spinner : MonoBehaviour
{
    //config param
    [Header("Config")]
    [SerializeField] Vector3 workOri;
    [SerializeField] float oriR = 0.5f;
    [SerializeField] int slice = 100;

    [SerializeField] GameObject massCylinderPrefab;
    [SerializeField] float torque = 100f;
    [SerializeField] float maxAngVelocity = 100f;

    [SerializeField] Vector3 InitPosToPlay;

    //for debug
    [Header("Debug")]
    [SerializeField] Vector3 vel;
    [SerializeField] Vector3 angVel;

    //cashed reference
    Rigidbody rb;
    float height;
    //[SerializeField] 
    float[] radiuses;

    public int Slice() { return slice; }
    public float Radiuses(int num) { return radiuses[num]; }
    public void SetRadius(float radius, int num) { radiuses[num] = radius; }
    public float AngularVelocityY(){ return rb.angularVelocity.y; }

    void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        int numberGameSessions = FindObjectsOfType<Spinner>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        height = GetObjectHeight();
        radiuses = new float[slice];
        InitSpinnerRadius();
    }

    private void InitSpinnerRadius()
    {
        for (int hnum = 0; hnum < slice; hnum++)
        {
            radiuses[hnum] = oriR;
        }
    }

    public void RotateSpinner(float rotateSpeed)
    {
        transform.Rotate(0f, Time.deltaTime * rotateSpeed, 0f); ;
    }

    public void SetSpinnerPhysics()
    {
        if (radiuses.Length <= 1) return;
        SetMass();
        AddMassCylinders();
    }
    
    public void ModifySpinnerShape()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Vector3[] vertices = meshFilter.mesh.vertices;

        int slice = radiuses.Length - 1;
        float dh = height / slice;

        for (int hnum = 0; hnum < slice; hnum++)
        {
            for (int vernum = 0; vernum < vertices.Length; vernum++)
            {
                Vector3 workVec = vertices[vernum] - workOri;

                if ((workVec.y >= dh * hnum) & ((workVec.y < dh * (hnum + 1)) | (hnum >= slice - 1)))
                {
                    float orgRadius = Mathf.Sqrt(workVec.x * workVec.x + workVec.z * workVec.z);
                    if (orgRadius > 0)
                    {
                        float polRadius = InterpolatedValue(radiuses[hnum], radiuses[hnum + 1], dh * hnum, dh * (hnum + 1), workVec.y);
                        float ratio = polRadius / orgRadius;
                        vertices[vernum] = new Vector3(workVec.x * ratio, workVec.y, workVec.z * ratio) + workOri;
                    }
                }
            }
        }
        meshFilter.mesh.vertices = vertices;

        //MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>() as MeshCollider;
        //meshCollider.convex = true;
    }

    private float GetObjectHeight()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Vector3[] vertices = meshFilter.mesh.vertices;
        float maxh = vertices[0].y;
        float minh = vertices[0].y;
        for (int vernum = 1; vernum < vertices.Length; vernum++)
        {
            if (vertices[vernum].y > maxh) maxh = vertices[vernum].y;
            if (vertices[vernum].y < minh) minh = vertices[vernum].y;
        }

        return maxh - minh;
    }

    private void SetMass()
    //Set mass for shaped spinner
    {
        float vol = 0;
        foreach (float radius in radiuses)
        {
            vol += radius * radius *Mathf.PI;
        }
        vol /= radiuses.Length;

        rb.mass *= vol / (oriR* oriR *Mathf.PI);
    }

    private void AddMassCylinders()
    {
        int slice = radiuses.Length-1;

        for(int hnum=0; hnum < slice; hnum++) 
        {
            GameObject massCylinder = Instantiate(massCylinderPrefab, transform.position, Quaternion.identity);

            float rScale = radiuses[hnum]*2;
            float hScale = massCylinder.transform.localScale.y / slice;
            massCylinder.transform.localScale = new Vector3(rScale, hScale, rScale);

            massCylinder.transform.parent = transform;
            float hpos = height / slice * hnum + hScale - height / 2;
            massCylinder.transform.localPosition = new Vector3(0f, hpos, 0f);
        }
    }

    private float InterpolatedValue(float y1, float y2, float x1, float x2, float x)
    {
        return (y2 - y1) / (x2 - x1) * (x - x1) + y1;
    }

    public void AddTorqueToSpinner()
    {
        rb.isKinematic = false;
        rb.maxAngularVelocity = maxAngVelocity;
        rb.AddTorque(Vector3.up * torque);
    }

    public void ReadyPosToPlay()
    {
        transform.rotation = Quaternion.identity;
        transform.position = InitPosToPlay;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
