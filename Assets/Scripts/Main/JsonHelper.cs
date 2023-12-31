using System;
using UnityEngine;

public class JsonHelper
{
    public static T[] GetJsonArray<T>(string json)
    {
        string newJson = "{\"Array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Array;
    }
}
