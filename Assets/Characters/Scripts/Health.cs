using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    }

    public void Damage(int amount)
    {
        if (_shield != null && _shield.IsBlocking)
            return;


        _currentHealth -= amount;

        OnHealthChanged?.Invoke(_startHealth, _currentHealth);
        _onHurtEvent?.Invoke();

        if (_attachMaterial != null)
            StartCoroutine(HandleColorFlicker());
        if (_currentHealth <= 0)
            Kill();
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

    void Kill()
    {
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








