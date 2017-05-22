using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public static FieldController Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    public Chessman[,] Chessmans { set; get; }
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    private bool flagIsSet1 = false;
    private bool trapeIsSet1 = false;
    private bool flagIsSet2 = false;
    private bool trapeIsSet2 = false;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Quaternion orientation = Quaternion.Euler(-90, 0, 0);

    public bool isFirstPlayerTurn = true;
    // Use this for initialization
    void Start () {
        SpawnAllChessmen();
        Instance = this;

    }
	


	// Update is called once per frame
	void Update () {
        UpdateSelection();
        DrawChessField();

        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0)
            {
                if(selectedChessman == null)
                {
                    if (isFirstPlayerTurn)
                    {
                        if (!trapeIsSet1 && flagIsSet1)
                        {
                            spawnTrapeFigure();
                        }
                        if (!flagIsSet1)
                        {
                            spawnFlagFigure();
                        }
                    }
                    else
                    {
                        if (!trapeIsSet2 && flagIsSet2)
                        {
                            spawnTrapeFigure();
                        }
                        if (!flagIsSet2)
                        {
                            spawnFlagFigure();

                        }
                    }
                    
                    selectChessman(selectionX, selectionY);
                }
                else
                {
                    moveChessman(selectionX, selectionY);
                }
            }
        }
    }

    private void selectChessman(int x, int y)
    {
        if (Chessmans[x, y] == null)
        {
            return;
        }
        if(Chessmans[x,y].isFirstPlayer != isFirstPlayerTurn)
        {
            return;
        }
        bool hasAtleastOneMove = false;
        allowedMoves = Chessmans[x, y].possibleMove();
        for (int x1 = 0; x1 < 7; x1++)
        {
            for (int y1 = 0; y1 < 6; y1++)
            {
                if (allowedMoves[x1,y1])
                {
                    hasAtleastOneMove = true;
                }
            }
        }
        if (!hasAtleastOneMove)
        {
            return;
        }
        selectedChessman = Chessmans[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);

    }

    private void spawnFlagFigure()
    {
        if (Chessmans[selectionX, selectionY] == null)
        {
            return;
        }
        if (Chessmans[selectionX, selectionY].isFirstPlayer != isFirstPlayerTurn)
        {
            return;
        }
        selectedChessman = Chessmans[selectionX, selectionY];

        activeChessman.Remove(selectedChessman.gameObject);
        Destroy(selectedChessman.gameObject);
        if (isFirstPlayerTurn)
        {
            SpawnChessman(6, selectionX , selectionY, Quaternion.Euler(0, 0, 0));
            flagIsSet1= true;
        }
        else
        {
            SpawnChessman(8, selectionX, selectionY, Quaternion.Euler(0, 0, 0));
            flagIsSet2 = true;
        }
        
        
    }

    private void spawnTrapeFigure()
    {
        if (Chessmans[selectionX, selectionY] == null)
        {
            return;
        }
        if (Chessmans[selectionX, selectionY].isFirstPlayer != isFirstPlayerTurn)
        {
            return;
        }
        selectedChessman = Chessmans[selectionX, selectionY];

        activeChessman.Remove(selectedChessman.gameObject);
        Destroy(selectedChessman.gameObject);

        if (isFirstPlayerTurn)
        {
            SpawnChessman(7, selectionX, selectionY, Quaternion.Euler(0, 0, 0));
            trapeIsSet1 = true;
        }
        else
        {
            SpawnChessman(9, selectionX, selectionY, Quaternion.Euler(0, 0, 0));
            trapeIsSet2 = true;
        }
        selectedChessman = null;

    }

    private void moveChessman(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            Chessman enemieChessman = Chessmans[x, y];

            bool win = true;
            if (enemieChessman != null && enemieChessman.isFirstPlayer != isFirstPlayerTurn)
            {
                Type typeOfEnemie = enemieChessman.GetType();
                Type typeOfSelecedFigure = selectedChessman.GetType();


                // if it is the Flagbearer end the game               
                if (typeOfEnemie == typeof(FlagFigure))
                {
                    endTheGame();
                    activeChessman.Remove(enemieChessman.gameObject);
                    Destroy(enemieChessman.gameObject);
                    return;
                }
                if (typeOfSelecedFigure == typeof(RockFigure) && typeOfEnemie == typeof(PaperFigure))
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    win = false;
                }
                if (typeOfSelecedFigure == typeof(RockFigure) && typeOfEnemie == typeof(ScissorsFigure))
                {
                    activeChessman.Remove(enemieChessman.gameObject);
                    Destroy(enemieChessman.gameObject);
                    win = true;
                }
                if (typeOfSelecedFigure == typeof(ScissorsFigure) && typeOfEnemie == typeof(RockFigure))
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    win = false;
                }
                if (typeOfSelecedFigure == typeof(ScissorsFigure) && typeOfEnemie == typeof(PaperFigure))
                {
                    activeChessman.Remove(enemieChessman.gameObject);
                    Destroy(enemieChessman.gameObject);
                    win = true;
                }
                if (typeOfSelecedFigure == typeof(PaperFigure) && typeOfEnemie == typeof(RockFigure))
                {
                    activeChessman.Remove(enemieChessman.gameObject);
                    Destroy(enemieChessman.gameObject);
                    win = true;
                }
                if (typeOfSelecedFigure == typeof(PaperFigure) && typeOfEnemie == typeof(ScissorsFigure))
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    win = false;
                }
                if (typeOfSelecedFigure == typeOfEnemie)
                {
                    // newChoice
                }
                if (typeOfEnemie == typeof(TrapeFigure))
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    win = false;
                }

            }

                if (win)
                {
                    // execute move
                    Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
                    selectedChessman.transform.position = GetTileCenter(x, y);
                    selectedChessman.setPosition(x, y);
                    Chessmans[x, y] = selectedChessman;
                }

            
                       
            isFirstPlayerTurn = !isFirstPlayerTurn;
            
        }
        BoardHighlights.Instance.hidehighlights();
        selectedChessman = null;
    }



    private void SpawnChessman(int index, int x, int y, Quaternion orientation)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent < Chessman >();
        Chessmans[x, y].setPosition(x, y);
        activeChessman.Add(go);
    }

    private void SpawnAllChessmen()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[7,6];

        // Spwan secound Team
        for (int x = 0; x <= 6; x++)
        {
            for (int y = 4; y <= 5; y++)
            {
                int type = UnityEngine.Random.Range(3, 6);
                SpawnChessman(type, x, y, Quaternion.Euler(-90, 180, 0));
            }
        }

        // Spwan first Team
        for (int x = 0; x <= 6; x++)
        {

            for (int y = 0; y <= 1; y++)
            {
                int type = UnityEngine.Random.Range(0, 3);
                SpawnChessman(type, x, y, Quaternion.Euler(-90, 0, 0));
            }
        }


    }


    private void UpdateSelection()
    {
        if (!Camera.main) {
            return;
        }           
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
}
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void DrawChessField()
    {
        Vector3 widthLine = Vector3.right * 7;
        Vector3 heightLine = Vector3.forward * 6;

        for (int i = 0; i <= 6; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
        }
        for (int i = 0; i <= 7; i++)
        {
            Vector3 start = Vector3.right * i;
            Debug.DrawLine(start, start + heightLine);
        }

        // Draw the Selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY  + Vector3.right * (selectionX + 1));
        }
    }
    private void endTheGame()
    {
        if (isFirstPlayerTurn)
        {
            Debug.Log("FirstPlayer wins");
        }
        else
        {
            Debug.Log("Secound Player wins");
        }
        foreach (GameObject go in activeChessman)
        {
            Destroy(go);
        }
        isFirstPlayerTurn = true;
        BoardHighlights.Instance.hidehighlights();
        SpawnAllChessmen();
    }

}
