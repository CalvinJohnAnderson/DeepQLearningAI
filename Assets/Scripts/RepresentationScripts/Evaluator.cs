using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Evaluator
{
    //private Dictionary<Vector2Int, int> grid;
    //private int[,] gridArray = new int[10, 10];

    public static int EvaluateGrid(Dictionary<Vector2Int, int> currentGrid)//0 for no winner, 1 for player 1, 2 for player 2
    {
        //grid = currentGrid;
        //gridArray = GridConverter();
        int outcome = 0;

        for (int i = 0; i < 10; i++)//vertical win check
        {
            for (int j = 0; j <= 6 ; j++)
            {
                //Debug.Log("Checking on line x: " + i + " : y: " + j);
                if (currentGrid[new Vector2Int(i, j)] == currentGrid[new Vector2Int(i,j+1)] && 
                    currentGrid[new Vector2Int(i,j+1)] == currentGrid[new Vector2Int(i,j+2)] && 
                    currentGrid[new Vector2Int(i,j+2)] == currentGrid[new Vector2Int(i,j+3)] &&
                    currentGrid[new Vector2Int(i,j+3)] > 1)
                {
                    //Debug.LogWarning("Game is over");
                    outcome = currentGrid[new Vector2Int(i, j)] - 1;
                }
            }
        }

        for (int j = 0; j < 10; j++)//horizontal win check
        {
            for (int i = 0; i <=6; i++)
            {
                if (currentGrid[new Vector2Int(i, j)] == currentGrid[new Vector2Int(i+1,j)] && 
                    currentGrid[new Vector2Int(i+1,j)] == currentGrid[new Vector2Int(i+2,j)] && 
                    currentGrid[new Vector2Int(i+2,j)] == currentGrid[new Vector2Int(i+3,j)] &&
                    currentGrid[new Vector2Int(i+3,j)] > 1)
                {
                    outcome = currentGrid[new Vector2Int(i, j)] - 1;
                }
            }
        }
        
        for (int y = 0; y <= 6; y++)//diagonal win check (bottom left to top right)
        {
            for (int x = 0; x <= 6; x++)
            {
                if (currentGrid[new Vector2Int(x, y)] == currentGrid[new Vector2Int(x+1,y+1)] && 
                    currentGrid[new Vector2Int(x+1,y+1)] == currentGrid[new Vector2Int(x+2,y+2)] && 
                    currentGrid[new Vector2Int(x+2,y+2)] == currentGrid[new Vector2Int(x+3,y+3)] &&
                    currentGrid[new Vector2Int(x+3,y+3)] > 1)
                {
                    outcome = currentGrid[new Vector2Int(x, y)] - 1;
                }
            }
        }
        
        
        for (int y = 0; y <= 6; y++)//diagonal win check (bottom right to top left)
        {
            for (int x = 9; x >= 3; x--)
            {
                //Debug.Log("Checking on line x: " + x + " : y: " + y);
                if (currentGrid[new Vector2Int(x, y)] == currentGrid[new Vector2Int(x-1,y+1)] && 
                    currentGrid[new Vector2Int(x-1,y+1)] == currentGrid[new Vector2Int(x-2,y+2)] && 
                    currentGrid[new Vector2Int(x-2,y+2)] == currentGrid[new Vector2Int(x-3,y+3)] &&
                    currentGrid[new Vector2Int(x-3,y+3)] > 1)
                {
                    outcome = currentGrid[new Vector2Int(x, y)] - 1;
                }
            }
        }

        return outcome;
    }

    public static STATE RewardState(int x, int y, Dictionary<Vector2Int, int> currentGrid)//gets the state of the current placed tile
    {
        Debug.Log($"Checking at coords x:{x} ; y:{y}");
        //horizontal check
        if (x-1 >= 0 && currentGrid[new Vector2Int(x - 1, y)] == currentGrid[new Vector2Int(x, y)])
        {
            if (x-2 >=0 && currentGrid[new Vector2Int(x - 2, y)] == currentGrid[new Vector2Int(x, y)] || x+1 <=9 && currentGrid[new Vector2Int(x+1, y)] == currentGrid[new Vector2Int(x,y)])
            {
                return STATE.TRIPLE;
            }
            return STATE.DOUBLE;
        }
        if (x+1 <= 9 && currentGrid[new Vector2Int(x + 1, y)] == currentGrid[new Vector2Int(x, y)])
        {
            if (x+2 <=9 && currentGrid[new Vector2Int(x + 2, y)] == currentGrid[new Vector2Int(x, y)])
            {
                return STATE.TRIPLE;
            }

            return STATE.DOUBLE;
        }
        
        //Vertical check
        if (y-1 >=0 && currentGrid[new Vector2Int(x, y-1)] == currentGrid[new Vector2Int(x, y)])
        {
            if (y-2 >= 0 && currentGrid[new Vector2Int(x, y -2)] == currentGrid[new Vector2Int(x, y)] || y+1 <=9 && currentGrid[new Vector2Int(x, y+1)] == currentGrid[new Vector2Int(x,y)])
            {
                return STATE.TRIPLE;
            }
            return STATE.DOUBLE;
        }
        if (y+1 <=9 && currentGrid[new Vector2Int(x , y+1)] == currentGrid[new Vector2Int(x, y)])
        {
            if (y+2 <=9 && currentGrid[new Vector2Int(x, y+2)] == currentGrid[new Vector2Int(x, y)])
            {
                return STATE.TRIPLE;
            }

            return STATE.DOUBLE;
        }
        
        //diagonal check (bottom left to top right)

        if (x-1 >=0 && y-1 >=0 && currentGrid[new Vector2Int(x - 1, y - 1)] == currentGrid[new Vector2Int(x, y)])
        {
            if (x-2 >=0 && y-2 >=0 && currentGrid[new Vector2Int(x - 2, y - 2)] == currentGrid[new Vector2Int(x, y)] || x+1 <=9 && y+1 <=9 && currentGrid[new Vector2Int(x+1, y+1)] == currentGrid[new Vector2Int(x,y)])
            {
                return STATE.TRIPLE;
            }

            return STATE.DOUBLE;
        }

        if (x+1 <=9 && y+1<=9 && currentGrid[new Vector2Int(x + 1, y + 1)] == currentGrid[new Vector2Int(x, y)])
        {
            if (x+2 <=9 && y+2 <=9 && currentGrid[new Vector2Int(x + 2, y + 2)] == currentGrid[new Vector2Int(x, y)])
            {
                return STATE.TRIPLE;
            }

            return STATE.DOUBLE;
        }
        
        //diagonal check (top left to bottom right)

        if (x-1 >=0 && y+1 <=9 && currentGrid[new Vector2Int(x - 1, y + 1)] == currentGrid[new Vector2Int(x, y)])
        {
            if (x-2 >=0 && y+2<=9 && currentGrid[new Vector2Int(x - 2, y + 2)] == currentGrid[new Vector2Int(x, y)] ||
                x+1<=9 && y-1>=0 && currentGrid[new Vector2Int(x + 1, y - 1)] == currentGrid[new Vector2Int(x, y)])
            {
                return STATE.TRIPLE;
            }

            return STATE.DOUBLE;
        }

        if (x+1 <=9 && y-1>=0 && currentGrid[new Vector2Int(x + 1, y - 1)] == currentGrid[new Vector2Int(x, y)])
        {
            if (x+2<=9 && y-2>=0 && currentGrid[new Vector2Int(x + 2, y - 2)] == currentGrid[new Vector2Int(x, y)])
            {
                return STATE.TRIPLE;
            }

            return STATE.DOUBLE;
        }
        
        //default return
        return STATE.SINGLE;
    }

    public static List<STATE> GetFutureRewards(int x, int y, Dictionary<Vector2Int, int> currentGrid)
    {
        List<STATE> surroundingRewards = new List<STATE>();//Left, up, right, down // change to left, topLeft, up, topRight, right, bottomRight, down, bottomLeft
        //horizontal check (left tile)
        if (x-1>=0 && currentGrid[new Vector2Int(x - 1, y)] == 0)//checks if tile is available for future placements
        {
            if (x+1<=9 && currentGrid[new Vector2Int(x + 1, y)] == currentGrid[new Vector2Int(x, y)])
            {
                if (x+2<=9 && currentGrid[new Vector2Int(x + 2, y)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }
        
        //Diagonal check (topLeft)
        if (x-1>=0 && y+1<=9 && currentGrid[new Vector2Int(x - 1, y + 1)] == 0)
        {
            if (x+1<=9 && y-1>=0 && currentGrid[new Vector2Int(x+1, y-1)] == currentGrid[new Vector2Int(x, y)])
            {
                if (x+2<=9 && y-2>=0 && currentGrid[new Vector2Int(x+2, y-2)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }
            
            
        
        // Vertical check (up tile)
        if (y+1<=9 && currentGrid[new Vector2Int(x, y + 1)] == 0)//checks if tile is available for future placements
        {
            if (y-1>=0 && currentGrid[new Vector2Int(x, y-1)] == currentGrid[new Vector2Int(x, y)])
            {
                if (y-2>=0 && currentGrid[new Vector2Int(x, y-2)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }
        
        //diagonal check (topRight)
        if (x+1<=9 && y+1<=9 && currentGrid[new Vector2Int(x + 1, y + 1)] == 0)
        {
            if (x-1>=0 && y-1>=0 && currentGrid[new Vector2Int(x-1, y-1)] == currentGrid[new Vector2Int(x, y)])
            {
                if (x-2>=0 && y-2>=0 && currentGrid[new Vector2Int(x-2, y-2)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }
        
        //horizontal check (right tile)
        if (x+1<=9 && currentGrid[new Vector2Int(x + 1, y)] == 0) //checks if tile is available for future placements
        {
            if (x-1>=0 && currentGrid[new Vector2Int(x-1, y)] == currentGrid[new Vector2Int(x, y)])
            {
                if (x-2>=0 && currentGrid[new Vector2Int(x-2, y)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }
        
        //diagonal check (bottom right)
        if (x+1<=9 && y-1>=0 && currentGrid[new Vector2Int(x + 1, y - 1)] == 0)
        {
            if (x-1>=0 && y+1<=9 && currentGrid[new Vector2Int(x-1, y+1)] == currentGrid[new Vector2Int(x, y)])
            {
                if (x-2>=0 && y+2<=9 && currentGrid[new Vector2Int(x-2, y+2)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }
        
        //vertical check (down tile)
        if (y-1>=0 && currentGrid[new Vector2Int(x, y -1)] == 0)//checks if tile is available for future placements
        {
            if (y+1<=9 && currentGrid[new Vector2Int(x, y+1)] == currentGrid[new Vector2Int(x, y)])
            {
                if (y+2<=9 && currentGrid[new Vector2Int(x, y+2)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }
        
        //diagonal check (bottomleft)
        if (x-1>=0 && y-1>=0 && currentGrid[new Vector2Int(x - 1, y - 1)] == 0)
        {
            if (x+1<=9 && y+1<=9 && currentGrid[new Vector2Int(x+1, y+1)] == currentGrid[new Vector2Int(x, y)])
            {
                if (x+2<=9 && y+2<=9 && currentGrid[new Vector2Int(x+2, y+2)] == currentGrid[new Vector2Int(x, y)])
                {
                    surroundingRewards.Add(STATE.WIN);
                }
                else
                {
                    surroundingRewards.Add(STATE.TRIPLE);
                }
            }
            else
            {
                surroundingRewards.Add(STATE.DOUBLE);
            }
        }
        else
        {
            surroundingRewards.Add(STATE.ZERO);
        }

        foreach (var state in surroundingRewards)
        {
            Debug.Log($"Added state: {state}");
        }
        
        return surroundingRewards;
    }

}
