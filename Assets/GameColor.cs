using System.Collections.Generic;
using UnityEngine;

public enum GameColor
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Purple,
    Brown
}

public static class GameColorUtils
{
    public static Color GetRealColor(GameColor? gameColor)
    {
        return gameColor switch
        {
            GameColor.Red => Color.red,
            GameColor.Orange => new Color(1, 165f / 255, 0, 1),
            GameColor.Yellow => Color.yellow,
            GameColor.Green => Color.green,
            GameColor.Blue => Color.blue,
            GameColor.Purple => Color.magenta,
            GameColor.Brown => new Color(139f / 255, 69f / 255, 19f / 255, 1),
            _ => Color.white
        };
    }

    public static GameColor? MixColors(List<GameColor> gameColors)
    {
        int redPoints = 0, yellowPoints = 0, bluePoints = 0;

        // each color contributes 6 total points, equally distributed to component colors
        foreach (GameColor gameColor in gameColors) {
            switch (gameColor) {
                case GameColor.Red:
                    redPoints += 6;
                    break;
                
                case GameColor.Orange:
                    redPoints += 3;
                    yellowPoints += 3;
                    break;

                case GameColor.Yellow:
                    yellowPoints += 6;
                    break;
                
                case GameColor.Green:
                    yellowPoints += 3;
                    bluePoints += 3;
                    break;

                case GameColor.Blue:
                    bluePoints += 6;
                    break;
                
                case GameColor.Purple:
                    bluePoints += 3;
                    redPoints += 3;
                    break;

                case GameColor.Brown:
                    redPoints += 2;
                    yellowPoints += 2;
                    bluePoints += 2;
                    break;
            }
        }

        if (redPoints > yellowPoints && redPoints > bluePoints) return GameColor.Red;
        else if (yellowPoints > redPoints && yellowPoints > bluePoints) return GameColor.Yellow;
        else if (bluePoints > redPoints && bluePoints > yellowPoints) return GameColor.Blue;

        else if (redPoints == yellowPoints && redPoints == bluePoints) return GameColor.Brown;
        else if (redPoints == yellowPoints) return GameColor.Orange;
        else if (yellowPoints == bluePoints) return GameColor.Green;
        else if (redPoints == bluePoints) return GameColor.Purple;

        return null;
    }

    public static Color DarkenRealColor(Color color)
    {
        return new Color(
            Mathf.Max(color.r - 0.5f, 0),
            Mathf.Max(color.g - 0.5f, 0),
            Mathf.Max(color.b - 0.5f, 0),
            color.a
        );
    }
}