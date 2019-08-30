using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    public UnityEvent AtEndOfDialog;
    public AudioSource audioSource;
    public List<string> Dialogs;
    public List<AudioClip> DialogsAudio;
    public GameManager gameManager;
    private int id;
    public bool LoadFromJson;
    public string Local;
    public string Name;
    private Coroutine reveal;
    private bool revealing;
    public float speed = 0.2f;
    public TextMeshProUGUI textMesh;



    private void Awake()
    {
        if (LoadFromJson && GameManager.GameData.personagens != null)
            Dialogs = GameManager.GameData.personagens.Find(x => x.nome == Name).dialogos
                .Find(x => x.local == Local).frases;
    }

    private void OnEnable()
    {
        id = 0;

        ShowNextDialog();
    }

    private void Update()
    {
        // Press ENTER or SPACE to show next sentence
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            ShowNextDialog();

        // Press Q to repeat last sentence
        if (Input.GetKeyDown(KeyCode.Q))
            if (!revealing)
            {
                id--;
                ShowNextDialog();
            }
    }

    public void ShowNextDialog()
    {
        if (reveal != null)
            StopCoroutine(reveal);

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
            reveal = StartCoroutine(revealPhrase());
        }
        else
        {
            AtEndOfDialog.Invoke();
        }
    }

    private IEnumerator revealPhrase()
    {
        revealing = true;
        if (DialogsAudio.Count >= id + 1)
        {
            audioSource.clip = DialogsAudio[id];
            audioSource.Play();
        }

        var phrase = Dialogs[id];
        textMesh.maxVisibleCharacters = 0;
        textMesh.SetText(phrase);
        var size = phrase.Length;
        while (textMesh.maxVisibleCharacters < size)
        {
            textMesh.maxVisibleCharacters++;
            yield return new WaitForSeconds(speed);
        }

        id++;
        revealing = false;
    }
}