using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PaddleHitSfx : MonoBehaviour
{
    [Header("Clips de golpe")]
    public AudioClip[] hitClips; // puedes poner 1 o varios para variar
    [Range(0f, 1f)] public float volume = 0.85f;

    [Header("Detección")]
    public string ballTag = "Ball";   // ponle este tag a tu bola
    public float cooldown = 0.05f;    // evita spam si hay múltiples contactos
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;

    AudioSource src;
    float nextAllowed;

    void Awake()
    {
        src = GetComponent<AudioSource>();
        src.playOnAwake = false;
        src.loop = false;
        src.spatialBlend = 0f; // 2D
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(ballTag)) TryPlay();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ballTag)) TryPlay();
    }

    void TryPlay()
    {
        if (Time.time < nextAllowed) return;
        if (hitClips == null || hitClips.Length == 0) return;

        var clip = hitClips[Random.Range(0, hitClips.Length)];
        src.pitch = Random.Range(minPitch, maxPitch);
        src.PlayOneShot(clip, volume);

        nextAllowed = Time.time + cooldown;
    }
}
