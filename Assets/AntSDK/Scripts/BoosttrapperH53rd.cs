using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoosttrapperH53rd
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Object o = Object.Instantiate(Resources.Load("AntGamesSDK"));
        o.name = o.name.Replace("(Clone)", "").Trim();
        Object.DontDestroyOnLoad(o);

    }
}
