namespace general.Feature
{
    public class ReadonlyVsConst
    {
        class Readonly
        {
            // we may assign a readonly here
            private readonly int _readonlyField = 42;

            // also at constructor level
            public Readonly()
            {
                _readonlyField = 10;
            }

            public void ReassignReadonlyField()
            {
                // this doesn't compile
                // _readonlyField = -1;
            }
        }

        class Const
        {
            // const cannot be assigned outside of its declaration
            //  this includes the constructor!
            private const int SOME_CONST = 42;

            public Const()
            {
                // this doesn't compile
                // SOME_CONST = 10;
            }
        }
    }
}
