using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpawn : MonoBehaviour
{
    public GameObject enemy;

    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject Point4;

    private void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        int random = Random.Range(0, 4);

        switch(random)
        {
            case 0:
                Instantiate(enemy, Point1.transform.position, Point1.transform.rotation);
                break;
            case 1:
                Instantiate(enemy, Point2.transform.position, Point2.transform.rotation);
                break;
            case 2:
                Instantiate(enemy, Point3.transform.position, Point3.transform.rotation);
                break;
            case 3:
                Instantiate(enemy, Point4.transform.position, Point4.transform.rotation);
                break;
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(spawn());
    }
}
