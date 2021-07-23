using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject panelConfig = default;
    [SerializeField] private Animator panelAnimator = default;
    [SerializeField] private Button btnConfig = default;
    [SerializeField] private Button btnCloseConfig = default;


    private void Awake()
    {
        btnConfig.onClick.AddListener(PausePanel);
        btnCloseConfig.onClick.AddListener(Play);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void PausePanel()
    {
        panelConfig.SetActive(true);
        btnCloseConfig.gameObject.SetActive(true);
        //Time.timeScale = 0;
    }

    void Play()
    {
        panelConfig.SetActive(false);
        btnCloseConfig.gameObject.SetActive(false);
        //Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
