using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassesForExercise
{
    public class Battery
    {
        const int MAX_CAPACITY = 1000;
        private static Random r = new Random();
        public event Action ReachThreshold;
        public event Action ShutDown;

        //Add events to the class to notify upon threshhold reached and shut down!
        #region events
        #endregion
        private int Threshold { get; }
        public int Capacity { get; set; }
        public int Percent
        {
            get
            {
                return 100 * Capacity / MAX_CAPACITY;
            }
        }
        public Battery()
        {
            Capacity = MAX_CAPACITY;
            Threshold = 400;
        }

        public void Usage()
        {
            Capacity -= r.Next(50, 150);
            //Add calls to the events based on the capacity and threshhold
            #region Fire Events
            #endregion
            if (Capacity < Threshold)
            {
                if(ReachThreshold != null)
                {
                    if (Capacity == 0 && ShutDown !=null)
                    {
                        ShutDown();
                    }

                    ReachThreshold();
                }
            }
        }
    }

    class ElectricCar
    {
        public Battery Bat { get; set; }
        private int id;

        //Add event to notify when the car is shut down
        public event Action OnCarShutDown;

        public ElectricCar(int id)
        {
            this.id = id;
            Bat = new Battery();
            #region Register to battery events
            #endregion
            Bat.ReachThreshold += BatteryReachThresholdFunc;
            Bat.ShutDown += BatteryShutDownFunc;
        }

        public void BatteryReachThresholdFunc()
        {
            Console.WriteLine($"Battery Reached the threshold! Percentage: {}");
        }

        public void BatteryShutDownFunc()
        {
            Console.WriteLine("Battery is about to die!");
            this.OnCarShutDown();
            
        }
        public void StartEngine()
        {
            while (Bat.Capacity > 0)
            {
                Console.WriteLine($"{this} {Bat.Percent}% Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Bat.Usage();
            }
        }

        //Add code to Define and implement the battery event implementations
        #region events implementation
        #endregion

        public static void Progress(object sender, int precent)
        {
            if (sender is Battery)
            {
                Battery battery = (Battery)sender;

            }

        }

        public override string ToString()
        {
            return $"Car: {id}";
        }

    }

}
