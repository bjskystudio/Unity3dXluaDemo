using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class WorldHudItem : MonoBehaviour
{
    private Transform Target;
    private AorText Text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target!=null && this)
        {
            transform.position = Target.position;
        }
    }

    public void OnLateUpdate()
    {

    }
    public void SetData(Transform tar,string text, float Time = 0)
    {
        if (Text == null) Text = GetComponentInChildren<AorText>();
        if (Text != null) Text.text = text;
        Target = tar;
        if (Time!=0)
        {
            MoveSyncUpdate((int) (Time * 1000));
        }
    }
    private async void MoveSyncUpdate(int time)
    {
        await Task.Delay(time);
        gameObject.SetActive(false);
    }

}
