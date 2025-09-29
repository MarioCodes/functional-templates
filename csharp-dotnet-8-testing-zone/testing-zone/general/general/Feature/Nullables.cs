namespace general.Feature
{
    public class Nullables
    {
        public void CheckIfNullableIntHasValue()
        {
            int? numMayBeNull = null;
            if(numMayBeNull is int numValue)
            {
                // this prints in case numMayBeNull is not null
                Console.WriteLine(numValue);
            }
        }

        public void CheckIfListIsEmpty()
        {
            List<string> someList = ["something"];
            if(someList is not [])
            {
                Console.WriteLine("list is not empty");
            }
        }
    }
}
