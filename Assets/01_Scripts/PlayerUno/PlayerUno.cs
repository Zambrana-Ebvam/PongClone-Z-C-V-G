using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUno : MonoBehaviour
{
    public float speed = 10f;     // velocidad de movimiento
    public float limitY = 4.5f;   // límite en Y (ajustar según tu escena)

    void Update()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            move = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            move = -1f;
        }

        // mover paddle
        transform.Translate(Vector2.up * move * speed * Time.deltaTime);

        // mantener dentro del área
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -limitY, limitY);
        transform.position = pos;
    }
}

