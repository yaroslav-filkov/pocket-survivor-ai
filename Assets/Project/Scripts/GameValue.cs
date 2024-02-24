using System;

[Serializable]
public class GameValue
{
    public event Action<int> OnValueChanged;
    public static event Action<int> OnValueAnyChanged;

    private int value;
    public int StartValue {  get; private set; }
    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            if (value != this.value)
            {
                this.value = value;
                OnValueChanged?.Invoke(value);
                OnValueAnyChanged?.Invoke(value);
            }
        }
    }

    public GameValue()
    {
        Value = 0;
        StartValue = Value;
    }

    public GameValue(int value)
    {
        Value = value;
        StartValue = Value;
    }

    public static GameValue operator +(GameValue c1, GameValue c2)
    {
        int resultValue = c1.Value + c2.Value;
        return new GameValue(resultValue);
    }

    public static bool operator ==(GameValue c1, GameValue c2)
    {
        return c1.Value == c2.Value;
    }

    public static bool operator !=(GameValue c1, GameValue c2)
    {
        return c1.Value != c2.Value;
    }

    public static GameValue operator -(GameValue c1, GameValue c2)
    {
        int resultValue = c1.Value - c2.Value;
        return new GameValue(resultValue);
    }

    public static GameValue operator /(GameValue c1, GameValue c2)
    {
        int resultValue = c1.Value / c2.Value;
        return new GameValue(resultValue);
    }

    public static GameValue operator *(GameValue c1, GameValue c2)
    {
        int resultValue = c1.Value * c2.Value;
        return new GameValue(resultValue);
    }

    public int SubtractAndReturnCurrentValue(int takeValue)
    {
        Value -= takeValue;
        return Value;
    }
  
    public bool TryTake(int takeValue)
    {
        if (Value >= takeValue)
        {
            Value -= takeValue;
            return true;
        }
        return false;
    }

    public int TakeNeed(int needValue)
    {
        int takenValue = Math.Min(Value, needValue);
        Value -= takenValue;
        return takenValue;
    }
    public int AddNeed(int addValue)
    {
        var preVal = Value;
        Value = Math.Min(StartValue, Value + addValue);
        return Value - preVal;
    }

    public void Load(int loadingValue)
    {
        Value = loadingValue;
    }

    public static implicit operator int(GameValue gameValue)
    {
        return gameValue.Value;
    }

    public static implicit operator GameValue(int value)
    {
        return new GameValue(value);
    }

    public static bool operator <=(GameValue c1, GameValue c2)
    {
        return c1.Value <= c2.Value;
    }

    public static bool operator >=(GameValue c1, GameValue c2)
    {
        return c1.Value >= c2.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override bool Equals(object obj)
    {
        return obj is GameValue value &&
               Value == value.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}