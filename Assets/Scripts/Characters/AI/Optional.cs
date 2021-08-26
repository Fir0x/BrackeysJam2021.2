using System;

public class Optional<T>
{
    private T _element;
    private bool _isPresent;

    private Optional(T element, bool isPresent)
    {
        _element = element;
        _isPresent = isPresent;
    }

    public T Get()
    {
        if (_isPresent)
            throw new InvalidOperationException("No value is present.");

        return _element;
    }

    public bool IsPresent()
    {
        return _isPresent;
    }

    public void IfPresent(Action<T> consumer)
    {
        if (_isPresent)
            consumer.Invoke(_element);
    }

    public static Optional<T> From(T element)
    {
        return new Optional<T>(element, true);
    }

    public static Optional<T> Empty()
    {
        return new Optional<T>(default, false);
    }
}
