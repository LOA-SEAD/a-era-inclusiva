using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PairVisualTag : MonoBehaviour
{
    public GameObject badgeRoot;
    public TextMeshProUGUI badgeText;

    [Header("Cena onde a badge pode aparecer")]
    public string allowedSceneName = "HTPI";

    private bool CanShowBadge()
    {
        return SceneManager.GetActiveScene().name == allowedSceneName;
    }

    private void Awake()
    {
        ClearPair();

        // Se não estiver na cena HTPI, desliga este script.
        if (!CanShowBadge())
        {
            enabled = false;
        }
    }

    private void OnEnable()
    {
        if (!CanShowBadge())
        {
            ClearPair();
        }
    }

    public void SetPairNumber(int pairNumber)
    {
        if (!CanShowBadge())
        {
            ClearPair();
            return;
        }

        if (badgeRoot != null)
        {
            badgeRoot.SetActive(true);
        }

        if (badgeText != null)
        {
            badgeText.SetText(pairNumber.ToString());
        }
    }

    public void ClearPair()
    {
        if (badgeRoot != null)
        {
            badgeRoot.SetActive(false);
        }

        if (badgeText != null)
        {
            badgeText.SetText("");
        }
    }
}