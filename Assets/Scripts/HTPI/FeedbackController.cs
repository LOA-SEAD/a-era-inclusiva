using System;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    public HTPIController htpiController;
    public SceneController sceneController;
    public Dictionary<ClassDemanda, int> results;

    public Confirmation confirmation;

    public void Setup()
    {
        results = new Dictionary<ClassDemanda, int>();

        foreach (var resolucao in htpiController._resolucoes)
        {
            results[resolucao.Key] = resolucao.Key.EfficiencyOf(resolucao.Value);
        }
    }

    public void ShowConfirmation(object sender, EventArgs eventArgs)
    {
        confirmation.Message.SetText("Deseja finalizar o dia?");

        confirmation.AcceptButton.onClick.RemoveAllListeners();
        confirmation.DenyButton.onClick.RemoveAllListeners();

        confirmation.AcceptButton.onClick.AddListener(() =>
        {
            GameManager.PlayerData.Day++;
            Debug.Log("Dia atualizado para: " + GameManager.PlayerData.Day);

            GameManager.PlayerData.AulaConcluida = false;
            GameManager.PlayerData.SelectedMethodologyIds.Clear();
            GameManager.PlayerData.ClearResolutions();

            GameManager.Save();
            Debug.Log("Dia salvo: " + GameManager.PlayerData.Day);

            sceneController.ChangeTo("Scenes/FinalAula");
        });

        confirmation.DenyButton.onClick.AddListener(() =>
        {
            confirmation.gameObject.SetActive(false);
        });

        confirmation.gameObject.SetActive(true);
    }
}
