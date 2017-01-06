# Clone Ranger #

<a href="http://pahunt.no-ip.org:8081/viewType.html?buildTypeId=CloneRanger_Build&guest=1">
	<img src="http://pahunt.no-ip.org:8081/app/rest/builds/buildType:(id:CloneRanger_Build)/statusIcon"/>
</a>

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

## Questions? ##

Either create a [Github issue](https://github.com/woodburysoft/CloneRanger/issues/new) or shoot me a question on Twitter where I'm [@woodburysoft](https://twitter.com/woodburysoft).
