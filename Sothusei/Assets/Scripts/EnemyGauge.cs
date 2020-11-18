using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGauge : MonoBehaviour
{
    [SerializeField]
    private Image yellowGauge;
    [SerializeField]
    private Image redGauge;

    public GameObject Enemy;

    private EnemyManager enemy;
    private Tween redGaugeTween;

    public void GaugeReduction(float reducationValue, float time = 1f)
    {
        var valueFrom = enemy.life / enemy.maxLife;
        var valueTo = (enemy.life - reducationValue) / enemy.maxLife;

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

internal class Tween
{
}