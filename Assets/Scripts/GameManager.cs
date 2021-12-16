using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject robberPrefab;
    public GameObject copPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnPlayer()
    {
        // set up character
        GameObject obj;
        if (PhotonNetwork.IsMasterClient)
        {
            obj = robberPrefab;
        }
        else
        {
            obj = copPrefab;
        }

        // spawn character
        Vector3 randPos = new Vector3(Random.Range(-25, 25), obj.transform.position.y, Random.Range(-25, 25));
        PhotonNetwork.Instantiate(obj.name, randPos, Quaternion.identity);
    }

}
