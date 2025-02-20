using UnityEngine;

public class LayerScrolling : MonoBehaviour
{
    private float startpos, length;
    public GameObject cam;
    [Range(0f, 1f)]
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * speed;
        float movement = cam.transform.position.x * (1 - speed);

        transform.position = new Vector3(startpos + distance, transform.position.y);

        if (movement > startpos + length)
        {
            startpos += length;
        }
        else if (movement < startpos - length)
        {
            startpos -= length;
        }
    }
}
