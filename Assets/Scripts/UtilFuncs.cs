using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilFuncs
{
    /// <summary>
    /// Takes a <see cref="GridDirection"/> and returns a <see cref="Vector2Int"/> representing it.<br/>
    /// NOTE: diagonal <see cref="GridDirection"/>s will <i>not</i> have normalized representations.
    /// </summary>
    /// <param name="dir">The direction to "convert" to a <see cref="Vector2Int"/>.</param>
    /// <returns>A <see cref="Vector2Int"/> representation of <paramref name="dir"/> (made of <see cref="Vector2Int"/>'s 
    /// directional properties).</returns>
    public static Vector2Int GridDirToV2Int(GridDirection dir)
    {
        switch (dir)
        {
            case GridDirection.LeftForward:
                return Vector2Int.left + Vector2Int.up;
            case GridDirection.Forward:
                return Vector2Int.up;
            case GridDirection.RightForward:
                return Vector2Int.right + Vector2Int.up;
            case GridDirection.Right:
                return Vector2Int.right;
            case GridDirection.RightBack:
                return Vector2Int.right + Vector2Int.down;
            case GridDirection.Back:
                return Vector2Int.down;
            case GridDirection.LeftBack:
                return Vector2Int.left + Vector2Int.down;
            case GridDirection.Left:
                return Vector2Int.left;
            default:
                return Vector2Int.zero;
        }
    }

    /// <summary>
    /// Takes in any number of keys and returns the first one of them that's down, in the order they were passed.<br/>
    /// This allows you to use a switch statement with <paramref name="keyDown"/> instead of a ton of else-ifs.<br/><br/>
    /// <i>Originally made for 603 Game 3 by Patrick Mitchell.</i>
    /// </summary>
    /// <param name="keyDown">The first key in <paramref name="codes"/> that was found to be down, in the order they were passed.<br/>
    /// Equals <see cref="KeyCode.None"/> if none of them were found to be down.</param>
    /// <param name="codes">The keys you want to check. Will return the first one down, in the order you pass them.</param>
    /// <returns>Whether any of the keys in <paramref name="codes"/> was down or not.</returns>
    private static bool GetMultiKeyDown(out KeyCode keyDown, params KeyCode[] codes)
    {
        for (int i = 0; i < codes.Length; i++)
        {
            if (Input.GetKeyDown(codes[i]))
            {
                keyDown = codes[i];
                return true;
            }
        }

        keyDown = KeyCode.None;
        return false;
    }
}