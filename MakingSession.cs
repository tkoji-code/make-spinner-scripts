using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakingSession : MonoBehaviour
{
    //config param
    [SerializeField] float rotateSpeed = 10f;

    //cashed reference
    Spinner spinner;

    // Start is called before the first frame update
    void Start()
    {
        spinner = FindObjectOfType<Spinner>();
    }

    // Update is called once per frame
    void Update()
    {
        spinner.RotateSpinner(rotateSpeed);
    }

}
