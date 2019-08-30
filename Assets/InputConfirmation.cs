using TMPro;

public class InputConfirmation : Confirmation
{
    public TMP_InputField InputField;

    private void Awake()
    {
        InputField.ActivateInputField();
        InputField.Select();
    }
}