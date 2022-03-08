using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomGen randomGen = new RandomGen();
            bool running = true;
            while (running)
            {
                Console.WriteLine("Enter input: '0' - Exit, '1' - Run, '2' - Customise");
                string input = Console.ReadLine();
                if(int.TryParse(input, out int value))
                {
                    switch (value)
                    {
                        case 0:
                            {
                                Console.WriteLine("Exiting program...");
                                running = false;
                                break;
                            }
                        case 1:
                            {
                                Console.WriteLine("Running program...");
                                randomGen.RunProgram();
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Customising program...");
                                randomGen.SetValues();
                                break;
                            }
                        default: break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input: Could not parse " + input);
                }
            }

        }
    }

    class RandomGen
    {
        int _minRange;
        int _maxRange;
        int _userGenNum;
        int _randGenNum;
        Random _randomClass = new Random();

        public RandomGen()
        {
            this._minRange = 1;
            this._maxRange = 10;
            this._userGenNum = 5;
            this._randGenNum = 5;
        }

        public void SetValues()
        {
            _setMinRange();
            _setMaxRange();
            _setUserGenTotal();
            _setRandGenTotal();
            Console.WriteLine("Customisation complete.");
        }

        void _setMinRange()
        {
            while (true)
            {
                Console.WriteLine("Please enter a minimum number for the range (Must be greater than 0): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num > 0)
                    {
                        this._minRange = num;
                        Console.WriteLine("Minimum number set to " + num);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: " + num + " not greater than 0");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input: Could not parse " + input);
                }
            }
        }

        void _setMaxRange()
        {
            while (true)
            {
                Console.WriteLine("Please enter a maximum number for the range (Must be greater than " + _minRange + "): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num > _minRange)
                    {
                        this._maxRange = num;
                        Console.WriteLine("Maximum number set to " + num);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: " + num + " not greater than " + _minRange);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input: Could not parse " + input);
                }
            }
        }

        void _setUserGenTotal()
        {
            while (true)
            {
                Console.WriteLine("Please enter a number for total user generated numbers (Must be higher than 0 and lower than " + _getRangeTotal());
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num >= _getRangeTotal())
                    {
                        Console.WriteLine("Invalid input: " + num + " not less than " + _getRangeTotal());
                    }
                    else if (num > 0)
                    {
                        _userGenNum = num;
                        Console.WriteLine("Total user generated numbers set to: " + num);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: " + num + " not greater than 0");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input: Could not parse " + input);
                }
            }
        }

        void _setRandGenTotal()
        {
            while (true)
            {
                Console.WriteLine("Please enter a number for total random generated numbers (Must be higher than 0 and lower than " + _getRangeTotal());
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (num >= _getRangeTotal())
                    {
                        Console.WriteLine("Invalid input: " + num + " not less than " + _getRangeTotal());
                    }
                    else if (num > 0)
                    {
                        _randGenNum = num;
                        Console.WriteLine("Total random generated numbers set to: " + num);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: " + num + " not greater than 0");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input: Could not parse " + input);
                }
            }
        }

        int _getRangeTotal()
        {
            return _maxRange - _minRange + 1;
        }

        public void RunProgram()
        {
            int[] userNumbers = _generateUserNumbers();
            int[] randomNumbers = _generateRandNumbers();
            int numFound = 0;
            Console.WriteLine("Generation complete");
            for (int i = 0; i < userNumbers.Length; i++)
            {
                if (_binarySearch(randomNumbers, userNumbers[i]))
                {
                    numFound = numFound + 1;
                    Console.WriteLine("Number #" + (i+1) + " (" + userNumbers[i] + ") found in randomly generated list!");
                }
            }
            switch (numFound)
            {
                case 0:
                    {
                        Console.WriteLine("No numbers were found in randomly generated list.");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Found 1 number in randomly generated list!");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Found " + numFound + " numbers in randomly generated list!");
                        break;
                    }
            }
            Console.WriteLine("Program complete.");
        }

        int[] _generateUserNumbers()
        {
            int[] userNumbers = new int[_userGenNum];
            for(int i = 0; i < userNumbers.Length; i++)
            {
                while (true)
                {
                    Console.WriteLine("Please enter unique number #" + (i + 1));
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int num))
                    {
                        if(num < _minRange || num > _maxRange)
                        {
                            Console.WriteLine("Invalid Input: " + num + " not in range between " + _minRange + " and " + _maxRange);
                        }
                        else if(_linearSearch(userNumbers,num))
                        {
                            Console.WriteLine("Invalid Input: " + num + " already in list: " + _getIntString(userNumbers));
                        }
                        else
                        {
                            userNumbers[i] = num;
                            Console.WriteLine("Number #" + (i + 1) + " set to value " + num);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: Could not parse " + input);
                    }
                }
            }
            //Array.Sort(userNumbers);
            Console.WriteLine("User numbers set to: " + _getIntString(userNumbers));
            return userNumbers;
        }

        int[] _generateRandNumbers()
        {
            int[] randNumbers = new int[_randGenNum];
            for(int i = 0; i < randNumbers.Length; i++)
            {
                while (true)
                {
                    int num = _randomClass.Next(_minRange, _maxRange + 1);
                    if(_linearSearch(randNumbers,num)|| num < _minRange || num > _maxRange)
                    {
                        continue;
                    }
                    else
                    {
                        randNumbers[i] = num;
                        break;
                    }
                }
            }
            //Array.Sort(randNumbers);
            Console.WriteLine("Random numbers set to: " + _getIntString(randNumbers));
            return randNumbers;
        }

        bool _linearSearch(int[] list, int num)
        {
            for(int i = 0; i < list.Length; i++)
            {
                if(num == list[i])
                {
                    return true;
                }
            }
            return false;
        }
        bool _binarySearch(int[] list, int num)
        {
            int min = 0;
            int max = list.Length - 1;
            Array.Sort(list);
            return _binarySearch(list,num,min,max);
        }
        bool _binarySearch(int[] list, int num, int min, int max)
        {
            if(max >= min)
            {
                int mid = (min + max) / 2;
                if(list[mid] == num) return true;
                if(list[mid] > num) return _binarySearch(list, num, min, mid - 1);
                else return _binarySearch(list, num, mid + 1, max);
            }
            return false;
        }

        string _getIntString(int[] list)
        {
            string str = "";
            for(int i = 0; i < list.Length; i++)
            {
                str = str + list[i] + " ";
            }
            return str;
        }
    }
}
