﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    public List<string> Dialogs;
    public List<AudioClip> DialogsAudio;
    public AudioSource audioSource;
    public TextMeshProUGUI textMesh;
    public float speed = 0.2f;
    public UnityEvent AtEndOfDialog;
    private int id = 0;

    void Start()
    {
        ShowNextDialog();
    }

    public void ShowNextDialog()
    {
        StopAllCoroutines();
        if (Dialogs.Count >= id + 1)
        {
            audioSource.Stop();
            StartCoroutine(revealPhrase());
            id++;
        }
        else
        {
            AtEndOfDialog.Invoke();
        }
    }

    IEnumerator revealPhrase()
    {
        if (DialogsAudio.Count >= id + 1)
        {
            audioSource.clip = DialogsAudio[id];
            audioSource.Play();
        }

        string phrase = Dialogs[id];
        textMesh.maxVisibleCharacters = 0;
        textMesh.SetText(phrase);
        int size = phrase.Length;
        while (textMesh.maxVisibleCharacters < size)
        {
            textMesh.maxVisibleCharacters++;
            yield return new WaitForSeconds(speed);
        }
    }
}
