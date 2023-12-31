using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;

    private ProgressBar _playerHealthbar = null;
    private ProgressBar _artifactHealthbar = null;

    // Start is called before the first frame update
    void Start()
    {
        //UI
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root != null)
        {
            _playerHealthbar = _root.Q<ProgressBar>("PlayerHealthbar");
           _artifactHealthbar = _root.Q<ProgressBar>("ArtifactHealth");

            PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
            if (player != null)
            {

                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth)
                {
                    // initialize
                    UpdatePlayerHealth(playerHealth.StartHealth, playerHealth.CurrentHealth);
                    // hook to monitor changes
                    playerHealth.OnHealthChanged += UpdatePlayerHealth;
                }
            }

            BasicArtifact artifact = FindObjectOfType<BasicArtifact>();
            if (artifact != null)
            {

                Health artifactHealth = artifact.GetComponent<Health>();
                if (artifactHealth)
                {
                    // initialize
                    UpdateArtifactHealth(artifactHealth.StartHealth, artifactHealth.CurrentHealth);
                    // hook to monitor changes
                    artifactHealth.OnHealthChanged += UpdateArtifactHealth;
                }
            }
        }
    }

    public void UpdatePlayerHealth(float startHealth, float currentHealth)
    {
        if (_playerHealthbar == null) return;

        _playerHealthbar.value = currentHealth / startHealth;
        _playerHealthbar.title = string.Format("Player: {0}/{1}", currentHealth, startHealth);
    }

    public void UpdateArtifactHealth(float startHealth, float currentHealth)
    {
        if (_artifactHealthbar == null) return;

        _artifactHealthbar.value = currentHealth / startHealth;
        _artifactHealthbar.title = string.Format(" Artifact: {0}/{1}", currentHealth, startHealth);
    }
}


