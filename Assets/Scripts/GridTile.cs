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
    [SerializeField] private Collider tileCollider;
    [SerializeField] private Renderer tileRenderer;
    [SerializeField] private Material unselectedMat;
    [SerializeField] private Material selectedMat;

    public Vector2Int IndexInGrid { get; private set; }

    private Dictionary<GridDirection, GridTile> tileAdj = new Dictionary<GridDirection, GridTile>();
    private GameObject occupant;

    //--- Methods ---//
    private void OnEnable()
    {
        ConstructionManager.cursorStateChange += OnPlacingStateChange;
        ConstructionManager.tileHover += OnTileHover;
        ConstructionManager.buildOnTile += OnBuildOnTile;
        ConstructionManager.destroyTileBuilding += OnDestroyTileBuilding;
    }
    private void OnDisable()
    {
        ConstructionManager.cursorStateChange -= OnPlacingStateChange;
        ConstructionManager.tileHover -= OnTileHover;
        ConstructionManager.buildOnTile -= OnBuildOnTile;
        ConstructionManager.destroyTileBuilding -= OnDestroyTileBuilding;
    }

    private void OnPlacingStateChange(CursorState newState)
    {
        switch (newState)
        {
            case CursorState.Building:
                //Enable this tile's rend/coll if not occupied.
                ToggleTilePresence(!occupant);
                break;
            case CursorState.Destroying:
                //Enable this tile's rend/coll if this tile has an occupant.
                ToggleTilePresence(occupant);
                break;
            case CursorState.Neutral:
            default:
                tileRenderer.enabled = false;
                break;
        }
    }

    private void ToggleTilePresence(bool enabled)
    {
        //tileCollider.enabled = enabled;
        tileCollider.enabled = enabled;

        tileRenderer.enabled = enabled;
    }

    private void OnTileHover(GameObject tileHovered, bool hovered)
    {
        if (tileHovered == gameObject)
        {
            tileRenderer.material = hovered ? selectedMat : unselectedMat;
        }
    }

    private void OnBuildOnTile(GameObject tile, GameObject buildPrefab, Vector3 buildOffset)
    {
        //Only build if this tile is the one being built on *AND* is not already occupied
        if (tile == gameObject && !occupant)
        {
            occupant = Instantiate(buildPrefab, transform);
            occupant.transform.NegateParentScale();
            occupant.transform.position += buildOffset;
        }
    }

    private void OnDestroyTileBuilding(GameObject tile)
    {
        if (tile == gameObject && occupant)
        {
            Destroy(occupant);
            occupant = null;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.collider, this.GetComponent<Collider>());
        }
    }

        public GridTile GetAdjTile(GridDirection direction)
    {
        return tileAdj[direction];
    }
}