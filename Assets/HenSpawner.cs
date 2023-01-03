using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenSpawner : MonoBehaviour
{
    public bool activateSpawner = false; 
    public GameObject HenPrefab;
    public float timeBetweenSpawns;
    public float movementRadius = 1; 
    public bool SpawnHen = true;

    // Update is called once per frame
    void Update()
    {
        SpawnNewHen();
    }


    private void SpawnNewHen()
    {
        if (activateSpawner)
        {
            if (SpawnHen)
            {
                SpawnHen = false;
                GameObject newHen = Instantiate(HenPrefab, transform.position, Quaternion.identity);
                newHen.transform.parent = transform;
                newHen.GetComponent<HenMovement>().henPlaceTransform = transform;
                StartCoroutine("SpawnNewHenCorutine");
            }
        }
    }

    private IEnumerator SpawnNewHenCorutine()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnHen = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, movementRadius);
    }
}
