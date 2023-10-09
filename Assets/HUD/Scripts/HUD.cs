using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;

    private ProgressBar _healthbar = null;

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
            _healthbar = _root.Q<ProgressBar>(); //this will find the first progressbar in the hud, for now as there is only one, that is fine, if we need to be more specific we could pass along a string parameter to define the name of the element.

            PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
            if (player != null)
            {

                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth)
                {
                    // initialize
                    UpdateHealth(playerHealth.StartHealth, playerHealth.CurrentHealth);
                    // hook to monitor changes
                    playerHealth.OnHealthChanged += UpdateHealth;
                }
            }
        }
    }

    public void UpdateHealth(float startHealth, float currentHealth)
    {
        if (_healthbar == null) return;

        _healthbar.value = currentHealth / startHealth;
        _healthbar.title = string.Format("{0}/{1}", currentHealth, startHealth);
    }
}


