using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startHealth = 10;
    private ShieldBehaviour _shield = null;

    private int _currentHealth = 0;

    [SerializeField] private UnityEvent _onHurtEvent;


    public float StartHealth { get { return _startHealth; } }
    public float CurrentHealth { get { return _currentHealth; } }

    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChanged;

    [SerializeField] private Color _flickerColor = Color.white;
    [SerializeField] private float _flickerDuration = 0.1f;
    private Material _attachMaterial;

    const string COLOR_PARAMETER = "_BaseColor";


    [SerializeField] private Slider _healthBarPrefab = null;
    private Slider _healthBar = null;

    void Awake()
    {
        _currentHealth = _startHealth;
        
        _shield = GetComponent<ShieldBehaviour>();
    }

    private void Start()
    {
        Renderer renderer = transform.GetComponentInChildren<Renderer>();
        if(renderer)
        {
            _attachMaterial = renderer.material;
        }

        // Instantiating enemy health
        if (_healthBarPrefab == null)
            return; 

        _healthBar = Instantiate(_healthBarPrefab, this.transform.position, this.transform.rotation);
        _healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        _healthBar.maxValue = StartHealth;
        _healthBar.value = StartHealth;
    }

    private void Update()
    {
        if (_healthBar == null)
            return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 2.0f);
        Debug.Log(Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 2.0f));
        _healthBar.gameObject.SetActive(screenPos.z >  0);
        _healthBar.transform.position = screenPos;
    }

    public void Damage(int amount)
    {
        if (_shield != null && _shield.IsBlocking)
            return;

         if(_healthBar)
        {
            _healthBar.value -= amount;
            _currentHealth -= amount;

            //OnHealthChanged?.Invoke(_startHealth, _currentHealth);
            //_onHurtEvent?.Invoke();

            if (_attachMaterial != null)
                StartCoroutine(HandleColorFlicker());

            if (_healthBar.value <= 0)
                Kill();
        }
         else
        {
            _currentHealth -= amount;

            OnHealthChanged?.Invoke(_startHealth, _currentHealth);
            _onHurtEvent?.Invoke();

            if (_attachMaterial != null)
                StartCoroutine(HandleColorFlicker());
            if (_currentHealth <= 0)
                Kill();
        }
      
    }

    private IEnumerator HandleColorFlicker()
    {
        Color startColor =  _attachMaterial.GetColor(COLOR_PARAMETER);
        float timer = _flickerDuration;
        float normalizedTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            normalizedTime = Mathf.Clamp01(timer / _flickerDuration);

            var currentColor = startColor;
            var targetColor = _flickerColor;
            var lerpTime = 1.0f - ((normalizedTime - 0.5f) * 2.0f);

            if(normalizedTime < 0.5f)
            {
                currentColor = _flickerColor;
                targetColor = startColor;
                lerpTime = 1.0f - (normalizedTime * 2.0f);
            }

            var finalColor = Color.Lerp(currentColor, targetColor, lerpTime);
            _attachMaterial.SetColor(COLOR_PARAMETER, finalColor);
            yield return new WaitForEndOfFrame();

        }
        _attachMaterial.SetColor(COLOR_PARAMETER, startColor);
    }

    public void Kill()
    {
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(_attachMaterial == null)
        {
            Destroy(_attachMaterial);
        }
    }
}








