using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        protected const byte k_NumOfWheels = 16;
        protected const byte k_MaxAirPressure = 28;
        protected const float k_FuelTankCapacity = 120;
        private const int k_NumberOfSpecialfeatures = 2;
        protected readonly EnergySource m_FuelType = EnergySource.Soler;
        protected bool m_IsCarryingToxicMaterials;
        protected float m_CargoVolume;
        protected float m_CurrentFuelLevel;

        public Truck(string i_OwnerName = "DefName", string i_OwnerPhoneNumber = "000000", string i_ModelName = "DefModel", string i_LicensePlate = "DefLicensePLT", float i_FuelLeft = 0, float i_CargoVolume = 0, bool i_IsCarryingToxicMaterials = false)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.m_ModelName = i_ModelName;
            this.m_LicensePlate = i_LicensePlate;
            this.m_EngergyLeftPrecent = (i_FuelLeft / k_FuelTankCapacity) * 100;
            this.m_CargoVolume = i_CargoVolume;
            this.m_IsCarryingToxicMaterials = i_IsCarryingToxicMaterials;
            this.m_CurrentFuelLevel = i_FuelLeft;
            this.m_Wheels = InitiateWheels(k_NumOfWheels, k_MaxAirPressure);
        }

        public bool IsCarryingToxicMaterials
        {
            get { return m_IsCarryingToxicMaterials; }
        }

        public float CurrentFuelLevel
        {
            get { return m_CurrentFuelLevel; }
        }
       
        public float MaxFuelTankCapacity
        {
            get { return k_FuelTankCapacity; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
        }

        public override float MaxAirPressure
        {
            get { return k_MaxAirPressure; }
        }

        public override EnergySource EnergySource
        {
            get { return m_FuelType; }
        }

        public override void RefillEnergySource(float i_EnergyToAdd, EnergySource i_EnergyType)
        {
            if (!this.m_FuelType.Equals(i_EnergyType))
            {
                throw new ArgumentException("wrong type of fuel", i_EnergyType.ToString());
            }

            if (i_EnergyToAdd + m_CurrentFuelLevel > k_FuelTankCapacity)
            {
                i_EnergyToAdd = k_FuelTankCapacity - m_CurrentFuelLevel;
            }

            this.m_CurrentFuelLevel += i_EnergyToAdd;
        }

        public override Tuple<string, string[]>[] SpecialFeatursAndPossibleValues()
        {
            Tuple<string, string[]>[] namesAndPossibleValues = new Tuple<string, string[]>[k_NumberOfSpecialfeatures];
            namesAndPossibleValues[0] = new Tuple<string, string[]>("does contains Toxic Materials", new[] { "true", "false" });
            namesAndPossibleValues[1] = new Tuple<string, string[]>("Cargo Volume", new[] { "A Positive Number" });
            return namesAndPossibleValues;
        }

        public override object ParseSpecialFeature(string i_SpecialFeature, string i_FeatureKey)
        {
            object validSpecialFeature = null;
             if(i_FeatureKey.Equals("does contains Toxic Materials"))
               {
                    if (bool.TryParse(i_SpecialFeature, out bool hasToxicMaterials))
                    {
                        validSpecialFeature = hasToxicMaterials;
                    }
                    else
                    {
                    throw new FormatException("need true or false");
                    }
               }                   
               else if(i_FeatureKey.Equals("Cargo Volume"))
               {
                    if (float.TryParse(i_SpecialFeature, out float volumeOfCargo) && volumeOfCargo >= 0)
                    {
                        validSpecialFeature = volumeOfCargo;
                    }
                    else
                    {
                        throw new FormatException("needs a non-negative Volume of cargo");
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
            m_IsCarryingToxicMaterials = (bool)i_ValidSpecialFeatures[0];
            m_CargoVolume = (float) i_ValidSpecialFeatures[1];
        }

        public override void SetEnergyLevel(float i_CurrentFuelLevel)
        {
            if (i_CurrentFuelLevel > k_FuelTankCapacity || i_CurrentFuelLevel < 0)
            {
                throw new ValueOutOfRangeException(k_FuelTankCapacity, 0);
            }

            m_CurrentFuelLevel = i_CurrentFuelLevel;
        }

        public void SetCurrentFuelLevel(float i_CurrentFuelLevel)
        {
            if (i_CurrentFuelLevel > k_FuelTankCapacity || i_CurrentFuelLevel < 0)
            {
                throw new ValueOutOfRangeException(k_FuelTankCapacity, 0);
            }

            m_CurrentFuelLevel = i_CurrentFuelLevel;
        }

        public override void UpdateWheelsManufactereAndAirPressure(float i_WheelsAirPressure, string i_WheelsManufacture)
        {
            if (i_WheelsAirPressure > k_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(k_MaxAirPressure, k_MaxAirPressure);
            }

            foreach(Wheel wheel in m_Wheels)
            {
                wheel.SetCurrentAirPressuer(i_WheelsAirPressure);
                wheel.Manufacture = i_WheelsManufacture;
            }
        }

        public override string ToString()
        {
            string toxicMaterial;
            if (this.IsCarryingToxicMaterials)
            {
                toxicMaterial = "Yes";
            }
            else
            {
                toxicMaterial = "No";
            }

            string menu = string.Format(@"
Please select one of the following options by entering the wanted number and then press enter:
Truck's owner's name: {0}
Model name: {1}
License plate: {2}
Is carrying toxic materials: {3}
Truck's cargo volume: {4}
Truck's current fuel level: {5}
Truck's fuel tank capacity: {6}s
Truck's fuel usage in precents: {7}
Status of Truck in the Garage: {8}
List of Truck's wheels: {9}
", this.OwnersName, this.ModelName, this.LicensePlate, toxicMaterial, this.CargoVolume, this.CurrentFuelLevel, this.MaxFuelTankCapacity, this.m_EngergyLeftPrecent, this.Status, this.WheelsListToString());

            return menu;
        }
    }
}
