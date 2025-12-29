using UnityEngine;

public class carMoveMenu : MonoBehaviour
{
    public float radius;
    public float speed;
    public Vector3 center;

    float angle = 0;
    void Update()
    {
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 offset = new Vector3(x, y, 0);
        transform.position = center + offset;

        float angleDelta = (speed / radius) * Time.deltaTime;
        angle += angleDelta;

        Vector3 tangent = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0);
        transform.up = tangent;

    }
}
