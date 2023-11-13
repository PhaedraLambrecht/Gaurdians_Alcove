using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _isActive = false;

    public bool Activeportal
    {
        get { return _isActive; }
        set { _isActive = value; }
    }


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(_isActive)
        {
            int CurrentScene = SceneManager.GetActiveScene().buildIndex + 1;
            _gameManager.TriggerNextScene(CurrentScene);
        }
    }
}
