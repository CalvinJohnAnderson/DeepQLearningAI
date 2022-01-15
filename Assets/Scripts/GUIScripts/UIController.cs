using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;
    private bool player1 = false;

    public void InitializeText(int playerTurn)
    {
        if (playerTurn == 2)
        {
            infoText.text = "Player 1";
            player1 = true;
        }
        else
        {
            infoText.text = "AI";
            player1 = false;
        }
    }

    public void ChangeTurns()
    {
        if (player1)
        {
            infoText.text = "AI";
            player1 = false;
        }
        else
        {
            infoText.text = "Player 1";
            player1 = true;
        }
    }

    public void EndGame(int winner)
    {
        infoText.text = "GameOver: Winner = Player " + winner;
    }
}
