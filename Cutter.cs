using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float height = 1f;
    [SerializeField] float yOri;
    [SerializeField] float xOri = 0.5f;
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    
    //cashed reference
    Spinner spinner;

    //for debug

    // Start is called before the first frame update
    void Start()
    {
        spinner = FindObjectOfType<Spinner>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shape(); 
    }

    private void Shape()
    {
        int hPosNo = GetHPosNo();

        if (hPosNo >= 0 & hPosNo < spinner.Slice())
        {
            float rPos = -transform.position.x - xOri;

            if (rPos > 0 & rPos < spinner.Radiuses(hPosNo))
            {
                spinner.SetRadius(rPos, hPosNo);
                spinner.ModifySpinnerShape();
            }
        }
    }

    private int GetHPosNo()
    {
        if(spinner.Slice() == 0) { return -1; }
        float dh = height / spinner.Slice();
        float yPos = transform.position.y - yOri;
        return Mathf.FloorToInt(yPos / dh);
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
    }

}
