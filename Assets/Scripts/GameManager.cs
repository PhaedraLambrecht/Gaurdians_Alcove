using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private bool _checkArtifact = true;
    [SerializeField] private GameObject _artifact = null;



    private void Update()
    {
        if (_player == null ||
           (_checkArtifact && _artifact == null) )
        {
            TriggerGameOver();
        }


    }

    void TriggerGameOver()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(currentScene);

    }

    public void TriggerNextScene(int i)
    {
        LoadLevel(i);
    }

    private void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }

}
