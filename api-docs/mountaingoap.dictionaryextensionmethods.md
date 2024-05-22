# DictionaryExtensionMethods

Namespace: MountainGoap

Extension method to copy a dictionary of strings and objects.

```csharp
public static class DictionaryExtensionMethods
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DictionaryExtensionMethods](./mountaingoap.dictionaryextensionmethods.md)

## Methods

### **Add(ConcurrentDictionary&lt;String, Object&gt;, String, Object)**

Add functionality for ConcurrentDictionary.

```csharp
public static void Add(ConcurrentDictionary<string, object> dictionary, string key, object value)
```

#### Parameters

`dictionary` ConcurrentDictionary&lt;String, Object&gt;<br>
Dictionary to which to add an item.

`key` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Key to add.

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
Value to add.

### **Copy(Dictionary&lt;String, Object&gt;)**

Copies the dictionary to a shallow clone.

```csharp
internal static Dictionary<string, object> Copy(Dictionary<string, object> dictionary)
```

#### Parameters

`dictionary` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Dictionary to be copied.

#### Returns

[Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
A shallow copy of the dictionary.

### **Copy(ConcurrentDictionary&lt;String, Object&gt;)**

Copies the concurrent dictionary to a shallow clone.

```csharp
internal static ConcurrentDictionary<string, object> Copy(ConcurrentDictionary<string, object> dictionary)
```

#### Parameters

`dictionary` ConcurrentDictionary&lt;String, Object&gt;<br>
Dictionary to be copied.

#### Returns

ConcurrentDictionary&lt;String, Object&gt;<br>
A shallow copy of the dictionary.

### **Copy(Dictionary&lt;String, ComparisonValuePair&gt;)**

Copies the dictionary to a shallow clone.

```csharp
internal static Dictionary<string, ComparisonValuePair> Copy(Dictionary<string, ComparisonValuePair> dictionary)
```

#### Parameters

`dictionary` [Dictionary&lt;String, ComparisonValuePair&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Dictionary to be copied.

#### Returns

[Dictionary&lt;String, ComparisonValuePair&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
A shallow copy of the dictionary.

### **Copy(Dictionary&lt;String, String&gt;)**

Copies the dictionary to a shallow clone.

```csharp
internal static Dictionary<string, string> Copy(Dictionary<string, string> dictionary)
```

#### Parameters

`dictionary` [Dictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Dictionary to be copied.

#### Returns

[Dictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
A shallow copy of the dictionary.

### **CopyNonNullable(Dictionary&lt;String, Object&gt;)**

Copies the dictionary to a shallow clone.

```csharp
internal static Dictionary<string, object> CopyNonNullable(Dictionary<string, object> dictionary)
```

#### Parameters

`dictionary` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Dictionary to be copied.

#### Returns

[Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
A shallow copy of the dictionary.
