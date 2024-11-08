using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;       // Movement speed in m/s
    public float fireRate = 0.3f;   // Seconds/shot (Unused)
    public float health = 10;       // Damage needed to destroy this
    public int score = 100;         // Points earned for destroying this

    protected BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Check whether this Enemy has gone off the bottom of the screen
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)) {
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.GetComponent<ProjectileHero>() != null) 
        {
            Destroy(otherGO);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
