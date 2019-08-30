using UnityEngine;

public class ActionListWrapper : MonoBehaviour
{
    private Animator _animator;
    public ActionList actionList;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public void Show()
    {
        _animator.SetTrigger("Show");
    }

    public void Hide()
    {
        _animator.SetTrigger("Hide");
    }

    public void ShowActions()
    {
        _animator.SetTrigger("Actions");
    }

    public void Return()
    {
        _animator.SetTrigger("Return");
    }
}