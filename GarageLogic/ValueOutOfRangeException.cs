using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, Exception i_InnerExeption)
            : base(string.Format("the value you entered is out of range, either its bigger than {0} or smaller than {1}", i_MaxValue.ToString(), i_MinValue.ToString()), i_InnerExeption)
        {
            this.m_MaxValue = i_MaxValue;
            this.m_MinValue = i_MinValue;
        }

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base(string.Format("the value you entered is out of range, either its bigger than {0} or smaller than {1}", i_MaxValue.ToString(), i_MinValue.ToString()))
        {
            this.m_MaxValue = i_MaxValue;
            this.m_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get { return m_MaxValue; }
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }
    }
}