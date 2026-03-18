using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveTowardsTarget : MonoBehaviour
{
    public bool targetIsPlayer = true;
    public Transform alternativeTarget;

    private Transform playerTransform;
    private Rigidbody2D rigidbody2D;

    public float moveSpeed = 15;
    public float maxSpeed = 50;

    public bool moveInXDirection = true;
    public bool moveInYDirection = true;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerTransform = PlayerController.instance.transform;
    }

    void FixedUpdate()
    {
        Transform target = targetIsPlayer ? playerTransform : alternativeTarget;
        if (target == null) return;

        Vector2 dir = (target.position - transform.position);
        if (dir.sqrMagnitude <= 0.0001f) return;
        dir.Normalize();

        Vector2 moveDirection = new Vector2(
            moveInXDirection ? dir.x : 0f,
            moveInYDirection ? dir.y : 0f
        );

        // If Rigidbody2D is kinematic, AddForce does nothing — move using MovePosition
        if (rigidbody2D.bodyType == RigidbodyType2D.Kinematic)
        {
            Vector2 desiredVelocity = moveDirection * moveSpeed;
            Vector2 newPos = rigidbody2D.position + desiredVelocity * Time.fixedDeltaTime;
            rigidbody2D.MovePosition(newPos);
        }
        else
        {
            // Dynamic or other body types: use forces and clamp velocity
            rigidbody2D.AddForce(moveDirection * moveSpeed);
            if (rigidbody2D.linearVelocity.sqrMagnitude > (maxSpeed * maxSpeed))
            {
                rigidbody2D.linearVelocity = rigidbody2D.linearVelocity.normalized * maxSpeed;
            }
        }
        /*Vector2 moveDirection = Vector2.zero;
        Transform target = alternativeTarget;

        if(targetIsPlayer)
        {
            target = playerTransform;
        }

        moveDirection = (target.position - transform.position).normalized;
        moveDirection = new Vector2(
            moveInXDirection ? moveDirection.x : 0,
            moveInYDirection ? moveDirection.y : 0
        );

        rigidbody2D.AddForce(Vector3.ClampMagnitude(moveDirection * moveSpeed,maxSpeed));*/
    }
}
