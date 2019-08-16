using UnityEngine;

namespace Utilities
{
    public class Utilities
    {
        public static bool isTwoColorsEqual(Color32 cora, Color32 corb)
        {
            return (cora.r == corb.r && cora.g == corb.g && cora.b == corb.b);
        }

        public static bool isColorBright(Color32 cor)
        {
            if (cor.r > 255 / 2 && cor.g > 255 / 2 && cor.b < 255 / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}