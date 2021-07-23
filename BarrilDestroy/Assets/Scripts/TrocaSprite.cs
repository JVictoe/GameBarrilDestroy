//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TrocaSprite : MonoBehaviour
//{

//    [SerializeField] private SpriteRenderer[] _newSprite = default;
//    [SerializeField] private Principal _principal;

//    // Start is called before the first frame update
//    void Start()
//    {
//        _newSprite = _newSprite[0].sprite;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(_principal.score >= 10)
//        {
//            _newSprite[0].sprite = _newSprite[1].sprite; 
//        }
//        else if (_principal.score >= 20)
//        {
//            _newSprite[0].sprite = _newSprite[0].sprite;
//        }
//    }
//}
