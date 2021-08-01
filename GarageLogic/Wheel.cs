using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacture;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        internal Wheel(string i_Manufacture, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            this.m_Manufacture = i_Manufacture;
            this.m_CurrentAirPressure = i_CurrentAirPressure;
            this.m_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacture
        {
            get { return m_Manufacture; }
            set { m_Manufacture = value; }
        }

        public float CurrentAirPresure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public void SetCurrentAirPressuer (float i_CurrentAirPressure)
        {
            if (i_CurrentAirPressure < 0 || i_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(m_MaxAirPressure, 0);
            }

            this.m_CurrentAirPressure = i_CurrentAirPressure;
        }
        
        public bool Inflate(float i_AirToAdd)
        {
            bool ans = false;
            if (i_AirToAdd + this.m_CurrentAirPressure <= this.m_MaxAirPressure)
            {
                this.m_CurrentAirPressure += i_AirToAdd;
                ans = true;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaxAirPressure, 0);
            }

            return ans;
        }
    }
}
