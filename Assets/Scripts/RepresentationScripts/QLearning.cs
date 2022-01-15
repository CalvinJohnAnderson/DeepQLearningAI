using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QLearning : MonoBehaviour
{
    private Dictionary<Vector2Int, int> currentGrid;
    private float currentQValue;
    
    private KeyValue[,] KeyValuePairs = new KeyValue[10,10];

    private void UpdateKeyValuePairs(STATE state, Vector2Int action, float qValue)
    {
        KeyValuePairs[action.x, action.y] = new KeyValue(state, action, qValue);
    }

    public void CalculateQValue(STATE state, int x, int y, Dictionary<Vector2Int,int> currentGrid)
    {
        List<STATE> futureRewards = Evaluator.GetFutureRewards(x, y, currentGrid);
        currentQValue = RewardCalculater.CalculateValue(currentQValue, state, futureRewards);
        UpdateKeyValuePairs(state, new Vector2Int(x,y), currentQValue);
    }

    public List<STATE> GetFutureRewards(int x, int y, Dictionary<Vector2Int, int> currentGrid)
    {
        List<STATE> futureRewards = Evaluator.GetFutureRewards(x, y, currentGrid);
        return futureRewards;
    }

    
}
