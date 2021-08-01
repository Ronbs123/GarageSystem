using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        protected const byte k_NumOfWheels = 4;
        protected const float k_MaxAirPressure = 32;
        protected const int k_NumberOfSpecialfeatures = 2;
        private CarColors m_Color;
        private NumOfCarDoors m_NumOfDoors;
        
        public Car(string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_LicensePlate, NumOfCarDoors i_NumberOfDoors, CarColors i_CarColor)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.m_ModelName = i_ModelName;
            this.m_LicensePlate = i_LicensePlate;
            this.m_NumOfDoors = i_NumberOfDoors;
            this.m_Color = i_CarColor;
            this.m_Wheels = InitiateWheels(k_NumOfWheels, k_MaxAirPressure);
        }

        public CarColors CarColor
        {
            get { return this.m_Color; }
        }

        public NumOfCarDoors NumOfDoors
        {
            get { return this.m_NumOfDoors; }
        }

        public override float MaxAirPressure
        {
            get { return k_MaxAirPressure; }
        }

        public override Tuple<string, string[]>[] SpecialFeatursAndPossibleValues()
        {
            Tuple<string, string[]>[] namesAndPossibleValues = new Tuple<string, string[]>[k_NumberOfSpecialfeatures];
            namesAndPossibleValues[0] = new Tuple<string, string[]>("Color", Enum.GetNames(typeof(CarColors)));
            namesAndPossibleValues[1] = new Tuple<string, string[]>("Number of doors", Enum.GetNames(typeof(NumOfCarDoors)));

            return namesAndPossibleValues;
        }

        public override object ParseSpecialFeature(string i_SpecialFeature, string i_FeatureKey)
        {
            object validSpecialFeature = null;
            if (i_FeatureKey.Equals("Color"))
            {
                if (Enum.TryParse(i_SpecialFeature, true, out CarColors color))
                {
                    validSpecialFeature = color;
                }
                else
                {
                    throw new FormatException("only the colors red, white, black and silver are valid");
                }
            }
            else if (i_FeatureKey.Equals("Number of doors"))
            {
                if (Enum.TryParse(i_SpecialFeature, true, out NumOfCarDoors numOfDoors))
                {
                    validSpecialFeature = numOfDoors;
                }
                else
                {
                    throw new FormatException("only cars with 2,3,4 or 5 doors");
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
            m_Color = (CarColors)i_ValidSpecialFeatures[0];
            m_NumOfDoors = (NumOfCarDoors)i_ValidSpecialFeatures[1];
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
