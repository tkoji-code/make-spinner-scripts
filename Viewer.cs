using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotSpeed = 50f;
    [SerializeField] GameObject cameraPivot;
    [SerializeField] float Lmin = 0.6f;
    [SerializeField] float Lmax = 6f;

    // Update is called once per frame
    void Update()
    {
        float deltaB = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
        float deltaA = Input.GetAxis("Vertical") * Time.deltaTime * rotSpeed;
        cameraPivot.transform.Rotate(deltaA, deltaB, 0f,Space.World);

        float deltaZ = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            deltaZ =  Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.RightShift))
        {
            deltaZ = -Time.deltaTime * moveSpeed; 
        }
        float newZPos = Mathf.Clamp(Camera.main.transform.localPosition.z + deltaZ, -Lmax, -Lmin);
        Camera.main.transform.localPosition = new Vector3(0f, 0f, newZPos);
    }
}
