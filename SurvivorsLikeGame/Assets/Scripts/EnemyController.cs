using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public CharacterStats Stats;

    [SerializeField] GameSceneDirector sceneDirector;
    Rigidbody2D rigidbody2d;

    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;

    Vector2 forward;

    enum State
    {
        Alive,
        Dead,
    }
    State state;
    // Start is called before the first frame update
    void Start()
    {
        Init(this.sceneDirector, CharacterSettings.Instance.Get(100));
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();

        moveEnemy();
    }

    public void Init(GameSceneDirector sceneDirector, CharacterStats characterStats)
    {
        this.sceneDirector = sceneDirector;
        this.Stats = characterStats;

        rigidbody2d = GetComponent<Rigidbody2D>();

        float random = Random.Range(0.8f, 1.2f);
        float speed = 1 / Stats.MoveSpeed * random;

        float addx = 0.8f;
        float x = addx * random;

        transform.DOScale(x, speed)
            .SetRelative()
            .SetLoops(-1, LoopType.Yoyo);

        float addz = 10f;
        float z = Random.Range(-addz, addz) * random;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = z;

        transform.eulerAngles = rotation;
        transform.DORotate(new Vector3(0, 0, -z), speed)
            .SetLoops(-1, LoopType.Yoyo);

        PlayerController player = sceneDirector.Player;
        Vector2 dir = player.transform.position - transform.position;
        forward = dir;

        state = State.Alive;
    }

    void moveEnemy()
    {
        if(State.Alive != state) return;

        if(MoveType.TargetPlayer == Stats.MoveType)
        {
            PlayerController player = sceneDirector.Player;
            Vector2 dir = player.transform.position - transform.position;
            forward = dir;
        }

        rigidbody2d.position += forward.normalized * Stats.MoveSpeed * Time.deltaTime;
    }

    void updateTimer()
    {
        if(0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }

        if(0 < Stats.AliveTime)
        {
            Stats.AliveTime -= Time.deltaTime;
            if(0 > Stats.AliveTime) setDead(false);
        }
    }

    void setDead(bool createXP = true)
    {
        if(State.Alive != state) return;

        rigidbody2d.simulated = false;

        transform.DOKill();

        transform.DOScaleY(0, 0.5f).OnComplete(() => Destroy(gameObject));

        if (createXP)
        {
            // TODO:経験値生成
        }

        state = State.Dead;
    }

    private void OnCollisionEnter(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collition)
    {

    }
}
