using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static bool isTwoColorsEqual(Color32 cora, Color32 corb)
    {
        return (cora.r == corb.r && cora.g == corb.g && cora.b == corb.b);
    }
}
