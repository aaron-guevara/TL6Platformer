using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public Vector2 raycastOffset;
    public float rayDistance;
    public float speed;
    public float viewDistance;

    //caching variables
    private Vector3 moveDirection;
    private Vector3 rayStart;
    private Vector3 rayOffset;

    private Transform player;

    private bool hasTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rayOffset = new Vector3(raycastOffset.x, raycastOffset.y, 0);
        moveDirection = new Vector3(1, 0, 0); 
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetValidity();

        bool walkable = WalkableGround();
        bool wall = WallInFront();

        if (hasTarget)
        {
            moveDirection.x = player.position.x - transform.position.x;
            moveDirection.x = (moveDirection.x / Mathf.Abs(moveDirection.x));

            if (walkable && !wall)
            {
                transform.position += moveDirection * speed * Time.deltaTime;
            }
        }
        else
        {
            if (!walkable || wall)
            {
                Flip();
            }
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        rayStart = transform.position + rayOffset;

        Debug.DrawLine(rayStart, rayStart + Vector3.down * rayDistance, Color.red);
        Debug.DrawLine(transform.position, transform.position + moveDirection, Color.green);
    }

    private bool WalkableGround()
    {
        rayStart = transform.position + rayOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, rayDistance, LayerMask.GetMask("Default"));

        return hit.collider != null;
    }

    private bool WallInFront()
    {
        rayStart = transform.position + rayOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.right * moveDirection.x, transform.localScale.x / 2 + 0.01f);

        return hit.collider != null;
    }

    private void Flip()
    {
        moveDirection.x *= -1;
        rayOffset.x *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void CheckTargetValidity()
    {
        Vector3 localPos = player.position - transform.position;
        localPos.y = 0;
        localPos.z = 0;

        float squareDist = localPos.sqrMagnitude;

        if (squareDist > viewDistance * viewDistance)
        {
            rayDistance = 1;
            hasTarget = false;
            return;
        }

        float local = (localPos.x / Mathf.Abs(localPos.x));
        float lookDir = (moveDirection.x / Mathf.Abs(moveDirection.x));

        if (Mathf.Abs(local - lookDir) < 0.001f)
        {
            hasTarget = true;
            rayDistance = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.ToLower() == "player")
        {
            collision.collider.gameObject.GetComponent<PlayerController>().Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToLower() == "stomp")
        {
            gameObject.SetActive(false);
        }
    }
}
/*using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public Vector2 raycastOffset;
    public float rayDistance;
    public float speed;
    public float viewDistance;

    //caching variables
    private Vector3 moveDirection;
    private Vector3 rayStart;
    private Vector3 rayOffset;

    private Transform player;

    private bool hasTarget;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rayOffset = new Vector3(raycastOffset.x, raycastOffset.y, 0);
        moveDirection = new Vector3(1, 0, 0); 
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetValidity();

        bool walkable = WalkableGround();
        bool wall = WallInFront();

        if (hasTarget)
        {
            moveDirection.x = player.position.x - transform.position.x;
            moveDirection.x = (moveDirection.x / Mathf.Abs(moveDirection.x));

            if (walkable && !wall)
            {
                transform.position += moveDirection * Time.deltaTime;
            }
        }
        else{
            if (!walkable || wall)
            {
                moveDirection.x *= -1;
                rayOffset *= -1;
            }
            transform.position += moveDirection * Time.deltaTime;
        }

        rayStart = transform.position + rayOffset;

        Debug.DrawLine(rayStart, rayStart + Vector3.down, Color.red);

        Debug.DrawLine(transform.position, transform.position + moveDirection, Color.green);
    }


    private bool WalkableGround()
    {
        rayStart = transform.position + rayOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, rayDistance, LayerMask.GetMask("Default"));

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private bool WallInFront()
    {
        rayStart = transform.position + rayOffset;
        RaycastHit2D hit 
        = Physics2D.Raycast(rayStart, Vector2.right * moveDirection.x, transform.localScale.x /2 + .01f);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private void CheckTargetValidity()
    {
        Vector3 localPos = player.position - transform.position;
        localPos.y = 0;
        localPos.z = 0;

        float squareDist = localPos.sqrMagnitude;

        if(squareDist > viewDistance * viewDistance)
        {
            rayDistance = 1;
            hasTarget = false;
            return;
        }

        float local = (localPos.x / Mathf.Abs(localPos.x));
        float lookDir = (moveDirection.x / Mathf.Abs(moveDirection.x));


        if (Mathf.Abs(local - lookDir) < 0.001f)
        {
            hasTarget = true;
            rayDistance = 2;
        }



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.ToLower() == "player")
        {
            collision.collider.gameObject.GetComponent<PlayerController>().Hurt();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToLower() == "stomp")
        {
            gameObject.SetActive(false);
        }
    }
}
*/