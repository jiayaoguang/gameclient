using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateEnemyProcessor : Processor
{

    private GameObject enemyPrefab;
    public CreateEnemyProcessor() {

        enemyPrefab = Resources.Load<GameObject>("Prefabs/Player");
    }

    

    public void process(byte[] msg)
    {

        GameObject gameObject = GameObject.Instantiate( enemyPrefab );

        gameObject.GetComponent<Transform>().position = new Vector3(10,0,10);
        gameObject.SetActive(true);


        Debug.Log(" create enemy ");


    }

  
}
