using UnityEngine;
using UnityEngine.UI;

public class RedPointPrefabData : MonoBehaviour
{
    [SerializeField]
    private GameObject _numberNode = null;

    [SerializeField]
    private GameObject _normalNode = null;

    [SerializeField]
    private Text _text = null;

    public void SwitchNumberShow(bool isNumber)
    {
        if(_numberNode != null)
            _numberNode.SetActive(isNumber);
        if(_normalNode != null)
            _normalNode.SetActive(!isNumber);
    }

    public void SetNumber(int number)
    {
        var limitNum = CSRedPointManager.Instance.LimitNum;
        var limitShowStr = CSRedPointManager.Instance.LimitShowStr;
        if (_text != null)
            _text.text = number > limitNum ? limitShowStr : number.ToString();
    }
}
