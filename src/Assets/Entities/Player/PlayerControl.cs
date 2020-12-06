using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float playerSpeed = 10f;
    public GameObject PlayerLaser;
    public float laserSpeed;
    public float fireRate = 0.2f;
    public float PlayerHealth = 250f;
    private Vector3 ViewBorders;
    private Vector3 ShipPadding = new Vector3(0.45f,0.45f,0);
    private GameObject OneShoot;
    public LevelManager LevelManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectail missle = collision.gameObject.GetComponent<Projectail>();
        if (missle && missle.Parent != "Player")
        {
            missle.Hit();
            PlayerHealth -= missle.GetDamage();
            if (PlayerHealth <= 0)
            {
                Destroy(this.gameObject);
                LevelManager.LoadLv("Lose");
            }
        }
    }
    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        ViewBorders = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        ViewBorders += ShipPadding;
    }
	void Fire()
    {
        Vector3 LaserPos = new Vector3(this.transform.position.x+0.5f, this.transform.position.y, this.transform.position.z);
        OneShoot = Instantiate(PlayerLaser, LaserPos, Quaternion.identity) as GameObject;
        OneShoot.GetComponent<Projectail>().Parent = "Player";
        OneShoot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
    }
	// Update is called once per frame
	void Update () {
        float frameRateIndependentSpeed = playerSpeed*Time.deltaTime;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(new Vector2(0, frameRateIndependentSpeed));
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(new Vector2(0, -frameRateIndependentSpeed));
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(new Vector2(-frameRateIndependentSpeed, 0));   
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(new Vector2(frameRateIndependentSpeed, 0));
        }
        // Limit movement to borders
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, ViewBorders.x, -ViewBorders.x),
                                              Mathf.Clamp(this.transform.position.y, ViewBorders.y, -ViewBorders.y),
                                              Mathf.Clamp(this.transform.position.z, ViewBorders.z, -ViewBorders.z));
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("Fire",0.000001f,fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
        }
    }
}
