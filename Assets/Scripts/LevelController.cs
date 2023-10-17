using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Monster[] _monsters;

    void OnEnable() 
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    void Update()
    {
        if(MonstersAreAllDead())
        {
            GoNextLevel();
        }
    }

    void GoNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    bool MonstersAreAllDead()
    {
        foreach (var item in _monsters)
        {
            if(item.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
