using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SkinUtil
{

    private const string PATH = "Countries";
    public const string PLAYER_PREFS = "PLAYER_SKIN";
    public const string BOT_PREFS = "BOT_SKIN";

    public static void SetSkinPlayer(int id)
    {
        PlayerPrefs.SetInt(PLAYER_PREFS, id);
    }
    
    public static void SetSkinBot(int id)
    {
        PlayerPrefs.SetInt(BOT_PREFS, id);
    }
    
    public static Sprite GetSkin(int id)
    {
        return Resources.Load<Sprite>($"{PATH}/{id}");
    }
    
    public static Sprite GetSkinPlayer()
    {
        var id = PlayerPrefs.GetInt(PLAYER_PREFS, 1);
        return Resources.Load<Sprite>($"{PATH}/{id}");
    }
    
    public static Sprite GetSkinBot()
    {
        var id = PlayerPrefs.GetInt(BOT_PREFS, 1);
        return Resources.Load<Sprite>($"{PATH}/{id}");
    }

    public static List<Sprite> GetSkins()
    {
        var list = Resources.LoadAll<Sprite>(PATH).ToList();
        return list.OrderBy(sprite=>sprite.name).ToList();
    }
    
}
