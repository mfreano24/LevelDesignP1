using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShooter : MonoBehaviour
{
    public GameObject rocketPrefab;

    float cooldownTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;

            if(cooldownTimer < 0.0f)
            {
                cooldownTimer = 0.0f;
            }
        }

        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0.0f)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(transform.position, position, Color.red, 2.5f);
            Rocket r = Instantiate(rocketPrefab, transform, false).GetComponent<Rocket>();
            r.SetDirection(position - (Vector2)transform.position);
            cooldownTimer = 0.25f;
        }
    }
}
