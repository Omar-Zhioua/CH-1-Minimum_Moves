void CalculateMinimumMoves()
{ 
    // Coordinates of the square you want to get to
    Console.WriteLine("Enter x :");
    int x = int.Parse(Console.ReadLine());
    Console.WriteLine("Enter y :");
    int y = int.Parse(Console.ReadLine());
    int[] position = new[] { x, y };
    
    // Get all moves till you get to a smaller square surroinding the point (x,y)
    List<int[]> path1 = GetClose(position);
    
    // Get the shortest path from that square to the desired coordinates (x,y)
    List<int[]> path2 = Bfs(path1[path1.Count - 1],position);
    
    // Combine the two paths to get the completed path
    List<int[]> path = new List<int[]>(path1);
    path.AddRange(path2.Skip(1));

    Console.WriteLine($"Minimum Moves : {path.Count - 1}");
    
    foreach (var pos in path)
    {
        Console.WriteLine($"({pos[0]},{pos[1]})");
    }
}

// Program initialization
CalculateMinimumMoves();

// Close search breath-first-search
List<int[]> Bfs(int[] start,int[] end)
{   
    // store the moves,visted pointes and each point's parent
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
            // Tracing the path using the parent list
            return BackTrace(parent,start,new int[]{x,y});
        }
        else
        {
            // Add each visited square's next move to the possibleMoves list
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

// Backtrace using a parent list where each element contains a square coordinates and its previous square 
List<int[]> BackTrace(List<List<int[]>> parent , int[] start ,int[] end)
{
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
    
    return path;
}

// Get the knight closer to the desired square to avoid long program runtime
List<int[]> GetClose(int [] position)
{
    List<int[]> path = new List<int[]>();
    
    path.Add(new int[] {0,0});
    
    int x = path[path.Count - 1][0];
    int y = path[path.Count - 1][1];
    
     
    while (!(x >= position[0] - 7 && x <= position[0] + 7 && y >= position[1] - 7 && y <= position[1] + 7))// An error is caused because of this line and the value 7
    {
        Console.WriteLine($"({x}, {y})");
        
        // Using line segments(les droits) to figure out the next point to jump to
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
    
    return path;
}
 