using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentBarrierController : MonoBehaviour
{
    Dictionary<string, List<Transform>> barrierDict = new Dictionary<string, List<Transform>>();

    // Start is called before the first frame update
    void Start()
    {
        Transform[] childBarrier = GetComponentsInChildren<Transform>();

        foreach (Transform child in childBarrier)
        {
            string tag = child.gameObject.tag;

            if (!barrierDict.ContainsKey(tag))
            {
                barrierDict[tag] = new List<Transform>();
            }

            barrierDict[tag].Add(child);
        }


    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
