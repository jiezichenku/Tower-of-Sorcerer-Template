using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class Enemy : MonoBehaviour
{
    public int enemyID;
    private Dictionary<EnemyAttribute, int> enemyAttributes = new Dictionary<EnemyAttribute, int>();
    public Dictionary<EnemyAttribute, int> getAttributes()
    {
        return enemyAttributes;
    }
    private List<EnemySkill> enemySkills = new List<EnemySkill>();
    public List<EnemySkill> getSkills()
    {
        return enemySkills;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadEnemyData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void battle()
    {
        BraverStatus braverStatus = BraverStatus.GetInstance();
        braverStatus.getAttributes().TryGetValue(BraverAttribute.Health, out int braverHealth);
        int damage = EnemyProperty.GetInstance().DamageCalculate(this);
        if (braverHealth > damage && damage > 0)
        {
            braverStatus.UpdateStatus(BraverAttribute.Health, -1 * damage);
            
            Destroy(this.gameObject);
        }
    }

    private void LoadEnemyData()
    {
        JObject JsonData = Model.GetEnemyData(enemyID);
        foreach (EnemyAttribute a in Enum.GetValues(typeof(EnemyAttribute)))
        {
            string strAttribute = a.ToString();
            if (strAttribute != "Skill")
            {
                enemyAttributes.Add(a, (int)JsonData.GetValue(strAttribute));
            }
            else
            {
                JArray skillListArray = (JArray)JsonData.GetValue(strAttribute);
                List<string> skillList = skillListArray.ToObject<List<string>>();
                if (skillList.Count > 0)
                {
                    foreach (string strSkill in skillList)
                    {
                        enemySkills.Add((EnemySkill)Enum.Parse(typeof(EnemySkill), strSkill));
                    }
                }
            }
        }
    }
}
