using UnityEditor;
using UnityEngine;

public class ClearPlayerPrefs
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs data cleared!");
    }
}