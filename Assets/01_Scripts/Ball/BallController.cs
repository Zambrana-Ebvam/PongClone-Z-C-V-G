using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f; // Velocidad de la pelota
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Lanzar la pelota en una dirección aleatoria al inicio
        float dirX = Random.Range(0, 2) == 0 ? -1 : 1;
        float dirY = Random.Range(-1f, 1f);

        rb.velocity = new Vector2(dirX, dirY).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Rebota automáticamente gracias al Rigidbody2D con física
        // Puedes poner sonidos o efectos aquí si quieres
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detectar si la pelota pasa las porterías (lado izquierdo o derecho)
        if (collision.CompareTag("GoalLeft"))
        {
            Debug.Log("Punto para Player 2!");
            ResetBall(1); // Lanza hacia la derecha
        }
        else if (collision.CompareTag("GoalRight"))
        {
            Debug.Log("Punto para Player 1!");
            ResetBall(-1); // Lanza hacia la izquierda
        }
    }

    void ResetBall(int dirX)
    {
        // Reinicia la pelota al centro
        transform.position = Vector2.zero;

        // Nueva dirección con el eje X indicado
        float dirY = Random.Range(-1f, 1f);
        rb.velocity = new Vector2(dirX, dirY).normalized * speed;
    }
}
