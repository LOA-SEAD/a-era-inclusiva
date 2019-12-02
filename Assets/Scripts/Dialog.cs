using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    
    private UIMaster uiMaster;
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
    public Image CharacterImage;
    public ClassPersonagem npc;
    public TextMeshProUGUI CharacterName;

    public void EndOfClosingAnimation()
    {
        AtEndOfClosingAnimation.Invoke();
    }

    public void LoadDialog()
    {
        Debug.Log(Name);

        if (!LoadFromJson && Phrases!=null)
        {
            Debug.Log(Name);
            npc = GameManager.GameData.Personagens.Find(x => x.nome == Name);
            if (npc == null)
                return;
            npc.LoadExpressions();
            GetComponent<Animator>().SetTrigger("Show");
            ShowNextDialog();
            return;
        }
        if (!LoadFromJson || GameManager.GameData == null || GameManager.GameData.Personagens == null) return;
        npc = GameManager.GameData.Personagens.Find(x => x.nome == Name);
        if (npc == null)
            return;

        npc.LoadExpressions();
        GetComponent<Animator>().SetTrigger("Show");

        var dialogs = npc.dialogos.FindAll(x => x.local == Local);
        Phrases = dialogs.FirstOrDefault(x => x.introducao && !GameManager.PlayerData.Dialogs.Contains(Name + Local))
            ?.frases;
        if (Phrases != null)
        {
            GameManager.PlayerData.Dialogs.Add(Name + Local);
            GameManager.Save();
        }
        else
        {
            Phrases = dialogs.FirstOrDefault(x => !x.introducao)?.frases;
        }
        

        ShowNextDialog();
    }

    private void OnEnable()
    {
        uiMaster.Enable();
        
    }

    private void OnDisable()
    {
        uiMaster.Disable();
    }

    public void OnDataLoaded(object sender, EventArgs e)
    {

        LoadDialog();
    }

    public void Awake()
    {


        id = 0;
        if (GameManager.GameData == null || !GameManager.GameData.Loaded)
            GameData.GameDataLoaded += OnDataLoaded;
        else
        {
            LoadDialog();
        }
        uiMaster = new UIMaster();
        uiMaster.UI.Repeat.performed += ctx => RepeatDialog();

    }
    


    private void Update()
    {
        
    }

    public void RepeatDialog()
    {
        id--;
        ShowNextDialog();
    }

    public void ShowNextDialog()
    {
        if (id + 1 < npc.expressoes.Count)
        {
            CharacterImage.sprite = npc.images[npc.expressoes[id]];
        }
        else if( npc.images.ContainsKey(ClassFala.padrao))
            CharacterImage.sprite = npc.images[ClassFala.padrao];

        if (reveal != null)
            StopCoroutine(reveal);

        if (revealing)
        {
            textMesh.maxVisibleCharacters = Phrases[id].Length;
            id++;
            revealing = false;
            return;
        }

        if (Phrases!=null && Phrases.Count > id)
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

    private void Start()
    {
        
        if (CharacterName != null)
            CharacterName.SetText(Name);

    }



 

   
}