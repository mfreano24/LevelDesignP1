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

    Vector3 StartingPosition = Vector3.zero;

    private void Start()
    {
        StartingPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(StartingPosition == Vector3.zero)
        {
            StartingPosition = transform.position;
        }
        
        Gizmos.color = Color.green;
        Vector3 amp = new Vector3();
        switch (direction)
        {
            case Direction.Horizontal:
                amp = new Vector3(Amplitude, 0.0f);
                Gizmos.DrawLine(StartingPosition - amp, StartingPosition + amp);
                Gizmos.DrawSphere(StartingPosition - amp, 0.25f);
                Gizmos.DrawSphere(StartingPosition + amp, 0.25f);
                break;

            case Direction.Vertical:
                amp = new Vector3(0.0f, Amplitude);
                Gizmos.DrawLine(StartingPosition - amp, StartingPosition + amp);
                Gizmos.DrawSphere(StartingPosition - amp, 0.25f);
                Gizmos.DrawSphere(StartingPosition + amp, 0.25f);
                break;
        }



    }

    void FixedUpdate()
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

        transform.position = StartingPosition + (Vector3)Offset;
    }
}





