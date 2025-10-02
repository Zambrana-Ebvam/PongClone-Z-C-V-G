using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f; // Velocidad de la pelota
    private Rigidbody2D rb;

    // Ángulo mínimo permitido en Y (para evitar que se quede recta)
    public float minY = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void LaunchBall()
    {
        float dirX = Random.Range(0, 2) == 0 ? -1 : 1;
        float dirY = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(dirX, dirY).normalized;

        // Forzar ángulo mínimo
        if (Mathf.Abs(dir.y) < minY)
        {
            dir.y = (dir.y >= 0 ? minY : -minY);
        }

        rb.velocity = dir.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Rebote especial con jugadores
        if (collision.gameObject.CompareTag("Player"))
        {
            float y = HitFactor(transform.position,
                                collision.transform.position,
                                collision.collider.bounds.size.y);

            float x = collision.transform.position.x < 0 ? 1 : -1;

            Vector2 dir = new Vector2(x, y).normalized;

            // Forzar ángulo mínimo
            if (Mathf.Abs(dir.y) < minY)
            {
                dir.y = (dir.y >= 0 ? minY : -minY);
            }

            rb.velocity = dir * speed;
        }
        // Rebote con paredes
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 dir = rb.velocity.normalized;

            // Determinar si es pared superior o inferior
            if (collision.transform.position.y > 0) // pared arriba
            {
                if (dir.y >= 0) dir.y = -minY; // aseguramos que vaya hacia abajo
            }
            else // pared abajo
            {
                if (dir.y <= 0) dir.y = minY; // aseguramos que vaya hacia arriba
            }

            // Forzar ángulo mínimo
            if (Mathf.Abs(dir.y) < minY)
            {
                dir.y = (dir.y > 0 ? minY : -minY);
            }

            rb.velocity = dir.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoalLeft"))
        {
            Debug.Log("Punto para Player 2!");
            GameManager.instance.AddScore(2); // 👈 actualizar score
            ResetBall(1);
        }
        else if (collision.CompareTag("GoalRight"))
        {
            Debug.Log("Punto para Player 1!");
            GameManager.instance.AddScore(1); // 👈 actualizar score
            ResetBall(-1);
        }
    }


    void ResetBall(int dirX)
    {
        transform.position = Vector2.zero;
        float dirY = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(dirX, dirY).normalized;

        // Forzar ángulo mínimo
        if (Mathf.Abs(dir.y) < minY)
        {
            dir.y = (dir.y >= 0 ? minY : -minY);
        }

        rb.velocity = dir * speed;
    }

    // Calcula dónde tocó en la paleta (-1 abajo, 0 centro, 1 arriba)
    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }
}
