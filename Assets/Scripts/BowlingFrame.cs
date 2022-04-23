using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [Serializable]
    public struct BowlingFrame
    {
        int frameId;
        [SerializeField]
        int scoreOne;
        [SerializeField]
        int scoreTwo;
        [SerializeField]
        int totalScore;
        bool frameComplete;

        public BowlingFrame(int number)
        {
            this.frameId = number;
            this.scoreOne = -1;
            this.scoreTwo = -1;
            this.totalScore = -1;
            frameComplete = false;
        }

        public int getFrameNumber()
        {
            return this.frameId;
        }

        public bool isFrameComplete()
        {
            return frameComplete;
        }

        public void updateScore(int pinsHit)
        {
            if (this.scoreOne == -1)
            {
                scoreOne = pinsHit;
                totalScore = pinsHit;
            }
            else
            {
                scoreTwo = pinsHit;
                totalScore = scoreOne + scoreTwo;
                frameComplete = true;
            }
        }

        public int getScoreOne()
        {
            return scoreOne;
        }

        public int getScoreTwo()
        {
            return scoreTwo;
        }

        public int getTotalScore()
        {
            return totalScore;
        }
    }


