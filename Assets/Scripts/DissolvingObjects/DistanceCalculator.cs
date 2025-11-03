using System;
using UnityEngine;

public static class DistanceCalculator
{
    public static float Distance(Collider collider1, Collider collider2)
    {
        if (collider1 == null || collider2 == null) 
            throw new Exception();

        Bounds bounds0 = collider1.bounds;
        Bounds bounds1 = collider2.bounds;

        SdBounds(bounds0.center, bounds1, out Vector3 conjecture0);
        SdBounds(bounds1.center, bounds0, out Vector3 conjecture1);

        return Vector3.Distance(conjecture0, conjecture1);
    }

    static float SdBounds(Vector3 point, Bounds bounds, out Vector3 contact)
    {
        Vector3 dir = point - bounds.center;
        float sd = SdBox(dir, bounds.extents);

        contact = point - dir.normalized * sd;

        return sd;
    }

    static float SdBox(Vector3 p, Vector3 b)
    {
        Vector3 q = new Vector3(Mathf.Abs(p.x), Mathf.Abs(p.y), Mathf.Abs(p.z)) - b;
        return Vector3.Magnitude(Vector3.Max(q, Vector3.zero)) + Mathf.Min(Mathf.Max(q.x, Mathf.Max(q.y, q.z)), 0f);
    }
}