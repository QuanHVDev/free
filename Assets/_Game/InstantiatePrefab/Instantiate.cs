using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField] private List<GameObject> objs;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var obj in objs)
        {
            Instantiate(obj, transform);
        }
    }
}
