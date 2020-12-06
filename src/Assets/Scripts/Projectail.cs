using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectail : MonoBehaviour {
    public float damage = 100f;
    public string Parent;
    public float GetDamage()
    {
        return damage;
    }
    public void Hit()
    {
        Destroy(this.gameObject);
    }
}
