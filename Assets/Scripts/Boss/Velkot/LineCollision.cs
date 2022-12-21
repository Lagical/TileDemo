using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VelkotLaserwipe), typeof(PolygonCollider2D))]
public class LineCollision : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;

    private VelkotLaserwipe laser;

    private List<Vector2> colliderPoints = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<VelkotLaserwipe>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        colliderPoints = CalculateColliderPoints();
        polygonCollider2D.SetPath(0, colliderPoints);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (colliderPoints != null)
            colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
    }
    private List<Vector2> CalculateColliderPoints()
    {


        Vector3 startPos = laser.lineRenderer.GetPosition(0);
        Vector3 endPos = laser.lineRenderer.GetPosition(1);

        // Get the Width of the Line
        float width = laser.lineRenderer.startWidth;

        // m = (y2- y1) / (x2 - x1)
        float m = (endPos.y - startPos.y) / (endPos.x - startPos.x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 * m * m, 0.5f));

        // Calculate the offset from each point to the collision vertex
        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        // Generate the Colliders Vetices
        List<Vector2> colliderPositions = new List<Vector2>
        {
            startPos + offsets[0],
            endPos + offsets[0],
            endPos + offsets[1],
            startPos + offsets[1],
        };
        return colliderPositions;
    }
}
