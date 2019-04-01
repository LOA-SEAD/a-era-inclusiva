using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    public string Name;
    public string Local;
    public List<string> Dialogs;
    public bool LoadFromJson;
    public List<AudioClip> DialogsAudio;
    public AudioSource audioSource;
    public TextMeshProUGUI textMesh;
    public float speed = 0.2f;
    public UnityEvent AtEndOfDialog;
    private int id = 0;
    private bool revealing;

    void Start()
    {
        if(LoadFromJson && Game.Characters!=null)
            Dialogs = Game.Characters.personagens.Find(x => x.nome == Name).dialogos.Find(x => x.local == Local).frases;
        ShowNextDialog();
    }

    public void ShowNextDialog()
    {
        StopAllCoroutines();

        if (revealing)
        {
            textMesh.maxVisibleCharacters = Dialogs[id].Length;
            id++;
            revealing = false;
            return;
        }
        if (Dialogs.Count > id)
        {
            audioSource.Stop();
            StartCoroutine(revealPhrase());
        }
        else
        {
            AtEndOfDialog.Invoke();
        }
    }

    IEnumerator revealPhrase()
    {
        revealing = true;
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
        id++;
        revealing = false;
    }
}
