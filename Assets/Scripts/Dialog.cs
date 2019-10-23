using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Dialog : MonoBehaviour
{
    public UnityEvent AtEndOfDialog;
        public UnityEvent AtEndOfClosingAnimation;

    public AudioSource audioSource;
    [FormerlySerializedAs("Dialogs")] public List<string> Phrases;
    public List<AudioClip> DialogsAudio;
    private int id;
    public bool LoadFromJson;
    public string Local;
    public string Name;
    private Coroutine reveal;
    private bool revealing;
    public float speed = 0.2f;
    public TextMeshProUGUI textMesh;

    public void EndOfClosingAnimation() {
        AtEndOfClosingAnimation.Invoke();
    }
    public void LoadDialog()
    {
        if (!LoadFromJson || GameManager.GameData == null || GameManager.GameData.Personagens == null) return;
        var npc = GameManager.GameData.Personagens.Find(x => x.nome == Name);
        if (npc == null)
            return;
        Phrases = npc.dialogos.Find(x => x.local == Local).frases;
        if(Phrases == null)
            return;
        GetComponent<Animator>().SetTrigger("Show");
        id = 0;
        ShowNextDialog();
    }

    public void OnDataLoaded(object sender, EventArgs e)
    {
        LoadDialog();
    }

    public void Awake()
    {
        if (GameManager.GameData == null || !GameManager.GameData.Loaded) 
            GameData.GameDataLoaded += OnDataLoaded;
        else {
            LoadDialog();
        }
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
            textMesh.maxVisibleCharacters = Phrases[id].Length;
            id++;
            revealing = false;
            return;
        }

        if (Phrases.Count > id)
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

        var phrase = Phrases[id];
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

    void OnDestroy()
    {
        GameData.GameDataLoaded -= OnDataLoaded;
    }
}