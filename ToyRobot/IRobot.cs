namespace ToyRobot;

public interface IRobot
{
    public bool Place(int x, int y, Direction direction);
    public bool Move();
    public bool Right();
    public bool Left();
    public string Report();

}