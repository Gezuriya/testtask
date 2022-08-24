using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEnemies : MonoBehaviour
{
    [SerializeField] GameObject[] spawners, obst;
    public float Min, Max;
    int i;
    private void Start()
    {
        SpawnerContr();
    }


    public void SpawnerContr()
    {
        i = Random.Range(0, 2);
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(obst[Random.Range(0, obst.Length)], spawners[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(Min, Max));
        }
    }
}
