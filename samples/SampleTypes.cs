namespace samples;

// === Basic Microtype Scenarios ===

// Class wrapping string - most common use case
[MicroType<string>]
public partial class UserId;

// Struct wrapping int - value type semantics
[MicroType<int>]
public partial struct ProductId;

// Wrapping Guid - common for IDs
[MicroType<Guid>]
public partial class OrderId;

// Sealed class - should generate private readonly field
[MicroType<long>]
public sealed partial class AccountId;

// Wrapping decimal for money/price types
[MicroType<decimal>]
public partial struct Price;

// === Custom Member Override Tests ===

// Override ToString - generator should skip generating it
[MicroType<Guid>]
public partial class CustomToString
{
    public override string ToString() => $"Custom-{inner}";
}

// Override Equals(object) and GetHashCode - generator should skip both
[MicroType<int>]
public partial class CustomEquality
{
    public override bool Equals(object? obj) =>
        obj is CustomEquality other && inner == other.inner;

    public override int GetHashCode() => inner.GetHashCode();
}

// Override Equals(T) - generator should skip typed Equals
[MicroType<int>]
public partial struct CustomTypedEquals
{
    public bool Equals(CustomTypedEquals other) => inner == other.inner;
}

// Provide custom operators - generator should skip them
[MicroType<int>]
public partial struct CustomOperators
{
    public static bool operator ==(CustomOperators left, CustomOperators right) =>
        left.inner == right.inner;

    public static bool operator !=(CustomOperators left, CustomOperators right) =>
        !(left == right);
}

// Provide custom Parse - generator should skip it
[MicroType<string>]
public partial class CustomParse
{
    protected string Parse(in string source) =>
        string.IsNullOrWhiteSpace(source) ? throw new ArgumentException("Invalid") : source;
}

// === Complex Scenarios ===

// Inheritable microtype (non-sealed) - should use protected readonly field
[MicroType<string>]
public partial class InheritableId;

// All custom members provided - generator should only emit conversions and constructor
[MicroType<int>]
public partial class FullyCustom
{
    protected int Parse(in int value) => value;
    public override bool Equals(object? obj) => obj is FullyCustom other && inner == other.inner;
    public bool Equals(FullyCustom? other) => inner == other?.inner;
    public override int GetHashCode() => inner.GetHashCode();
    public override string ToString() => inner.ToString();
    public static bool operator ==(FullyCustom? left, FullyCustom? right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(FullyCustom? left, FullyCustom? right) => !(left == right);
}
