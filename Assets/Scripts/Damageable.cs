using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private int hp = 0;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if(hp<=0)
            {
                NotifyDeath();
            }
        }
    }

    public int MAX_HP = 1;

    public UnityEvent OnDeath;
    public UnityEvent OnDamaged;

    private void Awake()
    {
        Hp = MAX_HP;
    }
    private void NotifyDeath()
    {
        OnDeath?.Invoke();
    }

    public void Damage(int dmg)
    {
        if (CanBeDamaged())
        {
            this.Hp -= dmg;
            OnDamaged?.Invoke();
        }
    }

    public bool CanBeDamaged()
    {
        return this.Hp > 0;
    }

    [ContextMenu("Test_Damage_1DMG")]
    public void TEST_DoDamage_1DMG()
    {
        Damage(1);
    }

}
