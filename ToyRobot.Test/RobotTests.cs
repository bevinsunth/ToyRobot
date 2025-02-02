namespace ToyRobot.Test;

public class RobotTests
{
    private readonly IRobot _robot = new Robot(5);

    [Theory]
    [InlineData(0, 0, Direction.NORTH, true)]
    [InlineData(4, 4, Direction.SOUTH, true)]
    [InlineData(-1, 0, Direction.NORTH, false)]
    [InlineData(0, -1, Direction.SOUTH, false)]
    [InlineData(5, 0, Direction.EAST, false)]
    [InlineData(0, 5, Direction.WEST, false)]
    public void Place_ShouldReturnExpectedResult(int x, int y, Direction direction, bool expected)
    {
        // Act
        var result = _robot.Place(x, y, direction);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Move_WhenNotPlaced_ShouldReturnFalse()
    {
        // Act
        var result = _robot.Move();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(0, 0, Direction.NORTH, 0, 1, true)]
    [InlineData(0, 4, Direction.NORTH, 0, 4, false)] // Would fall off
    [InlineData(0, 0, Direction.SOUTH, 0, 0, false)] // Would fall off
    [InlineData(0, 0, Direction.EAST, 1, 0, true)]
    [InlineData(4, 0, Direction.EAST, 4, 0, false)] // Would fall off
    [InlineData(1, 0, Direction.WEST, 0, 0, true)]
    public void Move_ShouldMoveCorrectly(int initialX, int initialY, Direction direction,
        int expectedX, int expectedY, bool expectedResult)
    {
        // Arrange
        _robot.Place(initialX, initialY, direction);

        // Act
        var result = _robot.Move();

        // Assert
        Assert.Equal(expectedResult, result);
        if (expectedResult)
        {
            _robot.Report(); // This should update the last reported position
            Assert.Equal($"{expectedX},{expectedY},{direction}", $"{_robot.X},{_robot.Y},{_robot.Direction}");
        }
    }

    [Theory]
    [InlineData(Direction.NORTH, Direction.WEST)]
    [InlineData(Direction.WEST, Direction.SOUTH)]
    [InlineData(Direction.SOUTH, Direction.EAST)]
    [InlineData(Direction.EAST, Direction.NORTH)]
    public void Left_ShouldRotateCorrectly(Direction initialDirection, Direction expectedDirection)
    {
        // Arrange
        _robot.Place(0, 0, initialDirection);

        // Act
        var result = _robot.Left();

        // Assert
        Assert.True(result);
        _robot.Report();
        Assert.Equal(expectedDirection, _robot.Direction);
    }

    [Theory]
    [InlineData(Direction.NORTH, Direction.EAST)]
    [InlineData(Direction.EAST, Direction.SOUTH)]
    [InlineData(Direction.SOUTH, Direction.WEST)]
    [InlineData(Direction.WEST, Direction.NORTH)]
    public void Right_ShouldRotateCorrectly(Direction initialDirection, Direction expectedDirection)
    {
        // Arrange
        _robot.Place(0, 0, initialDirection);

        // Act
        var result = _robot.Right();

        // Assert
        Assert.True(result);
        _robot.Report();
        Assert.Equal(expectedDirection, _robot.Direction);
    }

    [Fact]
    public void Report_WhenNotPlaced_ShouldReturnFalse()
    {
        // Act
        var result = _robot.Report();

        // Assert
        Assert.Null(result);
    }
    

    [Fact]
    public void ComplexMovement_ShouldWorkAsExpected()
    {
        // Arrange & Act
        _robot.Place(1, 2, Direction.EAST);
        _robot.Move();
        _robot.Move();
        _robot.Left();
        _robot.Move();

        // Assert
        _robot.Report();
        Assert.Equal("3,3,NORTH", $"{_robot.X},{_robot.Y},{_robot.Direction}");
    }
}