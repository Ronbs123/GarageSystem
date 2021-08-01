using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public static Vehicle GetVeichle(VehicleTypes i_VeichleType)
        {
            Vehicle veichle;
            if (i_VeichleType.Equals(VehicleTypes.ElectricMotorcycle))
            {
                veichle = new ElectricMotorcycle();
            }
            else if (i_VeichleType.Equals(VehicleTypes.GasMotorcycle))
            {
                veichle = new GasMotorcycle();
            }
            else if (i_VeichleType.Equals(VehicleTypes.ElectricCar))
            {
                veichle = new ElectricCar();
            }
            else if (i_VeichleType.Equals(VehicleTypes.GasCar))
            {
                veichle = new GasCar();
            }
            else if (i_VeichleType.Equals(VehicleTypes.Truck))
            {
                veichle = new Truck();
            }
            else
            {
                throw new ArgumentException(string.Format("error occired while trying to initialize {0}", i_VeichleType), i_VeichleType.ToString());
            }

            return veichle;
        }
    }
}