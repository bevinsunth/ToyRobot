using System.Runtime.CompilerServices;

namespace ToyRobot;

public class Robot(int tableSize) : IRobot
{
    private int? _x;
    private int? _y;
    private Direction _direction;
    private readonly int _tableSize = tableSize;

    public bool Place(int x, int y, Direction direction)
    {
        if (!IsValidPosition(x, y))
            return false;
        _x = x;
        _y = y;
        _direction = direction;
        return true;
    }

    public bool Move()
    {
        var newX = _x;
        var newY = _y;

        switch (_direction)
        {
            case Direction.NORTH:
                newX++;
                break;
            case Direction.SOUTH:
                newX--;
                break;
            case Direction.EAST:
                newY++;
                break;
            case Direction.WEST:
                newY--;
                break;
        }

        if (!IsValidPosition(newX, newY))
            return false;

        _x = newX;
        _y = newY;
        return true;
    }

    public bool Left()
    {
        _direction = _direction switch
        {
            Direction.NORTH => Direction.WEST,
            Direction.WEST => Direction.SOUTH,
            Direction.SOUTH => Direction.EAST,
            Direction.EAST => Direction.NORTH,
            _ => _direction
        };
        return true;
    }

    public bool Right()
    {
        _direction = _direction switch
        {
            Direction.NORTH => Direction.EAST,
            Direction.EAST => Direction.SOUTH,
            Direction.SOUTH => Direction.WEST,
            Direction.WEST => Direction.NORTH,
            _ => _direction
        };
        return true;
    }

    private bool IsValidPosition(int? x, int? y)
    {
        return x >= 0 && x < _tableSize && y >= 0 && y < _tableSize;
    }

    public string Report() => $"{_x},{_y},{_direction}";
}

public enum Direction
{
    NORTH = 0,
    SOUTH = 1,
    EAST = 2,
    WEST = 3,
}