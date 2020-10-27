using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   public int _health;
   public int _numOfHearts;

   public Image[] hearts;
   public Sprite _fullHeart;
   public Sprite _halfHeart;
   public Sprite _emptyHeart;
   
    void Awake()
    {
        _health = 10;
    }

   void Update()
    {
        int i;
        int _temp = _health;
        for (i=0; i<hearts.Length; i++){
                if (_temp > 1){
                    hearts[i].sprite = _fullHeart;
                    _temp -= 2;
                }else if(_temp == 1){
                    hearts[i].sprite = _halfHeart;
                    _temp--;
                }else {
                    hearts[i].sprite = _emptyHeart;
                }
        }
    }
    public void changeHealth(int damage)
    {
        _health -= damage;
    } 
}
