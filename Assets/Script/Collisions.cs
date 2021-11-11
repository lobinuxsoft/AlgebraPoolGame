using System.Runtime.CompilerServices;
using UnityEngine;

public static class Collisions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IntersectCirclePolygon(CircleCollider circle, Vector3[] vertices, out Vector3 normal, out float depth)
    {
        normal = Vector3.zero;
        depth = float.MaxValue;

        Vector3 axis = Vector3.zero;

        float axisDepth = 0;
        float minA, maxA, minB, maxB;

        // Busco entre todos los vertices de A para generar una proyeccion y ver si hay una separacion
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 va = vertices[i];
            Vector3 vb = vertices[(i + 1) % vertices.Length];

            Vector3 edge = vb - va;
            axis = new Vector3(-edge.y, edge.x);

            ProjectVertices(vertices, axis, out minA, out maxA);
            ProjectCircle(circle, axis, out minB, out maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            axisDepth = Mathf.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        int closestPointIndex = FindClosestPointOnPolygon(circle.Center, vertices);
        Vector3 closestPoint = vertices[closestPointIndex];

        axis = closestPoint - circle.Center;

        ProjectVertices(vertices, axis, out minA, out maxA);
        ProjectCircle(circle, axis, out minB, out maxB);

        if (minA >= maxB || minB >= maxA)
        {
            return false;
        }

        axisDepth = Mathf.Min(maxB - minA, maxA - minB);

        if (axisDepth < depth)
        {
            depth = axisDepth;
            normal = axis;
        }

        depth /= normal.magnitude;

        normal.Normalize();

        Vector3 polygonCenter = FindArithmeticMean(vertices);

        Vector3 direction = polygonCenter - circle.Center;

        if (Vector3.Dot(direction, normal) < 0)
        {
            normal = -normal;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int FindClosestPointOnPolygon(Vector3 circleCenter, Vector3[] vertices)
    {
        int result = -1;
        float minDistance = float.MaxValue;

        for (int i = 0; i < vertices.Length; i++)
        {
            float distance = Vector3.Distance(vertices[i], circleCenter);

            if (distance < minDistance)
            {
                minDistance = distance;
                result = i;
            }
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ProjectCircle(CircleCollider circle, Vector3 axis, out float min, out float max)
    {
        Vector3 direction = axis.normalized;
        Vector3 directionAndRadius = direction * circle.Radius;

        Vector3 p1 = circle.Center + directionAndRadius;
        Vector3 p2 = circle.Center - directionAndRadius;

        min = Vector3.Dot(p1, axis);
        max = Vector3.Dot(p2, axis);

        if(min > max)
        {
            float aux = min;
            min = max;
            max = aux;
        }
    }

    // Teorema de la separacion de ejes
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IntersectPolygons(Vector3[] verticesA, Vector3[] verticesB, out Vector3 normal, out float depth) 
    {
        normal = Vector3.zero;
        depth = float.MaxValue;

        // Busco entre todos los vertices de A para generar una proyeccion y ver si hay una separacion
        for (int i = 0; i < verticesA.Length; i++)
        {
            Vector3 va = verticesA[i];
            Vector3 vb = verticesA[(i + 1) % verticesA.Length];

            Vector3 edge = vb - va;
            Vector3 axis = new Vector3(-edge.y, edge.x);

            ProjectVertices(verticesA, axis, out float minA, out float maxA);
            ProjectVertices(verticesB, axis, out float minB, out float maxB);

            if(minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axisDepth = Mathf.Min(maxB - minA, maxA - minB);

            if(axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        // Lo mismo que con los vertices A pero con los vertices B
        for (int i = 0; i < verticesB.Length; i++)
        {
            Vector3 va = verticesB[i];
            Vector3 vb = verticesB[(i + 1) % verticesB.Length];

            Vector3 edge = vb - va;
            Vector3 axis = new Vector3(-edge.y, edge.x);

            ProjectVertices(verticesA, axis, out float minA, out float maxA);
            ProjectVertices(verticesB, axis, out float minB, out float maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            float axisDepth = Mathf.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }
        }

        depth /= normal.magnitude;

        normal.Normalize();

        Vector3 centerA = FindArithmeticMean(verticesA);
        Vector3 centerB = FindArithmeticMean(verticesB);

        Vector3 direction = centerB - centerA;

        if(Vector3.Dot(direction, normal) < 0)
        {
            normal = -normal;
        }

        return true;
    }

    private static Vector3 FindArithmeticMean(Vector3[] vertices)
    {
        float sumX = 0;
        float sumY = 0;

        for (int i = 0; i < vertices.Length; i++)
        {
            sumX += vertices[i].x;
            sumY += vertices[i].y;
        }

        return new Vector3(sumX / (float)vertices.Length, sumY / (float)vertices.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ProjectVertices(Vector3[] vertices, Vector3 axis, out float min, out float max)
    {
        min = float.MaxValue;
        max = float.MinValue;
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            float projection = Vector3.Dot(v, axis);

            if (projection < min) { min = projection; }
            if (projection > max) { max = projection; }
        }
    }

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
