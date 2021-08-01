using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        protected const float k_MaxBatteryUsage = 2.1f;
        protected readonly EnergySource m_TypeOfEnergySource = EnergySource.Electricity;
        protected float m_BatteryRemainingPower;

        public ElectricCar(string i_OwnerName = "DefName", string i_OwnerPhoneNumber = "000000", string i_ModelName = "DefModel", string i_LicensePlate = "DefLicensePLT", float i_BatteryRemainingPower = 0, NumOfCarDoors i_NumberOfDoors = NumOfCarDoors.five, CarColors i_CarColor = CarColors.white) :
                           base(i_OwnerName, i_OwnerPhoneNumber, i_ModelName, i_LicensePlate, i_NumberOfDoors, i_CarColor)
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
Car's owner's name: {0}
Model name: {1}
License plate: {2}
Number of Doors: {3}
Car's color: {4}
Car's battery remaining power: {5}
Car's Max battery usage: {6}
Car's battery usage in precents: {7}
Status of Car in the Garage: {8}
List of Car's wheels: {9}", 
this.OwnersName, this.ModelName, this.LicensePlate, this.NumOfDoors, this.CarColor, this.CurrentBatteryRemainingPower, this.MaxBatteryUsage, this.m_EngergyLeftPrecent, this.Status, this.WheelsListToString());

            return menu;
        }
    }
}
