using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSalaDeAula : MonoBehaviour
{
    private UIMaster uimaster;
    private DemandToggle _selectedDemand;

    public ActionListWrapper actionListWrapper;
    public BarraInferior barraInferior;
    public SceneController sceneController;
    public SpeechBubble speechBubble;

    public int HappinessFactor = 0;
    public bool happinessDecreasePaused;
    public GameObject avatar;

    public DemandToggle SelectedDemand
    {
        set
        {
            _selectedDemand = value;

            if (_selectedDemand != null)
            {
                string audioDaDemanda = GetAudioDaDemanda(_selectedDemand.Demand);
                Speak(_selectedDemand.Demand.descricao, audioDaDemanda);
            }
        }
        get { return _selectedDemand; }
    }

    private void Start()
    {
        if (GameManager.PlayerData != null)
        {
            Setup(this, EventArgs.Empty);
        }
        else
        {
            SaveManager.DataLoaded += Setup;
        }
    }

    private void Setup(object sender, EventArgs eventArgs)
    {
        InvokeRepeating("CheckIfEnd", 1, 2);
        StartCoroutine("DecreaseHappiness");

        AudioManager.instance.PlaySfx((int)SoundType.BellRing);
        AudioManager.instance.PlayAmbience((int)SoundType.AmbienceClass);
        AudioManager.instance.StopMusic();

        avatar.GetComponent<Image>().sprite = GameManager.GetAvatarImage();
    }

    public IEnumerator DecreaseHappiness()
    {
        while (true)
        {
            while (happinessDecreasePaused)
            {
                yield return null;
            }

            GameManager.PlayerData.Happiness -= HappinessFactor / 3;
            barraInferior.UpdateHappinessIcon();

            yield return new WaitForSeconds(5);
        }
    }

    public void UseAction(ClassAcao action)
    {
        actionListWrapper.Hide();

        if (_selectedDemand == null)
        {
            Speak("Não posso fazer isso sem ter escolhido a demanda!");
            return;
        }

        if (action == null)
        {
            Speak("Ação inválida.");
            return;
        }

        var demand = _selectedDemand.Demand;

        HappinessFactor -= demand.nivelUrgencia;

        Destroy(_selectedDemand.gameObject);

        var e = demand.acoesEficazes.FirstOrDefault(x => x.idAcao == action.id);

        demand.resolvida = true;

        if (e == null)
        {
            GameManager.PlayerData.Happiness -= 10;

            Speak("Acho que isso não funcionou muito bem");
            AudioManager.instance.PlaySfx((int)SoundType.AnswerWrong);
            barraInferior.UpdateHappinessIcon();

            GameManager.Save();

            _selectedDemand = null;
            CheckIfEnd();

            return;
        }

        Debug.Log("antes " + GameManager.PlayerData.Happiness);

        GameManager.PlayerData.Happiness += e.efetividade / 10;
        barraInferior.UpdateHappinessIcon();

        Debug.Log("depois " + GameManager.PlayerData.Happiness);

        AudioManager.instance.PlaySfx((int)SoundType.AnswerRight);
        Speak(e.efetividade);
        barraInferior.IncrementScore(e.efetividade);

        GameManager.Save();

        _selectedDemand = null;
        CheckIfEnd();
    }

    private string GetAudioDaDemanda(ClassDemanda demanda)
    {
        if (demanda == null)
        {
            return "";
        }

        // Avatar 0 = professor masculino
        if (GameManager.PlayerData.SelectedAvatar == 0)
        {
            return demanda.audioProfessor;
        }

        // Qualquer outro valor = professora feminina
        return demanda.audioProfessora;
    }

    private void CheckIfEnd()
    {
        var demandasDoDia = GameManager.GameData.Demandas
            .Where(x => x.dia == GameManager.PlayerData.Day)
            .ToList();

        if (demandasDoDia.Count > 0 && demandasDoDia.All(x => x.resolvida))
        {
            End();
        }
    }

    public void End()
    {
        Debug.Log("AULA FINALIZADA - salvando checkpoint.");

        AudioManager.instance.PlaySfx((int)SoundType.BellRing);

        if (VoiceManager.Instance != null)
        {
            VoiceManager.Instance.StopVoice();
        }

        GameManager.PlayerData.AulaConcluida = true;
        GameManager.Save();

        Debug.Log("AulaConcluida salva como: " + GameManager.PlayerData.AulaConcluida);

        sceneController.ChangeTo("Scenes/HTPI");
    }

    public void Speak(string demandaDescricao, string audioSrc = "")
    {
        speechBubble.SetText(demandaDescricao);
        speechBubble.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(audioSrc) && VoiceManager.Instance != null)
        {
            VoiceManager.Instance.PlayVoice(audioSrc);
        }
    }

    public void Speak(int points)
    {
        speechBubble.ShowResult(points);
        speechBubble.gameObject.SetActive(true);
    }
}