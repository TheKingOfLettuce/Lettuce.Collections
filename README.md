# Lettuce.Collections

Various collections and data structures for your Lettuce needs

## BiDirectionalDictionary

A dictionary that contains two way associations between the keys and values. That is, you can use the dictionary like normal to pass in a key and get the value out, but you can also pass in the value to get the key out

### Examples

```csharp
BiDirectionalDictionary<string, string> nickNames = new BiDirectionalDictionary<string, string>();
nickNames.Add("Cameron", "Cam");
nickNames.Add("Tommy", "Tom");

nickNames.GetValue("Cameron"); // returns "Cam"
nickNames.GetKey("Cam"); // returns "Cameron"
```