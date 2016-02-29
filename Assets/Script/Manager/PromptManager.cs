using UnityEngine;
using System.Collections;

public class PromptManager : ISingleton<PromptManager>
{
    /// <summary>
    /// 显示浮窗
    /// </summary>
    /// <param name="prompt"></param>
    public void ShowPrompt(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            zzLogger.LogError("prompt is null");

            return;
        }

        //ViewManager.Instance.CloseUI("PromptUIPanel");

        //ViewManager.Instance.OpenUI("PromptUIPanel");
//         Transform trans = UIManager.Instance.OpenUI("PromptUIPanel");
//         if (trans == null)
//         {
//             return;
//         }
// 
//         PromptUIPanel panel = trans.GetComponent<PromptUIPanel>();
//         panel.ShowPrompt(prompt);
    }
}