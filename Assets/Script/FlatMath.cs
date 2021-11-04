using System;
using System.Runtime.CompilerServices;

public static class FlatMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Magnitude(FlatVector v)
    {
        return (float)Math.Sqrt(v.x * v.x + v.y * v.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Distance(FlatVector a, FlatVector b)
    {
        float dx = a.x - b.x;
        float dy = a.y - b.y;
        return (float)Math.Sqrt(dx * dx + dy * dy);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FlatVector Normalize(FlatVector v)
    {
        float magnitude = Magnitude(v);
        return new FlatVector(v.x / magnitude, v.y / magnitude);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Dot(FlatVector a, FlatVector b)
    {
        return a.x * b.x + a.y * b.y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cross(FlatVector a, FlatVector b)
    {
        return a.x * b.y - a.y * b.x;
    }
}
