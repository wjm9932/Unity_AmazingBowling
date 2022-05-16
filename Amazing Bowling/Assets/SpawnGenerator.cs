using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    private List<GameObject> propList = new List<GameObject>();
    private BoxCollider area;
    public GameObject[] propPrefabs;
    public int count = 100;

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();

        for(int i = 0; i < count; i++)
        {
            Spawn();
        }

        area.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefabs = propPrefabs[selection];
        Vector3 spawnPos = GetRandomPosition();

        propList.Add(Instantiate(selectedPrefabs, spawnPos, Quaternion.identity));
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x/2f, size.x/2f);
        float posY = basePos.y + Random.Range(-size.y/2f, size.y/2f);
        float posZ = basePos.z + Random.Range(-size.z/2f, size.z/2f);

        Vector3 SpawnPos = new Vector3(posX, posY, posZ);

        return SpawnPos;
    }

    public void Reset()
    {
        for(int i = 0; i < propList.Count; i++)
        {
            propList[i].transform.position = GetRandomPosition();
            propList[i].SetActive(true);
        }
    }
}
