using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject john;

    void Update()
    {
        if (john == null) return;
        
        Vector3 position = transform.position;
        position.x = john.transform.position.x;
        transform.position = position;
    }
}