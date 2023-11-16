using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserDataController : SingletonBehaviourDontDestroy<UserDataController>
{
    Dictionary<string, object> datas = new();

    public T GetData<T>(string key,out bool isNew) where T: IUserData, new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            T t = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
            isNew = false;
            return t;
        }
        else
        {
            isNew = true;
            return new T();
        }
    }

    public void SetData<T>(string key, T value, bool forceSaved = false) where T: IUserData
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(value));

        if(forceSaved || true) // temprorary here
        {
            
        }
    }
}
public class UserDataKeys
{
    public const string USER_PROGRESSION = "data.user_prog";
}

public interface IUserData
{
    
}

public class ProcessData : IUserData
{
    public bool IsShowSkip = true;
    public bool IsShowSwipe = true;
    public bool IsShowHint = true;
    public int currentLevel = 0;
}

