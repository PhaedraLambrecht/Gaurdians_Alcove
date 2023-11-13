using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CountDownTimer : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;

    private Label _timerLabel = null;


    private float _currentTime = 0.0f;
    private bool _running = false;
    public bool IsRunning
    {
        get { return _running; }
       private set { _running = value; }
    }


    // Start is called before the first frame update
    private void Start()
    {
        //UI
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }

        if (_root != null)
        {
            _timerLabel = _root.Q<Label>("CountdownText");
        }

        SetCurrentTime(0.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_currentTime <= 0.0f)
        {
            IsRunning = false;
            return;
        }

            
        IsRunning = true;
        _currentTime -= 1 * Time.deltaTime;

        _timerLabel.text = _currentTime.ToString("0");
    }

    public void SetCurrentTime(float currenttime)
    {
        _currentTime = currenttime;
    }
}
