using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumberManager : MonoBehaviour
{
    public List<int> scrollingLockCode;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < scrollingLockCode.Count; i++)
        {
            scrollingLockCode[i] = Random.Range(0, 9);
        }
    }
}
