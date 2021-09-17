using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 StartingPosition;
    private void Start()
    {
        StartingPosition = transform.position;
    }

    private void Update()
    {
        transform.position = StartingPosition + new Vector3(0.0f, 0.1f * Mathf.Sin(2.0f * Time.time));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().OnDeath();
        }

    }
}
