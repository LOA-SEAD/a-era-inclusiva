using System.Collections.Generic;
using System.Linq;

public class PlayerData
{
    public int Day;
    private int _happiness;
    public int Happiness
    {
        get => _happiness;
        set
        {
            if (value > 100)
            {
                _happiness = 100;
            }
            else if (value < 0)
            {
                _happiness = 0;
            }
            else
            {
                _happiness = value;
            }
        }
    }

    public int Points;
    public List<string> Dialogs;
    public int SelectedAvatar;

    // Guarda as 9 metodologias escolhidas:
    // 3 Diálogos, 3 Recursos e 3 Ações de aula.
    public List<int> SelectedMethodologyIds;

    // Guarda as resoluções do HTPI:
    // índice da demanda do dia + ação escolhida.
    public List<ResolucaoSave> SelectedResolutions;
    public bool AulaConcluida;

    public PlayerData()
    {
        SelectedMethodologyIds = new List<int>();
        SelectedResolutions = new List<ResolucaoSave>();

        Happiness = 100;
        Points = 0;
        Day = 1;
        Dialogs = new List<string>();
        SelectedAvatar = 0;
        AulaConcluida = false;
    }

    public PlayerData(SaveData saveData)
    {
        Points = saveData.Points;
        Day = saveData.Day;
        Happiness = saveData.Happiness;
        Dialogs = saveData.Dialogs ?? new List<string>();
        SelectedAvatar = saveData.SelectedAvatar;
        AulaConcluida = saveData.AulaConcluida;
        SelectedMethodologyIds = saveData.SelectedMethodologyIds ?? new List<int>();
        SelectedResolutions = saveData.SelectedResolutions ?? new List<ResolucaoSave>();
    }

    public void ToggleMethodology(ClassAcao action)
    {
        if (action == null)
        {
            return;
        }

        if (SelectedMethodologyIds.Contains(action.id))
        {
            SelectedMethodologyIds.Remove(action.id);
        }
        else
        {
            SelectedMethodologyIds.Add(action.id);
        }
    }

    public List<ClassAcao> GetSelectedMethodologies()
    {
        if (GameManager.GameData == null || GameManager.GameData.Acoes == null)
        {
            return new List<ClassAcao>();
        }

        return GameManager.GameData.Acoes
            .Where(acao => SelectedMethodologyIds.Contains(acao.id))
            .ToList();
    }

    public List<ClassAcao> GetSelectedMethodologiesByType(string type)
    {
        return GetSelectedMethodologies()
            .Where(acao => acao.tipo == type)
            .ToList();
    }

    public int CountSelectedMethodologiesByType(string type)
    {
        return GetSelectedMethodologiesByType(type).Count;
    }

    public void RemoveMethodologiesByType(string type)
    {
        if (GameManager.GameData == null || GameManager.GameData.Acoes == null)
        {
            return;
        }

        var idsToRemove = GameManager.GameData.Acoes
            .Where(acao => acao.tipo == type)
            .Select(acao => acao.id)
            .ToList();

        SelectedMethodologyIds.RemoveAll(id => idsToRemove.Contains(id));
    }

    public bool HasNineMethodologies()
    {
        return SelectedMethodologyIds.Count == 9;
    }

    public void SetResolution(int demandIndex, int actionId)
    {
        var existingResolution = SelectedResolutions
            .FirstOrDefault(x => x.DemandIndex == demandIndex);

        if (existingResolution != null)
        {
            existingResolution.ActionId = actionId;
        }
        else
        {
            SelectedResolutions.Add(new ResolucaoSave(demandIndex, actionId));
        }
    }

    public void RemoveResolution(int demandIndex)
    {
        SelectedResolutions.RemoveAll(x => x.DemandIndex == demandIndex);
    }

    public void ClearResolutions()
    {
        SelectedResolutions.Clear();
    }
}
