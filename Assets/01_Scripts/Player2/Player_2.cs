using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{
    public float speed = 10f; // velocidad de movimiento
    public float limitY = 4.5f; // l�mite en Y (aj�stalo seg�n tu escena)

    void Update()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move = -1f;
        }

        // movimiento
        transform.Translate(Vector2.up * move * speed * Time.deltaTime);

        // l�mites para que no se salga de la pantalla
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -limitY, limitY);
        transform.position = pos;
    }
}
