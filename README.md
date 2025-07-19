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

## ProbabilityTable

A collection of items that have associated weights, allowing for random item selection to be selected more or less often than others.

### Examples

```csharp
ProbabilityTable<string> names = new ProbabilityTable<string>();
names.AddItem("Common", 10);
names.AddItem("Uncommon", 1);
names.AddItem("Rare", .1);
names.AddItem("Really Rare", .01);

names.GetRandomItem();
// for deterministic selections, or if you have your own RNG implementation that produces RANDOM_FLOAT (0.0 - 1.0)
float weightRoll = RANDOM_FLOAT * names.TotalWeight;
names.GetRandomItem(weightRoll);
```