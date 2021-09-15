using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    float speed = 15.0f;
    Vector2 direction;

    Rigidbody2D rb;

    ParticleSystem ps;
    PlayerMovement pm;
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GameObject.Find("ExplosionPS").GetComponent<ParticleSystem>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }


    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (Time.fixedDeltaTime * speed * direction.normalized));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            Debug.Log("Play explosion!");

            //particle
            GameObject temp = GameObject.Find("ExplosionPS");
            ps = temp.GetComponent<ParticleSystem>();
            ps.transform.position = transform.position;
            ps.Play();

            //add explosion force to the player
            pm.ExplosionForce(transform.position);

            Destroy(gameObject);
        }
    }
}
