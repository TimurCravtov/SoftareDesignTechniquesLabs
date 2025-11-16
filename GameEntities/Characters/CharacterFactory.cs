using System;
using System.Collections.Generic;
using Laboratory.Reports;

namespace Laboratory.Characters
{
    // Flyweight factory for CharacterType instances.
    // CharacterType is treated as an extrinsic/shared state and reused by name.
    public class CharacterFactory
    {
        private readonly Dictionary<string, CharacterType> _types = new(StringComparer.OrdinalIgnoreCase);

        // Returns an existing CharacterType for the given name or creates and stores a new one.
        public CharacterType GetOrCreate(string name, int baseHealth, string[] sprite)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (_types.TryGetValue(name, out var existing)) return existing;

            var type = new CharacterType(baseHealth, sprite, name);
            _types[name] = type;
            return type;
        }

        // Try to retrieve by name; returns true if found.
        public bool TryGet(string name, out CharacterType? type)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return _types.TryGetValue(name, out type);
        }

        // Expose all known types (read-only view).
        public IEnumerable<CharacterType> GetAll() => _types.Values;
    }
}
