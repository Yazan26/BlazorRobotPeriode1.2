using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjectionExample.Data;

public interface IDatabaseService
{
    Task SaveBatteryPercentage(int percentage);
    Task SaveObjectCount(int count);
    Task SaveDistance(int distance);
}