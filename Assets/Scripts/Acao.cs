using UnityEngine;

public class Acao : MonoBehaviour
{
    public ClassAcao acao;

    // Start is called before the first frame update
    public void OnChange(bool selected)
    {
        acao.selected = true;
    }
}