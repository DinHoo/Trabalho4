using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager
{
    public delegate void GameEvent();

    public static event GameEvent canalColorChange;

    public static void triggerCanalColorChange()
    {
        if (canalColorChange != null)
        {
            canalColorChange();
        }
    }
}
