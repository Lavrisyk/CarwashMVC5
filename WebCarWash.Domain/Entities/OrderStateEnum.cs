namespace WebCarWash.Domain.Entities
{
    public enum OrderState
    {
        None = 0,
        IsForm = 1,         // формируется
        IsConfirmed,    // подтвержден
        IsCancel,       // отмена
        IsDone          // выполнен
    }
}