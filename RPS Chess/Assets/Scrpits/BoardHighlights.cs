using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour {

public static BoardHighlights Instance { set; get; }

    public GameObject highlightPrefab;
    private List<GameObject> hightlights;

    private void Start()
    {
        Instance = this;
        hightlights = new List<GameObject>();

    }
    private GameObject getHightlightObject()
    {
        GameObject go = hightlights.Find(g => !g.activeSelf);
        if ( go == null)
        {
            go = Instantiate(highlightPrefab);
            hightlights.Add(go);
        
        }
        return go;
    }
    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (moves[x,y])
                {
                    GameObject go = getHightlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(x+0.5f, 0, y + 0.5f);
                }
            }
        }
        
    }
    public void hidehighlights()
    {
        foreach (GameObject go in hightlights)
        {
            go.SetActive(false);
        }
    }
}
