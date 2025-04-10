Console.WriteLine("Enter x :");
int x = int.Parse(Console.ReadLine());
Console.WriteLine("Enter y :");
int y = int.Parse(Console.ReadLine());

List<int[]> BackTrace(List<List<int[]>> parent , int[] start ,int[] end)
{
    Console.WriteLine("Backtrace working...");
    List<int[]> path = new List<int[]>();
    path.Add(end);
    while (!(path[path.Count - 1][0] == start[0] && path[path.Count - 1][1] == start[0]))
    {
        for (int i = 0; i < parent.Count - 1; i++)
        {
            if (parent[i][0][0] == path[path.Count -1][0] && parent[i][0][1] == path[path.Count -1][1])
            {
                path.Add(new int[] { parent[i][1][0], parent[i][1][1] });
                break;
            }
        }
    }
    
    path.Reverse();
    
    Console.WriteLine("Backtrace finished.");
    
    return path;
}

List<int[]> Bfs(int[] start,int[] end)
{   
    List<int[]> possibleMoves = new List<int[]>();
    List<int[]> visited = new List<int[]>();
    List<List<int[]>> parent = new List<List<int[]>>();
    
    possibleMoves.Add(start);
    
    
    while (possibleMoves.Count > 0)
    {   
        int x = possibleMoves[0][0];
        int y = possibleMoves[0][1];
        
        possibleMoves.RemoveAt(0);
        visited.Add(new int[] { x, y });
        
        if (x == end[0] && y == end[1])
        {
            return BackTrace(parent,start,new int[]{x,y});
        }
        else
        {
            if (!(visited.Any(v => v.SequenceEqual(new int[] { x + 2 , y + 1}))))
            {
                possibleMoves.Add(new int[] { x + 2, y + 1 });
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x + 2, y + 1 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            }
            
            if (!(visited.Any(v => v.SequenceEqual(new int[] { x + 1, y + 2 }))))
            {
                possibleMoves.Add(new int[] { x + 1 , y + 2 });
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x + 1, y + 2 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            }

            if (x - 2 >= 0 && !(visited.Any(v => v.SequenceEqual(new int[] { x - 2 , y + 1 }))))
            {
                possibleMoves.Add(new int[] { x - 2 , y  + 1});
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x - 2, y + 1 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            }
            if (x - 1 >= 0 && !(visited.Any(v => v.SequenceEqual(new int[] { x - 1 , y + 2 }))))
            {
                possibleMoves.Add(new int[] { x - 1 , y  + 2});
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x - 1, y + 2 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            }
            if (x - 2 >= 0 && y - 1 >= 0 && !(visited.Any(v => v.SequenceEqual(new int[] { x - 2, y - 1 }))))
            {
                possibleMoves.Add(new int[] { x - 2 , y - 1});
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x - 2, y - 1 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            }
            if (x - 1 >= 0 && y - 2 >= 0 && !(visited.Any(v => v.SequenceEqual(new int[] { x - 1, y - 2 }))))
            {
                possibleMoves.Add(new int[] { x - 1 , y - 2});
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x - 1, y - 2 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            }if (y - 1 >= 0 && !(visited.Any(v => v.SequenceEqual(new int[] { x + 2, y - 1 }))))
            {
                possibleMoves.Add(new int[] { x + 2 , y - 1});
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x + 2, y - 1 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            }if (y - 2 >= 0 && !(visited.Any(v => v.SequenceEqual(new int[] { x + 1, y - 2 }))))
            {
                possibleMoves.Add(new int[] { x + 1 , y  - 2});
                List<int[]> innerList = new List<int[]>
                {
                    new[] { x + 1, y - 2 },
                    new[] { x, y }
                };
                parent.Add(innerList);
            
            }
        }

    }
    
    return possibleMoves;
}

List<int[]> GetClose(int [] position)
{
    List<int[]> path = new List<int[]>();
    
    path.Add(new int[] {0,0});
    
    int x = path[path.Count - 1][0];
    int y = path[path.Count - 1][1];
    
    while (!(x >= position[0] - 7 && x <= position[0] + 7 && y >= position[1] - 7 && y <= position[1] + 7))
    {
        Console.WriteLine($"({x}, {y})");
        
        int c = y - x;
        int d = y + x;
        int endc = (position[1] - y) - (position[0] - x);
        int endd = (position[1] - y) + (position[0] - x);
        
        //First verification
        
        if (c == endc || c + 1 == endc)
        {
            if (position[1] > y)
            {
                path.Add(new int[] { x + 2, y + 1});
            }
            else
            {
                path.Add(new int[] { x - 1, y - 2 });
            }
        }
        else if (c - 1 == endc)
        {
            if (position[1] > y)
            {
                path.Add(new int[] { x + 1, y + 2});
            }
            else
            {
                path.Add(new int[] { x - 2, y - 1 });
            }
        }
        
        //Second verification
        
        else if (d == endd || d + 1 == endd)
        {
            if (position[1] > y)
            {
                path.Add(new int[] { x - 1, y + 2 });
            }
            else
            {
                path.Add(new int[] { x + 2, y - 1 });
            }
        }
        else if (d - 1 == endd)
        {
            if (position[1] > y)
            {
                path.Add(new int[] { x - 2, y + 1 });
            }
            else
            {
                path.Add(new int[] { x + 1, y - 2 });
            }
        }
        
        //Third verification

        else if (endc < c && endd > d)
        {
            path.Add(new int[] { x + 2, y + 1 });
        }

        else if (endc > c && endd > d)
        {
            path.Add(new int[] { x + 1, y + 2 });
        }

        else if (endd < d && endc > c)
        {
            path.Add(new int[] { x - 2, y + 1 });
        }

        else if (endd < d && endc < c)
        {
            path.Add(new int[] { x + 1, y - 2 });
        }
        
        
        x = path[path.Count - 1][0];
        y = path[path.Count - 1][1];
        
    }
    
    
    /*CalculateMinimumMoves(new int[]{x,y}, position)*/

    return path;
}

List<int[]> CalculateMinimumMoves(int[] position)
{ 
    List<int[]> path1 = GetClose(position);
    
    Console.WriteLine($"Bfs start working...");
    
    List<int[]> path2 = Bfs(path1[path1.Count - 1],position);
    
    List<int[]> path = new List<int[]>(path1);
    
    path.AddRange(path2.Skip(1));

    Console.WriteLine($"Minimum Moves : {path.Count - 1}");
    
    foreach (var pos in path)
    {
        Console.WriteLine($"({pos[0]},{pos[1]})");
    }
    
    return path;
}


CalculateMinimumMoves(new int[]{x,y});