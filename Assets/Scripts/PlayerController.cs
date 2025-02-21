using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //inputs
    [SerializeField] private KeyCode _left = KeyCode.A;
    [SerializeField] private KeyCode _right = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.W;

    //Mocement values

    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _friction = 10.0f;
    [SerializeField] private float _fallThreshold = -10.0f;

    //Physics
    private Rigidbody2D  _rb= null;
    private bool _isGrounded = false;
    private Vector2 _startingPosition;

    //Animation
    public Animator animator;
    private bool _facingRight = true;

    //BCMode
    private bool bc = false;

    //Health
    public Image healthBar;
    private float healthAmount = 100.0f;
    private float damage = 50.0f;


    //gameover
    public CanvasGroup gameOverCanvas;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if(!_rb){
            Debug.Log("Failed to get rb");
        }

        _startingPosition = transform.position;
    
    }


    // Update is called once per frame
    void Update()
    {
        updateBCMode();
        GetPlayerMovement();
        GroundCheck();

        animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));

        if (healthAmount <= 0 || transform.position.y < _fallThreshold) 
        {
            //ResetToStart();
            GameOver();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    void GetPlayerMovement(){
        if(Input.GetKey(_left)){
        _rb.linearVelocityX = -1 * _maxSpeed;
        if(_facingRight){
            Flip();
        }
        }
        else if (Input.GetKey(_right)){
            _rb.linearVelocityX = _maxSpeed;
            if(!_facingRight){
                Flip();
            }
        }
        else
        {
            _rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX, 0.0f, Time.deltaTime * _friction);
        }

        if(Input.GetKeyDown(_jump) && _isGrounded)
        {
            _rb.linearVelocityY = _jumpForce;
            _isGrounded = false;
        }
    }

    void GroundCheck()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f);

        if (groundHit.collider != null)
        {
            animator.SetBool("Jumping", false);
            _isGrounded = true;
        }
        else
        {
            animator.SetBool("Jumping", true);
            _isGrounded = false;
        }
    }

    public void Hurt()
    {
        if(!bc){
        GetComponent<SpriteRenderer>().color = Color.red;
        takeDamage(damage);
    }
    }

    public void takeDamage(float damage)
    {
        if (!bc){
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
        _maxSpeed = 7f;
        }
    }


    private void updateBCMode()
    {
        bc = PlayerPrefs.GetInt("BCMode", 0) == 1;
    }
    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ResetToStart()
    {
        transform.position = _startingPosition;
        _rb.linearVelocity = Vector2.zero;
    }
    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
