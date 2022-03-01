using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Originally made by Mason Hayes and adjusted by Patrick Mitchell/Kesavan Shanmugasundaram for 603 Game 3
/// </summary>
public class GridTile : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Editor Only Fields")]
    [Tooltip("List of the Dict's values so they can be seen in the inspector; clockwise, starting from LeftForward.")]
    [SerializeField] private List<GridTile> adjacentTiles = new List<GridTile>();
#endif
    [Header("Normal Fields")]
    [SerializeField] private Renderer tileRenderer;
    [SerializeField] private Material unselectedMat;
    [SerializeField] private Material selectedMat;

    public Vector2Int IndexInGrid { get; private set; }

    private Dictionary<GridDirection, GridTile> tileAdj = new Dictionary<GridDirection, GridTile>();

    //--- Methods ---//
    private void OnEnable()
    {
        ConstructionManager.cursorStateChange += OnPlacingStateChange;
        ConstructionManager.tileHover += OnTileHover;
    }
    private void OnDisable()
    {
        ConstructionManager.cursorStateChange -= OnPlacingStateChange;
        ConstructionManager.tileHover -= OnTileHover;
    }

    private void OnPlacingStateChange(CursorState newState)
    {
        switch (newState)
        {
            case CursorState.Building:
                tileRenderer.enabled = true;
                break;
            case CursorState.Destroying:
                tileRenderer.enabled = false;
                break;
            default:
                break;
        }
    }

    private void OnTileHover(GameObject tileHovered, bool hovered)
    {
        if (tileHovered == gameObject)
        {
            tileRenderer.material = hovered ? selectedMat : unselectedMat;
        }
    }

    public void InitIndex(Vector2Int index) => IndexInGrid = index;
    public void InitIndex(int xInd, int yInd) => InitIndex(new Vector2Int(xInd, yInd));

    public GridTile[,] ConnectTiles(GridTile[,] tileMatrix, Vector2Int pos)
    {
        //Apparently casting the GetValues array to an... actual array is a little faster. Why not, I guess?
        //https://stackoverflow.com/questions/105372/how-to-enumerate-an-enum
        foreach (GridDirection dir in (GridDirection[])Enum.GetValues(typeof(GridDirection)))
        {
            //Init each direction in the adjacency array to null, then assign the proper adjacency if there is one
            tileAdj.Add(dir, null);
            TrySetConnection(tileMatrix, pos, dir);
        }

#if UNITY_EDITOR
        //Adds the dictionary values to the appropriate lists (they keys aren't much help)
        foreach (GridTile tile in tileAdj.Values)
            adjacentTiles.Add(tile);
#endif

        return tileMatrix;
    }

    public void TrySetConnection(GridTile[,] tileMatrix, Vector2Int pos, GridDirection connectDir)
    {
        //Use Vector2Int's directional vector properties to discern the indices of the tile in connectDir direction.
        pos += UtilFuncs.GridDirToV2Int(connectDir);

        if (pos.x >= 0 && pos.x < tileMatrix.GetLength(0))
        {
            if (pos.y >= 0 && pos.y < tileMatrix.GetLength(1))
            {
                tileAdj[connectDir] = tileMatrix[pos.x, pos.y];
            }
        }
    }

    public GridTile GetAdjTile(GridDirection direction)
    {
        return tileAdj[direction];
    }
}