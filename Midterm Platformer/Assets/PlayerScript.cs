using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    public float _jumpSpeed = 3f;
    public float _fallMultiplier = 2.5f;
    public float _lowJumpMultiplier = 2f;

    public Animator _animator;
    Rigidbody2D _body;
    int health;
    public Transform _attackPoint;
    public float _attackRange = 0.5f;

    public LayerMask enemyLayers;


    public int _attackDamage = 4;
    public float _attackRate = 2f;
    float _nextAttackTime = 0f;
    

    float _hitTime = 1f;
    float _hitTimer = 0;
    bool _canBeHit = true;
    public bool isGrounded = false;

    bool _knockFromRight = false;
    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {   
        _hitTimer += Time.deltaTime;
        if (_hitTimer > _hitTime)
            _canBeHit = true;
        if (Time.time >= _nextAttackTime){
            if (Input.GetButtonDown("Fire1")){
                Attack();
                _nextAttackTime = Time.time + 1f/_attackRate;
            }
        }
        
            
        Jump();
        if (_body.velocity.y < 0){//Great jump tutorial https://www.youtube.com/watch?v=7KiK0Aqtmzc that helps jumping feel better
            _body.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier-1) * Time.deltaTime;
        }else if (_body.velocity.y > 0 && !Input.GetButton("Jump")){
            _body.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier-1) * Time.deltaTime;
        }
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        _animator.SetFloat("Speed", Mathf.Abs(movement.x));
        transform.position += movement * Time.deltaTime * _speed;
        if (Input.GetAxis("Horizontal") < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (Input.GetAxis("Horizontal") > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);

        if (isGrounded){
            _animator.SetBool("IsJumping", false);
        }
    }
    void Jump()
    {
        _animator.SetBool("IsJumping", true);
        if (Input.GetButtonDown("Jump") && isGrounded == true){
            GetComponent<Rigidbody2D>().velocity = Vector2.up * _jumpSpeed;
        }    
    }
    
    void Attack() 
    {
        
        _animator.SetTrigger("Attack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<EnemyScript>().Damage(_attackDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (!_canBeHit)
            return;
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") ){
            if (_body.position.x > collider.GetComponent<Rigidbody2D>().position.x)
                _knockFromRight = false;
            else 
                _knockFromRight = true;
            if (_knockFromRight){
                _body.velocity = new Vector2(-5f, 3f);
            }else if (!_knockFromRight){
                _body.velocity = new Vector2(5f, 3f);
            }

            GetComponent<Health>().changeHealth(2);
            _hitTimer = 0;
            _canBeHit = false;
        }
    }
}
