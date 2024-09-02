using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

[RequireComponent(typeof(AudioSource))]

public class EfeitoDigitador : MonoBehaviour
{
    private TextMeshProUGUI componenteTexto;
    private AudioSource _audioSorce;
    private string mensagemOriginal;

    public bool imprmindo;
    public float tempoEntreLetras = 0.08f;

    private void Awake()
    {
        TryGetComponent(out componenteTexto);
        TryGetComponent(out _audioSorce);
        mensagemOriginal = componenteTexto.text;
        componenteTexto.text = "";
    }

    private void OnEnable()
    {
        ImprimirMensagem(mensagemOriginal);
    }

    private void OnDisable()
    {
        componenteTexto.text = mensagemOriginal;
    }

    public ImprimirMensagem(string mensagem)
    {
        if(gameObject.activeInHierarchy)
        {
            if(imprmindo) return;
            imprmindo = true;
            StartCoroutine(mensagem);
        }
    }

    IEnumerator LetraPorLetra(string mensagem)
    {
        string msg = "";
        foreach (var letra in mensagem)
        {
            msg += letra;
            componenteTexto.text = msg;
            _audioSorce.Play();
            yield return new WaitForSeconds(tempoEntreLetras);
        
        }

        imprmindo = false;
        StopAllCoroutines();
    }
}