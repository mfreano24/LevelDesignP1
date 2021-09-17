using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Amplitude;

    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public Direction direction;
    Vector2 Offset = new Vector2();

    Vector3 StartingPosition;

    private void Start()
    {
        StartingPosition = transform.position;

        if (Offset == Vector2.zero)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    Offset = new Vector2((Amplitude) * Mathf.Sin(Time.time), 0.0f);
                    break;

                case Direction.Vertical:
                    Offset = new Vector2(0.0f, (Amplitude) * Mathf.Sin(Time.time));
                    break;

            }
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Offset == Vector2.zero)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    Offset = new Vector2((Amplitude) * Mathf.Sin(Time.time), 0.0f);
                    break;

                case Direction.Vertical:
                    Offset = new Vector2(0.0f, (Amplitude) * Mathf.Sin(Time.time));
                    break;

            }
        }

        Gizmos.DrawLine(StartingPosition, StartingPosition + (Vector3)Offset);
        Gizmos.DrawLine(StartingPosition, StartingPosition - (Vector3)Offset);
    }
    */

    void Update()
    {
        transform.position = StartingPosition + (Vector3)Offset;
    }
}
