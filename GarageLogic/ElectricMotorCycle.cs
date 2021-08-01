using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        protected const float k_MaxBatteryUsage = 1.2f;
        protected readonly EnergySource m_TypeOfEnergySource = EnergySource.Electricity;
        protected float m_BatteryRemainingPower;

        public ElectricMotorcycle(string i_OwnerName = "DefName", string i_OwnerPhoneNumber = "000000", string i_ModelName = "DefModel", string i_LicensePlate = "DefLicensePLT", int i_CcEngineCapacity = 100, LicenseType i_LicenseType = LicenseType.A, float i_BatteryRemainingPower = 0) :
                                    base(i_OwnerName, i_OwnerPhoneNumber, i_ModelName, i_LicensePlate, i_CcEngineCapacity, i_LicenseType)
        {
            this.m_EngergyLeftPrecent = (i_BatteryRemainingPower / k_MaxBatteryUsage) * 100;
            this.m_BatteryRemainingPower = i_BatteryRemainingPower;
        }

        public float MaxBatteryUsage
        {
            get { return k_MaxBatteryUsage; }
        }

        public float CurrentBatteryRemainingPower
        {
            get { return m_BatteryRemainingPower; }
        }

        public override EnergySource EnergySource
        {
            get { return m_TypeOfEnergySource; }
        }

        public override void RefillEnergySource(float i_EnergyToAdd, EnergySource i_EnergyType)
        {
            if (!this.m_TypeOfEnergySource.Equals(i_EnergyType))
            {
                throw new ArgumentException("wrong type of fuel", i_EnergyType.ToString());
            }

            if (i_EnergyToAdd + m_BatteryRemainingPower > k_MaxBatteryUsage)
            {
                i_EnergyToAdd = k_MaxBatteryUsage - m_BatteryRemainingPower;
            }

            this.m_BatteryRemainingPower += i_EnergyToAdd;
        }

        public override void SetEnergyLevel(float i_CurrentFuelLevel)
        {
            if (i_CurrentFuelLevel > k_MaxBatteryUsage || i_CurrentFuelLevel < 0)
            {
                throw new ValueOutOfRangeException(k_MaxBatteryUsage, 0);
            }

            this.m_BatteryRemainingPower = i_CurrentFuelLevel;
        }
 
        public override string ToString()
        {
            string menu = string.Format(@"
Please select one of the following options by entering the wanted number and then press enter:
Motorcycle's owner's name: {0}
Model name: {1}
License plate: {2}
Motorcycle's Cc Engine capacity: {3}
Motorcycle's battery remaining power: {4}
Motorcycle's Max battery usage: {5}
Motorcycle's battery usage in precents: {6}
Status of Motorcycle in the Garage: {7}
List of Motorcycle's wheels: {8}
", this.OwnersName, this.ModelName, this.LicensePlate, this.CcEngineCapacity, this.CurrentBatteryRemainingPower, this.MaxBatteryUsage, this.m_EngergyLeftPrecent, this.Status, this.WheelsListToString());

            return menu;
        }
    }
}
