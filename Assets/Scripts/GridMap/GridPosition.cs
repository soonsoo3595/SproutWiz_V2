using System;

public class GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int y;

    public GridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               y == position.y;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        return $"x: {x}, y: {y}";
    }

    public static bool operator ==(GridPosition a, GridPosition b)
    {
        // 두 객체 모두 null인 경우 true를 반환
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        // 하나의 객체만 null인 경우 false를 반환
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;


        return (a.x == b.x) && (a.y == b.y);
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public static bool operator >(GridPosition a, GridPosition b)
    {
        return a.x > b.x && a.y > b.y;
    }

    public static bool operator <(GridPosition a, GridPosition b)
    {
        return a.x < b.x && a.y < b.y;
    }

    public static bool operator >=(GridPosition a, GridPosition b)
    {
        return a.x >= b.x && a.y >= b.y;
    }

    public static bool operator <=(GridPosition a, GridPosition b)
    {
        return a.x <= b.x && a.y <= b.y;
    }


    // GridPosition과 정수 직접비교
    public static bool operator >(GridPosition a, (int x, int y) b)
    {
        return a.x > b.x && a.y > b.y;
    }

    public static bool operator <(GridPosition a, (int x, int y) b)
    {
        return a.x < b.x && a.y < b.y;
    }

    public static bool operator >=(GridPosition a, (int x, int y) b)
    {
        return a.x >= b.x && a.y >= b.y;
    }

    public static bool operator <=(GridPosition a, (int x, int y) b)
    {
        return a.x <= b.x && a.y <= b.y;
    }

    public static bool operator ==(GridPosition a, (int x, int y) b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(GridPosition a, (int x, int y) b)
    {
        return !(a.x == b.x && a.y == b.y);
    }
}
