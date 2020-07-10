using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int level = 0;
    private int totalNPCs;

    public List<GameObject> storePrefabs;

    public List<Store> stores;
    public List<NPC> NPCs;

    public int followExposureChange = 10;

    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public Store GetTargetStore()
    {
        int random = Random.Range(0, stores.Count - 1);
        return stores[random];
    }

    public void GenerateLevel()
    {

    }

}
