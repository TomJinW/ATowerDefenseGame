using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Originally made by Mason Hayes and adjusted by Patrick Mitchell/Kesavan Shanmugasundaram for 603 Game 3
/// </summary>
public class GridManager : MonoBehaviour
{
    [SerializeField] private GridTile tilePrefab;
    [Tooltip("The dimensions of the grid; y = z, since the grid is flat along the XZ plane.")]
    [SerializeField] [Min(1)] private Vector2Int gridSize;
    [Tooltip("The distance between each tile's center in the grid.")]
    [SerializeField] private Vector2 tileSpacing;
    [Tooltip("The distance between each tile's edge in the grid. If 0, a sphere centered on a tile would touch " +
        "its orthogonal neighbors.")]
    [SerializeField] [Min(0)] private Vector2 tilePadding;

    public Vector2Int GridSize { get => gridSize; }
    public Vector2 TileSpacing { get => tileSpacing; }

    private GridTile[,] tiles;

    void Start()
    {
        if (!tilePrefab)
        {
            Debug.LogError($"{name}'s GridManager needs a tile prefab to instantiate!");
            return;
        }

        tiles = new GridTile[gridSize.x, gridSize.y];

        //Instantiate the tiles
        //Z position (not y; the grid is flat along the XZ plane)
        for (int z = 0; z < gridSize.y; z++)
        {
            //X position
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector3 pos = new Vector3(tileSpacing.x * x, 0, tileSpacing.y * z);
                GridTile tile = Instantiate(tilePrefab, pos + transform.position, Quaternion.identity, transform);
                tile.name = $"Tile [{x}, {z}]";
                tile.InitIndex(x, z);
                Vector2 paddedSpacing = tileSpacing - tilePadding;
                tile.transform.localScale = Vector3.Scale(tile.transform.localScale, new Vector3(paddedSpacing.x, 1, paddedSpacing.y));
                tiles[x, z] = tile;
            }
        }

        //Z position
        for (int z = 0; z < gridSize.y; z++)
        {
            //X position
            for (int x = 0; x < gridSize.x; x++)
            {
                tiles = tiles[x, z].ConnectTiles(tiles, new Vector2Int(x, z));
            }
        }
    }

    public GridTile GetTile(int xIndex, int yIndex) => tiles[xIndex, yIndex];
    public GridTile GetTile(Vector2Int indices) => tiles[indices.x, indices.y];

    public Vector3 GetGridLinePos(Vector2Int tileIndex, GridDirection dir)
    {
        Vector2 halfSpaceInDir = UtilFuncs.GridDirToV2Int(dir) * (tileSpacing / 2);
        return tiles[tileIndex.x, tileIndex.y].transform.position + new Vector3(halfSpaceInDir.x, 0, halfSpaceInDir.y);
    }
}