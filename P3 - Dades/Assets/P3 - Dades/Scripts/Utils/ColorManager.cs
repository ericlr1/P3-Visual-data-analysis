using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // Static dictionary for global access
    private static Dictionary<string, Color> Colors = new Dictionary<string, Color>
    {
        { "Light Green", new Color(0.8f, 1.0f, 0.8f, 1.0f) },
        { "Dark Green", new Color(0.0f, 0.5f, 0.0f, 1.0f) },
        { "Pastel Pink", new Color(1.0f, 0.8f, 0.8f, 1.0f) },
        { "Sky Blue", new Color(0.5f, 0.8f, 1.0f, 1.0f) },
        { "Golden Yellow", new Color(1.0f, 0.85f, 0.2f, 1.0f) },
        { "Bright Red", new Color(1.0f, 0.0f, 0.0f, 1.0f) },
        { "Soft Violet", new Color(0.8f, 0.6f, 1.0f, 1.0f) },
        { "Coral", new Color(1.0f, 0.5f, 0.31f, 1.0f) },
        { "Teal", new Color(0.0f, 0.5f, 0.5f, 1.0f) },
        { "Lavender", new Color(0.9f, 0.9f, 1.0f, 1.0f) },
        { "Mint Green", new Color(0.6f, 1.0f, 0.6f, 1.0f) },
        { "Rose Gold", new Color(0.72f, 0.43f, 0.47f, 1.0f) },
        { "Crimson", new Color(0.86f, 0.08f, 0.24f, 1.0f) },
        { "Sunset Orange", new Color(1.0f, 0.37f, 0.0f, 1.0f) },
        { "Royal Blue", new Color(0.25f, 0.41f, 0.88f, 1.0f) },
        { "Emerald", new Color(0.31f, 0.78f, 0.47f, 1.0f) },
        { "Deep Purple", new Color(0.4f, 0.0f, 0.4f, 1.0f) },
        { "Beige", new Color(0.96f, 0.96f, 0.86f, 1.0f) },
        { "Chocolate", new Color(0.82f, 0.41f, 0.12f, 1.0f) },
        { "Peach", new Color(1.0f, 0.8f, 0.64f, 1.0f) },
        { "Turquoise", new Color(0.25f, 0.88f, 0.82f, 1.0f) },
        { "Slate Gray", new Color(0.44f, 0.5f, 0.56f, 1.0f) },
        { "Olive", new Color(0.5f, 0.5f, 0.0f, 1.0f) },
        { "Ivory", new Color(1.0f, 1.0f, 0.94f, 1.0f) },
        { "Cyan", new Color(0.0f, 1.0f, 1.0f, 1.0f) },
        { "Magenta", new Color(1.0f, 0.0f, 1.0f, 1.0f) },
        { "Amber", new Color(1.0f, 0.75f, 0.0f, 1.0f) },
        { "Charcoal", new Color(0.21f, 0.27f, 0.31f, 1.0f) },
        { "Lime", new Color(0.75f, 1.0f, 0.0f, 1.0f) },
        { "Periwinkle", new Color(0.8f, 0.8f, 1.0f, 1.0f) },
        { "Maroon", new Color(0.5f, 0.0f, 0.0f, 1.0f) },
        { "Burnt Orange", new Color(0.8f, 0.33f, 0.0f, 1.0f) },
        { "Neon Green", new Color(0.2f, 1.0f, 0.2f, 1.0f) },
        { "Steel Blue", new Color(0.27f, 0.51f, 0.71f, 1.0f) },
        { "Sea Foam", new Color(0.62f, 0.89f, 0.77f, 1.0f) },
        { "Brick Red", new Color(0.8f, 0.25f, 0.33f, 1.0f) },
        { "Fuchsia", new Color(1.0f, 0.0f, 0.5f, 1.0f) },
        { "Gold", new Color(1.0f, 0.84f, 0.0f, 1.0f) },
        { "Forest Green", new Color(0.13f, 0.55f, 0.13f, 1.0f) },
        { "Ocean Blue", new Color(0.0f, 0.75f, 1.0f, 1.0f) }
    };

    // Method to get a color
    public static Color GetColor(string name)
    {
        if (Colors.TryGetValue(name, out Color color))
        {
            return color;
        }
        else
        {
            Debug.LogError($"Color with name '{name}' does not exist.");
            return Color.black; // Return a default color if not found
        }
    }
}
