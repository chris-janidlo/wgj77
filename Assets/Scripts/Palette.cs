using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Palette
{
    public static Dictionary<PaletteIdentifier, Color> Colors = new Dictionary<PaletteIdentifier, Color>() {
        { PaletteIdentifier.On, new Color(17, 30, 3) },
        { PaletteIdentifier.Off, new Color(141, 153, 128) }
    };
}

public enum PaletteIdentifier
{
    On, Off
}
