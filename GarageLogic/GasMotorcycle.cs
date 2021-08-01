using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GasMotorcycle : Motorcycle
    {
        protected const float k_FuelTankCapacity = 7;
        protected readonly EnergySource m_FuelType = EnergySource.Octan95;
        protected float m_CurrentFuelLevel;

        public GasMotorcycle(string i_OwnerName = "DefName", string i_OwnerPhoneNumber = "000000", string i_ModelName = "DefModel", string i_LicensePlate = "DefLicensePLT", int i_CcEngineCapacity = 100, LicenseType i_LicenseType = LicenseType.A, float i_FuelLeft = 0) :
                             base(i_OwnerName, i_OwnerPhoneNumber, i_ModelName, i_LicensePlate, i_CcEngineCapacity, i_LicenseType)
        {
            this.m_EngergyLeftPrecent = (i_FuelLeft / k_FuelTankCapacity) * 100;
            this.m_CurrentFuelLevel = i_FuelLeft;
        }

        public float CurrentFuelLevel
        {
            get { return m_CurrentFuelLevel; }
        }

        public override EnergySource EnergySource
        {
            get { return m_FuelType; }
        }

        public float MaxFuelTankCapacity
        {
            get { return k_FuelTankCapacity; }
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

        public override void SetEnergyLevel(float i_CurrentFuelLevel)
        {
            if (i_CurrentFuelLevel > k_FuelTankCapacity || i_CurrentFuelLevel < 0)
            {
                throw new ValueOutOfRangeException(k_FuelTankCapacity, 0);
            }

            m_CurrentFuelLevel = i_CurrentFuelLevel;
        }

        public override string ToString()
        {
            string menu = string.Format(@"
Please select one of the following options by entering the wanted number and then press enter:
Motorcycle's owner's name: {0}
Model name: {1}
License plate: {2}
Motorcycle's Cc Engine capacity: {3}
Motorcycle's current fuel level: {4}
Motorcycle's fuel tank capacity: {5}
Motorcycle's fuel usage in precents: {6}
Status of Motorcycle in the Garage: {7}
List of Motorcycle's wheels: {8}
", this.OwnersName, this.ModelName, this.LicensePlate, this.CcEngineCapacity, this.CurrentFuelLevel, this.MaxFuelTankCapacity, this.m_EngergyLeftPrecent, this.Status, this.WheelsListToString());

            return menu;
        }
    }
}
