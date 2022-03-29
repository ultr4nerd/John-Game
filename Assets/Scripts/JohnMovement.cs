using System;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed;
    public float jumpForce;
    public AudioClip hit;

    private static readonly int Running = Animator.StringToHash("running");

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _horizontal;
    private bool _grounded;
    private float _lastShoot;
    private int _health = 5;

    public void Hit()
    {
        _health -= 1;
        if (_health == 0)
        {
            if (Camera.main != null) Camera.main.GetComponent<AudioSource>().PlayOneShot(hit);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (_horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (_horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        _animator.SetBool(Running, _horizontal != 0.0f);

        // Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        _grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.1f);

        if (Input.GetKeyDown(KeyCode.W) && _grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > _lastShoot + 0.25f)
        {
            Shoot();
            _lastShoot = Time.time;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void Shoot()
    {
        Vector3 direction = transform.localScale.x == 1.0f ? Vector2.right : Vector2.left;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
}