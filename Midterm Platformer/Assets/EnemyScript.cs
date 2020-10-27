using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int _maxHealth = 10;
    int _currentHealth;
    bool _knockFromRight = false;

    GameObject _enemy;
    public GameObject _player;

    public HealthBar _healthBar;

    public Animator _anim;
    public Rigidbody2D rb;
    public bool isGrounded;
    void Start()
    {
        _currentHealth = _maxHealth;
        rb = GetComponent<Rigidbody2D>();
        _healthBar.setMaxHealth(_maxHealth);
    }
    public void Damage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.setHealth(_currentHealth);
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
    }
    void Die()
    {
        Debug.Log("Enemy died :)");
        //Die anim
        //disable

    }
}
