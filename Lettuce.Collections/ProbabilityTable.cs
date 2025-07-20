namespace Lettuce.Collections;

/// <summary>
/// Represents a collection of weighted items to allow for non-linear random selection
/// </summary>
/// <typeparam name="T">the type of items in the collection</typeparam>
public class ProbabilityTable<T> where T : notnull {
    /// <summary>
    /// The total weight of all items in the table, can be used for custom random weight calls
    /// </summary>
    public float TotalWeight => _totalWeight;

    private readonly Dictionary<T, float> _weights;
    private float _totalWeight;

    /// <summary>
    /// Constructs a new instance of the <see cref="ProbabilityTable{T}"/>
    /// </summary>
    public ProbabilityTable() {
        _weights = new Dictionary<T, float>();
    }

    /// <summary>
    /// Adds an item with the given weight
    /// </summary>
    /// <param name="item">the item to add to the table</param>
    /// <param name="weight">the weight associated with the item</param>
    /// <exception cref="ArgumentException">throws if the weight is not a positive number, or if the item is already in the table</exception>
    public void AddItem(T item, float weight) {
        if (weight <= 0) {
            throw new ArgumentException("Must pass a positive weight in the Probability Table", nameof(weight));
        }
        if (_weights.ContainsKey(item)) {
            throw new ArgumentException("Item already exists in the table.");
        }

        _weights.Add(item, weight);
        _totalWeight += weight;
    }

    /// <summary>
    /// Removes an item from the table, if any
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <returns>true if it removed the item, false otherwise</returns>
    public bool RemoveItem(T item) {
        if (!_weights.ContainsKey(item)) {
            return false;
        }

        _totalWeight -= _weights[item];
        _ = _weights.Remove(item);
        return true;
    }

    /// <summary>
    /// Updates the weight of an item in the table
    /// </summary>
    /// <param name="item">the item to update</param>
    /// <param name="weight">the new weight to associate with the item</param>
    /// <exception cref="ArgumentException">throws if the item is not in the table</exception>
    public void UpdateItem(T item, float weight) {
        if (!_weights.ContainsKey(item)) {
            throw new ArgumentException("Cannot update item, it is not in the table");
        }

        _weights[item] = weight;
    }

    /// <summary>
    /// Gets a random item using <see cref="Random.Shared"/> to get a random weight roll
    /// </summary>
    /// <returns>a random item in the table</returns>
    /// <seealso cref="GetRandomItem(float)"/>
    public T GetRandomItem() {
        float weight = Random.Shared.NextSingle() * _totalWeight;
        return GetRandomItem(weight);
    }

    /// <summary>
    /// Gets a random item with the passed weight roll
    /// </summary>
    /// <param name="weightRoll">the weight to use when fetching a random item</param>
    /// <returns>A random item in the map at that weight</returns>
    /// <exception cref="ArgumentException">throws if the weight roll is higher than the total weight of the table</exception>
    /// <exception cref="Exception">throws if there are no items in the table</exception>
    public T GetRandomItem(float weightRoll) {
        if (_weights.Count == 0) {
            throw new Exception("No items in probability table");
        }
        if (weightRoll > _totalWeight) {
            throw new ArgumentException($"Passed in weight is higher than total weight {weightRoll} vs {_totalWeight}", nameof(weightRoll));
        }

        foreach (T key in _weights.Keys) {
            weightRoll -= _weights[key];
            if (weightRoll < 0) {
                return key;
            }
        }

        throw new Exception($"Failed to get random item {weightRoll} vs {_totalWeight}");
    }
}