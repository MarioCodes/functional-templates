using System;

namespace testingZone.Feature
{
    public class PassByValueAndReference
    {
        public void Start()
        {
            // pass by value
            int value = 10;
            PassByValue(value);
            Console.WriteLine($"pass by value: {value}");

            // to use reference - always use out. it doesn't need to be initialized first
            int value2;
            PassByReferenceOut(out value2);
            Console.WriteLine($"pass by out reference: {value2}");

            // reference always needs to be initialized - even when this initial value is never used
            int value3 = -1;
            PassByReferenceRef(ref value3);
            Console.WriteLine($"pass by ref reference: {value3}");
        }

        public void PassByValue(int value)
        {
            value = 5;
        }

        public void PassByReferenceOut(out int value)
        {
            value = 3;
        }

        public void PassByReferenceRef(ref int value)
        {
            value = 1;
        }
    }
}
