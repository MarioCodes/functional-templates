using BasicAzureFunction.Models;

namespace BasicAzureFunction.Services.Interfaces
{
    public interface IAnimalFeeder
    {
        public Task Feed(Animal animal, int value);
    }
}
