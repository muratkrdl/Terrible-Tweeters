using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Sprite deadSprite;
    [SerializeField] ParticleSystem _particleSystem;

    bool _hasDied = false;

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(ShouldDieFromCollision(other))
        {
            StartCoroutine(Die());
        }
    }

    bool ShouldDieFromCollision(Collision2D other)
    {
        if(_hasDied) { return false; }

        Bird bird = other.gameObject.GetComponent<Bird>();

        if(other.contacts[0].normal.y < -0.5f)
        {
            return true;
        }

        return bird != null;
    }

    IEnumerator Die()
    {
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        _particleSystem.Play();
        _hasDied = true;
        
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

}
