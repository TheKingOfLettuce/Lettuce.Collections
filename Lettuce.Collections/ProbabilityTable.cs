namespace Lettuce.Collections;

public class ProbabilityTable<T> where T : notnull {
    private readonly List<KeyValuePair<T, float>> _weights;
    private readonly Dictionary<T, int> _weightIndexMap;

    public ProbabilityTable() {
        _weights = new List<KeyValuePair<T, float>>();
        _weightIndexMap = new Dictionary<T, int>();
    }

    public void AddItem(T item, float weight) {
        if (weight <= 0) {
            throw new ArgumentException("Must pass a positive weight in the Probability Table", nameof(weight));
        }
        if (_weightIndexMap.ContainsKey(item)) {
            throw new ArgumentException("Item already exists in the table.");
        }

        KeyValuePair<T, float> pair = new KeyValuePair<T, float>(item, weight);
        _weights.Add(pair);
        _weightIndexMap.Add(item, _weights.Count - 1);
    }

    public bool RemoveItem(T item) {
        if (!_weightIndexMap.ContainsKey(item)) {
            return false;
        }

        int index = _weightIndexMap[item];
    }
}