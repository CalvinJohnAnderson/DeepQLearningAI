using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RewardCalculater
{
    private static float learnRate = 0.1f;
    private static float discountRate = 0.9f;
    
    //calculates the new q value of the state and action
    public static float CalculateValue(float currentQValue, STATE rewardState, List<STATE> predictedRewards)//reward = number of own tiles it has been placed next to
    {
        List<float> futureRewards = new List<float>();
        float reward = 1;//play around with initial reward for placing a lone tile
        switch (rewardState)
        {
            case STATE.DOUBLE:
                reward = 2;
                break;
            case STATE.TRIPLE:
                reward = 3;
                break;
            case STATE.WIN:
                reward = 100;
                //return 10f;//returns 10 if game has been won
                break;
        }

        foreach (var state in predictedRewards)
        {
            switch (state)
            {
                case STATE.ZERO:
                    futureRewards.Add(0);
                    break;
                case STATE.SINGLE:
                    futureRewards.Add(1);
                    break;
                case STATE.DOUBLE:
                    futureRewards.Add(2);
                    break;
                case STATE.TRIPLE:
                    futureRewards.Add(3);
                    break;
                case STATE.WIN:
                    futureRewards.Add(100);
                    break;
            }
        }
        
        float deltaQ = reward + discountRate * futureRewards.Max() - currentQValue;
        float qValue = currentQValue + learnRate * deltaQ;

        return qValue;
    }
}
