using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tallet : MonoBehaviour
{
    [SerializeField] float feedSpeed = 0.2f;
    [SerializeField] float yMin = 0f;
    [SerializeField] float yMax = 1.2f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * feedSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
    }
}
