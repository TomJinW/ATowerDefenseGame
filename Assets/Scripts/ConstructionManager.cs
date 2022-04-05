using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private GameObject buildPrefab;

    [SerializeField] private SingleTargetTower [] buildPrefabs;

    public int buildPrefabIndex = 0;

    [SerializeField] private Vector3 buildOffset;
    [SerializeField] [Min(0)] private int tileLayerIndex = 5;

    private CursorState currentCursorState = CursorState.Building;
    private GameObject hoveredTile;

    public static Action<CursorState> cursorStateChange;
    /// <summary>
    /// Invoked when the mouse starts/stops hovering over a tile.<br/><br/>
    /// <b>Arguments:</b><br/>
    /// - <see cref="GameObject"/>: The tile in question.<br/>
    /// - <see cref="bool"/>: Whether the mouse has started or stopped hovering over the tile.
    /// </summary>
    public static Action<GameObject, bool> tileHover;
    /// <summary>
    /// <b>Arguments:</b><br/>
    /// - <see cref="GameObject"/>: The tile being built upon.<br/>
    /// - <see cref="GameObject"/>: The tower to build on the tile.<br/>
    /// - <see cref="Vector3"/>: The build offset; the position to add to the tower's default spawn position.
    /// </summary>
    public static Action<GameObject, GameObject, Vector3> buildOnTile;
    /// <summary>
    /// <b>Arguments:</b><br/>
    /// - <see cref="GameObject"/>: The tile we want to remove a building from.<br/>
    /// </summary>
    public static Action<GameObject> destroyTileBuilding;


    [SerializeField] private GameManager gameManager;

    private void Start()
    {
       
        if (!playerCam)
        {
            playerCam = Camera.main;
            if (!playerCam) { Debug.LogError("No camera assigned to ConstructionManager and no main camera found."); }
        }
    }

    private void OnEnable()
    {
        cursorStateChange += OnCursorStateChange;
    }
    private void OnDisable()
    {
        cursorStateChange -= OnCursorStateChange;
    }

    private void OnCursorStateChange(CursorState newState)
    {
        currentCursorState = newState;
    }

    private void Update()
    {
        cursorStateChange?.Invoke(CursorState.Building);
        switch (currentCursorState)
        {
            case CursorState.Building:
                CastAndCheckInput(() => buildOnTile?.Invoke(hoveredTile, buildPrefabs[buildPrefabIndex].gameObject, buildOffset));
                break;
            case CursorState.Destroying:
                CastAndCheckInput(() => destroyTileBuilding?.Invoke(hoveredTile));
                break;
            case CursorState.Neutral:
                SwitchStateOnInput();
                break;
            default:
                break;
        }
    }

    private bool CastFromMouseToTiles()
    {
        Ray mouseRay = playerCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit, 10000, 1 << tileLayerIndex))
        {
            if (hoveredTile != hit.collider.gameObject)
            {
                tileHover?.Invoke(hoveredTile, false);
            }
            hoveredTile = hit.collider.gameObject;
            tileHover?.Invoke(hoveredTile, true);

            return true;
        }
        else if (hoveredTile)
        {
            tileHover?.Invoke(hoveredTile, false);
            hoveredTile = null;
        }

        return false;
    }

    private void CastAndCheckInput(Action onInput)
    {
        if (CastFromMouseToTiles())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Time.timeScale != 0) {
                    SingleTargetTower tower = buildPrefabs[buildPrefabIndex];
                    if (tower.cost <= gameManager.money)
                    {
                        gameManager.money -= tower.cost;
                        onInput();
                        cursorStateChange?.Invoke(CursorState.Neutral);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            cursorStateChange?.Invoke(CursorState.Neutral);
        }
    }

    private void SwitchStateOnInput()
    {
        if (UtilFuncs.GetMultiKeyDown(out KeyCode key,
                    KeyCode.Alpha1, KeyCode.Keypad1, KeyCode.Alpha2, KeyCode.Keypad2))
        {
            switch (key)
            {
                case KeyCode.Alpha1:
                case KeyCode.Keypad1:
                    cursorStateChange?.Invoke(CursorState.Building);
                    break;
                case KeyCode.Alpha2:
                case KeyCode.Keypad2:
                    cursorStateChange?.Invoke(CursorState.Destroying);
                    break;
                default:
                    break;
            }
        }
    }
}