using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        internal string m_LicensePlate;
        protected float m_EngergyLeftPrecent;
        internal List<Wheel> m_Wheels;
        protected string m_OwnerName;
        protected string m_OwnerPhoneNumber;
        internal VehicleStatus m_Status = VehicleStatus.undertreatment;

        public string ModelName
        {
            get { return this.m_ModelName; }
            set { this.m_ModelName = value; }
        }

        public string LicensePlate
        {
            get { return this.m_LicensePlate; }
            set { this.m_LicensePlate = value; }
        }

        public VehicleStatus Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }

        public string OwnersName
        {
            get { return this.m_OwnerName; }
            set { this.m_OwnerName = value; }
        }

        public string OwnersPhoneNumber
        {
            get { return this.m_OwnerPhoneNumber; }
            set { this.m_OwnerPhoneNumber = value; }
        }

        public abstract float MaxAirPressure
        {
            get;
        }

        public abstract EnergySource EnergySource
        {
            get;
        }

        public List<Wheel> InitiateWheels(byte i_NumOfWheels, float i_MaxAirPressure)
        {
            List<Wheel> carWheels = new List<Wheel>();
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                Wheel wheelToAdd = new Wheel("DefulteManufacture", 0f, i_MaxAirPressure);
                carWheels.Add(wheelToAdd);
            }

            return carWheels;
        }

        public string WheelsListToString()
        {
            byte index = 1;
            StringBuilder wheelsList = new StringBuilder();
            string stringToAppeand;
            foreach (Wheel wheel in this.m_Wheels)
            {
                wheelsList.Append(Environment.NewLine);
                stringToAppeand = string.Format(@"tire {0} : Manufacure -  {1} , Current air pressure -  {2} ", index++, wheel.Manufacture, wheel.CurrentAirPresure);
                wheelsList.Append(stringToAppeand);
            }

            return wheelsList.ToString();
        }

        public abstract Tuple<string, string[]>[] SpecialFeatursAndPossibleValues();

        public abstract object ParseSpecialFeature(string i_SpecialFeature, string i_FeatureKey);

        public abstract void SetParamatersOfSpecialFeatures(params object[] i_SpecialFeatures);

        public abstract void RefillEnergySource(float i_EnergyToAdd, EnergySource i_EnergyType);

        public abstract void UpdateWheelsManufactereAndAirPressure(float i_WheelsAirPressure, string i_WheelsManufacture);

        public abstract void SetEnergyLevel(float i_CurrentFuelLevel);

        public override abstract string ToString();
    }
}
