global using System;
using Microsoft.Extensions.DependencyInjection;
using ToyRobot;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IRobot>(provider => new Robot(5))
    .BuildServiceProvider();

var robot = serviceProvider.GetRequiredService<IRobot>();

Console.WriteLine("Hello Human! I'm your Toy Robot Andy. Place me on the table to start.");
Console.WriteLine("Available commands: PLACE X,Y,DIRECTION, MOVE, LEFT, RIGHT, REPORT, EXIT");
Console.WriteLine("Example: PLACE 0,0,NORTH");

while (true)
{
    var command = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

    if (command == "EXIT")
        break;

    var isCommandSuccess = false;
    var isCommandValid = true;

    if (command.StartsWith("PLACE "))
    {
        var parameters = command.Substring(6).Split(',');
        
        if (parameters.Length == 3 &&
            int.TryParse(parameters[0], out int newX) &&
            int.TryParse(parameters[1], out int newY) &&
            Enum.TryParse<Direction>(parameters[2], out Direction newDirection))
        {
            isCommandSuccess = robot.Place(newX, newY, newDirection);
        }
        else
        {
            isCommandValid = false;
            Console.WriteLine("Invalid PLACE command. Format: PLACE X,Y,DIRECTION");
        }
    }
    else
    {
        switch (command)
        {
            case "MOVE":
                isCommandSuccess = robot.Move();
                break;
            case "LEFT":
                isCommandSuccess = robot.Left();
                break;
            case "RIGHT":
                isCommandSuccess = robot.Right();
                break;
            case "REPORT":
                string report = robot.Report();
                if (!string.IsNullOrWhiteSpace(report))
                {
                    isCommandSuccess = true;
                    Console.WriteLine(report);
                }
                break;
            default:
                isCommandValid = false;
                break;
        }
    }

    if (!isCommandValid)
        Console.WriteLine("Invalid command. Available commands: PLACE X,Y,DIRECTION, MOVE, LEFT, RIGHT, REPORT, EXIT");

    if (!isCommandSuccess)
        Console.WriteLine("Command unsuccessful. Make sure you have placed me on the table first and try not to push me off the table please.");
}

Console.WriteLine("Goodbye! Press any key to exit.");
Console.ReadLine();