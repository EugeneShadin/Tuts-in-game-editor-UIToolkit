using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class CreateEnemyModalWindowModal : AViewModel
{
    public Action<EnemyData> Created { get; }

    public CreateEnemyModalWindowModal(Action<EnemyData> created)
    {
        Created = created;
    }

    public void SendData(EnemyData data)
    {
        Created?.Invoke(data);
    }
}

public class CreateEnemyModalWindow : AModalWindow<CreateEnemyModalWindowModal>
{
    private readonly TextField _enemyName;
    private readonly EnumField _enemyType;
    private readonly IntegerField _health;
    private readonly IntegerField _damage;

    public CreateEnemyModalWindow(VisualElement rootVisualElement) : base(rootVisualElement)
    {
        SetTitle("Create Enemy");

        _enemyName = Body.AddField( "Name").AddClass("input-field-vertical-layout");
        AddSeparator(Body);
        _enemyType = Body.AddEnumField(EnemyType.Goblin, "Type").AddClass("input-field-vertical-layout");
        _health = Body.AddIntField("Health").AddClass("input-field-vertical-layout");
        _damage = Body.AddIntField("Damage").AddClass("input-field-vertical-layout");

        Footer.AddButton(CreateClicked, "Create").AddClass(ButtonStyle.Primary);
    }

    private void CreateClicked()
    {
        var newEnemy = new EnemyData
        {
            Type = (EnemyType)_enemyType.value,
            Name = _enemyName.value,
            Health = _health.value,
            Damage = _damage.value
        };

        Model.SendData(newEnemy);
    }

    protected override void BeforeShow()
    {
        _enemyName.SetValueWithoutNotify(string.Empty);
        _enemyType.SetValueWithoutNotify(EnemyType.Goblin);
        _health.SetValueWithoutNotify(0);
        _damage.SetValueWithoutNotify(0);
    }
}