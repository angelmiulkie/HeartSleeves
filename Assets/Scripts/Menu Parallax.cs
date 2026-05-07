using UnityEngine;
using UnityEngine.InputSystem;

public class MenuParallax : MonoBehaviour
{
    // Variables
    public float offsetMultiplier = 1f;
    public float smoothTime = 0.3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}
