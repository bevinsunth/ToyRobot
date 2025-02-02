using System.Runtime.CompilerServices;

namespace ToyRobot;

public class Robot(int tableSize) : IRobot
{
    public int? X { private set; get; }
    public int? Y { private set; get; }
    public Direction Direction {private set; get; }
    private readonly int _tableSize = tableSize;

    public bool Place(int x, int y, Direction direction)
    {
        if (!IsValidPosition(x, y))
            return false;
        X = x;
        Y = y;
        Direction = direction;
        return true;
    }

    public bool Move()
    {
        var newX = X;
        var newY = Y;

        switch (Direction)
        {
            case Direction.NORTH:
                newY++;
                break;
            case Direction.SOUTH:
                newY--;
                break;
            case Direction.EAST:
                newX++;
                break;
            case Direction.WEST:
                newX--;
                break;
        }

        if (!IsValidPosition(newX, newY))
            return false;

        X = newX;
        Y = newY;
        return true;
    }

    public bool Left()
    {
        Direction = Direction switch
        {
            Direction.NORTH => Direction.WEST,
            Direction.WEST => Direction.SOUTH,
            Direction.SOUTH => Direction.EAST,
            Direction.EAST => Direction.NORTH,
            _ => Direction
        };
        return true;
    }

    public bool Right()
    {
        Direction = Direction switch
        {
            Direction.NORTH => Direction.EAST,
            Direction.EAST => Direction.SOUTH,
            Direction.SOUTH => Direction.WEST,
            Direction.WEST => Direction.NORTH,
            _ => Direction
        };
        return true;
    }

    private bool IsValidPosition(int? x, int? y)
    {
        return x >= 0 && x < _tableSize && y >= 0 && y < _tableSize;
    }

    public string Report()
    {
        if (IsValidPosition(X, Y))
            return $"{X},{Y},{Direction}";
        else
            return null;
    }
}

public enum Direction
{
    NORTH = 0,
    SOUTH = 1,
    EAST = 2,
    WEST = 3,
}