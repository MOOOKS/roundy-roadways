using UnityEngine;
using UnityEngine.InputSystem;

public class zoom : MonoBehaviour
{
    public InputActionReference zoomAction; // assign in Inspector
    public float zoomStep = 1f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        zoomAction.action.Enable();
    }

    void Update()
    {
        Vector2 scrollDelta = zoomAction.action.ReadValue<Vector2>();
        float scrollY = scrollDelta.y;

        if (Mathf.Abs(scrollY) > 0.01f)
        {
            float newSize = Mathf.Clamp(_cam.orthographicSize - scrollY * zoomStep, minZoom, maxZoom);
            _cam.orthographicSize = newSize;
        }
    }
}
