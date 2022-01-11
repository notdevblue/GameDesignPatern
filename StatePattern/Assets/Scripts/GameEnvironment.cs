using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvironment
{
    static private GameEnvironment _instance;

    public List<GameObject> checkpointList = new List<GameObject>();

    public Transform safePos;

    static public GameEnvironment Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameEnvironment();
                _instance.checkpointList.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));

                _instance.checkpointList.Sort((e, x) => e.name.CompareTo(x.name));
                _instance.safePos = GameObject.FindGameObjectWithTag("SafePlace").transform;

            }

            return _instance;
        }
    }
}
