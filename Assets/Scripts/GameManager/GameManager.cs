using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sirenix.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private bool playerTurn;//false for player 1 true for ai
    [SerializeField] private TileGenerater tileGenerater;
    private int playerTurn = 2;//2 player1; 3 player2
    [SerializeField] private PlaceTile placeTile;
    [SerializeField] private UIController uiController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerTurn = 2;
        uiController.InitializeText(2);
    }

    public void ChangeTurn()
    {
        uiController.ChangeTurns();
        if (playerTurn == 2)
        {
            playerTurn = 3;
        }
        else
        {
            playerTurn = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTile(int coords, string axis)
    {
        Debug.Log("Calling tileChecker");
        placeTile.TileChecker(coords, axis, playerTurn);
    }

    public void EndGame(int winner)
    {
        uiController.EndGame(winner);
    }
}
