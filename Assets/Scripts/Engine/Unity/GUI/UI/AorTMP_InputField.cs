using System.Collections;
using TMPro;
using UnityEngine;

public class AorTMP_InputField : TMP_InputField
{
    public int UCharacterLimit = 0;
    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (!string.IsNullOrEmpty(m_Text))
        {
            if (characterLimit > 0 && m_Text.GetUWidthLength() > UCharacterLimit)
                m_Text = m_Text.Substring(0, UCharacterLimit);
        }
    }
}
