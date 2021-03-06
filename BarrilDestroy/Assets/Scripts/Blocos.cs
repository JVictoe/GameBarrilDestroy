using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocos : MonoBehaviour
{
    public void AnimaDireita()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(100.0f);
        Invoke(nameof(ApagaBloco), 2.0f);
        AudioManager.instance.PlayAudio(1);
    }

    public void AnimaEsquerda()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(10, 2);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddTorque(-100.0f);
        Invoke(nameof(ApagaBloco), 2.0f);
        AudioManager.instance.PlayAudio(1);
    }

    void ApagaBloco()
    {
        Destroy(this.gameObject);
    }
}
