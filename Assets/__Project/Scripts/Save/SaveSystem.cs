using UnityEngine;

public class SaveSystem
{
    public void Save(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
        if (!PlayerPrefs.HasKey(key))
            Debug.Log($"Key {key} isn't creating");
        Debug.Log($"Key {key} not delete");
    }
    public string Load(string key)
    {
        if (!PlayerPrefs.HasKey(key)) return null;
        return PlayerPrefs.GetString(key);
    }
    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        if (!PlayerPrefs.HasKey(key))
            Debug.Log($"Key {key} delete");
        Debug.Log($"Key {key} not delete");
    }
}
