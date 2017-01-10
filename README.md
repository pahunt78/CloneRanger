# Clone Ranger #

<a href="http://pahunt.no-ip.org:8081/viewType.html?buildTypeId=CloneRanger_Build&guest=1">
	<img src="http://pahunt.no-ip.org:8081/app/rest/builds/buildType:(id:CloneRanger_Build)/statusIcon"/>
</a>

## Purpose ##

Clone Ranger is a .NET library that provides methods to perform a deep clone of any .NET object. It is designed to make usage as simple and require as little ceremony as possible.

## Install instructions ##

```shell
nuget install clone-ranger -pre
```

## Examples ##

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

More examples are available in the [Examples project](https://github.com/woodburysoft/CloneRanger/tree/master/src/CloneRanger.Examples)

## Questions? ##

Either create a [Github issue](https://github.com/woodburysoft/CloneRanger/issues/new) or shoot me a question on Twitter where I'm [@woodburysoft](https://twitter.com/woodburysoft).