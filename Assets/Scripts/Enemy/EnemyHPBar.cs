using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{


    private Transform target;
    private Slider slider;
    private RectTransform recTransform;
    private Vector2 offest;

    private void Awake()
    {

        recTransform = GetComponent<RectTransform>();
    }

    public void Init(Transform _target,float value)
    {
        
        target = _target;
        UpdateHP(value);
    }

    private void Update()
    {
        UpdatePos();
    }

    void UpdatePos()
    {
        if(target!=null)
        {
            Vector2 player2DPosition = Camera.main.WorldToScreenPoint(target.position);
            recTransform.position = player2DPosition + offest;

            //血条超出屏幕就不显示  
            if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
            {
                recTransform.gameObject.SetActive(false);
            }
            else
            {
                recTransform.gameObject.SetActive(true);
            }
        }
    }

    public void UpdateHP(float value)
    {
        slider.value = value;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
