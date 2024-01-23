using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoosttrapperH53rd
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute() => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("BilUnityJS")));
}
