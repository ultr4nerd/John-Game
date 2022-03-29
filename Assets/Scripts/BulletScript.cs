using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip sound;
    public float speed;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (Camera.main != null) Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _direction * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        JohnMovement john = other.GetComponent<JohnMovement>();
        GruntScript grunt = other.GetComponent<GruntScript>();

        if (john != null)
        {
            john.Hit();
        }

        if (grunt != null)
        {
            grunt.Hit();
        }

        DestroyBullet();
    }
}