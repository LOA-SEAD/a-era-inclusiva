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

    [FormerlySerializedAs("Dialogs")]
    public List<string> Phrases;

    // Mantém suporte ao modo antigo pelo Inspector
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

    private ClassFala falaAtual;

    public void EndOfClosingAnimation()
    {
        AtEndOfClosingAnimation.Invoke();
    }

    public void LoadDialog()
    {
        id = 0;

        string nomeLimpo = Name != null ? Name.Trim() : "";
        string localLimpo = Local != null ? Local.Trim() : "";
        string chaveDialogo = nomeLimpo + localLimpo;

        Debug.Log("========== CARREGANDO DIÁLOGO ==========");
        Debug.Log("Name no Inspector: [" + Name + "]");
        Debug.Log("Local no Inspector: [" + Local + "]");
        Debug.Log("Chave limpa: [" + chaveDialogo + "]");

        if (GameManager.GameData == null || GameManager.GameData.Personagens == null)
        {
            Debug.LogWarning("GameData ou lista de personagens está nula.");
            return;
        }

        npc = GameManager.GameData.Personagens.Find(x => x.nome != null && x.nome.Trim() == nomeLimpo);

        if (npc == null)
        {
            Debug.LogWarning("Personagem não encontrado: [" + nomeLimpo + "]");
            return;
        }

        npc.LoadExpressions();

        GetComponent<Animator>().SetTrigger("Show");

        if (!LoadFromJson && Phrases != null)
        {
            falaAtual = null;
            ShowNextDialog();
            return;
        }

        if (!LoadFromJson)
        {
            Debug.LogWarning("LoadFromJson está desmarcado no Inspector.");
            return;
        }

        if (GameManager.PlayerData == null)
        {
            Debug.LogWarning("PlayerData está nulo.");
            return;
        }

        if (GameManager.PlayerData.Dialogs == null)
        {
            GameManager.PlayerData.Dialogs = new List<string>();
        }

        if (npc.dialogos == null)
        {
            Debug.LogWarning("O personagem [" + nomeLimpo + "] não tem lista de diálogos.");
            return;
        }

        var dialogs = npc.dialogos
            .Where(x => x.local != null && x.local.Trim() == localLimpo)
            .ToList();

        Debug.Log("Total de diálogos encontrados para " + nomeLimpo + " / " + localLimpo + ": " + dialogs.Count);
        Debug.Log("Diálogos já salvos antes: " + string.Join(", ", GameManager.PlayerData.Dialogs));

        foreach (var dialogo in dialogs)
        {
            Debug.Log(
                "Diálogo encontrado | local: [" + dialogo.local + "]" +
                " | introducao: " + dialogo.introducao +
                " | frases: " + (dialogo.frases != null ? dialogo.frases.Count.ToString() : "null")
            );
        }

        falaAtual = dialogs.FirstOrDefault(x =>
            x.introducao && !GameManager.PlayerData.Dialogs.Contains(chaveDialogo)
        );

        if (falaAtual != null)
        {
            Debug.Log("Entrou no diálogo de INTRODUÇÃO. Salvando chave: [" + chaveDialogo + "]");

            Phrases = falaAtual.frases;

            if (!GameManager.PlayerData.Dialogs.Contains(chaveDialogo))
            {
                GameManager.PlayerData.Dialogs.Add(chaveDialogo);
            }

            GameManager.Save();

            Debug.Log("Diálogos salvos depois do Save: " + string.Join(", ", GameManager.PlayerData.Dialogs));
        }
        else
        {
            Debug.Log("Introdução já vista ou não encontrada. Procurando diálogo comum.");

            falaAtual = dialogs.FirstOrDefault(x => !x.introducao);

            if (falaAtual != null)
            {
                Debug.Log("Entrou no diálogo comum de: " + nomeLimpo + " / " + localLimpo);
                Phrases = falaAtual.frases;
            }
            else
            {
                Debug.LogWarning("Nenhum diálogo comum encontrado para: " + nomeLimpo + " / " + localLimpo);
            }
        }

        ShowNextDialog();
    }

    private void OnEnable()
    {
        if (uiMaster != null)
        {
            uiMaster.Enable();
        }
    }

    private void OnDisable()
    {
        if (uiMaster != null)
        {
            uiMaster.Disable();
        }
    }

    public void OnDataLoaded(object sender, EventArgs e)
    {
        LoadDialog();
    }

    public void Awake()
    {
        id = 0;

        uiMaster = new UIMaster();
        uiMaster.UI.Repeat.performed += ctx => RepeatDialog();

        if (GameManager.GameData == null || !GameManager.GameData.Loaded)
        {
            GameData.GameDataLoaded += OnDataLoaded;
        }
        else
        {
            LoadDialog();
        }
    }

    public void RepeatDialog()
    {
        id = Mathf.Max(id - 1, 0);
        ShowNextDialog();
    }

    public void ShowNextDialog()
    {
        if (Phrases == null || Phrases.Count == 0)
        {
            Debug.LogWarning("Lista de frases vazia ou nula.");
            AtEndOfDialog.Invoke();
            return;
        }

        if (reveal != null)
        {
            StopCoroutine(reveal);
        }

        if (revealing)
        {
            if (id >= 0 && id < Phrases.Count)
            {
                textMesh.maxVisibleCharacters = Phrases[id].Length;
                id++;
            }

            revealing = false;
            return;
        }

        if (id < Phrases.Count)
        {
            AtualizarExpressao();

            if (audioSource != null)
            {
                audioSource.Stop();
            }

            if (VoiceManager.Instance != null)
            {
                VoiceManager.Instance.StopVoice();
            }

            reveal = StartCoroutine(RevelarFrase());
        }
        else
        {
            if (VoiceManager.Instance != null)
            {
                VoiceManager.Instance.StopVoice();
            }

            AtEndOfDialog.Invoke();
        }
    }

    private void AtualizarExpressao()
    {
        string expressao = ClassFala.padrao;

        if (falaAtual != null)
        {
            expressao = falaAtual.GetExpressao(id);
        }
        else if (npc != null && npc.expressoes != null && id < npc.expressoes.Count)
        {
            expressao = npc.expressoes[id];
        }

        if (npc != null && npc.images != null && npc.images.ContainsKey(expressao))
        {
            CharacterImage.sprite = npc.images[expressao];
        }
        else if (npc != null && npc.images != null && npc.images.ContainsKey(ClassFala.padrao))
        {
            CharacterImage.sprite = npc.images[ClassFala.padrao];
        }
    }

    private IEnumerator RevelarFrase()
    {
        revealing = true;

        TocarAudioDaFrase();

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

    private void TocarAudioDaFrase()
    {
        if (LoadFromJson && falaAtual != null)
        {
            string audioSrc = falaAtual.GetAudio(id);

            if (!string.IsNullOrEmpty(audioSrc) && VoiceManager.Instance != null)
            {
                VoiceManager.Instance.PlayVoice(audioSrc);
            }

            return;
        }

        if (DialogsAudio != null && DialogsAudio.Count >= id + 1 && audioSource != null)
        {
            audioSource.clip = DialogsAudio[id];
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        GameData.GameDataLoaded -= OnDataLoaded;

        if (VoiceManager.Instance != null)
        {
            VoiceManager.Instance.StopVoice();
        }
    }

    private void Start()
    {
        if (CharacterName != null)
        {
            CharacterName.SetText(Name);
        }
    }
}