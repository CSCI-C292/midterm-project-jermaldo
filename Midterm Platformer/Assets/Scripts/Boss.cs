using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour
{
    
    public Transform _player;
    public Rigidbody2D rb;
    public Transform _attackPoint;
    public LayerMask _playerLayer;

    //Health Variables
    public HealthBar _bar;
    public int _maxHealth = 50;
    int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        _bar.setMaxHealth(_maxHealth);
    }
    
    
    public void LookTowardsPlayer() 
    {
        if (_player.transform.position.x > transform.position.x){
            GetComponent<SpriteRenderer>().flipX = false;
        }else{
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void Attack()
    {   

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(_attackPoint.position, 2f, _playerLayer);
        
        foreach(Collider2D objects in hitObjects){
            if (objects.name == "Player"){
                objects.GetComponent<Health>().changeHealth(5);
            }
        }
    }
    public void Damage(int damage)
    {
        _currentHealth -= damage;
        
    }

    public void Update()
    {
        _bar.setHealth(_currentHealth);
        if (_currentHealth <= 0){
            Die();
        }
    }
    
    void Die()
    {
        GetComponentInChildren<Collider2D>().enabled = false;
        Destroy(gameObject);
        this.enabled = false;
        SceneManager.LoadScene("EndScreen");
    }
}