using BasicAzureFunction.Models;

namespace BasicAzureFunction.Services.Interfaces
{
    public interface IOnlineAnimalFeeder
    {
        public Task Feed(Animal animal, int value);
    }
}
