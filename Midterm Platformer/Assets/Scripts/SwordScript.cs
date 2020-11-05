using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public float _speed = 5f;
    public Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Collider2D>().enabled = true;
        _rb.velocity = transform.right*_speed;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }
    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            collider.GetComponentInParent<EnemyScript>().Damage(4);
            GetComponentInChildren<Collider2D>().enabled = false;
            _rb.velocity = Vector2.zero;
        }else if (collider.gameObject.layer == LayerMask.NameToLayer("Boss")){
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            collider.GetComponentInParent<Boss>().Damage(4);
            GetComponentInChildren<Collider2D>().enabled = false;
            _rb.velocity = Vector2.zero;
        } 
    }
    public void Return(Vector3 _player)
    {
        while (transform.position.x != _player.x)
            transform.position = Vector2.MoveTowards(transform.position, _player, _speed*Time.deltaTime);
        Destroy(gameObject);
    }
}
