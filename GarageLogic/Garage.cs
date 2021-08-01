using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class Garage
    {
        public static List<Vehicle> VeichlesInGarage = new List<Vehicle>();

        // AddCarToGarage method returns 1 if the veichle was already in the garage in 0 else
        public static int AddVeichleToGarage(Vehicle i_Veichle) 
        {
            int ans = 0;
            foreach (Vehicle veichle in VeichlesInGarage)
            {
                if(veichle.m_LicensePlate == i_Veichle.m_LicensePlate)
                {
                    ans = 1;
                    break;
                }
            }

            if (ans == 0)
            {
                VeichlesInGarage.Add(i_Veichle);
            }

            i_Veichle.Status = VehicleStatus.undertreatment;

            return ans;
        }

        public static Dictionary<string, VehicleStatus> VeichlesInGarageByStatus(VehicleStatus i_Status)
        {
            Dictionary<string, VehicleStatus> VeichleList = new Dictionary<string, VehicleStatus>();

            foreach (Vehicle veichle in VeichlesInGarage)
            {
                if (veichle.Status.Equals(i_Status))
                {
                    VeichleList.Add(veichle.m_LicensePlate, veichle.m_Status);
                }
            }

            return VeichleList;
        }

        public static Dictionary<string, VehicleStatus> AllVeichlesInGarage()
        {
            Dictionary<string, VehicleStatus> VeichleList = new Dictionary<string, VehicleStatus>();
            foreach (Vehicle veichle in VeichlesInGarage)
            {
                VeichleList.Add(veichle.m_LicensePlate, veichle.m_Status);
            }

            return VeichleList;
        }

        // returns true if it changed successfuly and false if the veichle is not in the garage
        public static bool ChangeVeichleStatus(string i_LicensePlate, VehicleStatus i_NewStauts)
        {
            bool ans = false;
            foreach (Vehicle veichle in VeichlesInGarage)
            {
                if (veichle.m_LicensePlate == i_LicensePlate)
                {
                    veichle.Status = i_NewStauts;
                    ans = true;
                }
            }

            return ans;
        }

        // returns true if all wheels inflated and false if didn't find the veichle by its LicensePlate
        public static bool InflateVeichleWheelsToMaximum(string i_LicensePlate)
        {
            bool ans = false;
            foreach (Vehicle veichle in VeichlesInGarage)
            {
                if (veichle.m_LicensePlate.Equals(i_LicensePlate))
                {
                    foreach (Wheel wheel in veichle.m_Wheels)
                    {
                        float airToAdd = wheel.MaxAirPressure - wheel.CurrentAirPresure;
                        wheel.Inflate(airToAdd);
                    }

                    ans = true;
                }
            }

            return ans;
        }

        public static bool FillUpVeichleEnergySource(string i_LicensePlate, EnergySource i_EnergyType, float i_EnergyToAdd)
        {
            bool ans = false;
            foreach (Vehicle veichle in VeichlesInGarage)
            {
                if (veichle.m_LicensePlate == i_LicensePlate)
                {
                    veichle.RefillEnergySource(i_EnergyToAdd, i_EnergyType);
                    ans = true;
                }
            }

            return ans;
        }

        public static Vehicle GetVeichleByLicensePlate(string i_LicensePlate) 
        {
            Vehicle matchedVeichle = null;
            foreach (Vehicle veichle in VeichlesInGarage)
            {
                if (veichle.LicensePlate.Equals(i_LicensePlate))
                {
                    matchedVeichle = veichle;
                }
            }

            return matchedVeichle;
        }
    }
}
