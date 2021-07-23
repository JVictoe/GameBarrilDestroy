using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class configs : MonoBehaviour
{

    [SerializeField] private GameObject panelConfig = default;
    [SerializeField] private Button btnConfig = default;
    [SerializeField] private Button btnCloseConfig = default;


    private void Awake()
    {
        btnConfig.onClick.AddListener(ActivePanel);
        btnCloseConfig.onClick.AddListener(ClosePanel);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ActivePanel()
    {
        panelConfig.SetActive(true);
        btnCloseConfig.gameObject.SetActive(true);
        Aguarda();
        Time.timeScale = 0;  
    }
    
    IEnumerator Aguarda()
    {
        yield return new WaitForSeconds(0.05f);
    }

    void ClosePanel()
    {
        Time.timeScale = 1;
        panelConfig.SetActive(false);
        btnCloseConfig.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
