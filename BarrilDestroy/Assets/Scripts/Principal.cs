using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Principal : MonoBehaviour
{

    [SerializeField] private GameObject jogadorFelpudo;
    [SerializeField] private Rigidbody2D jogadorFelpudoR;
    [SerializeField] private GameObject felpudoIdle;
    [SerializeField] private GameObject felpudoBate;

    [SerializeField] private Sprite[] _iamgeFundo = default;
    [SerializeField] private GameObject panelConfig = default;
    [SerializeField] private Animator panelAnimator = default;
    [SerializeField] private Button btnConfig = default;
    [SerializeField] private Button[] btnCloseConfig = default;

    public GameObject barrel;
    public GameObject enemyRight;
    public GameObject enemyLeft;

    float escalaJogadorHorizontal;

    private List<Blocos> listaBlocos;

    bool ladoPersonagem;

    public AudioClip somPerde;

    public TextMeshProUGUI pontuacao;
    public int score;

    bool comecou;
    bool acabou;

    bool pausaTudo = true;

    [SerializeField] private BarraDeTempo gameEngine;

    [SerializeField] public TextMeshProUGUI textLife = default;
    //[SerializeField] public SpriteRenderer imgLife = default;
    int numberLife = 10;
    bool moreLife = false;

    void PausePanel()
    {
        panelConfig.SetActive(true);
        for (int i = 0; i <= 1; i++)
        {
            btnCloseConfig[i].gameObject.SetActive(true);
        }
        
        Invoke(nameof(Pause), 0.3f);
    }

    void Pause()
    {
        pausaTudo = false;
        Time.timeScale = 0;
    }

    void Play()
    {
        panelConfig.SetActive(false);
        for (int i = 0; i <= 1; i++)
        {
            btnCloseConfig[i].gameObject.SetActive(false);
        }

        pausaTudo = true;

        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic();
        moreLife = true;
        escalaJogadorHorizontal = transform.localScale.x;

        felpudoBate.SetActive(false);
        listaBlocos = new List<Blocos>();
        CreateBarrelStart();

        btnConfig.onClick.AddListener(PausePanel);
        for(int i = 0; i <= 1; i++)
        {
            btnCloseConfig[i].onClick.AddListener(Play);
        }
        

        //pontuacao.transform.position = new Vector2(Screen.width / 2,
        //    Screen.height / 2);
        //pontuacao.text = "Toque para iniciar!";
        //pontuacao.fontSize = 15;
        textLife.text = numberLife.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(pausaTudo)
        {
            if (!acabou)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (!comecou)
                    {
                        comecou = true;
                        gameEngine.Comecou();
                    }
                    if (Input.mousePosition.x > Screen.width / 2)
                    {
                        BateDireita();
                    }
                    else
                    {
                        BateEsquerda();
                    }
                    listaBlocos.RemoveAt(0);
                    ReposicionaBloco();
                    ConfereJogada();
                }
            }
        }  
    }

    void BateDireita()
    {
        ladoPersonagem = true;
        felpudoIdle.SetActive(false);
        felpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(1.1f, jogadorFelpudo.transform.position.y);
        jogadorFelpudo.transform.localScale = new Vector2(-escalaJogadorHorizontal,
            jogadorFelpudo.transform.localScale.y);
        Invoke(nameof(VoltaAnimacao), 0.25f);
        listaBlocos[0].AnimaDireita();
    }

    void BateEsquerda()
    {
        ladoPersonagem = false;
        felpudoIdle.SetActive(false);
        felpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(-1.1f, jogadorFelpudo.transform.position.y);
        jogadorFelpudo.transform.localScale = new Vector2(escalaJogadorHorizontal,
            jogadorFelpudo.transform.localScale.y);
        Invoke(nameof(VoltaAnimacao), 0.25f);
        listaBlocos[0].AnimaEsquerda();
    }

    void VoltaAnimacao()
    {
        felpudoIdle.SetActive(true);
        felpudoBate.SetActive(false);
    }

    GameObject CreateNewBarrel(Vector2 posicao)
    {
        GameObject newBarrel;

        if (Random.value > 0.5f || listaBlocos.Count < 2)
        {
            newBarrel = Instantiate(barrel);
        }
        else
        {
            if (Random.value > 0.5f)
            {
                newBarrel = Instantiate(enemyRight);
            }
            else
            {
                newBarrel = Instantiate(enemyLeft);
            }
        }
        newBarrel.transform.position = posicao;

        return newBarrel;
    }

    void CreateBarrelStart()
    {
        for (int i = 0; i <= 7; i++)
        {
            GameObject objetoBarril = CreateNewBarrel(new Vector2(0, jogadorFelpudo.transform.position.y + (i * 0.99f)));
            Blocos blocos = objetoBarril.GetComponent<Blocos>();
            listaBlocos.Add(blocos);
        }
    }

    void ReposicionaBloco()
    {
        GameObject objetoBarril = CreateNewBarrel(new Vector2(0, jogadorFelpudo.transform.position.y + (8 * 0.99f)));
        Blocos blocos = objetoBarril.GetComponent<Blocos>();
        listaBlocos.Add(blocos);

        for (int i = 0; i <= 7; i++)
        {
            listaBlocos[i].transform.position = new Vector2(
                listaBlocos[i].transform.position.x,
                listaBlocos[i].transform.position.y - 0.99f
                );
        }
    }

    void ConfereJogada()
    {
        if (listaBlocos[0].gameObject.CompareTag("barril"))
        {
            if ((listaBlocos[0].name == "barrilesq1(Clone)" && !ladoPersonagem) ||
                (listaBlocos[0].name == "barrildir1(Clone)" && ladoPersonagem))
            {
                if(numberLife <= 1)
                {
                    textLife.text = "0";
                    FimDeJogo();
                }
                else
                {
                    numberLife--;
                    Debug.LogError("VIDA :: " + numberLife);
                    textLife.text = numberLife.ToString();
                }
                
            }
            else
            {
                MarcaPonto();
            }
        }
        else
        {
            MarcaPonto();
        }
    }

    void MarcaPonto()
    {
        score++;
        if(score == 50) moreLife = true;
        if (score == 100) moreLife = true;
        if (score == 150) moreLife = true;
        if (score == 200) moreLife = true;
        if (score == 250) moreLife = true;
        if (score == 300) moreLife = true;
        if (score == 350) moreLife = true;
        if (score == 400) moreLife = true;
        if (score == 450) moreLife = true;
        if (score == 500) moreLife = true;
        if (score == 600) moreLife = true;
        if (score == 700) moreLife = true;

        NewLife();
        pontuacao.text = score.ToString();
        //pontuacao.fontSize = 15;
        //pontuacao.color = new Color(0f, 0f, 0f, 255f);
        gameEngine.Aumenta();
    }

    public void FimDeJogo()
    {
        acabou = true;
        felpudoBate.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);
        felpudoIdle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);

        jogadorFelpudoR.isKinematic = false;
        

        if (ladoPersonagem)
        {
            jogadorFelpudoR.AddTorque(300.0f);
            jogadorFelpudoR.velocity = new Vector2(5.0f, 3.0f);
        }
        else
        {
            jogadorFelpudoR.AddTorque(-300.0f);
            jogadorFelpudoR.velocity = new Vector2(-5.0f, 3.0f);
        }

        AudioManager.instance.StopSound();
        AudioManager.instance.PlayAudio(2);

        Invoke(nameof(RecarregaCena), 2);
    }

    void RecarregaCena()
    {
        SceneManager.LoadScene("2 - Game1");
    }

    void NewLife()
    {
        if (score >= 50 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if(score >= 100 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 150 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 200 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 300 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 350 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 400 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 450 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 500 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 600 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
        else if (score >= 700 && moreLife == true)
        {
            numberLife += 1;
            textLife.text = numberLife.ToString();
            moreLife = false;
        }
    }

}
