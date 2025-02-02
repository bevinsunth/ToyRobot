namespace ToyRobot;

public interface IRobot
{
    public int? X { get; }
    public int? Y { get; }
    public Direction Direction { get; }
    public bool Place(int x, int y, Direction direction);
    public bool Move();
    public bool Right();
    public bool Left();
    public string Report();

}