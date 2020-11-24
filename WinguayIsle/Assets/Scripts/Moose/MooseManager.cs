using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooseManager : MonoBehaviour
{
    [Header("Moose data")]
    public GameObject moosePrefab;
    public Bounds mooseSpawn;
    public Bounds moosePasture;
    public int moosePopulationStart;
    public float moosePadding;
    public List<GameObject> moosePopulation;


    private GameObject wolf;
    // Start is called before the first frame update
    void Start()
    {
        wolf = GameObject.FindGameObjectWithTag("Wolf");
        MooseSpawn(moosePrefab, mooseSpawn, moosePopulationStart);
        
    }

    void MooseSpawn(GameObject _prefab,Bounds _prefabSpawn,int _originalPopulation)
    {
        for(int i=0;i< _originalPopulation; i++)
        {
            Vector3 spawnLocation = new Vector3();
            spawnLocation.x = Random.Range(_prefabSpawn.min.x, _prefabSpawn.max.x);
            spawnLocation.y = Random.Range(_prefabSpawn.min.y, _prefabSpawn.max.y);
            spawnLocation.z = 0;

            GameObject newMoose = Instantiate(_prefab, spawnLocation, Quaternion.identity, transform);
            newMoose.GetComponent<MooseAI>().StartBehaviours(moosePasture,moosePadding,wolf);
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(mooseSpawn.center, mooseSpawn.size);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(moosePasture.center, moosePasture.size);
    }
}
