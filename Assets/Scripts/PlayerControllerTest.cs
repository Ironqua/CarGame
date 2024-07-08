using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket h�z�

    void Update()
    {
        // WASD tu�lar�yla hareket
        float horizontal = Input.GetAxis("Horizontal"); // A ve D tu�lar�
        float vertical = Input.GetAxis("Vertical"); // W ve S tu�lar�

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
