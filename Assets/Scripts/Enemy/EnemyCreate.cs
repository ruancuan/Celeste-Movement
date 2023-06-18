using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    public int createNum = 10;
    public float startWait = 3;
    public float createInterval = 5;
    public Transform createPosition;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemy());
    }
    private WaitForSeconds createWait;
    IEnumerator CreateEnemy() {
        if (enemyPrefab == null) {
            yield break;
        }
        createWait = new WaitForSeconds(createInterval);
        yield return new WaitForSeconds(startWait);
        for (int k = 0; k < createNum; k++) {
            CreateEnemyObject();
            yield return createWait;
        }
    }

    private void CreateEnemyObject() {
        GameObject go = Instantiate(enemyPrefab);
        go.transform.position = createPosition.position;
        go.transform.rotation = createPosition.rotation;
        go.transform.localScale = Vector3.one;
        go.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
