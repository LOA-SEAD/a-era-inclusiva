using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class AulaInfoController : MonoBehaviour
{
    public TextMeshProUGUI conteudoTituloText;
    public TextMeshProUGUI conteudoTextoText;
    public TextMeshProUGUI atividadesTextoText;
    public TextMeshProUGUI detalhesTextoText;

    private void Start()
    {
        if (GameManager.GameData != null && GameManager.GameData.Loaded)
        {
            MostrarAulaDoDia();
        }
        else
        {
            GameData.GameDataLoaded += OnGameDataLoaded;
        }
    }

    private void OnDestroy()
    {
        GameData.GameDataLoaded -= OnGameDataLoaded;
    }

    private void OnGameDataLoaded(object sender, EventArgs e)
    {
        MostrarAulaDoDia();
    }

    public void MostrarAulaDoDia()
    {
        if (GameManager.GameData == null ||
            GameManager.GameData.Aulas == null ||
            GameManager.PlayerData == null)
        {
            Debug.LogWarning("Não foi possível carregar as informações da aula.");
            return;
        }

        var aula = GameManager.GameData.Aulas
            .FirstOrDefault(x => x.dia == GameManager.PlayerData.Day);

        if (aula == null)
        {
            Debug.LogWarning("Nenhuma aula encontrada para o dia " + GameManager.PlayerData.Day);
            return;
        }

        
        if (conteudoTextoText != null)
        {
            conteudoTextoText.richText = true;
            conteudoTextoText.SetText(aula.disciplina);
        }

        if (atividadesTextoText != null)
        {
            atividadesTextoText.richText = true;
            atividadesTextoText.SetText(aula.atividades);
        }

        if (detalhesTextoText != null)
        {
            detalhesTextoText.richText = true;
            detalhesTextoText.SetText(aula.detalhes);
        }
    }
}