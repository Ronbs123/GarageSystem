using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal static class UserInteraction
    {
        internal static void RunUI()
        {
            Console.WriteLine(@"                     ___    __                   _        _           __       _    
| | _  |  _  _ __  _  |  _ |_ _|_ _ __  _  __ _ |_|__  _||_) _ __  _ /__ __ _ (_| _ 
|^|(/_ | (_ (_)|||(/_ | (_)|__ |_(_||||(_| | _> | || |(_|| \(_)| |_> \_| | (_|__|(/_");
            System.Threading.Thread.Sleep(3000);
            bool keepRuning = true;
            while (keepRuning)
            {
                Console.Clear();
                Console.WriteLine(@"                     ___    __                   _        _           __       _    
| | _  |  _  _ __  _  |  _ |_ _|_ _ __  _  __ _ |_|__  _||_) _ __  _ /__ __ _ (_| _ 
|^|(/_ | (_ (_)|||(/_ | (_)|__ |_(_||||(_| | _> | || |(_|| \(_)| |_> \_| | (_|__|(/_");
                string mainMenu = @"
Please select one of the following options by entering the wanted number and then press enter:
1. Add new veichle to the Garage
2. Show Veichles plate numbers
3. Change existing veichle status in the garage
4. Inflate tiers of a veichle to the maximum
5. Fill up energy to veichle (gas or electricity)
6. Show specific veichle details
";
                Console.WriteLine(mainMenu);
                string mode = Console.ReadLine();
                float modesNumber = 7;
                while (!isValidMode(mode, modesNumber))
                {
                    mode = Console.ReadLine();
                }

                if (mode.Equals("1"))
                {
                    AddNewVeichle();
                }
                else if (mode.Equals("2"))
                {
                    ShowVeichlePlateNumbers();
                }
                else if (mode.Equals("3"))
                {
                    ChangeExistingVeichleStatusInGarage();
                }
                else if (mode.Equals("4"))
                {
                    InflateTiers();
                }
                else if (mode.Equals("5"))
                {
                    FillUpEnergyToVeichle();
                }
                else if (mode.Equals("6"))
                {
                    ShowSpecificVeichleDetails();
                }
            }
        }

        internal static void AddNewVeichle()
        {
            Console.Clear();
            Console.WriteLine("Please enter the veichle's license plate number");
            string licensePlate = Console.ReadLine();
            Vehicle veichle = Garage.GetVeichleByLicensePlate(licensePlate);
            if (veichle != null)
            {
                veichle.Status = VehicleStatus.undertreatment;
                Console.WriteLine("Vichle with license plate {0} was already in the garage, then setted its status to 'Undertreatment'", licensePlate);
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else
            {
                string modelName = string.Empty;
                string OwnerName = string.Empty;
                string OwnerPhoneNumber = string.Empty;
                string wheelsManufacture = string.Empty;
                float validWheelsAirPressure = 0;

                VehicleTypes wantedVeichleType;
                object[] specialFeatures = null;
                //// gets type of vehicle
                Console.WriteLine("Please enter the type of the veichle you want to enter to the garage the possible values are:");
                foreach (string veichleTypes in Enum.GetNames(typeof(VehicleTypes)))
                {
                    Console.WriteLine(veichleTypes);
                }

                string veichleType = Console.ReadLine();
                while (!isValidVeichleType(veichleType, out wantedVeichleType))
                {
                    veichleType = Console.ReadLine();
                }

                ////gets owners name
                Console.WriteLine("Please enter the name of the owners of the veichle");
                OwnerName = Console.ReadLine();
                while (!isValidName(OwnerName))
                {
                    OwnerName = Console.ReadLine();
                }

                Console.WriteLine("Please enter the model of the veichle you want to enter to the garage");
                modelName = Console.ReadLine();
                Console.WriteLine("Please enter the owner's phone number, please use only digits with no other marks like '-'");
                string phoneNumber = Console.ReadLine();
                while (!isValidPhoneNumber(phoneNumber))
                {
                    phoneNumber = Console.ReadLine();
                }

                /* in this point we have already:
                 * 1. owner's name
                 * 2. owner's phone number
                 * 3. veichle type
                 * 4. veichle model
                 * 5. veichle license plate
                */

                //// init owners name, owners phone number, model name, licenes plate.
                Vehicle newVeichle = VehicleFactory.GetVeichle(wantedVeichleType);
                newVeichle.OwnersName = OwnerName;
                newVeichle.OwnersPhoneNumber = OwnerPhoneNumber;
                newVeichle.ModelName = modelName;
                newVeichle.LicensePlate = licensePlate;

                SetCurrentEnergyLevel(newVeichle);

                //// init wheels
                Console.WriteLine("Please enter the wheel's manufacture");
                wheelsManufacture = Console.ReadLine();
                Console.WriteLine("Please enter the wheel's air pressure");
                string wheelsAirPressure = Console.ReadLine();
                while (!isValidAirPressure(newVeichle, wheelsAirPressure, out validWheelsAirPressure))
                {
                    wheelsAirPressure = Console.ReadLine();
                }

                newVeichle.UpdateWheelsManufactereAndAirPressure(validWheelsAirPressure, wheelsManufacture);

                //// init special features
                specialFeatures = GetSpecialFeatures(newVeichle);
                if (specialFeatures != null)
                {
                    newVeichle.SetParamatersOfSpecialFeatures(specialFeatures);
                }

                Garage.AddVeichleToGarage(newVeichle);
                Console.WriteLine("a new vehicle was added succesfully, press any key to continue");
                Console.ReadLine();
            }
        }

        internal static void ShowVeichlePlateNumbers()
        {
            Console.Clear();
            string statusMenu = @"
Please select one of the following options by entering the name of the wanted status ,!!or enter '0' to show  all!!,  and then press enter:
- Undertreatment
- Repaired
- Paied
";
            Console.WriteLine(statusMenu);
            string veichlesStatus = Console.ReadLine();
            VehicleStatus validVeichleStatus = VehicleStatus.undertreatment;
            //// validating argument
            if (!veichlesStatus.Equals("0"))
            {
                while (!isValidVeichleStatus(veichlesStatus, out validVeichleStatus))
                {
                    veichlesStatus = Console.ReadLine();
                }
            }
            //// End of validation
            Dictionary<string, VehicleStatus> veicheListByStatus = new Dictionary<string, VehicleStatus>();
            if (veichlesStatus.Equals("0"))
            {
                veicheListByStatus = Garage.AllVeichlesInGarage();
            }
            else
            {
                veicheListByStatus = Garage.VeichlesInGarageByStatus(validVeichleStatus);
            }
            //// printing the list of the veichles
            foreach (KeyValuePair<string, VehicleStatus> veichle in veicheListByStatus)
            {
                Console.WriteLine(veichle.Key);
            }
            //// end of printing
            Console.WriteLine("Please press enter to continue");
            Console.ReadLine();
        }

        internal static void ChangeExistingVeichleStatusInGarage()
        {
            Console.Clear();
            Console.WriteLine("Please enter the veichle licene plate number");
            string veichleLicensePlate = Console.ReadLine();
            Vehicle vehicleToRefuel = Garage.GetVeichleByLicensePlate(veichleLicensePlate);
            bool isVehicleExistsInGarage = true; 
            if (vehicleToRefuel == null)
            {
                Console.WriteLine("no such vehicle exist in the garage! Press enter to return to main menu");
                Console.ReadLine();

                isVehicleExistsInGarage = false;
            }

            if (isVehicleExistsInGarage)
            {
                string statusMenu = @"
Please select one of the following options by entering the wanted option and then press enter:
- Undertreatment
- Repaired
- Paied
";
                Console.WriteLine(statusMenu);
                string veichlesStatus = Console.ReadLine();
                VehicleStatus validVeichleStatus;
                //// validting argument
                while (!isValidVeichleStatus(veichlesStatus, out validVeichleStatus))
                {
                    veichlesStatus = Console.ReadLine();
                }
                //// End of validation
                if (Garage.ChangeVeichleStatus(veichleLicensePlate, validVeichleStatus))
                {
                    Console.WriteLine("the status of the veichle with license plate {0} was changed succesfully", veichleLicensePlate);
                }

                Console.WriteLine("please press enter to exit to the main menu");
                Console.ReadLine();
            }
        }

        internal static void InflateTiers()
        {
            Console.Clear();
            Console.WriteLine("Please enter the veichle licene plate number");
            string veichleLicensePlate = Console.ReadLine();
            if (Garage.InflateVeichleWheelsToMaximum(veichleLicensePlate))
            {
                Console.WriteLine("the wheels of the veichle with license plate {0} were inflated succesfully", veichleLicensePlate);
            }
            else
            {
                Console.WriteLine("A veichle with license plate {0} was not found in the garage", veichleLicensePlate);
            }

            Console.WriteLine("please press enter to exit to the main menu");
            Console.ReadLine();
        }

        internal static void FillUpEnergyToVeichle()
        {
            Console.Clear();
            Console.WriteLine("Please enter the veichle licene plate number");
            string veichleLicensePlate = Console.ReadLine();
            Vehicle vehicleToRefuel = Garage.GetVeichleByLicensePlate(veichleLicensePlate);
            bool isVehicleExistsInGarage = true;
            if (vehicleToRefuel == null)
            {
                Console.WriteLine("no such vehicle exist in the garage! Press enter to return to the main menu");
                Console.ReadLine();
                isVehicleExistsInGarage = false;
            }

            if (isVehicleExistsInGarage)
            {
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                string[] energyources = Enum.GetNames(typeof(EnergySource));
                int index = 1;
                Console.WriteLine("Please enter the name of type of energy source you want to add:");
                foreach (string energysource in Enum.GetNames(typeof(EnergySource)))
                {
                    Console.WriteLine("{0}. {1}", index++, energysource);
                }

                string isValidEnergyType = Console.ReadLine();
                EnergySource validEnergyType;
                //// start of argument validation
                while (!ValidateEnergy(vehicleToRefuel, isValidEnergyType, out validEnergyType))
                {
                    isValidEnergyType = Console.ReadLine();
                }
                //// End of validation

                Console.WriteLine(@"how much would you like to add? 
notice that if youll try add more than the remaining tank capacity, it will fill up to the maximum");
                string isValidEnergy;
                float energyToAdd = 0;
                isValidEnergy = Console.ReadLine();
                while (!isValidEnergyToAdd(isValidEnergy, out energyToAdd))
                {
                    isValidEnergy = Console.ReadLine();
                }

                Garage.FillUpVeichleEnergySource(vehicleToRefuel.LicensePlate, validEnergyType, energyToAdd);
                Console.WriteLine("refill succeed, please press enter to exit to the main menu");
                Console.ReadLine();
            }
        }

        internal static void ShowSpecificVeichleDetails()
        {
            Console.Clear();
            Console.WriteLine("Please enter the veichle licene plate number");
            string veichleLicensePlate = Console.ReadLine();
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            bool isFound = false;
            foreach (Vehicle veichle in Garage.VeichlesInGarage)
            {
                if (veichle.LicensePlate.Equals(veichleLicensePlate))
                {
                    Console.WriteLine(veichle.ToString());
                    isFound = true;
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("A veichle with license plate {0} was not found in the garage", veichleLicensePlate);
            }

            Console.WriteLine("Press enter to exit to the main menu");
            Console.ReadLine();
        }

        internal static bool isValidMode(string i_Mode, float i_NumberOfOptions)
        {
            bool ans = false;
            int mode = 0;
            bool isNumber = int.TryParse(i_Mode, out mode);
            if (isNumber && mode >= 1 && mode <= i_NumberOfOptions)
            {
                ans = true;
            }

            if (ans == false)
            {
                Console.WriteLine("Make sure you entered a valid option, please try again");
            }

            return ans;
        }

        private static bool ValidateEnergy(Vehicle i_VehicleToRefuel, string i_EnergyType, out EnergySource o_ValidEnergyType)
        {
            bool ans = false;
            EnergySource validEnergyType;
            if (Enum.TryParse(i_EnergyType, true, out validEnergyType) && i_VehicleToRefuel.EnergySource.Equals(validEnergyType))
            {
                ans = true;
            }

            if (ans == false)
            {
                Console.WriteLine("Make sure you entered a valid energy type for this veichle, which is {0}, please try again", i_VehicleToRefuel.EnergySource.ToString());
            }

            o_ValidEnergyType = validEnergyType;
            return ans;
        }

        private static bool isValidEnergyToAdd(string i_EnergyToAdd, out float o_EnergyToAdd)
        {
            bool ans = false;
            float validEnergyToAdd = 0;
            if (float.TryParse(i_EnergyToAdd, out validEnergyToAdd))
            {
                ans = true;
            }

            if (ans == false)
            {
                Console.WriteLine("Make sure you entered a valid veichle status, please try again");
            }

            o_EnergyToAdd = validEnergyToAdd;
            return ans;
        }

        private static bool isValidVeichleStatus(string i_Status, out VehicleStatus o_VeichleStatus)
        {
            bool ans = false;
            VehicleStatus validVichleStatus;
            if (i_Status.Equals('0'))
            {
                ans = true;
            }

            if (Enum.TryParse(i_Status, true, out validVichleStatus))
            {
                ans = true;
            }

            if (OnlyNumbers(i_Status) || ans == false)
            {
                Console.WriteLine("Make sure you entered a valid veichle status, please try again");
                ans = false;
            }

            o_VeichleStatus = validVichleStatus;
            return ans;
        }

        private static bool isValidVeichleType(string i_VeichleType, out VehicleTypes o_VeichleType)
        {
            bool ans = false;
            VehicleTypes veichleType;
            if (Enum.TryParse(i_VeichleType, true, out veichleType))
            {
                ans = true;
            }

            if (OnlyNumbers(i_VeichleType) || ans == false)
            {
                Console.WriteLine("Make sure you are entering the name of the type correctly, as GasCar, GasMotorcycle, etc...");
                ans = false;
            }

            o_VeichleType = veichleType;
            return ans;
        }

        private static bool isValidName(string i_Name)
        {
            bool ans = true;
            if (i_Name.Equals(string.Empty))
            {
                ans = false;
                Console.WriteLine("Make sure you entered a valid Name (only letters), please try again");
            }
            else
            {
                foreach (char letter in i_Name)
                {
                    if (!char.IsLetter(letter))
                    {
                        ans = false;
                        Console.WriteLine("Make sure you entered a valid Name (only letters), please try again");
                        break;
                    }
                }
            }

            return ans;
        }

        private static bool isValidPhoneNumber(string i_PhoneNumber)
        {
            bool ans = true;
            if (i_PhoneNumber.Equals(string.Empty))
            {
                ans = false;
                Console.WriteLine("Make sure you entered a valid phone number (only digits), please try again");
            }
            else
            {
                foreach (char digit in i_PhoneNumber)
                {
                    if (!char.IsDigit(digit))
                    {
                        ans = false;
                        Console.WriteLine("Make sure you entered a valid phone number (only digits), please try again");
                        break;
                    }
                }
            }
            
            return ans;
        }

        private static bool isValidAirPressure(Vehicle i_Veichle, string i_WheelsAirPressure, out float o_ValidWheelsAirPressure)
        {
            bool ans = true;
            if (!float.TryParse(i_WheelsAirPressure, out o_ValidWheelsAirPressure))
            {
                Console.WriteLine("Make sure you entered valid air pressure, please try again");
                ans = false;
            }

            if (o_ValidWheelsAirPressure > i_Veichle.MaxAirPressure)
            {
                Console.WriteLine("make sure you do not exceed the max air pressure possible in this type of veichle which is {0}", i_Veichle.MaxAirPressure);
                ans = false;
            }

            return ans;
        }

        private static object[] GetSpecialFeatures(Vehicle i_Vehicle)
        {
            object[] SpecialFeaturs = null;
            Tuple<string, string[]>[] namesAndPossibleValues = i_Vehicle.SpecialFeatursAndPossibleValues();
            if (namesAndPossibleValues != null)
            {
                SpecialFeaturs = new object[namesAndPossibleValues.Length];
                int currentObject = 0;
                foreach (Tuple<string, string[]> nameAndPssibleValue in namesAndPossibleValues)
                {
                    string nameOfValue = getSpecialFeaturesAsStringList(nameAndPssibleValue.Item2);
                    string customiseMassageSpecialFeature = string.Format(
                        @"
Please enter {0}, possible values are:
{1}",
                    nameAndPssibleValue.Item1,
                        nameOfValue);
                    Console.WriteLine(customiseMassageSpecialFeature);
                    SpecialFeaturs[currentObject] = getSpecialFeature(nameAndPssibleValue.Item1, i_Vehicle);
                    currentObject++;
                }
            }

            return SpecialFeaturs;
        }

        private static string getSpecialFeaturesAsStringList(string[] i_PossibleValues)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string PossibleValue in i_PossibleValues)
            {
                sb.Append(PossibleValue);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        private static object getSpecialFeature(string i_FeatureKey, Vehicle i_Vehicle)
        {
            object SpecialFeature = null;
            bool tryAgain = true;
            while (tryAgain)
            {
                try
                {
                    string input = Console.ReadLine();
                    SpecialFeature = i_Vehicle.ParseSpecialFeature(input, i_FeatureKey);
                    tryAgain = false;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"{e.Message}. Try again");
                }
            }

            return SpecialFeature;
        }

        private static void SetCurrentEnergyLevel(Vehicle i_Vehicle)
        {
            string userArgument;
            float currentEnergyLevel = 0;
            bool notValid = true;
            bool succeededParse;
            Console.WriteLine(@"please enter the current energy level of the vehicle,
for gas using vehicle enter remaining fuel,
for electric vehicle the remaining power of the buttery");
            while (notValid)
            {
                userArgument = Console.ReadLine();
                succeededParse = float.TryParse(userArgument, out currentEnergyLevel);
                if (succeededParse)
                {
                    try
                    {
                        i_Vehicle.SetEnergyLevel(currentEnergyLevel);
                        notValid = false;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine($"current energy level cannot exceed the energy capacity, please enter values between {ex.MinValue} to {ex.MaxValue}");
                    }
                }
                else
                {
                    Console.WriteLine("Make sure the current energy level you entered is valid, please try again");
                }
            }
        }

        private static bool OnlyNumbers(string i_Input)
        {
            bool ans = true;
            if (i_Input.Equals(string.Empty))
            {
                ans = false;
            }
            else
            {
                foreach (char c in i_Input)
                {
                    if (!char.IsDigit(c))
                    {
                        ans = false;
                    }
                }
            }

            return ans;
        }
    }
}
