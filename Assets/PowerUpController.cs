using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public ControladorSalaDeAula controladorSalaDeAula;

    public DemandController demandController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Elogiar()
    {
        if (GameManager.PlayerData.Points < 50)
            return;
        GameManager.PlayerData.Points -= 50;
        StartCoroutine("DisableHappinessDecrease");
    }
    public void ChamarEstagiario()
    {
        if (GameManager.PlayerData.Points < 50)
            return;
        GameManager.PlayerData.Points -= 50;
        StartCoroutine("DisableHappinessDecrease");
    }

    public IEnumerator DisableHappinessDecrease()
    {
        controladorSalaDeAula.happinessDecreasePaused = true;
        yield return new WaitForSeconds(30);
        controladorSalaDeAula.happinessDecreasePaused = false;

    }
}