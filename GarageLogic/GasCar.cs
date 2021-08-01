using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GasCar : Car
    {
        protected const float k_FuelTankCapacity = 60;
        protected readonly EnergySource m_FuelType = EnergySource.Octan96;
        protected float m_CurrentFuelLevel;

        public GasCar(string i_OwnerName = "DefName", string i_OwnerPhoneNumber = "000000", string i_ModelName = "DefModel", string i_LicensePlate = "DefLicensePLT", float i_FuelLeft = 0, NumOfCarDoors i_NumberOfDoors = NumOfCarDoors.five, CarColors i_CarColor = CarColors.white) :
                      base(i_OwnerName, i_OwnerPhoneNumber, i_ModelName, i_LicensePlate, i_NumberOfDoors, i_CarColor)
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
Car's owner's name: {0}
Model name: {1}
License plate: {2}
Number of Doors: {3}
Car's color: {4}
Car's current fuel level: {5}
Car's fuel tank capacity: {6}
Car's fuel usage in precents: {7}
Status of Car in the Garage: {8}
List of Car's wheels: {9}
", this.OwnersName, this.ModelName, this.LicensePlate, this.NumOfDoors, this.CarColor, this.CurrentFuelLevel, this.MaxFuelTankCapacity, this.m_EngergyLeftPrecent, this.Status, this.WheelsListToString());

            return menu;
        }
    }
}
