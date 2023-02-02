using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class FloodgatesConditionPipe : FloodgatesPipe
{
    [SerializeField]
    [OnValueChanged(nameof(UpdateConditions))]
    [ListDrawerSettings(ListElementLabelName = nameof(ConditionPipe.conditionName))]
    protected List<ConditionPipe> conditions = new List<ConditionPipe>();

    public override bool IsOpen()
    {
        if (conditions.Count == 0)
            return base.IsOpen();

        int conditionMetCounter = 0;
        int counter = 0;

        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i] == null) continue;
            if (conditions[i].floodgate.IsOpen())
            {
                if (debug)
                    Debug.Log(conditions[i].floodgate.name);
                conditionMetCounter++;
            }

            counter++;

            if (conditions[i].comparison == ConditionPipe.Operator.Or)
            {
                bool previousConditionsMet = conditionMetCounter == counter;

                if (previousConditionsMet)
                    return true;
                else
                    conditionMetCounter = counter = 0;
            }

        }
        return conditionMetCounter == counter;
    }

    private void OnValidate()
    {
        UpdateConditions();
    }

    private void UpdateConditions()
    {
        if (conditions.Count == 0) return;

        for (int i = 0; i < conditions.Count; i++)
        {
            conditions[i].showOperator = conditions.Count >= i + 2;
        }
    }

    public override void AddAllFloodgates()
    {
        base.AddAllFloodgates();
        var childs = GetComponentsInChildren<Floodgate>();

        foreach (var c in childs)
            if (!_floodgates.Contains(c))
                _floodgates.Add(c);
    }
}

[System.Serializable]
public class ConditionPipe
{
    public string conditionName => (floodgate ? floodgate.floodgateName : "")
    + (showOperator ? $" \t{comparison}" : "");

    public enum Operator { And, Or }
    public Floodgate floodgate;
    [ShowIf(nameof(showOperator))]
    public Operator comparison;
    public bool showOperator { get; set; }
}
