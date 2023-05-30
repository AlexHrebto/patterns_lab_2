using System;
using System.Collections.Generic;

// Интерфейс компонента
interface IComponent
{
    void Operation();
}

// Класс-компонент, реализующий интерфейс IComponent
class Leaf : IComponent
{
    private int _value;

    public Leaf(int value)
    {
        _value = value;
    }

    public void Operation()
    {
        Console.Write(_value + " ");
    }
}

// Абстрактный класс-компонент, объединяющий в себе листья и контейнеры
abstract class Component : IComponent
{
    protected string _name;
    protected List<IComponent> _children = new List<IComponent>();

    public Component(string name)
    {
        _name = name;
    }

    public void Add(IComponent component)
    {
        _children.Add(component);
    }

    public void Remove(IComponent component)
    {
        _children.Remove(component);
    }

    public virtual void Operation()
    {
        Console.Write(_name + ": [");
        foreach (var child in _children)
        {
            child.Operation();
        }
        Console.Write("] ");
    }
}

// Класс-контейнер, содержащий список компонентов
class Composite : Component
{
    public Composite(string name) : base(name)
    {
    }

    public override void Operation()
    {
        base.Operation();
    }
}

// Абстрактный класс для цепочки обязанностей
abstract class Handler
{
    protected Handler _successor;

    public void SetSuccessor(Handler successor)
    {
        _successor = successor;
    }

    public abstract void HandleRequest(int request);
}

// Конкретный обработчик в цепочке обязанностей
class ConcreteHandler : Handler
{
    public override void HandleRequest(int request)
    {
        if (request >= 0 && request < 10)
        {
            Console.WriteLine($"{request} handled by ConcreteHandler");
        }
        else if (_successor != null)
        {
            _successor.HandleRequest(request);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание компонентов - листьев и контейнеров
        var leaf1 = new Leaf(1);
        var leaf2 = new Leaf(2);
        var leaf3 = new Leaf(3);
        var leaf4 = new Leaf(4);
        var leaf5 = new Leaf(5);

        var composite1 = new Composite("Composite1");
        var composite2 = new Composite("Composite2");

        // Добавление листьев в контейнеры
        composite1.Add(leaf1);
        composite1.Add(leaf2);

        composite2.Add(leaf3);
        composite2.Add(leaf4);
        composite2.Add(leaf5);

        // Добавление контейнеров в другой контейнер
        composite1.Add(composite2);

        // Выполнение операции на корневом элементе
        composite1.Operation();

        // Создание объектов цепочки обязанностей
        var handler1 = new ConcreteHandler();
        var handler2 = new ConcreteHandler();
        var handler3 = new ConcreteHandler();

        // Установка последовательности обработчиков в цепочке
        handler1.SetSuccessor(handler2);
        handler2.SetSuccessor(handler3);

        // Передача запроса по цепочке
        handler1.HandleRequest(5);
        handler1.HandleRequest(15);
    }
}