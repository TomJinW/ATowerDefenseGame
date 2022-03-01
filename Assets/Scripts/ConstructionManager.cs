using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    //TODO:
    //Raycast from cursor when in building/destroying mode
    //get GridTile component from hits and change its renderer

    public static Action<CursorState> cursorStateChange;
    public static Action<GameObject, bool> tileHover;
}