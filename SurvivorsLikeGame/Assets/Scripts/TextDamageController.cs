using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class TextDamageController : MonoBehaviour
{
    float destroyTime = 1;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector2(1,1), destroyTime/2)
            .SetRelative()
            .OnComplete(()=>
            {
                transform.DOScale(new Vector2(0,0), destroyTime/2)
                .OnComplete(()=> Destroy(gameObject));
            });
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;

        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);
        transform.position = pos;
    }

    public void Init(GameObject target, float damage)
    {
        this.target = target;
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();

        text.text = "" + (int)damage;

        if(target.GetComponent<PlayerController>())
        {
            text.color = Color.red;
        }
    }
}
