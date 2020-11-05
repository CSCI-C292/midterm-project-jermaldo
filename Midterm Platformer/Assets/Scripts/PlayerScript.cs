using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PlayerScript : MonoBehaviour
{
    //Movement Variables
    float _speed = 4f;
    public float _jumpSpeed = 7f;
    public float _fallMultiplier = 5f;
    public float _lowJumpMultiplier = 3f;


    //Object Variables
    public Animator _animator;
    Rigidbody2D _body;
    public LayerMask enemyLayers;
    public Transform _firePoint;
    public Object _sword;
    GameObject _swordClone;
    public AudioSource _swordAudio;
    public AudioSource _hitAudio;
    
    

    //Combat Variables
    int health;
    public Transform _attackPoint;
    public float _attackRange = 0.75f;
    public int _attackDamage = 4;
    public float _attackRate = 2f;
    float _nextAttackTime = 0f;
    float _hitTime = 1f;
    float _hitTimer = 0;
    bool _canBeHit = true;
    bool _hasSword = true;
    

    //Misc Variables
    public bool isGrounded = false;

    bool _knockFromRight = false;
    public static bool _hasKey = false;


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
            if (Input.GetButtonDown("Fire1") && _hasSword){
                Attack();
                _nextAttackTime = Time.time + 1f/_attackRate;
            }
        }
        if (Input.GetButtonDown("Fire2") && _hasSword){
            ThrowSword();
        }else if (Input.GetButtonDown("Fire2") && !_hasSword){
            ReturnSword();
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

        if (GetComponent<Health>()._health <= 0){
            Die();
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
        _swordAudio.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("collision");
            if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                enemy.GetComponentInParent<EnemyScript>().Damage(_attackDamage);
                Debug.Log("In enemy idf");
            } else if (enemy.gameObject.layer == LayerMask.NameToLayer("Boss")){
                enemy.GetComponentInParent<Boss>().Damage(_attackDamage);
                Debug.Log("In boss if");
            }
                
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Death"){
            Die();
        }
        if (collider.tag == "Key"){
            _hasKey = true;
            Destroy(collider.gameObject);   
        } 
        if (!_canBeHit)
            return;
        
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") ){
            _hitAudio.Play();
            if (_body.position.x > collider.GetComponent<Transform>().position.x)
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
    void ThrowSword()
    {
        //Throw Logic
        _hasSword = false;
        _swordClone = (GameObject) Instantiate(_sword, _firePoint.position, _firePoint.rotation);
        
    }
    void ReturnSword()
    {
        _swordClone.GetComponent<SwordScript>().Return(transform.position);
        _hasSword = true;
    }
    void Die()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
