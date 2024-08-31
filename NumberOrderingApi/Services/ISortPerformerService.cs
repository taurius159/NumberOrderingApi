using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public interface ISortPerformerService
    {
        int[] Sort(int[] numbers);
    }
}