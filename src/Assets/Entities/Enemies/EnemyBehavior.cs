using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float EnemyHealth = 150f;
    public GameObject EnemeyLaser;
    public float shotsPerSeconds = 0.5f;
    // Use this for initialization
    void Start () {
        //InvokeRepeating("EnemeyAction", Random.value, Random.value*2);
    }
	
	// Update is called once per frame
	void Update () {
        float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability)
        {
            EnemeyAction();
        }
    }
    void EnemeyAction()
    {
        GameObject EnemyShot = Instantiate(EnemeyLaser, this.transform.position, Quaternion.identity) as GameObject;
        EnemyShot.GetComponent<Projectail>().Parent = "Enemy";
        EnemyShot.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -5f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectail missle = collision.gameObject.GetComponent<Projectail>();
        if (missle && missle.Parent != "Enemy")
        {
            missle.Hit();
            EnemyHealth -= missle.GetDamage();
            if (EnemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }            
        }
    }
}
