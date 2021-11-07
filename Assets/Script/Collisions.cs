using System.Runtime.CompilerServices;
using UnityEngine;

public static class Collisions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IntersectCircles(CircleCollider A, CircleCollider B, out Vector3 normal, out float depth)
    {
        normal = Vector3.zero;
        depth = 0;

        float distantance = Vector3.Distance(A.Center, B.Center);
        float radii = A.Radius + B.Radius;

        if (distantance >= radii)
        {
            return false;
        }

        normal = (B.Center - A.Center).normalized;
        depth = radii - distantance;

        return true;
    }
}
