using UnityEngine;

public class setRoadSize : MonoBehaviour
{
    public float radius = 5f;
    public float thickness = 1f;
    public int segments = 64;

    public GameObject roundLine;
    public void roadSize(float size)
    {
        radius = size;
    }
    void Start()
    {
        GenerateVisualMesh();
        GenerateDonutCollider();

        if (roundLine != null)
        {
            Vector2 down = Vector2.down;
            roundLine.transform.position = (Vector2)transform.position + down * radius;
        }
    }

    void GenerateDonutCollider()
    {
        var collider = GetComponent<PolygonCollider2D>();
        collider.pathCount = 2;

        Vector2[] outer = new Vector2[segments];
        Vector2[] inner = new Vector2[segments];

        float angleStep = 2 * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            outer[i] = new Vector2(cos, sin) * (radius + thickness / 2f);
            inner[i] = new Vector2(cos, sin) * (radius - thickness / 2f);
        }

        System.Array.Reverse(inner); // Reverse so it's a hole

        collider.SetPath(0, outer);
        collider.SetPath(1, inner);
    }

    void GenerateVisualMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[segments * 2];
        int[] triangles = new int[segments * 6];

        float angleStep = 2 * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            vertices[i * 2] = dir * (radius - thickness / 2f);
            vertices[i * 2 + 1] = dir * (radius + thickness / 2f);
        }

        for (int i = 0; i < segments; i++)
        {
            int next = (i + 1) % segments;

            int a = i * 2;
            int b = i * 2 + 1;
            int c = next * 2;
            int d = next * 2 + 1;

            triangles[i * 6 + 0] = a;
            triangles[i * 6 + 1] = c;
            triangles[i * 6 + 2] = d;

            triangles[i * 6 + 3] = a;
            triangles[i * 6 + 4] = d;
            triangles[i * 6 + 5] = b;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }


}
