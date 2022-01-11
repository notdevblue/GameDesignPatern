using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global
{
    static public Dictionary<ePrefabs, GameObject> prefabsDictionary;
    static public Dictionary<string, Sprite> spriteDictionary;
    static public Vector2 referenceResolution;
    static public Image blackPannel;
}

public enum ePrefabs
{
    None = -1,
    MainCamera,
    HEROS = 1000,
    HeroMan,
    HeroGirl,
    MANAGRS = 2000,
    MGPool,
    MGGame,
    UI = 3000,
    UIRoot,
    UIRootLoading,
    UIRootTitle,
    UIRootGame
}

public enum eSceneName
{
    None = -1,
    Loading,
    Title,
    Game
}

public class GAmeSceneClass
{
    public static UIRoot globalUIRoot;
}