# CloneRanger #

## Install instructions ##

```shell
nuget install clone-ranger -pre
```

## Example ##

### Clone an object ###
```csharp
AnyClass original = new AnyClass();
Cloner cloner = new Cloner();
AnyClass clone = cloner.Clone(original);
```

### Clone an object using the extension method ###
```csharp
AnyClass original = new AnyClass();
AnyClass clone = original.Clone();
```

### Clone a list ###
```csharp
List<string> original = new List<string>();
List<string> clone = original.Clone();
```