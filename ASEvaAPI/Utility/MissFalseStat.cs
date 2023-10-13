using System;

namespace ASEva.Utility
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 漏检/误检结果
    /// </summary>
    public struct MissFalseResult
    {
        public bool missOccurred;
        public bool falseOccurred;
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 漏检/误检分析与统计
    /// </summary>
    public class MissFalseStat
    {
        public MissFalseStat()
        {
            Reset();
        }

        public void Reset()
        {
            missCount = 0;
            falseCount = 0;

            lastTestValue = null;
            lastGroundTruth = null;

            hasTrueTestValue = false;
            hasTrueGroundTruth = false;
        }

        public MissFalseResult Update(bool testValue, bool groundTruth)
        {
            var result = new MissFalseResult();
            result.falseOccurred = result.missOccurred = false;

            // test和GT值上升/下降沿提取
            bool testUp = false, testDown = false;
            bool gtUp = false, gtDown = false;

            if (lastTestValue == null)
            {
                if (testValue == false) lastTestValue = testValue;
            }
            else
            {
                var lastVal = lastTestValue.Value;
                if (lastVal == false && testValue == true) testUp = true;
                if (lastVal == true && testValue == false) testDown = true;
                lastTestValue = testValue;
            }

            if (lastGroundTruth == null)
            {
                if (groundTruth == false) lastGroundTruth = groundTruth;
            }
            else
            {
                var lastVal = lastGroundTruth.Value;
                if (lastVal == false && groundTruth == true) gtUp = true;
                if (lastVal == true && groundTruth == false) gtDown = true;
                lastGroundTruth = groundTruth;
            }

            // 上升沿重置hasXXX
            if (testUp)
            {
                hasTrueGroundTruth = false;
            }

            if (gtUp)
            {
                hasTrueTestValue = false;
            }

            // 判断是否有值
            if (testValue == true)
            {
                hasTrueTestValue = true;
            }

            if (groundTruth == true)
            {
                hasTrueGroundTruth = true;
            }

            // 下降沿计数
            if (testDown)
            {
                if (!hasTrueGroundTruth)
                {
                    falseCount++;
                    result.falseOccurred = true;
                }
            }

            if (gtDown)
            {
                if (!hasTrueTestValue)
                {
                    missCount++;
                    result.missOccurred = true;
                }
            }

            return result;
        }

        public int MissCount
        {
            get { return missCount; }
        }

        public int FalseCount
        {
            get { return falseCount; }
        }

        private int missCount;
        private int falseCount;

        private bool? lastTestValue;
        private bool? lastGroundTruth;

        private bool hasTrueTestValue;
        private bool hasTrueGroundTruth;
    }
}
