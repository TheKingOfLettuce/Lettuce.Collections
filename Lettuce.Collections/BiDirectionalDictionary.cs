namespace Lettuce.Collections;

/// <summary>
/// A two way dictionary that maps keys to values and values to keys. This collection <br/>
/// is backed by two <see cref="Dictionary{TKey, TValue}"/> collections
/// </summary>
/// <typeparam name="TKey">the key type to map both ways</typeparam>
/// <typeparam name="TValue">the value type to map both ways</typeparam>
public class BiDirectionalDictionary<TKey, TValue> where TKey : notnull where TValue : notnull {
    private readonly Dictionary<TKey, TValue> _keyValuePairs;
    private readonly Dictionary<TValue, TKey> _valueKeyPairs;

    /// <summary>
    /// Initializes the two backing <see cref="Dictionary{Key, TValue}"/> by calling the default constructor
    /// </summary>
    /// <seealso cref="Dictionary{TKey, TValue}()"/>
    /// <seealso cref="Dictionary{TValue, TKey}()"/>
    public BiDirectionalDictionary() {
        _keyValuePairs = new Dictionary<TKey, TValue>();
        _valueKeyPairs = new Dictionary<TValue, TKey>();
    }

    /// <summary>
    /// Gets the value that is associated to the provided key
    /// </summary>
    /// <param name="key">the key to check association with</param>
    /// <returns>the value associated to that key</returns>
    /// <exception cref="KeyNotFoundException">thrown if key is not found in the dictionary</exception>
    public TValue GetValue(TKey key) {
        if (!ContainsKey(key)) {
            throw new KeyNotFoundException($"Key {key} does not exist in the map");
        }

        return _keyValuePairs[key];
    }

    /// <summary>
    /// Attempts to fetch the value associated with the key
    /// </summary>
    /// <param name="key">the key to check</param>
    /// <param name="value">when this method returns, this will be the associated value of the key, or default if key not found</param>
    /// <returns><c>true</c> if the dictionary contained key, <c>false</c> otherwise</returns>
    public bool TryGetValue(TKey key, out TValue? value) {
        try {
            value = GetValue(key);
            return true;
        }
        catch (KeyNotFoundException) {
            value = default;
            return false;
        }
    }

    /// <summary>
    /// Gets the key that is associated with the provided value
    /// </summary>
    /// <param name="value">the value to check association with</param>
    /// <returns>the key associated to that value</returns>
    /// <exception cref="KeyNotFoundException">thrown if value is not found in the dictionary</exception>
    public TKey GetKey(TValue value) {
        if (!ContainsValue(value)) {
            throw new KeyNotFoundException($"Value {value} does not exist in the map");
        }

        return _valueKeyPairs[value];
    }

    /// <summary>
    /// Attempts to fetch the key associated with the value
    /// </summary>
    /// <param name="value">the value to check</param>
    /// <param name="key">when this method returns, this will be the associated key of the value, or default if value not found</param>
    /// <returns><c>true</c> if the dictionary contained value, <c>false</c> otherwise</returns>
    public bool TryGetKey(TValue value, out TKey? key) {
        try {
            key = GetKey(value);
            return true;
        }
        catch (KeyNotFoundException) {
            key = default;
            return false;
        }
    }

    /// <summary>
    /// Adds the pair of key, value to bi-directional associate
    /// </summary>
    /// <param name="key">the key to add</param>
    /// <param name="value">the value to add</param>
    /// <exception cref="ArgumentException">throws if the <paramref name="key"/> or <paramref name="value"/> is already in the dictionary</exception>
    /// <exception cref="ArgumentNullException">throws if the <paramref name="key"/> or <paramref name="value"/> is null</exception>
    public void Add(TKey key, TValue value) {
        if (ContainsKey(key)) {
            throw new ArgumentException("Key already exists in pair", nameof(key));
        }
        if (ContainsValue(value)) {
            throw new ArgumentException("Value already exists in pair", nameof(value));
        }

        _keyValuePairs.Add(key, value);
        _valueKeyPairs.Add(value, key);
    }

    /// <summary>
    /// Attempts to remove the bi-directional association via the key
    /// </summary>
    /// <param name="key">the key to remove in the association</param>
    /// <returns>the value in the association, or null if the key was not found in the dictionary</returns>
    public TValue? RemoveByKey(TKey key) {
        if (!ContainsKey(key)) {
            return default;
        }

        TValue toRemove = _keyValuePairs[key];
        _ = _keyValuePairs.Remove(key);
        _ = _valueKeyPairs.Remove(toRemove);

        return toRemove;
    }

    /// <summary>
    /// Attempts to remove the bi-directional association via the value
    /// </summary>
    /// <param name="value">the value to remove in the association</param>
    /// <returns>the key in the association, or null if the value was not found in the dictionary</returns>
    public TKey? RemoveByValue(TValue value) {
        if (!ContainsValue(value)) {
            return default;
        }

        TKey toRemove = _valueKeyPairs[value];
        _ = _valueKeyPairs.Remove(value);
        _ = _keyValuePairs.Remove(toRemove);

        return toRemove;
    }

    /// <summary>
    /// Checks to see if the key exists in the bi-directional dictionary
    /// </summary>
    /// <param name="key">the key to check</param>
    /// <returns><c>true</c> if the key exists, <c>false</c> otherwise</returns>
    public bool ContainsKey(TKey key) {
        return _keyValuePairs.ContainsKey(key);
    }

    /// <summary>
    /// Checks to see if the value exists in the bi-directional dictionary
    /// </summary>
    /// <param name="value">the value to check</param>
    /// <returns><c>true</c> if the value exists, <c>false</c> otherwise</returns>
    public bool ContainsValue(TValue value) {
        return _valueKeyPairs.ContainsKey(value);
    }
}