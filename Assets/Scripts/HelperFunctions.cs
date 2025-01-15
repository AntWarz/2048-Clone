using UnityEngine;

public static class HelperFunctions 
{
    public static bool IndicesInBounds(int x, int y, int boundXUpper, int boundXLower, int boundYUpper, int boundYLower)
    {
        if (x < boundXUpper && y < boundYUpper && x >= boundXLower && y >= boundYLower) return true;

        else return false;

    }
}
