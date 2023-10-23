using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _artifact = null;

    private void Update()
    {
        if (_player == null )
            //|| _artifact == null)
        {
            TriggerGameOver();
        }
        

    }

    void TriggerGameOver()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }
}



