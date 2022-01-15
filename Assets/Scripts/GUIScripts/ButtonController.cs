using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    public void PlaceTile9X()
    {
        SendPlacementInfo(9, "x");
    }
    public void PlaceTile8X()
    {
        SendPlacementInfo(8, "x");
    }
    public void PlaceTile7X()
    {
        SendPlacementInfo(7, "x");
    }
    public void PlaceTile6X()
    {
        SendPlacementInfo(6, "x");
    }
    public void PlaceTile5X()
    {
        SendPlacementInfo(5, "x");
    }
    public void PlaceTile4X()
    {
        SendPlacementInfo(4, "x");
    }
    public void PlaceTile3X()
    {
        SendPlacementInfo(3, "x");
    }
    public void PlaceTile2X()
    {
        SendPlacementInfo(2, "x");
    }
    public void PlaceTile1X()
    {
        SendPlacementInfo(1, "x");
    }
    public void PlaceTile0X()
    {
        SendPlacementInfo(0, "x");
    }
    /// <summary>
    /// grid button methods
    /// </summary>
    public void PlaceTile9Y()
    {
        SendPlacementInfo(9, "y");
    }
    public void PlaceTile8Y()
    {
        SendPlacementInfo(8, "y");
    }
    public void PlaceTile7Y()
    {
        SendPlacementInfo(7, "y");
    }
    public void PlaceTile6Y()
    {
        SendPlacementInfo(6, "y");
    }
    public void PlaceTile5Y()
    {
        SendPlacementInfo(5, "y");
    }
    public void PlaceTile4Y()
    {
        SendPlacementInfo(4, "y");
    }
    public void PlaceTile3Y()
    {
        SendPlacementInfo(3, "y");
    }
    public void PlaceTile2Y()
    {
        SendPlacementInfo(2, "y");
    }
    public void PlaceTile1Y()
    {
        SendPlacementInfo(1, "y");
    }
    public void PlaceTile0Y()
    {
        SendPlacementInfo(0, "y");
    }
    //////////////

    private void SendPlacementInfo(int coords, string axis)
    {
        Debug.Log("Starting player turn");
        gameManager.PlaceTile(coords, axis);
    }
}
