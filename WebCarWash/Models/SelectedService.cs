using WebCarWash.Domain.Entities;

namespace WebCarWash.Model
{
    public class SelectedService
    {
        public Service Service { get; set; }
        public bool IsCheck { get; set; }
    }
}