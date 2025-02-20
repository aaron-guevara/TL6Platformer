using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float _horGoal = 0.0f;
    [SerializeField] private float _vertGoal = 0.0f;

    [SerializeField] private float _moveTime = 1.0f;

    private Vector2 _startPos;
    private Vector2 _goalPos;
    private Vector2 _prevPos;

    private Rigidbody2D _playerRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        _goalPos = new Vector2(_startPos.x + _horGoal, _startPos.y + _vertGoal);
        _prevPos = _startPos;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time / _moveTime, 1.0f);

        // Interpolate New Pos
        Vector2 curPos = Vector2.Lerp(_startPos, _goalPos, t);
        transform.position = curPos;

        Vector2 platformVelocity = (curPos - _prevPos) / Time.deltaTime;

        if(_playerRigidbody != null)
        {
            Vector2 playerVel = _playerRigidbody.linearVelocity;
            playerVel.x = platformVelocity.x;
            _playerRigidbody.linearVelocity = playerVel;
            
        }

        _prevPos = curPos;
        
    }

//stick
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            _playerRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        }
    }
//unstick
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(collision.collider.GetComponent<Rigidbody2D>() == _playerRigidbody)
            {
                _playerRigidbody = null;
            }
        }
    }
}
