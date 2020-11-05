using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Health Variables
    public int _maxHealth = 10;
    public HealthBar _healthBar;
    int _currentHealth;

    
    //Object Variables
    GameObject _enemy;
    public GameObject _player;
    public Animator _anim;
    public Rigidbody2D rb;    
    public GameObject _deathParticles;
    public AudioSource _hit;

    //Move Variables
    float _moveSpeed = 1.5f;
    float _agroRange = 3f;

    //Misc Variables
    bool _knockFromRight = false;
    public bool isGrounded;

    


    void Start()
    {
        _currentHealth = _maxHealth;
        rb = GetComponent<Rigidbody2D>();
        
        _healthBar.setMaxHealth(_maxHealth);
    }

    public void Damage(int damage)
    {   
        _hit.Play();
        _currentHealth -= damage;
        _healthBar.setHealth(_currentHealth);
        rb.velocity = Vector2.zero;
        if (rb.position.x > _player.GetComponent<Rigidbody2D>().position.x)
            _knockFromRight = false;
        else 
            _knockFromRight = true;
        
        _anim.SetTrigger("Hurt");
        _anim.SetBool("Grounded", false);
        if (_knockFromRight){
            rb.velocity = new Vector2(-2f, 3f);
        }else if (!_knockFromRight){
            rb.velocity = new Vector2(2f, 3f);
        }
        if (_currentHealth <=0 )
        {
            Die();
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground"){
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground"){
            isGrounded = false;
        }
    }

    void Update()
    {
        
        if (isGrounded){
            _anim.SetBool("Grounded", true);
        }
        float _dist = Vector2.Distance(transform.position, _player.transform.position);
        if (_dist < _agroRange){
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if (_player.transform.position.x > transform.position.x){
            rb.velocity = new Vector2(_moveSpeed, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }else{
            rb.velocity = new Vector2(-_moveSpeed, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    void Die()
    {
        
        Instantiate(_deathParticles, transform.position, transform.rotation);
        GetComponentInChildren<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
        
        

    }
}
