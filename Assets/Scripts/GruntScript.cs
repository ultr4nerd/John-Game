using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject john;
    public GameObject bulletPrefab;
    public AudioClip hit;

    private float _lastShoot;
    private int _health = 3;
    
    public void Hit()
    {
        _health -= 1;
        if (_health == 0)
        {
            if (Camera.main != null) Camera.main.GetComponent<AudioSource>().PlayOneShot(hit);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
        if (john == null) return;
        
        var direction = john.transform.position - transform.position;
        transform.localScale = direction.x >= 0.0f ? new Vector3(1.0f, 1.0f, 1.0f) : new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(john.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > _lastShoot + 0.25f)
        {
            Shoot();
            _lastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction = transform.localScale.x == 1.0f ? Vector2.right : Vector2.left;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
}
