using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AdoptUI : MonoBehaviour
{
    [SerializeField] private List<Transform> townCatReqs;
    [SerializeField] private List<Transform> townCats;
    // 270 370

    public void Init(List<TagCat> tags)
    {
        if (tags.Count >= townCatReqs.Count)
        {
            Debug.LogError("MAX COUNT TAGs => return;");
            return;
        }
        for (int i = 0; i < tags.Count; i++)
        {
            
        }
    }
    
}
