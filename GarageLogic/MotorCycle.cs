using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        protected const byte k_NumOfWheels = 2;
        protected const float k_MaxAirPressure = 30;
        protected const int k_NumberOfSpecialfeatures = 2;
        protected float m_CcEngineCapacity;
        protected LicenseType m_LicenseType; 

        public Motorcycle(string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_LicensePlate, float i_CcEngineCapacity, LicenseType i_LicenseType)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.m_ModelName = i_ModelName;
            this.m_LicensePlate = i_LicensePlate;
            this.m_CcEngineCapacity = i_CcEngineCapacity;
            this.m_LicenseType = i_LicenseType;
            this.m_Wheels = InitiateWheels(k_NumOfWheels, k_MaxAirPressure);
        }

        public float CcEngineCapacity
        {
            get { return m_CcEngineCapacity; }
        }

        public override float MaxAirPressure
        {
            get { return k_MaxAirPressure; }
        }

        public override Tuple<string, string[]>[] SpecialFeatursAndPossibleValues()
        {
            Tuple<string, string[]>[] namesAndPossibleValues = new Tuple<string, string[]>[k_NumberOfSpecialfeatures];
            namesAndPossibleValues[0] = new Tuple<string, string[]>("Volume of engine in cc", new[] { "a positive number" });
            namesAndPossibleValues[1] = new Tuple<string, string[]>("Licenes type", Enum.GetNames(typeof(LicenseType)));

            return namesAndPossibleValues;
        }

        public override object ParseSpecialFeature(string i_SpecialFeature, string i_FeatureKey)
        {
            object validSpecialFeature = null;
            if (i_FeatureKey.Equals("Volume of engine in cc"))
            {
                if (float.TryParse(i_SpecialFeature, out float CcEngineCapacity) && CcEngineCapacity > 0)
                {
                    validSpecialFeature = CcEngineCapacity;
                }
                else
                {
                    throw new FormatException("need a valid volume of motor in cc as positive number");
                }
            }
            else if (i_FeatureKey.Equals("Licenes type"))
            {
                if (Enum.TryParse(i_SpecialFeature, true, out LicenseType licenseType))
                {
                    validSpecialFeature = licenseType;
                }
                else
                {
                    throw new FormatException("need a valid license type A, A1, AA or B");
                }
            }
            else
            {
                throw new ArgumentException("Index Out Of Bounds");
            }

            return validSpecialFeature;
        }

        public override void SetParamatersOfSpecialFeatures(params object[] i_ValidSpecialFeatures)
        {
            m_CcEngineCapacity = (float)i_ValidSpecialFeatures[0];
            m_LicenseType = (LicenseType)i_ValidSpecialFeatures[1];
        }

        public override void UpdateWheelsManufactereAndAirPressure(float i_WheelsAirPressure, string i_WheelsManufacture)
        {
            if (i_WheelsAirPressure > k_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(k_MaxAirPressure, k_MaxAirPressure);
            }

            foreach (Wheel wheel in m_Wheels)
            {
                wheel.SetCurrentAirPressuer(i_WheelsAirPressure);
                wheel.Manufacture = i_WheelsManufacture;
            }
        }
    }
}
