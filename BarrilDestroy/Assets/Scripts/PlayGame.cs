using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{

    [SerializeField] private Button _button = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _button.onClick.AddListener(PlayGames);
    }

    void PlayGames()
    {
        SceneManager.LoadScene("2 - Game", LoadSceneMode.Single);
        //Destroy(_button.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
