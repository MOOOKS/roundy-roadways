using System.Collections;
using UnityEngine;

public class carMove : MonoBehaviour
{
    float radius = 3;
    public float speed = 1f;
    public float skippingThreshold = 100f;
    public Vector3 center = Vector3.zero;
    public float angleStart;

    public Transform lane;

    public float angle;
    public float angleDeg;
    bool hasPassed = false;

    laneManager _laneManager;

    int index = -1;

    float lastJouleTime;
    float currentEPS;

    private void Start()
    {
        _laneManager = GetComponentInParent<laneManager>();
        speed = _laneManager.carSpeed;
        angle = angleStart * Mathf.Deg2Rad;

        radius = _laneManager.roadRadius != 0 ? _laneManager.roadRadius : 3;
        lastJouleTime = Time.time;
    }

    void Update()
    {
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 offset = new Vector3(x, y, 0);
        transform.position = center + offset;

        float angleDelta = (speed / radius) * Time.deltaTime;
        angle += angleDelta;

        angleDeg = (angle * Mathf.Rad2Deg) % 360f;
        if (angleDeg < 0) angleDeg += 360f;

        float eps = speed / (2f * Mathf.PI * radius);
        currentEPS = eps;

        if (index < 0)
        {
            upgrades.instance.carEps.Add(currentEPS);
            index = upgrades.instance.carEps.Count - 1;
        }
        else
        {
            upgrades.instance.carEps[index] = currentEPS;
        }

        if (speed >= skippingThreshold)
        {
            upgrades.instance.Joules += eps * Time.deltaTime;
        }
        else
        {
            if (!hasPassed && angleDeg > 265)
            {
                upgrades.instance.Joules++;
                hasPassed = true;

                float timeSinceLast = Time.time - lastJouleTime;
                if (timeSinceLast > 0f)
                {
                    currentEPS = 1f / timeSinceLast;
                    lastJouleTime = Time.time;
                }

                upgrades.instance.carEps[index] = currentEPS;
            }
            else if (hasPassed && angleDeg < 265)
            {
                hasPassed = false;
            }
        }

        // === ROTATE TO FACE FORWARD ON CIRCLE ===
        // The tangent vector (forward direction) is perpendicular to the radius vector
        Vector3 tangent = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0);
        transform.up = tangent; // Make the car "face" the tangent
    }
}
