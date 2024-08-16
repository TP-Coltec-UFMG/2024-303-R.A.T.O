using UnityEngine;

public class Paralaxe : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed = 0.5f;
    private float startPosition;
    private float length;
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.position.x * (1 - parallaxSpeed));
        float distance = (cam.position.x * parallaxSpeed);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (temp > startPosition + length) startPosition += length;
        else if (temp < startPosition - length) startPosition -= length;
    }
}
