using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsFigure : Chessman {

    public override bool[,] possibleMove()
    {
        bool[,] r = new bool[7, 6];
        Chessman chessman;
        if (isFirstPlayer)
        {
            //moveUp
            if (CurrentY != 5)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX, CurrentY + 1];
                if (chessman == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
                else
                {
                    if (!chessman.isFirstPlayer)
                    {
                        r[CurrentX, CurrentY + 1] = true;
                    }
                }

            }
            //moveDown
            if (CurrentY != 0)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (chessman == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
                else
                {
                    if (!chessman.isFirstPlayer)
                    {
                        r[CurrentX, CurrentY - 1] = true;
                    }
                }
            }
            //moveRight
            if (CurrentX != 6)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX + 1, CurrentY];
                if (chessman == null)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
                else
                {
                    if (!chessman.isFirstPlayer)
                    {
                        r[CurrentX + 1, CurrentY] = true;
                    }
                }
            }
            //moveLeft
            if (CurrentX != 0)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX - 1, CurrentY];
                if (chessman == null)
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
                else
                {
                    if (!chessman.isFirstPlayer)
                    {
                        r[CurrentX - 1, CurrentY] = true;
                    }
                }
            }
        }
        else
        {
            //moveUp
            if (CurrentY != 5)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX, CurrentY + 1];
                if (chessman == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
                else
                {
                    if (chessman.isFirstPlayer)
                    {
                        r[CurrentX, CurrentY + 1] = true;
                    }
                }
            }
            //moveDown
            if (CurrentY != 0)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (chessman == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
                else
                {
                    if (chessman.isFirstPlayer)
                    {
                        r[CurrentX, CurrentY - 1] = true;
                    }
                }
            }
            //moveRight
            if (CurrentX != 6)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX + 1, CurrentY];
                if (chessman == null)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
                else
                {
                    if (chessman.isFirstPlayer)
                    {
                        r[CurrentX + 1, CurrentY] = true;
                    }
                }
            }
            //moveLeft
            if (CurrentX != 0)
            {
                chessman = FieldController.Instance.Chessmans[CurrentX - 1, CurrentY];
                if (chessman == null)
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
                else
                {
                    if (chessman.isFirstPlayer)
                    {
                        r[CurrentX - 1, CurrentY] = true;
                    }
                }
            }
        }


        return r;

    }
}
