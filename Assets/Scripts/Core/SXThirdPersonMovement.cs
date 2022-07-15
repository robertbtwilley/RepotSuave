using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SXThirdPersonMovement : MonoBehaviour
{
    public Rigidbody shipRigidbody;

    public float shipSpeed;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            shipRigidbody.MovePosition(direction * shipSpeed * Time.deltaTime);
        }
    }
}
