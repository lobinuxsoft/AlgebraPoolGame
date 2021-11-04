using System;
using System.Runtime.CompilerServices;

[Serializable]
public struct FlatVector : IEquatable<FlatVector>
{
    public float x;
    public float y;

    public static readonly FlatVector Zero = new FlatVector(0f, 0f);

    public FlatVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FlatVector operator +(FlatVector a, FlatVector b)
    { 
        return new FlatVector(a.x + b.x, a.y + b.y); 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FlatVector operator -(FlatVector a, FlatVector b)
    {
        return new FlatVector(a.x - b.x, a.y - b.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FlatVector operator -(FlatVector v)
    {
        return new FlatVector(-v.x, -v.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FlatVector operator *(FlatVector v, float s)
    {
        return new FlatVector(v.x * s, v.y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FlatVector operator /(FlatVector v, float s)
    {
        return new FlatVector(v.x / s, v.y / s);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(FlatVector other)
    {
        return x == other.x && y == other.y;
    }

    public override bool Equals(object other)
    {
        if (!(other is FlatVector))
        {
            return false;
        }

        return Equals((FlatVector)other);
    }

    public override int GetHashCode()
    {
        return new { x, y}.GetHashCode();
    }

    public override string ToString()
    {
        return $"X: {x}, Y: {y}";
    }
}
