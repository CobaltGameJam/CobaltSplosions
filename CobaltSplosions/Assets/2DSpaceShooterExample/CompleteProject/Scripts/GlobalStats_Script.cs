using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats_Script : MonoBehaviour
{

    public static GlobalStats_Script Instance;

    public float BestTime = 0;
    public int BestScore = 0;

    void Awake ()
       {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
      }

}
