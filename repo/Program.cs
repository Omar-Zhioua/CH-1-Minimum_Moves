using System.Diagnostics;
using Utilities;

CalculateMinimumMoves();
return;

List<Point?> GetPath(Point end, Dictionary<Point, Point?> parents)
{
    List<Point?> path = [];
    var current = end;

    while (current != null)
    {
        path.Add(current);
        current = parents[current];
    }

    path.Reverse();
    return path;
}

List<Point> GetMoves(Point point)
{
    // All possible knight moves
    int[] dx = [2, 1, -1, -2, -2, -1, 1, 2];
    int[] dy = [1, 2, 2, 1, -1, -2, -2, -1];

    var moves = new List<Point>();

    // Get all points inside the chessboard
    for (var i = 0; i < dx.Length; i++)
    {
        var nx = point.X + dx[i];
        var ny = point.Y + dy[i];

        if (nx >= 0 && ny >= 0)
        {
            moves.Add(new Point(nx, ny));
        }
    }

    return moves;
}

List<Point?>? Bfs(Point start, Point end)
{
    var queue = new Queue<Point>();
    // Register the current point and the one that comes before
    var parents = new Dictionary<Point, Point?>();

    queue.Enqueue(start);
    parents.Add(start, null);  

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
         
        if (current.X == end.X && current.Y == end.Y)
        {
            return GetPath(current, parents);
        }

        foreach (var move in GetMoves(current).Where(move => !parents.ContainsKey(move)))
        {
            parents.Add(move,current); 
            queue.Enqueue(move);
        }
    }


    return null; 
}

int GetMultiplier(int value)
{
    return value switch
    {
        < 0 => -1,
        > 1 => 1,
        _ => 0
    };
}

List<Point> GetClosingPath(Point target)
{
    var path = new List<Point> { new Point(0, 0) };

    while (Math.Abs(path[^1].X - target.X) > 5 || Math.Abs(path[^1].Y - target.Y) > 5) // Condition to confirm that the current square is two square far at most
    {
        var current = path[^1];
        var dx = target.X - current.X; // Distance of x
        var dy = target.Y - current.Y; // Distance of y

        // The direction is the value of GetMultiplier (returns 1, -1 or 0)
        path.Add(Math.Abs(dx) >= Math.Abs(dy)
            ? new Point((current.X + 2) * GetMultiplier(dx), (current.Y + 1) * GetMultiplier(dy))
            : new Point((current.X + 1) * GetMultiplier(dx), (current.Y + 2) * GetMultiplier(dy)));
    }

    return path;
}

void CalculateMinimumMoves()
{
    Console.WriteLine("Enter x:");
    var x = int.Parse(Console.ReadLine() ?? "0");
    Console.WriteLine("Enter y:");
    var y = int.Parse(Console.ReadLine() ?? "0");

    // Step 1 : Get Closer to the square
    var end = new Point(x, y);
    var path1 = GetClosingPath(end);

    // Step 2 : Use Bfs to get the shortest path starting from a smaller chessboard
    var start = path1[^1];
    var path2 = Bfs(start, end);

    // Step 3 : Combine the two paths
    var fullPath = new List<Point?>(path1);
    fullPath.AddRange((path2 ?? []).Skip(1));

    // Step 4 : Showcase results
    foreach (var point in fullPath)
    {
        Debug.Assert(point != null, nameof(point) + " != null");
        Console.WriteLine($"({point.X}, {point.Y})");
    }
    Console.WriteLine($"\nMinimum Moves: {fullPath.Count - 1}");
    
    BuildScreenshots(fullPath);
}

void BuildScreenshots(List<Point?> path)
{
    var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ChessboardData");
    const string fileName = "moves.txt";
    var filePath = Path.Combine(directory, fileName);

    if (!Directory.Exists(directory))
    {
        Directory.CreateDirectory(directory);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Directory is ready for use");
        Console.ResetColor();
    }

    File.WriteAllText(filePath, "");

    RegisterPaths(path, filePath);
}

void RegisterPaths(List<Point?> path,string filePath)
{
    var moves = 0;
    var startAxisX = 0;
    var startAxisY = 7;
    var visited = new Dictionary<(int,int),int>();

    foreach (var step in path)
    {
        if (step != null)
        {
            visited.Add((step.X, step.Y), moves);

            if (step.Y > startAxisY)
            {
                startAxisY = step.Y;
                startAxisX = step.X - 7;
            }

            if (step.X > startAxisX + 7)
            {
                startAxisX = step.X - 7;
                startAxisY = step.Y;
            }
        }

        for (var i = startAxisY; i >= startAxisY - 7; i--)
        {
             for (var j = startAxisX; j <= startAxisX + 7; j++)
             {
                 File.AppendAllText(filePath,
                     visited.ContainsKey((j, i))
                         ? $"{visited[(j, i)]}".PadLeft(moves.ToString().Length, ' ')
                         : $".".PadLeft(moves.ToString().Length, ' '));

                 File.AppendAllText(filePath, $" ");
             }
             File.AppendAllText(filePath,"\n");
        }
        File.AppendAllText(filePath, $"\n\n");
        
        File.AppendAllText(filePath, $"Move: {moves + 1}");
        
        File.AppendAllText(filePath, $"\n\n");
        moves++;
    }
}