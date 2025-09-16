using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase;

public sealed class AddToMoneyStorageBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _enteredTriggerEvent;
    private ReactiveVariable<int> _pointsValue;
    private MoneyStorage _moneyStorage;

    public void Init(IEntity entity)
    {
        _enteredTriggerEvent = entity.GetEnteredTriggerEvent();
        _enteredTriggerEvent.Subscribe(OnTriggerEntered);

        _pointsValue = entity.GetPointsValue();
    }

    public void SetMoneyStorage(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }


    private void OnTriggerEntered()
    {
        _moneyStorage.EarnMoney(_pointsValue.Value);
    }

    public void Dispose(IEntity entity)
    {
        _enteredTriggerEvent.Unsubscribe(OnTriggerEntered);
    }
}
