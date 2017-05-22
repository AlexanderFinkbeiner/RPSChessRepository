using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chessman : MonoBehaviour {

    public int CurrentX { set; get; }
    public int CurrentY { set; get; }
    public bool isFirstPlayer;
    
    public void setPosition( int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
    public virtual bool[,] possibleMove()
    {
        return new bool[7,6];
    }
}
