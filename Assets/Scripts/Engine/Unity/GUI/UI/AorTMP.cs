﻿using System;
using TMPro;
using UnityEngine;

public class AorTMP : TextMeshProUGUI
{
    public static Action<string, AorTMP> OnAwake = null;

    public string languageKey;

    private bool m_bHasReplace = false;

    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif
        if (!string.IsNullOrEmpty(languageKey))
        {
            if (!m_bHasReplace)
            {
                if (OnAwake != null)
                {
                    OnAwake(languageKey, this);
                    if (Application.isPlaying)
                    {
                        m_bHasReplace = true;
                    }
                    return;
                }
            }
        }
    }
}