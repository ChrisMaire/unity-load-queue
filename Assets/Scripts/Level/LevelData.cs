using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public string DisplayName = "Level Name";
    public SceneField SceneFile;

    public static void LoadLevel(LevelData newLevel)
    {
        GameObject loaderObj = new GameObject("Level Loader");
        LevelLoader loader = loaderObj.AddComponent<LevelLoader>();
        loader.Load(newLevel);
    }
}
