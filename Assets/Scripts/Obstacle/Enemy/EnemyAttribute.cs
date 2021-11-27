using System.Collections;
using System.Collections.Generic;
public struct EnemyAttribute
{
    public int ID { get; private set; }
    public string name { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Defence { get; private set; }
    public int Gold { get; private set; }
    public int Experience { get; private set; }
    public List<EnemySkill> Skill { get; private set; }

    public EnemyAttribute(int id, string n, int h, int a, int d, int g, int e)
    {
        ID = id;
        name = n;
        Health = h;
        Attack = a;
        Defence = d;
        Gold = g;
        Experience = e;
        Skill = new List<EnemySkill>();
    }

    public void setSkill(List<EnemySkill> s)
    {
        Skill = s;
    }
}
