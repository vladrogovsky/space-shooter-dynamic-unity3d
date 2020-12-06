using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject enemyPref;
    public float FormationWidth = 10f;
    public float FormationHeight = 5f;
    public float EnemySpeed = 1f;
    public float SpawnDealy = 0.001f;
    private bool SpawnInProcess = false;
    private Vector3 ViewBorders;
    private Vector3 EnemyZonePadding;
    private int multiplayerX = 1;
    private int multiplayerY = 1;

    void RespawnEnemy()
    {
        Transform EnemyFreePos = NextFreePos();
        if (EnemyFreePos)
        {
            GameObject enemy = Instantiate(enemyPref, EnemyFreePos.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = EnemyFreePos;
            if (NextFreePos())
            {
                Invoke("RespawnEnemy", SpawnDealy);
            }
        }
    }

    void Start () {
        EnemyZonePadding = new Vector3(FormationWidth, FormationHeight, 0);
        float distance = this.transform.position.z - Camera.main.transform.position.z;
        ViewBorders = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        ViewBorders += EnemyZonePadding;
        RespawnEnemy();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position,new Vector3(FormationWidth, FormationHeight, 0));
    }

    private bool AllMembersDead()
    {
       foreach (Transform childPostiitonGameObject in this.transform)
        {
            if (childPostiitonGameObject.childCount>0)
            {
                return false;
            }
        }
        return true;
    }

    private Transform NextFreePos()
    {
        foreach (Transform childPostiitonGameObject in this.transform)
        {
            if (childPostiitonGameObject.childCount <= 0)
            {
                return childPostiitonGameObject.transform;
            }
        }
        return null;
    }


    void Update () {
        // X-ax
        float fixedEnemySpeedX = -EnemySpeed * multiplayerX * Time.deltaTime;
        Vector3 nextPostX = this.transform.position + new Vector3(fixedEnemySpeedX, 0, 0);
        if (nextPostX.x >= ViewBorders.x && nextPostX.x <= -ViewBorders.x)
        {
            this.transform.position = nextPostX;
        }
        else
        {
            multiplayerX *= -1;
        }
        if (AllMembersDead())
        {
            RespawnEnemy();
        }
            
        /*
        // ||LEGASY||
        // Y-ax
        float fixedEnemySpeedY = -EnemySpeed * multiplayerY * Time.deltaTime;
        Vector3 nextPostY = this.transform.position + new Vector3(0, fixedEnemySpeedY, 0);
        print(nextPostY.y - ViewBorders.y);
        if (nextPostY.y - ViewBorders.y > 0 && nextPostY.y - ViewBorders.y < 10)
        {
            this.transform.position = nextPostY;
        }
        else
        {
            multiplayerY *= -1;
        }*/
    }
}