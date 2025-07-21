using System;
using System.Collections.Generic;

namespace testingZone.Feature
{
    public class Nullables
    {
        public void checkIfNullableIntHasValue()
        {
            int? numMayBeNull = null;
            if(numMayBeNull is int numValue)
            {
                // this prints in case numMayBeNull is not null
                Console.WriteLine(numValue);
            }
        }

        public void checkIfListIsEmpty()
        {
            List<string> someList = ["something"];
            if(someList is not [])
            {
                Console.WriteLine("list is not empty");
            }
        }
    }
}
