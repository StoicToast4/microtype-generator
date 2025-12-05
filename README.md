

# MicrotypeGenerator

**MicrotypeGenerator** is a .NET Roslyn source generator that creates strongly-typed wrappers (microtypes) around primitive types, using a simple attribute. This helps you avoid primitive obsession and makes your code more expressive and type-safe.

## Features

- Generate strongly-typed wrappers (microtypes) for primitive types with a single attribute
- Implements value-based equality, hash code, and conversion operators (compares the wrapped value, not reference)
- Supports both classes and structs
- Implicit conversion to and from the underlying primitive type
- Auto-generates `ToString`, `Equals`, and `GetHashCode` methods (only if you don't provide your own)
- Supports a `Parse` method hook for validation or transformation
- No runtime dependency—everything is generated at compile time

## How it works

1. Reference this package as an analyzer in your .NET project (see below).
2. Annotate your partial class or struct with `[MicroTypeAttribute<T>]`, where `T` is the primitive type to wrap (e.g., `int`, `Guid`, `string`).
3. The generator emits a partial implementation that wraps the primitive type and provides equality, conversion, and utility methods. Equality is by-value: two microtypes are equal if their wrapped values are equal. If you define your own `Parse`, `Equals`, `GetHashCode`, or `ToString` methods, the generator will not overwrite them.

## Installation

Install via NuGet:

```sh
dotnet add package StoicTools.MicrotypeGenerator
```

Or add as an analyzer in your project file:

```xml
<ItemGroup>
  <PackageReference Include="StoicTools.MicrotypeGenerator" PrivateAssets="all" />
</ItemGroup>
```

## Usage Examples

### Basic: Strongly-typed `UserId` based on `Guid`

```csharp
[MicroTypeAttribute<Guid>]
public partial class UserId;
```

#### What gets generated

```csharp
public partial class UserId : System.IEquatable<UserId>, System.Numerics.IEqualityOperators<UserId, UserId, bool>
{
	private readonly Guid inner;

	public UserId(Guid value) => inner = value;
	public override bool Equals(object? obj) => obj is UserId other && inner.Equals(other.inner);
	public bool Equals(UserId? other) => inner == other?.inner;
	public override int GetHashCode() => inner.GetHashCode();
	public override string ToString() => inner.ToString();
	public static bool operator ==(UserId? left, UserId? right) => left?.Equals(right) ?? right is null;
	public static bool operator !=(UserId? left, UserId? right) => !(left == right);
	public static implicit operator Guid(UserId value) => value.inner;
	public static implicit operator UserId(Guid value) => new UserId(value);
}
```

### Advanced: Custom Validation with `Parse`

You can provide your own `Parse` method to validate or transform the input value. The generated constructor will call your `Parse` method:

```csharp
[MicroTypeAttribute<string>]
public partial struct Email
{
	// The generated constructor calls this Parse method to validate input
	private string Parse(in string value)
	{
		if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
			throw new ArgumentException("Invalid email address", nameof(value));
		return value.Trim();
	}
}
```

### Overriding Generated Methods

If you define your own `Equals`, `GetHashCode`, or `ToString`, the generator will not emit those methods, so you can customize behavior:

```csharp
[MicroTypeAttribute<int>]
public partial struct EvenNumber
{
	// Only even numbers allowed
	private int Parse(in int value)
	{
		if (value % 2 != 0)
			throw new ArgumentException("Value must be even", nameof(value));
		return value;
	}

	// Custom string representation
	public override string ToString() => $"Even({inner})";
}
```

## Guidelines

### Parse Method Visibility

- For **structs** (sealed by default): Use `private` or omit visibility (defaults to private)
- For **classes** (unsealed): Use `protected` to allow inheritance, or make the class `sealed` and use `private`

### Struct vs Class

- **Use struct** for:
  - Small, immutable value types (e.g., `UserId`, `OrderId`, `Email`)
  - Types that are copied frequently
  - Types that should have value semantics

- **Use class** for:
  - Types that need inheritance
  - Larger data structures
  - Types where reference semantics are preferred

## Supported Features

- **Primitive Wrapping:** Wraps any primitive or value type (e.g., `int`, `Guid`, `string`).
- **Value-based Equality:** Implements `IEquatable<T>`, equality operators, and overrides `Equals`/`GetHashCode` to compare the wrapped value (unless you provide your own).
- **Conversion:** Implicit conversion to and from the underlying type.
- **ToString:** Delegates to the underlying type’s `ToString` (unless you provide your own).
- **Validation/Transformation:** Provide a `Parse` method to validate or transform input values.
- **No runtime dependency:** All code is generated at compile time.

## Project Links

- [GitHub Repository](https://github.com/StoicToast4/microtype-generator)
- [License: MIT](./LICENSE)

## License

MIT License. See [LICENSE](./LICENSE) for details.
