using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyGauge : MonoBehaviour
{
    [SerializeField]
    private Image yellowGauge;
    [SerializeField]
    private Image redGauge;

    public GameObject Enemy;

    private EnemyManager enemy;
    private Tween redGaugeTween;

    public void GaugeReduction(float reducationValue, float time =  0.5f)
    {
        float valueFrom = (float)enemy.life / (float)enemy.maxLife;
        float valueTo = ((float)enemy.life - reducationValue) / enemy.maxLife;

        //Debug.Log("GaugeReduction");
        //Debug.Log(valueFrom);
        //Debug.Log(valueTo);

        // 黄色ゲージ減少
        yellowGauge.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        // 赤ゲージ減少
        redGaugeTween = DOTween.To(
            () => valueFrom,
            x => {
                redGauge.fillAmount = x;
            },
            valueTo,
            time
        );
    }

    public void SetPlayer(EnemyManager enemy)
    {
        this.enemy = enemy;
    }
}
